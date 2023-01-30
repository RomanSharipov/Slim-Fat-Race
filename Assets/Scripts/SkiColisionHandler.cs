using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiColisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            {
                player.UseSki();
                Debug.Log("used");
            }
    }
}
