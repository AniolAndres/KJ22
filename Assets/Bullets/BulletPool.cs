using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private LinkedList<BulletView> inactiveSmallBullets = new LinkedList<BulletView>();

    private LinkedList<BulletView> inactiveBigBullets = new LinkedList<BulletView>();

    [SerializeField]
    private BulletView bigBulletPrefab;

    [SerializeField]
    private BulletView smallBulletPrefab;

    [SerializeField]
    private Transform activeSmallBulletsHolder;

    [SerializeField]
    private Transform activeBigBulletsHolder;

    [SerializeField]
    private Transform inactiveSmallBulletsHolder;

    [SerializeField]
    private Transform inactiveBigBulletsHolder;

    public BulletView GetBigBullet() {
        if(inactiveBigBullets.Count == 0) {
            var newBullet = Instantiate(bigBulletPrefab, activeBigBulletsHolder);
            newBullet.OnDestroyed += SendBigBulletToPool;
            return newBullet;
        }

        var bullet = inactiveBigBullets.First();
        inactiveBigBullets.Remove(bullet);
        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(activeBigBulletsHolder);
        return bullet;
    }

    public BulletView GetSmallBullet() {
        if (inactiveSmallBullets.Count == 0) {
            var newBullet = Instantiate(smallBulletPrefab, activeSmallBulletsHolder);
            newBullet.OnDestroyed += SendSmallBulletToPool;
            return newBullet;
        }

        var bullet = inactiveSmallBullets.First();
        inactiveSmallBullets.Remove(bullet);
        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(activeSmallBulletsHolder);
        return bullet;
    }

    // Update is called once per frame
    void SendBigBulletToPool(BulletView bigBullet)
    {
        bigBullet.transform.SetParent(inactiveBigBulletsHolder);
        bigBullet.gameObject.SetActive(false);
        inactiveBigBullets.AddLast(bigBullet);
    }

    void SendSmallBulletToPool(BulletView smallBullet) {
        smallBullet.transform.SetParent(inactiveSmallBulletsHolder);
        smallBullet.gameObject.SetActive(false);
        inactiveSmallBullets.AddLast(smallBullet);
    }
}
