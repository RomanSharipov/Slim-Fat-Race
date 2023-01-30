using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingZone : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private bool _isLevelWithBridges = false;

    public BoxCollider BoxCollider => _boxCollider;
    public bool IsLevelWithBridges => _isLevelWithBridges;
} 
