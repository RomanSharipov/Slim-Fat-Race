using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveOnPoints : Action
{
    [SerializeField] private SharedEnemy _enemy;

    [SerializeField] private int  _countPoints;

    [SerializeField] private Vector3[] _points;
    [SerializeField] private int _currentPoint;
    [SerializeField] private float _currentDistanceTotargetPoint;

    private Vector3 _targetPoint = new Vector3();
    private float _minDistanceForNextPoint = 1.0f;

    public override void OnStart()
    {
        _countPoints = Random.Range(2, 5);
        InitPath(_countPoints);
        _enemy.Value.NavMeshAgent.SetDestination(_targetPoint);
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemy.Value.Snowball.Scale > 5)
        {
            
            return TaskStatus.Success;
        }

        if (_currentPoint == _points.Length)
        {
            _currentPoint = 0;
            return TaskStatus.Success;
        }
        _targetPoint = _points[_currentPoint];


        _currentDistanceTotargetPoint = Vector3.Distance(_enemy.Value.transform.position, _targetPoint);

        if (_currentDistanceTotargetPoint < _minDistanceForNextPoint)
        {
            _currentPoint++;
            if (_currentPoint == _points.Length)
            {
                _currentPoint = 0;
                return TaskStatus.Success;
            }
            _targetPoint = _points[_currentPoint];
            _enemy.Value.NavMeshAgent.SetDestination(_targetPoint);
            
            
        }
        _enemy.Value.EnemyMovement.MoveTo(/*Bridge.StartPoint.position, _speedMoving.Value, _speedRotation.Value*/);
        return TaskStatus.Running;
    }

    public void InitPath(int countPoints)
    {
        _points = new Vector3[countPoints];
        _currentPoint = 0;
        for (int i = 0; i < _points.Length; i++)
        {
            float x = Random.Range(_enemy.Value.CurrentMovingZone.transform.position.x - Random.Range(0, _enemy.Value.CurrentMovingZone.BoxCollider.bounds.extents.x), _enemy.Value.CurrentMovingZone.transform.position.x + Random.Range(0, _enemy.Value.CurrentMovingZone.BoxCollider.bounds.extents.x));
            float z = Random.Range(_enemy.Value.CurrentMovingZone.transform.position.z - Random.Range(0, _enemy.Value.CurrentMovingZone.BoxCollider.bounds.extents.z), _enemy.Value.CurrentMovingZone.transform.position.z + Random.Range(0, _enemy.Value.CurrentMovingZone.BoxCollider.bounds.extents.z));
            _points[i] = new Vector3(x, _enemy.Value.CurrentMovingZone.transform.position.y, z);
        }
        _targetPoint = _points[_currentPoint];
    }
}
