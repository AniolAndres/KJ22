using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationTrigger : MonoBehaviour
{
    [SerializeField]
    private BulletPool bulletPool;

    public event Action OnBossTriggerHit;

    public BulletPool GetPool() {
        return bulletPool;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var enemy = collision.gameObject.GetComponent<EnemyShipScript>();
        if (enemy != null) {
            enemy.Setup(bulletPool);
            return;
        }

        var bossTrigger = collision.gameObject.GetComponent<BossTrigger>(); 
        if(bossTrigger != null) {
            OnBossTriggerHit?.Invoke();
            return;
        }

        var bullet = collision.gameObject.GetComponent<BulletView>();
        if(bullet != null) {
            bullet.ClearBullet();
            return;
        }
        
    }

}
