
using System;
using UnityEngine;

public class MainComponent : MonoBehaviour
{
    [SerializeField]
    private StartMenuView StartMenu;

    [SerializeField]
    private GameStateView GameState;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu.gameObject.SetActive(true);
        StartMenu.OnGameStart += StartGame;
        GameState.gameObject.SetActive(false);
    }

    private void StartGame() {
        StartMenu.gameObject.SetActive(false);
        GameState.gameObject.SetActive(true);
        GameState.StartFirstLevel();
    }

}
