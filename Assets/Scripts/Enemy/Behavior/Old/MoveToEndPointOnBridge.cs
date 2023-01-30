using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToEndPointOnBridge : Action
{
    [SerializeField] private SharedEnemy _enemy;
    [SerializeField] private SharedFloat _speedRotation;
    [SerializeField] private SharedFloat _speedMoving;
    [SerializeField] private float _currentDistance;

    public Bridge Bridge => _enemy.Value.CurrentBridge;

    private float _minDistanceForSuccess = 1.0f;

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.Value.transform.position, Bridge.EndPoint.position);

        _enemy.Value.EnemyMovement.MoveTo(/*Bridge.EndPoint.position, _speedMoving.Value, _speedRotation.Value*/);

        if (_enemy.Value.Snowball.Scale == 0)
            return TaskStatus.Failure;

        if (_currentDistance < _minDistanceForSuccess)
        {
            Debug.Log("Succes");
            return TaskStatus.Success;

        }

        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        _enemy.Value.NavMeshAgent.SetDestination(Bridge.EndPoint.position);
    }
}
