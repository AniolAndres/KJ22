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



        var enemyScript = collision.gameObject.GetComponent<EnemyShipScript>();
        if(enemyScript != null && !enemyScript.IsDead && bulletData.isFriendly && enemyScript is IEnemy enemy) {
            enemy.TakeDamage(bulletData.damage);
            SpawnHitParticles();
            OnDestroyed?.Invoke(this);
        }

        
    }

    private void SpawnHitParticles() {
        
    }
}
