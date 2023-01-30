using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballCollisionAttackHandler : MonoBehaviour
{
    [SerializeField] private Snowball _snowball;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.TakeDamage(_snowball);
        }
    }
}
