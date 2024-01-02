using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]

public class PlayerMover : MonoBehaviour
{
    private readonly string _horizontal = "Horizontal";
    private readonly string _isRunning = "isRunning";
    private readonly string _isJumping = "isJumping";

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 300f;

    private Animator _animator;
    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _groundChecker = transform.GetChild(0).GetComponent<GroundChecker>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool(_isJumping, !_groundChecker.CheckGround());

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        float direction = Input.GetAxis(_horizontal);

        _animator.SetBool(_isRunning, direction != 0);
        transform.position += new Vector3(direction * _speed * Time.deltaTime, 0, 0);

        _spriteRenderer.flipX = direction < 0;
    }

    private void Jump()
    {
        if (_groundChecker.CheckGround() == false)
            return;

        _rigidbody.AddForce(Vector2.up * _jumpForce);
    }
}