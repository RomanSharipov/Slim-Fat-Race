using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] Animator _animator;

    private void OnEnable()
    {
        _enemy.Walked += Walk;
        _enemy.WasTakenDamage += Fall;
    }

    private void Walk(float height)
    {
        _animator.SetFloat(AnimatorConstants.Height, height);
    }

    private void OnDisable()
    {
        _enemy.Walked -= Walk;
        _enemy.WasTakenDamage -= Fall;
    }

    private void Fall()
    {
        _animator.SetTrigger(AnimatorConstants.Fall);
    }
}
