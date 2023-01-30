using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyOnBridge : Conditional
{
    [SerializeField] private SharedEnemy _enemy;

    public override TaskStatus OnUpdate()
    {
        return _enemy.Value.IsEnemyOnBridge ? TaskStatus.Success : TaskStatus.Failure;
    }
}
