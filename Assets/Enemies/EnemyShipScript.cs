using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShipScript : MonoBehaviour {

    [SerializeField]
    private int hp;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private int bulletDamage;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float bulletLifeTime;

    [SerializeField]
    private Image shipImage;

    [SerializeField]
    private BulletType bulletType;

    private BulletPool bulletPool;

    private float timer = 0.0f;

    private int currentHp;

    public bool IsDead => currentHp <= 0;

    public event Action<int> OnEnemyShipDestroyed;

    private Coroutine damageCoroutine;

    public void Setup(BulletPool pool) {
        this.bulletPool = pool;
        currentHp = hp;
    }

    public void TakeDamage(int damage) {

        if(bulletPool == null) {
            return;
        }

        currentHp -= damage;
        if(IsDead) {
            DestroyShip();
            return;
        }

        if(damageCoroutine != null) {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        damageCoroutine = StartCoroutine(LaunchDamageFlashCoroutine());
    }

    private IEnumerator LaunchDamageFlashCoroutine() {
        shipImage.color = Color.white;
        var duration = 0.3f;
        var timer = 0f;
        while(timer < duration) {
            timer += Time.smoothDeltaTime;

            var interpolator = timer / duration;
            interpolator = Mathf.Clamp01(interpolator);

            shipImage.color = Color.Lerp(Color.white, Color.black, interpolator);

            yield return null;
        }
        shipImage.color = Color.black;

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
        var smallBullet = bulletPool.GetBulletView(bulletType);
        smallBullet.SetUp(transform.position, new BulletData {
            bulletLifeTime = this.bulletLifeTime,
            bulletSpeed = this.bulletSpeed,
            isFriendly = false,
            damage = this.bulletDamage
        });
    }
}
