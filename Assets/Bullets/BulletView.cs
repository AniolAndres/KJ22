using System;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public event Action<BulletView> OnDestroyed;

    private BulletData bulletData;

    private Vector3 direction => bulletData.isFriendly ? Vector3.right : Vector3.left;

    private float timer = 0.0f;

    public void SetUp(Vector3 worldFiringPosition, BulletData bulletData) {
        transform.position = worldFiringPosition;
        timer = 0.0f;
        this.bulletData = bulletData;
    }

    public void Update() {

        transform.position += direction * bulletData.bulletSpeed * Time.smoothDeltaTime;

        timer += Time.smoothDeltaTime;

        if(timer > bulletData.bulletLifeTime) {
            OnDestroyed?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (bulletData.isFriendly) {
            var enemyScript = collision.gameObject.GetComponent<EnemyShipScript>();
            if(enemyScript != null && !enemyScript.IsDead) {
                enemyScript.TakeDamage(bulletData.damage);
                SpawnHitParticles();
                OnDestroyed?.Invoke(this);
                return;
            }

            var bossScript = collision.gameObject.GetComponent<BossBehaviour>();
            if(bossScript != null) {
                bossScript.TakeDamage(bulletData.damage);
                SpawnHitParticles();
                OnDestroyed?.Invoke(this);
                return;
            }

            return;
        }

        var playerScript = collision.gameObject.GetComponent<PlayerScript>();
        if(playerScript != null) {
            playerScript.TakeDamage(bulletData.damage);
            OnDestroyed?.Invoke(this);
            return;
        }
    }

    private void SpawnHitParticles() {
        
    }

    public void ClearBullet() {
        OnDestroyed?.Invoke(this);
    }
}
