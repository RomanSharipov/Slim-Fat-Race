using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Stickman
{
    [SerializeField] private EnemyMovingZone[] _enemyMovingZones;
    [SerializeField] private Bridge[] _bridges;
    [SerializeField] private float _minSizeForPhysicsSnowball = 1.0f;
    [SerializeField] private float _delayOnTakeDamage = 3.0f;
    [SerializeField] private Transform _elevatorPoint ;
    [SerializeField] private Transform _cartPoint ;
    [SerializeField] private Platform _platform ;
    
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private BehaviorTree _behaviorTree;

    private int _indexCurrentMovingZone;
    private EnemyMovement _enemyMovement;
    private bool _isEnemyOnBridge;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _delayJobSwitchOnMovement;
    private Coroutine _moveInElevatorJob;

    public EnemyMovingZone CurrentMovingZone => _enemyMovingZones[_indexCurrentMovingZone];
    public Bridge CurrentBridge => _bridges[_indexCurrentMovingZone];
    public EnemyMovement EnemyMovement => _enemyMovement;
    public bool IsEnemyOnBridge => _isEnemyOnBridge;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Transform ElevatorPoint => _elevatorPoint;
    public Transform CartPoint => _cartPoint;
    public Platform Platform => _platform;


    public event Action<float> Walked;
    public event Action WasTakenDamage;

    private new void Start()
    {
        base.Start();
        _enemyMovement = new EnemyMovement();
        _enemyMovement.Walked += OnWalked;
        _enemyMovement.Init(transform,this);
        _indexCurrentMovingZone = 0;
        _waitForSeconds = new WaitForSeconds(_delayOnTakeDamage);
    }

    public override void OnEnterOnBridge(Vector3 newPosition)
    {
        _isEnemyOnBridge = true;
        Snowball.SwitchOffPhysics();
        
    }

    public override void OnExitFromBridge()
    {
        _isEnemyOnBridge = false;

        if (Snowball.Scale > _minSizeForPhysicsSnowball)
        {
            Snowball.SwitchOnPhysics();
        }
    }

    public void SetIsEnemyOnPlane()
    {
        _isEnemyOnBridge = false;
    }

    public void SetNextMovingZone()
    {
        _indexCurrentMovingZone++;
    }

    private void OnDisable()
    {
        _enemyMovement.Walked -= OnWalked;
    }

    private void OnWalked()
    {
        Walked?.Invoke(Snowball.NormalizedScale);
    }

    public override void OnTakeDamage()
    {
        if (_delayJobSwitchOnMovement != null)
        {
            StopCoroutine(_delayJobSwitchOnMovement);
            _delayJobSwitchOnMovement = null;
        }

        WasTakenDamage?.Invoke();
        SwitchOffMovement();
        _delayJobSwitchOnMovement = StartCoroutine(SwitchOnMovementWithDelay());
    }

    public void SwitchOnMovement()
    {
        _behaviorTree.enabled = true;
    }

    public void SwitchOffMovement()
    {
        _behaviorTree.enabled = false;
    }

    public override void JumpTuCart(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        SwitchOffMovement();
        NavMeshAgent.enabled = false;
        base.JumpTuCart(cartPosition,splineComputer,cart);


    }
    
    public override void OnEndedFolowSpringBoard()
    {
        base.OnEndedFolowSpringBoard();
    }
    
    public override void OnStopFolowCart()
    {
        base.OnStopFolowCart();
    }
    
    public override void OnEnterOnSpringBoard()
    {
        Snowball.SwitchOffPhysics();

    }
    
    private IEnumerator SwitchOnMovementWithDelay()
    {
        yield return _waitForSeconds;
        SwitchOnMovement();
    }

    private void OnPlatformRiseWasOver()
    {
        transform.parent = null;
        _navMeshAgent.enabled = true;
    }

    public override void OnEnterOnElevator(Platform platform)
    {
        _navMeshAgent.enabled = false;
        transform.parent = platform.transform;
        platform.RiseWasOver += OnPlatformRiseWasOver;
    }

    public override void OnExitOnElevator(Platform platform)
    {
        platform.RiseWasOver -= OnPlatformRiseWasOver;
    }

}
