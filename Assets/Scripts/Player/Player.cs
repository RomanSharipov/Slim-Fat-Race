using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class Player : Stickman
{
    [SerializeField] private BafsHandler _bafsHandler;
    [SerializeField] InputOnPlane _inputOnPlane;
    [SerializeField] InputOnBridge _inputOnBridge;
    [SerializeField] private InputOnElevator _inputOnElevator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _minSizeForPhysicsSnowball = 1.0f;
    [SerializeField] private float _delayOnTakeDamage = 1.0f;
 

    private WaitForSeconds _waitForSeconds;
    private Coroutine _delayJobSwitchOnInput;

    public event Action PlayerStopped;
    public event Action<float> Walked;
    public event Action WasTakenDamage;

    public Joystick Joystick => _joystick;

    private new void Start()
    {
        base.Start();
        _inputOnPlane.Init(_joystick, _controller);
        _inputOnBridge.Init(_joystick, _controller);
        _inputOnElevator.Init(_joystick, _controller);
        SetInputOnPlane();
        
        _inputOnPlane.Stopped += OnStopped;
        _inputOnPlane.Walked += OnWalked;

        _inputOnBridge.Stopped += OnStopped;
        _inputOnBridge.Walked += OnWalked;
        _waitForSeconds = new WaitForSeconds(_delayOnTakeDamage);
    }

    private void OnWalked()
    {
        Walked?.Invoke(Snowball.NormalizedScale);
    }

    private void OnStopped()
    {
        PlayerStopped?.Invoke();
    }

    public void SetInputOnBridge()
    {
        _inputOnPlane.enabled = false;
        _inputOnElevator.enabled = false;
        _inputOnBridge.enabled = true;
    }

    public void TurnOffAllInputs()
    {
        _inputOnPlane.enabled = false;
        _inputOnBridge.enabled = false;
        _inputOnElevator.enabled = false;
    }
    
    public void SetInputOnPlane()
    {
        _inputOnBridge.enabled = false;
        _inputOnElevator.enabled = false;
        _inputOnPlane.enabled = true;
    }


  
    
    public override void OnEnterOnBridge(Vector3 newPosition)
    {
        SetInputOnBridge();
        transform.localPosition = newPosition;
        Snowball.SwitchOffPhysics();
    }

    public override void OnExitFromBridge()
    {
        SetInputOnPlane();

        if (Snowball.Scale > _minSizeForPhysicsSnowball)
        {
            Snowball.SwitchOnPhysics();
        }
    }


    public void UseSki()
    {
        _bafsHandler.UseSki();
    }
    
    public void SwitchOffCharacterController()
    {
        _controller.enabled = false;
    }

    public void SwitchOnCharacterController()
    {
        _controller.enabled = true;
    }

    public void SwitchOnInput()
    {
        _inputOnPlane.enabled = true;
    }

    public void SwitchOffInput()
    {
        _inputOnPlane.enabled = false;
    }

    private void OnDisable()
    {
        _inputOnPlane.Stopped -= OnStopped;
        _inputOnPlane.Walked -= OnWalked;

        _inputOnBridge.Stopped -= OnStopped;
        _inputOnBridge.Walked -= OnWalked;
    }

    public override void OnTakeDamage()
    {
        if (_delayJobSwitchOnInput != null)
        {
            StopCoroutine(_delayJobSwitchOnInput);
            _delayJobSwitchOnInput = null;
        }

        WasTakenDamage?.Invoke();
        SwitchOffInput();
        _delayJobSwitchOnInput = StartCoroutine(SwitchOnInputDelay());
    }

    private IEnumerator SwitchOnInputDelay()
    {
        yield return _waitForSeconds;
        SwitchOnInput();
    }
    
    public override void JumpTuCart(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        _inputOnPlane.enabled = false;
        base.JumpTuCart(cartPosition,splineComputer,cart);
    }
    
    public override void OnEndedFolowSpringBoard()
    {
        SetInputOnPlane();
        base.OnEndedFolowSpringBoard();
    }
    
    public override void OnStopFolowCart()
    {
        SetInputOnPlane();
        base.OnStopFolowCart();
    }
    
    public override void OnEnterOnSpringBoard()
    {
        Snowball.SwitchOffPhysics();
        _inputOnPlane.enabled = false;
        _inputOnBridge.enabled = false;
        _inputOnElevator.enabled = false;
    }

    public override void OnExitOnElevator(Platform platform)
    {
        platform.RiseWasOver -= OnPlatformRiseWasOver;
    }

    public override void OnEnterOnElevator(Platform platform)
    {
        transform.parent = platform.transform;
        SwitchOffCharacterController();
        Snowball.SwitchOffPhysics();
        _inputOnPlane.enabled = false;
        _inputOnBridge.enabled = false;
        platform.RiseWasOver += OnPlatformRiseWasOver;
    }


    private void OnPlatformRiseWasOver()
    {
        transform.parent = null;
        SwitchOnCharacterController();
    }
}
