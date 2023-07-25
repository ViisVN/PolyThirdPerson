using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Rigidbody _rb;
    public Animator _animator;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Character Data")]
    public float _Speed = 4f;
    public bool IsAttack = false;
    public bool IsOnair = false;
    public float AttackFrame = 40f;
    [Header("Weapon")]
    public BoxCollider _wpRb;
    public int count = 0;

    private void OnEnable()
    {
        WeaponFunctions.Triggered += (other) => GetEnemyName(other);
    }
    private void OnDisable()
    {
        WeaponFunctions.Triggered -= (other) => GetEnemyName(other);
    }
    private void GetEnemyName(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (checkGround_B())
        {
            IsOnair = false;
            _animator.SetBool("OnAir", IsOnair);
        }
        else
        {
            IsOnair = true;
            _animator.SetBool("OnAir", IsOnair);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (checkGround_B())
            {
                _rb.AddForce(new Vector3(0, 4f, 0), ForceMode.Impulse);
                _animator.SetTrigger("Jump");
            }
        }
        if(IsAttack)
        {
            _Speed = 0f;
            AttackFrame--;
            if(AttackFrame<=0)
            {
                IsAttack= false;
                _animator.SetBool("Attack", IsAttack);
                AttackFrame = 40f;
                _Speed = 4f;
            }
        }
        _move();
        MouseRotate();
        Attack();
        AirAttack();
    }
    private void _move()
    {
        if (Input.GetKey(KeyCode.W) && !IsAttack)
        {
            transform.position += transform.forward * Time.deltaTime * _Speed;
            _animator.SetFloat("Speed", _Speed);
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
        }
    }
    private void AirAttack()
    {
        if (Input.GetMouseButtonDown(0) && IsOnair)
        {
            _animator.SetTrigger("AirAttack");
        }
    }
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0)&&!_animator.GetBool("OnAir"))
        {
            IsAttack = true;
            _animator.SetTrigger("Attack");
            if(_animator.GetFloat("AtkPattern")==0)
            {
                _animator.SetFloat("AtkPattern", 1);
            }
            else
            {
                _animator.SetFloat("AtkPattern", 0);
            }
        }
    }
    private void MouseRotate()
    {
        float mouseRotate = Input.mousePosition.x;
        transform.rotation = Quaternion.Euler(0, mouseRotate * 2f, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }

    //Using Physics.OverLap
    private bool checkGround()
    {
        Collider[] groundCollider = Physics.OverlapSphere(groundCheck.position, 0.3f, groundLayer);

        return groundCollider.Length > 0;
    }

    private bool checkGround_B()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(groundCheck.transform.position, Vector3.down, out hit, 0.1f, groundLayer);
        return isHit;
    }

    private void StartAttackTrigger()
    {
        _wpRb.enabled = true;
    }
    private void EndAttackTrigger()
    {
        _wpRb.enabled = false;
    }

}
