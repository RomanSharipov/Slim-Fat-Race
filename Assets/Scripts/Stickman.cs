using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
public abstract class Stickman : MonoBehaviour
{
    [SerializeField] private Snowball _snowball;

    private SplineFollower _splineFollower;
    public Snowball Snowball => _snowball;

    private const int JumpPower = 3;
    private const int JumpDuration=1;
    
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }

    
    protected void Start()
    {
        _snowball.Init();
    }



    public abstract void OnEnterOnBridge(Vector3 newPosition);
    
    public abstract void OnExitFromBridge();

    public abstract void OnEnterOnElevator(Platform platform);
    
    public abstract void OnExitOnElevator(Platform platform);

    public abstract void  OnEnterOnSpringBoard();



    public abstract void OnTakeDamage();

    public virtual void OnEndedFolowSpringBoard()
    {
        Debug.Log(_splineFollower.follow );
        _splineFollower.follow = false;
        Debug.Log(_splineFollower.follow );
        _snowball.SwitchOnPhysics();
        _splineFollower.spline = null;
        ResetRotation();
        
    }
    
    public void FolowSpringBoard(SplineComputer splineComputer)
    {
        Folow(splineComputer);
        _splineFollower.SetPercent(0);
        OnEnterOnSpringBoard();
        _snowball.SwitchOffPhysics();
    }
    
    public virtual void OnStopFolowCart()
    {
        transform.parent = null;
        _snowball.SwitchOnPhysics();
        ResetRotation();
    }

    public virtual void JumpTuCart(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        _snowball.SwitchOffPhysics();
        StartCoroutine(JumpTuCartCorrutine(cartPosition, splineComputer, cart));
    }



    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
    
    public void TakeDamage(Snowball enemySnowball)
    {
        if (enemySnowball.Scale > Snowball.Scale)
        {
            Snowball.SetZeroScale();
            OnTakeDamage();
        }
    }
    
    private void Folow(SplineComputer splineComputer)
    {
        _splineFollower.follow = true;
        _splineFollower.spline = splineComputer;
    }
    
    private IEnumerator JumpTuCartCorrutine(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        transform.DOJump(cartPosition.position, JumpPower, 1, JumpDuration);
        yield return new WaitForSeconds(1);
        cart.StartFolow();
    }

}
