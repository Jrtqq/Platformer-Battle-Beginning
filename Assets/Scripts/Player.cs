using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerCollisionManager))]

public class Player : MonoBehaviour
{
    private int _health = 3;
    private int _coins = 0;
    private PlayerCollisionManager _collisionManager;

    private void Awake()
    {
        _collisionManager = GetComponent<PlayerCollisionManager>();
    }

    private void OnEnable()
    {
        _collisionManager.Hit += GetHit;
        _collisionManager.CoinAdded += AddCoin;
        _collisionManager.Healed += Heal;
    }

    private void OnDisable()
    {
        _collisionManager.Hit -= GetHit;
        _collisionManager.CoinAdded -= AddCoin;
        _collisionManager.Healed -= Heal;
    }

    private void AddCoin()
    {
        _coins++;
        Debug.Log("Coins - " + _coins);
    }

    private void Heal()
    {
        _health++;
        Debug.Log("Health - " + _health);
    }

    private void GetHit()
    {
        _health--;
        Debug.Log("Health - " + _health);

        if (_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
