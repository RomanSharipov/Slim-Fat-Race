using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineComputer))]
public class FinishColisionHandler : MonoBehaviour
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;
    private SplineComputer _splineComputer;

    private void Awake()
    {
        _splineComputer = GetComponent<SplineComputer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.TurnOffAllInputs();
            player.Snowball.InitSplineComputer(_splineComputer);
            _cameraSwitcher.Switch();
        }
    }
}
