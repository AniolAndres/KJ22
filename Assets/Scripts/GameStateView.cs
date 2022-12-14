
using System;
using UnityEngine;

public class GameStateView : MonoBehaviour
{
    [SerializeField]
    private LevelProvider levelProvider;

    [SerializeField]
    private Transform levelParent;

    [SerializeField]
    private Transform playerParent;

    [SerializeField]
    private Transform bossParent;

    [SerializeField]
    private PlayerScript playerPrefab;

    [SerializeField]
    private GameUiView gameUiView;

    [SerializeField]
    private float levelScrollSpeed;

    [SerializeField]
    private BulletPool bulletPool;

    [SerializeField]
    private EnemyActivationTrigger enemyActivationTrigger;

    [SerializeField]
    private BossBehaviour bossPrefab;

    [SerializeField]
    private AudioSource levelAudioSource;

    private BossBehaviour boss;

    private LevelController currentLevel;

    private int currentLevelIndex;

    private PlayerScript playerShip;

    public event Action OnBackToMain;

    private void OnEnable() {
        gameUiView.OnShipButtonPressed += SpawnShipButton;
        enemyActivationTrigger.OnBossTriggerHit += SpawnBoss;
    }

    private void OnDisable() {
        gameUiView.OnShipButtonPressed -= SpawnShipButton;
        enemyActivationTrigger.OnBossTriggerHit -= SpawnBoss;
    }

    private void SpawnBoss() {
        boss = Instantiate(bossPrefab, bossParent);
        boss.Init(bulletPool);
        boss.OnBossKilled += OnBossKilled;
    }

    private void OnBossKilled() {
        OnLevelComplete();
    }

    public void Clear() {
        //Clear everything upon going back to main
        if (boss != null) {
            boss.OnRemove();
            Destroy(boss.gameObject);
        }

        levelAudioSource.Stop();

        gameUiView.Clear();

        playerShip.OnRemove();
        playerShip.OnPlayerDeath -= OnLevelFail;
        Destroy(playerShip.gameObject);

        currentLevel.OnDestroy();
        Destroy(currentLevel.gameObject);

        bulletPool.OnClear();
    }

    public void StartFirstLevel() {

        var first = levelProvider.GetLevel(currentLevelIndex);
        currentLevel = Instantiate(first, levelParent);
        currentLevel.OnStart();
        currentLevel.SetScrollSpeed(levelScrollSpeed);
        playerShip = Instantiate(playerPrefab, playerParent);
        playerShip.OnPlayerDeath += OnLevelFail;
        playerShip.Init();

        levelAudioSource.Play();

        gameUiView.Setup();
        gameUiView.LinkPlayer(playerShip);
    }

    private void SpawnShipButton(ShipData shipData) {
        playerShip.SpawnShip(shipData, bulletPool);
    }

#if UNITY_EDITOR
    public void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            OnLevelComplete();
            return;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            OnLevelFail();
            return;
        }
    }

#endif

    public void SetNextLevel() {

        var next = levelProvider.GetLevel(++currentLevelIndex);
        if (next == null) {

            OnBackToMain?.Invoke();
            return;
        }

        //Clear everything upon going back to main
        if (boss != null) {
            boss.OnRemove();
            Destroy(boss.gameObject);
        }

        bulletPool.OnClear();

        playerShip.OnRemove();
        playerShip.OnPlayerDeath -= OnLevelFail;
        gameUiView.UnlinkPlayer();
        Destroy(playerShip.gameObject);

        currentLevel.OnDestroy();
        Destroy(currentLevel.gameObject);


        currentLevel = Instantiate(next, levelParent);
        currentLevel.OnStart();
        playerShip = Instantiate(playerPrefab, playerParent);
        playerShip.OnPlayerDeath += OnLevelFail;
        playerShip.Init();
        gameUiView.LinkPlayer(playerShip);

        levelAudioSource.Stop();
        levelAudioSource.Play();

        currentLevel.SetScrollSpeed(levelScrollSpeed);
    }

    private void OnLevelFail() {
        //Repeat? Back to main?
        levelAudioSource.Stop();
        OnBackToMain?.Invoke();
    }

    private void OnLevelComplete() {
        //Could do particles or stuff before this
        SetNextLevel();
    }
}
