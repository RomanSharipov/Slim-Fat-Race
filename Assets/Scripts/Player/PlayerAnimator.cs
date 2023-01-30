using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.PlayerStopped += Idle;
        _player.Walked += Walk;
        _player.WasTakenDamage += Fall;
    }

    private void Walk(float height)
    {
        _animator.SetFloat(AnimatorConstants.Height, height);
        _animator.SetBool(AnimatorConstants.Walk, true);
    }

    private void Idle()
    {
        _animator.SetBool(AnimatorConstants.Walk,false);
    }

    private void Fall()
    {
        _animator.SetTrigger(AnimatorConstants.Fall);
    }

    private void OnDisable()
    {
        _player.PlayerStopped -= Idle;
        _player.Walked -= Walk;
    }
}
