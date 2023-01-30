using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SetNextEnemyMovingZone : Action
{
    [SerializeField] private SharedEnemy _enemy;

    public override TaskStatus OnUpdate()
    {
        _enemy.Value.SetNextMovingZone();
        
        return TaskStatus.Failure;
    }

}
