using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUiView : MonoBehaviour
{
    [SerializeField]
    private Button FirstShipButton;

    [SerializeField]
    private Button SecondShipButton;

    [SerializeField]
    private Button ThirdShipButton;

    [SerializeField]
    private AlliedShipProvider shipProvider;

    [SerializeField]
    private CurrencyController currencyController;

    [SerializeField]
    private HealthController healthController;

    public event Action<ShipData> OnShipButtonPressed;

    private void OnEnable() {
        FirstShipButton.onClick.AddListener(FireFirstShipAction);
        SecondShipButton.onClick.AddListener(FireSecondShipAction);
        ThirdShipButton.onClick.AddListener(FireThirdShipAction);
    }

    private void OnDisable() {
        FirstShipButton.onClick.RemoveListener(FireFirstShipAction);
        SecondShipButton.onClick.RemoveListener(FireSecondShipAction);
        ThirdShipButton.onClick.RemoveListener(FireThirdShipAction);
    }

    private void FireFirstShipAction() {
        if (GameInputLocker.InputLocked) {
            return;
        }

        var shipData = shipProvider.GetFirstShip();
        OnShipButtonPressed?.Invoke(shipData);
    }

    private void FireSecondShipAction() {
        if (GameInputLocker.InputLocked) {
            return;
        }

        var shipData = shipProvider.GetSecondShip();
        OnShipButtonPressed?.Invoke(shipData);
    }

    private void FireThirdShipAction() {
        if (GameInputLocker.InputLocked) {
            return;
        }

        var shipData = shipProvider.GetThirdShip();
        OnShipButtonPressed?.Invoke(shipData);
    }

}
