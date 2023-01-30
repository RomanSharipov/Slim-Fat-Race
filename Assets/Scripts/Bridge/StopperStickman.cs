using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperStickman : MonoBehaviour
{
    [Header("Stopper Stickman (Barrier) - Settings")]
    [SerializeField] private float _startSpeed;

    [Header("Minimum required size for StartMove")]
    [SerializeField] private float _minRequiredSize;

    [Header("When the player got a on next plane, need to stop building the snowpath")]
    [SerializeField] private CollisionHandlerStopperStickman _collisionHandlerStopperStickman;
    [SerializeField] private Collider _barrier;
    [SerializeField] private Collider _stopperStickman;
    [SerializeField] private Bridge _bridge;

    private Snowball _snowball;

    private float _currentSpeed;

    public void OnEnable()
    {
        _collisionHandlerStopperStickman.SnowPathWasFullyBuilt += OnSnowPathWasFullyBuilt;
    }

    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * _currentSpeed;
    }

    public void StartMove()
    {
        _currentSpeed = _startSpeed;
    }

    public void StopMove()
    {
        _currentSpeed = 0;
    }

    private void Update()
    {
        MoveForward();
    }

    private void OnDisable()
    {
        _collisionHandlerStopperStickman.SnowPathWasFullyBuilt -= OnSnowPathWasFullyBuilt;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Snowball snowball))
        {
            _snowball = snowball;
            StopMove();
            _snowball.SetZeroMode();
            _snowball.SnowballBecomesZero -= StartMove;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snowball snowball))
        {
            if (snowball.Scale < _minRequiredSize)
            {
                StopMove();
                return;
            }
            _snowball = snowball;
            StartMove();
            _snowball.SnowballBecomesZero += StopMove;
            _snowball.SetUnRollMode();
        }
    }

    private void OnSnowPathWasFullyBuilt()
    {
        Debug.Log("SwitchOffPlayerBarrier");
        gameObject.SetActive(false);
        _stopperStickman.enabled = false;
        _collisionHandlerStopperStickman.gameObject.SetActive(false);
        StopMove();
        _bridge.SwitchOffEnterPlayerHandler();
    }
}
