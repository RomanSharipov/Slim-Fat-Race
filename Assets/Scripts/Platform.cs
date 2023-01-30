using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Platform : MonoBehaviour
{
    //[SerializeField] private BoxCollider _wallColdier;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private bool _riseIsOver = false;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    private Rigidbody _rigidbody;

    public bool RiseIsOver => _riseIsOver;
    public float Speed => _speed;

    public event Action RiseWasOver;
    public Coroutine _riseUpJob;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void RiseUp()
    {
        //_rigidbody.MovePosition(_endPoint.transform.position);
        _riseUpJob = StartCoroutine(Rise());
    }

    private IEnumerator Rise()
    {
        while (transform.position.y < _endPoint.transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.transform.position, _speed);
            
            
            yield return null;
        }
        Debug.Log("RiseWasOver");
        _riseIsOver = true;
        RiseWasOver?.Invoke();
        yield return null;
    }

    private void StopRise()
    {
        if (_riseUpJob != null)
        {
            StopCoroutine(_riseUpJob);
        }
        _riseUpJob = null;
    }

    private void OnEnable()
    {
        RiseWasOver += StopRise;
    }

    private void OnDisable()
    {
        RiseWasOver -= StopRise;
    }

}
