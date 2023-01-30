using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToNextPlane : Action
{
    [SerializeField] private SharedEnemy _enemy;
    
    [SerializeField] private SharedFloat _speedRotation;
    [SerializeField] private SharedFloat _speedMoving;
    [SerializeField] private float _currentDistance;

    private float _minDistanceForSuccess = 1.0f;

    public Bridge Bridge => _enemy.Value.CurrentBridge;

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.Value.transform.position, Bridge.NextPlanePoint.position);

        if (_currentDistance < _minDistanceForSuccess)
        {
            _enemy.Value.SetIsEnemyOnPlane();
            return TaskStatus.Failure;
        }

        _enemy.Value.EnemyMovement.MoveTo(/*Bridge.NextPlanePoint.position, _speedMoving.Value, _speedRotation.Value*/);
        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        _enemy.Value.NavMeshAgent.SetDestination(Bridge.NextPlanePoint.position);
    }

}
