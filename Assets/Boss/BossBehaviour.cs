using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private Transform[] bossWeapon;

    [SerializeField]
    private Transform bossHolder;

    [SerializeField]
    private float bulletLifeTime;

    [SerializeField]
    private int bulletDamage;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Image bossImage;

    [SerializeField]
    private BulletType bulletType;

    [SerializeField]
    private ExplosionView explosionPrefab;

    [SerializeField]
    private float explosionsInterval;

    [SerializeField]
    private int numberOfExplosions;

    [SerializeField]
    private AudioSource bossExplosionAudioSource;

    [SerializeField]
    private PlayableDirector disappearTimeline;

    public event Action OnBossKilled;

    private float timer = -2f;

    private BulletPool bulletPool;

    private int currentHp;

    private Coroutine coroutine;

    private float moveTimer = 0f;

    private bool isDead => currentHp <= 0;

    public void Init(BulletPool bulletPool) {
        this.bulletPool = bulletPool;
        currentHp = hp;
    }

    public void TakeDamage(int damage) {
        if (isDead) {
            return;
        }

        currentHp -= damage;
        if(currentHp <= 0) {
            StartCoroutine(SpawnExplosions());
        }

        if(coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(LaunchDamageFlashCoroutine());
    }

    private IEnumerator SpawnExplosions() {
        var explosionCounter = 2;

        for(int i = 0; i < numberOfExplosions; ++i) {

            var randomX = Random.Range(-175f, 75f);
            var randomY = Random.Range(-250f, 250f);

            var position = new Vector2(randomX, randomY);

            var explosion = Instantiate(explosionPrefab, this.transform.parent);
            explosion.transform.localPosition = position;

            if(explosionCounter++ > 2) {
                bossExplosionAudioSource.Play();
                explosionCounter = 0;
            }

            yield return new WaitForSeconds(explosionsInterval);
        }

        disappearTimeline.Play();

        bossImage.color = new Color(bossImage.color.r, bossImage.color.g, bossImage.color.b, 0f);

        yield return new WaitForSeconds(5f);

        OnBossKilled?.Invoke();
    }

    private IEnumerator LaunchDamageFlashCoroutine() {
        bossImage.color = Color.white;
        var duration = 0.15f;
        var timer = 0f;
        while (timer < duration) {
            timer += Time.smoothDeltaTime;

            var interpolator = timer / duration;
            interpolator = Mathf.Clamp01(interpolator);

            bossImage.color = Color.Lerp(Color.white, Color.black, interpolator);

            yield return null;
        }

        bossImage.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletPool == null) {
            return;
        }

        if (isDead) {
            return;
        }

        timer += Time.smoothDeltaTime;
        if(timer > timeBetweenShots) {
            timer -= timeBetweenShots;
            Shoot();
        }


        moveTimer += Time.smoothDeltaTime;

        var sine = Mathf.Sin(moveTimer);
        var secondSine = Mathf.Sin(moveTimer / 2f);

        bossHolder.localPosition = new Vector2(sine * 20f, secondSine * 20f);

    }

    private void Shoot() {
        foreach (var weaponTransform in bossWeapon) {
            var bigBullet = bulletPool.GetBulletView(bulletType);
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
