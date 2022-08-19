using System;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuView : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;

    public event Action OnGameStart;


    private void OnEnable() {
        startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame() {
        OnGameStart?.Invoke();
    }

    private void OnDisable() {
        startGameButton.onClick.RemoveListener(StartGame);
    }
}
