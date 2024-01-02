using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class PlayerCollisionManager : MonoBehaviour
{
    private readonly string _hitTrigger = "hit";

    public UnityAction Hit;
    public UnityAction CoinAdded;
    public UnityAction Healed;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out EnemyMover _))
        {
            _animator.SetTrigger(_hitTrigger);
            Hit?.Invoke();
        }

        if (collision.collider.TryGetComponent(out Coin _))
        {
            CoinAdded?.Invoke();
        }
        else if (collision.collider.TryGetComponent(out Heal _))
        {
            Healed?.Invoke();
        }
    }
}
