using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyOnPlane : Conditional
{
    [SerializeField] private SharedEnemy _enemy;

    public override TaskStatus OnUpdate()
    {
        return _enemy.Value.IsEnemyOnBridge == false ? TaskStatus.Success : TaskStatus.Failure;
    }
}
