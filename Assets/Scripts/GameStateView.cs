
using System;
using UnityEngine;

public class GameStateView : MonoBehaviour
{
    [SerializeField]
    private LevelProvider levelProvider;

    [SerializeField]
    private Transform levelParent;

    private LevelController currentLevel;

    private int currentLevelIndex;

    public void StartFirstLevel() {
        var first = levelProvider.GetLevel(currentLevelIndex);
        currentLevel = Instantiate(first, levelParent);
        currentLevel.OnLevelFailed += OnLevelFail;
        currentLevel.OnLevelComplete += OnLevelComplete;
        currentLevel.OnStart();
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
