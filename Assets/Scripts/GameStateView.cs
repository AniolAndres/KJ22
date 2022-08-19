
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateView : MonoBehaviour
{
    [SerializeField]
    private LevelProvider levelProvider;

    [SerializeField]
    private Transform levelParent;

    [SerializeField]
    private Transform playerParent;

    [SerializeField]
    private PlayerScript playerPrefab;

    [SerializeField]
    private GameUiView gameView;

    [SerializeField]
    private float levelScrollSpeed;

    private LevelController currentLevel;

    private int currentLevelIndex;

    private PlayerScript playerShip;

    private void OnEnable() {
        gameView.OnShipButtonPressed += SpawnShipButton;
    }

    private void OnDisable() {
        gameView.OnShipButtonPressed -= SpawnShipButton;
    }

    public void StartFirstLevel() {
        var first = levelProvider.GetLevel(currentLevelIndex);
        currentLevel = Instantiate(first, levelParent);
        currentLevel.OnLevelFailed += OnLevelFail;
        currentLevel.OnLevelComplete += OnLevelComplete;
        currentLevel.OnStart();
        currentLevel.SetScrollSpeed(levelScrollSpeed);
        playerShip = Instantiate(playerPrefab, playerParent);
    }

    private void SpawnShipButton(ShipData shipData) {
        playerShip.SpawnShip(shipData);
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

        currentLevel.OnLevelFailed -= OnLevelFail;
        currentLevel.OnLevelComplete -= OnLevelComplete;

        currentLevel.OnDestroy();
        Destroy(currentLevel.gameObject);
        var next = levelProvider.GetLevel(++currentLevelIndex);
        currentLevel = Instantiate(next, levelParent);
        currentLevel.OnStart();

        currentLevel.SetScrollSpeed(levelScrollSpeed);

        currentLevel.OnLevelFailed += OnLevelFail;
        currentLevel.OnLevelComplete += OnLevelComplete;
    }

    private void OnLevelFail() {
        //Repeat? Back to main?

    }

    private void OnLevelComplete() {
        //Could do particles or stuff before this
        SetNextLevel();
    }
}
