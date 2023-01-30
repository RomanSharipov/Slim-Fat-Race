using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveTowardsBridge : Action
{
    [SerializeField] private SharedEnemy _enemy;
    [SerializeField] private SharedFloat _speedRotation;
    [SerializeField] private SharedFloat _speedMoving;

    public Bridge Bridge => _enemy.Value.CurrentBridge;

    private float _minDistanceForSuccess = 1.0f;
    [SerializeField] private float _currentDistance;

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.Value.transform.position, Bridge.StartPoint.position);

        if (_currentDistance < _minDistanceForSuccess)
            return TaskStatus.Success;
        

        _enemy.Value.EnemyMovement.MoveTo(/*Bridge.StartPoint.position, _speedMoving.Value, _speedRotation.Value*/);
        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        _enemy.Value.NavMeshAgent.SetDestination(Bridge.StartPoint.position);
    }

}
