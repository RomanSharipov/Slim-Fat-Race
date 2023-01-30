
using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilRiseUpElevator : Action
{
    [SerializeField] private SharedEnemy _enemy;

    public override TaskStatus OnUpdate()
    {
        if (_enemy.Value.Platform.RiseIsOver)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
