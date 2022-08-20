using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BulletType {
    blue,
    green,
    yellow,
    narrow,
    round
}

public class BulletPool : MonoBehaviour {
    private LinkedList<BulletView> activeBullets = new LinkedList<BulletView>();

    private LinkedList<BulletView> inactiveYellowBullets = new LinkedList<BulletView>();
    private LinkedList<BulletView> inactiveBlueBullets = new LinkedList<BulletView>();
    private LinkedList<BulletView> inactiveGreenBullets = new LinkedList<BulletView>();
    private LinkedList<BulletView> inactiveNarrowBullets = new LinkedList<BulletView>();
    private LinkedList<BulletView> inactiveRoundBullets = new LinkedList<BulletView>();

    [SerializeField]
    private BulletView yellowBulletPrefab;
    [SerializeField]
    private BulletView greenBulletPrefab;
    [SerializeField]
    private BulletView blueBulletPrefab;
    [SerializeField]
    private BulletView narrowBulletPrefab;
    [SerializeField]
    private BulletView roundBulletPrefab;

    [SerializeField]
    private Transform activeYellowBulletsHolder;
    [SerializeField]
    private Transform activeBlueBulletsHolder;
    [SerializeField]
    private Transform activeGreenBulletsHolder;
    [SerializeField]
    private Transform activeNarrowBulletsHolder;
    [SerializeField]
    private Transform activeRoundBulletsHolder;

    [SerializeField]
    private Transform inactiveYellowBulletsHolder;
    [SerializeField]
    private Transform inactiveBlueBulletsHolder;
    [SerializeField]
    private Transform inactiveGreenBulletsHolder;
    [SerializeField]
    private Transform inactiveNarrowBulletsHolder;
    [SerializeField]
    private Transform inactiveRoundBulletsHolder;

    public BulletView GetBulletView(BulletType type) {
        switch (type) {
            case BulletType.blue:
                return GetBlueBullet();
            case BulletType.green:
                return GetGreenBullet();
            case BulletType.yellow:
                return GetYellowBullet();
            case BulletType.narrow:
                return GetNarrowBullet();
            case BulletType.round:
                return GetRoundBullet();             
            default:
                throw new ArgumentOutOfRangeException("Wrong bullet type!");
        }
    }

    private BulletView GetYellowBullet() {
        if (inactiveYellowBullets.Count == 0) {
            var newBullet = Instantiate(yellowBulletPrefab, activeYellowBulletsHolder);
            activeBullets.AddLast(newBullet);
            newBullet.OnDestroyed += SendYellowBulletToPool;
            return newBullet;
        }

        var bullet = inactiveYellowBullets.First();
        inactiveYellowBullets.Remove(bullet);
        activeBullets.AddLast(bullet);
        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(activeYellowBulletsHolder);
        return bullet;
    }

    private BulletView GetBlueBullet() {
        if (inactiveBlueBullets.Count == 0) {
            var newBullet = Instantiate(blueBulletPrefab, activeBlueBulletsHolder);
            activeBullets.AddLast(newBullet);
            newBullet.OnDestroyed += SendBlueBulletToPool;
            return newBullet;
        }

        var bullet = inactiveBlueBullets.First();
        inactiveBlueBullets.Remove(bullet);
        activeBullets.AddLast(bullet);
        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(activeBlueBulletsHolder);
        return bullet;
    }

    private BulletView GetGreenBullet() {
        if (inactiveGreenBullets.Count == 0) {
            var newBullet = Instantiate(greenBulletPrefab, activeGreenBulletsHolder);
            activeBullets.AddLast(newBullet);
            newBullet.OnDestroyed += SendGreenBulletToPool;
            return newBullet;
        }

        var bullet = inactiveGreenBullets.First();
        inactiveGreenBullets.Remove(bullet);
        bullet.gameObject.SetActive(true);
        activeBullets.AddLast(bullet);
        bullet.transform.SetParent(activeGreenBulletsHolder);
        return bullet;
    }
    private BulletView GetNarrowBullet() {
        if (inactiveNarrowBullets.Count == 0) {
            var newBullet = Instantiate(narrowBulletPrefab, inactiveNarrowBulletsHolder);
            activeBullets.AddLast(newBullet);
            newBullet.OnDestroyed += SendNarrowBulletToPool;
            return newBullet;
        }

        var bullet = inactiveNarrowBullets.First();
        inactiveNarrowBullets.Remove(bullet);
        activeBullets.AddLast(bullet);
        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(inactiveNarrowBulletsHolder);
        return bullet;
    }

    private BulletView GetRoundBullet() {
        if (inactiveRoundBullets.Count == 0) {
            var newBullet = Instantiate(roundBulletPrefab, inactiveRoundBulletsHolder);
            activeBullets.AddLast(newBullet);
            newBullet.OnDestroyed += SendRoundBulletToPool;
            return newBullet;
        }

        var bullet = inactiveRoundBullets.First();
        inactiveRoundBullets.Remove(bullet);
        bullet.gameObject.SetActive(true);
        activeBullets.AddLast(bullet);
        bullet.transform.SetParent(inactiveRoundBulletsHolder);
        return bullet;
    }

    public void OnClear() {
        var bulletList = new List<BulletView>(activeBullets);

        foreach (var bullet in bulletList) {
            bullet.ClearBullet();
        }
    }


    private void SendYellowBulletToPool(BulletView yellowBullet) {
        activeBullets.Remove(yellowBullet);
        yellowBullet.transform.SetParent(inactiveYellowBulletsHolder);
        yellowBullet.gameObject.SetActive(false);
        inactiveYellowBullets.AddLast(yellowBullet);
    }

    private void SendGreenBulletToPool(BulletView greenBullet) {
        activeBullets.Remove(greenBullet);
        greenBullet.transform.SetParent(inactiveGreenBulletsHolder);
        greenBullet.gameObject.SetActive(false);
        inactiveGreenBullets.AddLast(greenBullet);
    }
    private void SendBlueBulletToPool(BulletView blueBullet) {
        activeBullets.Remove(blueBullet);
        blueBullet.transform.SetParent(inactiveBlueBulletsHolder);
        blueBullet.gameObject.SetActive(false);
        inactiveBlueBullets.AddLast(blueBullet);
    }

    private void SendNarrowBulletToPool(BulletView narrowBullet) {
        activeBullets.Remove(narrowBullet);
        narrowBullet.transform.SetParent(inactiveNarrowBulletsHolder);
        narrowBullet.gameObject.SetActive(false);
        inactiveNarrowBullets.AddLast(narrowBullet);
    }
    private void SendRoundBulletToPool(BulletView roundBullet) {
        activeBullets.Remove(roundBullet);
        roundBullet.transform.SetParent(inactiveRoundBulletsHolder);
        roundBullet.gameObject.SetActive(false);
        inactiveRoundBullets.AddLast(roundBullet);
    }
}

