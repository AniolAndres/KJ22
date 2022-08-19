
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
    private Transform swarmParent;

    [SerializeField]
    private PlayerScript playerPrefab;

    [SerializeField]
    private GameUiView gameView;

    private LevelController currentLevel;

    private int currentLevelIndex;

    private List<AlliedShipScript> alliedShips = new List<AlliedShipScript>(100);

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
    }

    private void SpawnShipButton(ShipData shipData) {
        var shipView = Instantiate(shipData.alliedShipPrefab, swarmParent);
        shipView.Setup(shipData);
        alliedShips.Add(shipView);
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
