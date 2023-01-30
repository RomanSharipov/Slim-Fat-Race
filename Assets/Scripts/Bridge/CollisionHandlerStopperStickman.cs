using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerStopperStickman : MonoBehaviour
{
    public event Action SnowPathWasFullyBuilt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StopperStickman stopperStickman))
        {
            
            SnowPathWasFullyBuilt?.Invoke();
        }
    }
}
