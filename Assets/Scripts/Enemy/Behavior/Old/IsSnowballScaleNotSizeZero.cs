using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsSnowballScaleNotSizeZero : Conditional
{
    [SerializeField] private SharedEnemy _enemy;

    public override TaskStatus OnUpdate()
    {
        return _enemy.Value.Snowball.Scale == 0 ? TaskStatus.Failure : TaskStatus.Success;
    }
}
