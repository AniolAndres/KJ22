using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationTrigger : MonoBehaviour
{
    [SerializeField]
    private BulletPool bulletPool;

    public event Action<int> OnCurrencyReceived;

    public BulletPool GetPool() {
        return bulletPool;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var enemy = collision.gameObject.GetComponent<EnemyShipScript>();
        if (enemy != null) {
            enemy.Setup(bulletPool);
            enemy.OnEnemyShipDestroyed += OnEnemyDestroyed;
        }
    }

    private void OnEnemyDestroyed(int reward) {
        OnCurrencyReceived?.Invoke(reward);
    }
}
