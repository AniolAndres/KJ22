using System;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private Transform[] bossWeapon;

    [SerializeField]
    private float bulletLifeTime;

    [SerializeField]
    private int bulletDamage;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private float bulletSpeed;

    public event Action OnBossKilled;

    private float timer = 0f;

    private BulletPool bulletPool;

    private int currentHp;

    public void Init(BulletPool bulletPool) {
        this.bulletPool = bulletPool;
        currentHp = hp;
    }

    public void TakeDamage(int damage) {
        currentHp -= damage;
        if(currentHp <= 0) {
            OnBossKilled?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletPool == null) {
            return;
        }

        timer += Time.smoothDeltaTime;
        if(timer > timeBetweenShots) {
            timer -= timeBetweenShots;
            Shoot();
        }
    }

    private void Shoot() {
        foreach (var weaponTransform in bossWeapon) {
            var bigBullet = bulletPool.GetBulletView(BulletType.round);
            bigBullet.SetUp(weaponTransform.position, new BulletData {
                bulletLifeTime = bulletLifeTime,
                bulletSpeed = bulletSpeed,
                damage = bulletDamage,
                isFriendly = false
            });
        }
    }

    public void OnRemove() {
        
    }
}
