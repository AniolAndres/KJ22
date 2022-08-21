using System;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuView : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private AudioSource musicAudioSource;

    public event Action OnGameStart;

    public void PlayMusic() {
        musicAudioSource.Play();
    }

    public void StopMusic() {
        musicAudioSource.Stop();
    }


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
