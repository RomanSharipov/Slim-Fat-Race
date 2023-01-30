using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveTowardsCart : Action
{
    [SerializeField] private SharedEnemy _enemy;
    [SerializeField] private float _currentDistanceTotargetPoint;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _speedRotation = 9;

    private float _minDistanceForTarget = 1.0f;

    public override TaskStatus OnUpdate()
    {
        _currentDistanceTotargetPoint = Vector3.Distance(_enemy.Value.transform.position, _enemy.Value.CartPoint.position);

        if (_currentDistanceTotargetPoint < _minDistanceForTarget)
        {
            return TaskStatus.Success;
        }
        _enemy.Value.EnemyMovement.MoveTo();
        return TaskStatus.Running;

    }

    public override void OnStart()
    {
        _enemy.Value.NavMeshAgent.SetDestination(_enemy.Value.CartPoint.position);
    }

}
