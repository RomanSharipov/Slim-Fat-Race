using UnityEngine;
using DG.Tweening;

public class Bridge : MonoBehaviour
{
    [Header("The point to which the player is dragged")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _nextPlanePoint;
    [SerializeField] private float _durationDrag;
    [SerializeField] private BoxCollider _boxCollider;

    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;
    public Transform NextPlanePoint => _nextPlanePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.OnEnterOnBridge(_startPoint.position);
        }

        if (other.TryGetComponent(out Snowball snowball))
        {
            snowball.SwitchOffPhysics();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.OnExitFromBridge();
        }
    }

    public void SwitchOffEnterPlayerHandler()
    {
        _boxCollider.enabled = false;
    }
}
