using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionManager : MonoBehaviour
{
    private readonly string _enemyTag = "Enemy";
    private readonly string _coinTag = "Coin";
    private readonly string _healTag = "Heal";
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
        if (collision.collider.CompareTag(_enemyTag))
        {
            _animator.SetTrigger(_hitTrigger);
            Hit?.Invoke();
        }

        if (collision.collider.CompareTag(_coinTag))
        {
            CoinAdded?.Invoke();
        }
        else if (collision.collider.CompareTag(_healTag))
        {
            Healed?.Invoke();
        }
    }
}
