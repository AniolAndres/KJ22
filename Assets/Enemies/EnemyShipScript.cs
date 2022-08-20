using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour, IEnemy {

    [SerializeField]
    private float hp;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float bulletLifeTime;

    private BulletPool bulletPool;

    private float timer = 0.0f;

    private float currentHp;

    public bool IsDead => currentHp <= 0f;

    public event Action<int> OnEnemyShipDestroyed;

    public void Setup(BulletPool pool) {
        this.bulletPool = pool;
        currentHp = hp;
    }

    public void TakeDamage(float damage) {

        if(bulletPool == null) {
            return;
        }

        currentHp -= damage;
        if(IsDead) {
            DestroyShip();
        }
    }

    private void DestroyShip() {
        gameObject.SetActive(false);
        OnEnemyShipDestroyed?.Invoke(5);
    }

    private void Update() {

        if (bulletPool == null) {
            return;
        }

        if (IsDead) {
            return;
        }

        timer += Time.smoothDeltaTime;

        if(timer > timeBetweenShots) {
            Shoot();
            timer -= timeBetweenShots;
        }
    }

    private void Shoot() {
        var smallBullet = bulletPool.GetSmallBullet();
        smallBullet.SetUp(transform.position, new BulletData {
            bulletLifeTime = this.bulletLifeTime,
            bulletSpeed = this.bulletSpeed,
            isFriendly = false,
            damage = this.bulletDamage
        });
    }
}
