using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float edgeMargin;

    [SerializeField]
    private Transform swarmParent;

    [SerializeField]
    private float invulerabilityTime;

    [SerializeField]
    private int totalHp;

    [SerializeField]
    private PlayableDirector playableDirector;

    [SerializeField]
    private AudioSource pickupAudioSource;

    [SerializeField]
    private AudioSource damageSelfAudioSource;

    [SerializeField]
    private AudioClip healthPickupAudio;

    [SerializeField]
    private AudioClip energyPickupAudio;

    [SerializeField]
    private ExplosionView explosionPrefab;

    [SerializeField]
    private int amountOfExplosions;

    [SerializeField]
    private float timeBetweenExplosions;

    [SerializeField]
    private PlayableDirector disappearDirector;

    private RectTransform rectTransform => transform as RectTransform;

    private readonly CurveFactory curveFactory = new CurveFactory();

    private readonly List<AlliedShipScript> alliedShips = new List<AlliedShipScript>(100);

    private Vector2 parentSize;

    private int maxHp;
    private int currentHp;

    public event Action OnPlayerDeath;

    public event Action<int,int> OnDamageTaken;

    public event Action<int,int> OnHealReceived;

    public event Action<int> OnEnergyReceived;

    private bool isInvulerable;

    private bool isDead => currentHp <= 0;

    public void SpawnShip(ShipData shipData, BulletPool bulletPool) {
        var shipView = Instantiate(shipData.alliedShipPrefab, swarmParent);
        var curve = curveFactory.GetRandomCurve();
        shipView.Setup(shipData, curve, this, bulletPool);
        alliedShips.Add(shipView);
    }

    public void GiveEnergy(int amount) {
        pickupAudioSource.clip = energyPickupAudio;
        pickupAudioSource.Play();
        OnEnergyReceived?.Invoke(amount);
    }

    public void TakeDamage(int damage) {
        if (isInvulerable || isDead) {
            return;
        }

        var previous = currentHp;

        currentHp -= damage;
        if(currentHp < 0) {
            currentHp = 0;
        }

        var effectiveDamage = previous - currentHp;

        OnDamageTaken?.Invoke(currentHp, effectiveDamage);

        damageSelfAudioSource.Play();

        if (currentHp == 0) {
            StartCoroutine(DestroyPlayer());
            return;
        }

        StartCoroutine(LaunchInvulerability());
    }

    private IEnumerator DestroyPlayer() {

        bool played = false;

        for(int i = 0; i < amountOfExplosions; i++) {

            var randomX = Random.Range(-150f, 100f);
            var randomY = Random.Range(-150f, 100f);

            var position = new Vector2(randomX, randomY);

            if(i > amountOfExplosions/2 && !played) {
                played = true;
                disappearDirector.Play();
            }

            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, this.transform);
            explosion.transform.localPosition = position;

            yield return new WaitForSeconds(timeBetweenExplosions);
        }

        yield return new WaitForSeconds(2f);

        OnPlayerDeath?.Invoke();
    }

    public IEnumerator LaunchInvulerability() {
        isInvulerable = true;
        playableDirector.time = 0;
        playableDirector.Evaluate();
        playableDirector.Play();

        yield return new WaitForSeconds(invulerabilityTime);

        playableDirector.Stop();
        playableDirector.time = 0;
        playableDirector.Evaluate();
        isInvulerable = false;
    }
    
    public void Heal(int heal) {

        pickupAudioSource.clip = healthPickupAudio;
        pickupAudioSource.Play();

        var previousHp = currentHp;

        currentHp += heal;
        if(currentHp > maxHp) {
            currentHp = maxHp;
        }

        var effectiveHeal = currentHp - previousHp;

        if(effectiveHeal > 0) {
            OnHealReceived?.Invoke(currentHp, effectiveHeal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            return;
        }

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(5);
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            Heal(1);
        }

#endif


        var velocity = Vector2.zero;
        if (GameInputLocker.GetKey(KeyCode.W) || GameInputLocker.GetKey(KeyCode.UpArrow)) {
            velocity += Vector2.up;
        }
        
        if (GameInputLocker.GetKey(KeyCode.S) || GameInputLocker.GetKey(KeyCode.DownArrow)) {
            velocity += Vector2.down;
        }
        
        if (GameInputLocker.GetKey(KeyCode.D) || GameInputLocker.GetKey(KeyCode.RightArrow)) {
            velocity += Vector2.right;
        }
        
        if (GameInputLocker.GetKey(KeyCode.A) || GameInputLocker.GetKey(KeyCode.LeftArrow)) {
            velocity += Vector2.left;
        }
        var newPosition = rectTransform.anchoredPosition + speed * velocity * Time.smoothDeltaTime;

        if(newPosition.y > parentSize.y / 2f - edgeMargin) {
            newPosition.y = parentSize.y / 2f - edgeMargin;
        }
        else if (newPosition.y < -parentSize.y / 2f + edgeMargin) {
            newPosition.y = -parentSize.y / 2f + edgeMargin;
        }

        if (newPosition.x > parentSize.x / 2f - edgeMargin) {
            newPosition.x = parentSize.x / 2f - edgeMargin;
        } else if (newPosition.x < -parentSize.x / 2f + edgeMargin) {
            newPosition.x = -parentSize.x / 2f + edgeMargin;
        }

        rectTransform.anchoredPosition = newPosition;
    }

    public void OnRemove() {
        
    }

    public void Init() {
        var parentRectTransform = transform.parent.transform as RectTransform;
        parentSize = parentRectTransform.rect.size;
        maxHp = totalHp;
        currentHp = totalHp;
    }
}
