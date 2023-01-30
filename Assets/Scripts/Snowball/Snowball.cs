using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Events;

public class Snowball : MonoBehaviour
{
    [Header("Rotating object(Child)")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private GameObject _snowballMesh;
    [SerializeField] private SplineFollower _splineFollower;

    [Header("Scaling object(Child)")]
    [SerializeField] private float _scaleUpSpeed;
    [SerializeField] private float _scaleDownSpeed;
    [SerializeField] private float _scaleDownSpeedForFinish;
    [SerializeField] private GameObject _snowballScalingMesh;

    [Header("Rigidbody for press the snow")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _presserSnow;
    [SerializeField] private float _minSizeForPressSnow;
    [Header("Size snowball settings")]
    [SerializeField] private float _maxSize;
    [SerializeField] private float _minSize;

    [SerializeField] private SnowballCollisionAttackHandler _snowballCollisionAttackHandler;

    private SnowballRotation _snowballRotation;

    private ISnowballScalable _snowballCurrentScalingMode;
    private SnowballScalingUp _snowballScalingUp;
    private SnowballScalingDown _snowballScalingDown;
    private SnowballScalingDown _snowballScalingDownForFinish;
    private SnowballScalingZero _snowballScalingZero;
   

    public event Action SnowballBecomesZero;
    public event Action WasScaledUpMiddleSize;
    public event Action WasScaledDownMiddleSize;

    public float Scale => _snowballScalingMesh.transform.localScale.x;
    public float NormalizedScale => Scale / _maxSize;
    public SnowballCollisionAttackHandler SnowballCollisionAttackHandler => _snowballCollisionAttackHandler;

    public event UnityAction Stoped; 
    
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }
    
    public void Init()
    {
        _snowballRotation = new SnowballRotation(_snowballMesh.transform, _rotateSpeed);
        _snowballScalingUp = new SnowballScalingUp(_maxSize, _snowballScalingMesh.transform, _scaleUpSpeed);
        _snowballScalingUp.WasScaledUpSmallSize += LittleStage;
        _snowballScalingDown = new SnowballScalingDown(_minSize, _snowballScalingMesh.transform, _scaleDownSpeed);
        _snowballScalingDownForFinish = new SnowballScalingDown(_minSize, _snowballScalingMesh.transform, _scaleDownSpeedForFinish);
        _snowballScalingDown.SnowballBecomesZero += OnSnowballBecomesZero;
        _snowballScalingZero = new SnowballScalingZero();
        _snowballCurrentScalingMode = _snowballScalingUp;
        SetRollMode();
        SwitchOffPhysics();
    }

    public void InitSplineComputer(SplineComputer splineComputer)
    {
        _splineFollower.spline = splineComputer;
        _splineFollower.follow = true;
        SetUnRollModeForFinish();
        StartCoroutine(Rolling());
    }

    private IEnumerator Rolling()
    {
        SwitchOffPhysics();
        
        while (_snowballScalingMesh.transform.localScale.x>_minSize)
        {
            Roll();
            yield return null;
        }

        _splineFollower.follow = false;
        Stoped?.Invoke();
    }
    
    public void Roll()
    {
        _snowballRotation.Rotate();
        _snowballCurrentScalingMode.ChangeScale();
    }

    public void SetRollMode()
    {
        _snowballScalingUp.SetStartStages();
        _snowballCurrentScalingMode = _snowballScalingUp;
    }

    public void SetUnRollMode()
    {
        _snowballScalingDown.SetStartStages();
        _snowballCurrentScalingMode = _snowballScalingDown;
    }

    public void SetZeroMode()
    {
        Debug.Log("SetZeroMode");
        _snowballCurrentScalingMode = _snowballScalingZero;
    }

    public void SetUnRollModeForFinish()
    {
        _snowballCurrentScalingMode = _snowballScalingDownForFinish;
    }
    
    public void SwitchOffPhysics()
    {
        _rigidbody.isKinematic = true;
    }

    public void SwitchOnPhysics()
    {
        _rigidbody.isKinematic = false;
    }

    private void LittleStage()
    {
        SwitchOnPhysics();
    }

    private void OnDisable()
    {
        _snowballScalingUp.WasScaledUpSmallSize -= LittleStage;
        _snowballScalingDown.SnowballBecomesZero -= OnSnowballBecomesZero;
    }

    
    
    private void OnSnowballBecomesZero()
    {
        SwitchOffPhysics();
        SnowballBecomesZero?.Invoke();
    }

    public void SetZeroScale()
    {
        _snowballScalingMesh.transform.localScale = Vector3.zero;
        _snowballScalingUp.SetStartStages();
        SwitchOffPhysics();
    }
}
