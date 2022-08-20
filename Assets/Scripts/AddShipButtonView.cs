using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
using System;

public class AddShipButtonView : MonoBehaviour {

    [SerializeField]
    private Button button;

    [SerializeField]
    private TextMeshProUGUI damageText;

    [SerializeField]
    private TextMeshProUGUI rangeText;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private PlayableDirector pulseDirector;

    public event Action OnButtonClicked;

    public void Setup(ShipData shipData) {
        damageText.text = $"{shipData.damage}";
        rangeText.text = $"{ Mathf.Ceil(shipData.bulletLifeTime * shipData.bulletSpeed / 1000)}";
        costText.text = $"{shipData.cost}";
    }

    private void OnEnable() {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {

        pulseDirector.Stop();
        pulseDirector.time = 0;
        pulseDirector.Evaluate();
        pulseDirector.Play();

        OnButtonClicked?.Invoke();
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnClick);
    }

}