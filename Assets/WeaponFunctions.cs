using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    public static System.Action<Collider> Triggered;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Triggered?.Invoke(other);
        }
    }
}
