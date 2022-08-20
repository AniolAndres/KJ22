using System;
using UnityEngine;


[Serializable]
public class ShipData {

    public int cost;

    public float speed;

    public int damage;

    public float timeBetweenShots;

    public float bulletLifeTime;

    public float bulletSpeed;

    public BulletType bulletType;

    public AlliedShipScript alliedShipPrefab;
}
