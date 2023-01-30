using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsLevelWithBridges : Conditional
{
    [SerializeField] private SharedEnemy _enemy;

    public override TaskStatus OnUpdate()
    {
        if (_enemy.Value.CurrentMovingZone.IsLevelWithBridges == false)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

}
