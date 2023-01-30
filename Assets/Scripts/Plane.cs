using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.Snowball.SetRollMode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Snowball snowball))
        {
            snowball.SetZeroMode();
        }
    }
}
