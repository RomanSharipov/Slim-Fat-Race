using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsCompleteNewLevel : Conditional
{
    [SerializeField] private SharedEnemy _enemy;

    [SerializeField] private float _currentDistance;

    private float _minDistanceForSuccess = 3.0f;

    public Bridge Bridge => _enemy.Value.CurrentBridge;

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.Value.transform.position, Bridge.NextPlanePoint.position);

        if (_currentDistance < _minDistanceForSuccess)
        {
            _enemy.Value.SetIsEnemyOnPlane();
            _enemy.Value.SetNextMovingZone();
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }


}
