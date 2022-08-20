using System;
using UnityEngine;


public class AlliedShipScript : MonoBehaviour {

    private RectTransform rectTransform => transform as RectTransform;

    private float interpolator;

    private ShipData shipData;

    private IPolarCurve polarCurve;

    private PlayerScript player;

    private BulletPool bulletPool;

    private float timer = 0.0f;

    public void Setup(ShipData shipData, IPolarCurve polarCurve, PlayerScript player, BulletPool bulletPool) {
        this.polarCurve = polarCurve;
        this.player = player;
        this.bulletPool = bulletPool;
        this.shipData = shipData;
        timer = 0f;
    }

    private void Update() {
        interpolator += Time.smoothDeltaTime * shipData.speed;
        var position = polarCurve.CalculatePosition(interpolator);
        position = position + player.gameObject.transform.position;
        var newPosition = Vector2.Lerp(rectTransform.position, position, 0.8f);
        rectTransform.position = newPosition;


        timer += Time.smoothDeltaTime;

        if(timer > shipData.timeBetweenShots) {
            timer -= shipData.timeBetweenShots;
            var smallBullet = bulletPool.GetSmallBullet();
            smallBullet.SetUp(transform.position, new BulletData {
                bulletLifeTime = shipData.bulletLifeTime,
                bulletSpeed = shipData.bulletSpeed,
                isFriendly = true,
                damage = shipData.damage
            });
        }
    }
}
