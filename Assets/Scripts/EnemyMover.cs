using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]

public class EnemyMover : MonoBehaviour
{
    private readonly string _isChasing = "isChasing";
    private readonly string _hitTrigger = "hit";

    [SerializeField] private int _playerLayerMask;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;

    private bool _isAlive = true;

    private int _currentPoint = 0;
    private Transform _targetPosition;
    private int _visionRange = 10;
    private Animator _animator;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (_isAlive)
        {
            SetTargetPosition();
            Move();

            transform.localScale = new(GetObjectDirection(), 1, 1);
        }
        else
        {
            _collider.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Waypoint collidedWaypoint))
        {
            if (collidedWaypoint.Number == _waypoints[_currentPoint].GetComponent<Waypoint>().Number)
            {
                _currentPoint = GetNextPointNumber();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player _))
        {
            _animator.SetTrigger(_hitTrigger);
            _isAlive = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition.position, _speed * Time.deltaTime);
    }

    private void SetTargetPosition()
    {
        if (TryFindPlayer(out Transform player))
        {
            _animator.SetBool(_isChasing, true);
            _targetPosition = player;
        }
        else
        {
            _animator.SetBool(_isChasing, false);
            _targetPosition = _waypoints[_currentPoint];
        }
    }

    private bool TryFindPlayer(out Transform player)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, _visionRange, _playerLayerMask);

        if (hit.collider != null)
        {
            player = hit.transform;
            return true;
        }

        player = null;
        return false;
    }

    private int GetNextPointNumber()
    {
        if (_currentPoint + 1 == _waypoints.Length)
            return 0;

        return _currentPoint + 1;
    }

    private int GetObjectDirection()
    {
        Vector2 direction = _targetPosition.position - transform.position;

        if (direction.x < 0)
            return - 1;
        else
            return 1;
    }
}
