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

#if UNITY_EDITOR

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            currencyController.AddCurrency(20);
        }
    }
   

#endif
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

        if (!currencyController.HasEnoughCurrency(shipData.cost)) {
            return;
        }

        currencyController.SpendCurrency(shipData.cost);

        OnShipButtonPressed?.Invoke(shipData);
    }

    private void FireSecondShipAction() {
        if (GameInputLocker.InputLocked) {
            return;
        }

        var shipData = shipProvider.GetSecondShip();

        if (!currencyController.HasEnoughCurrency(shipData.cost)) {
            return;
        }

        currencyController.SpendCurrency(shipData.cost);

        OnShipButtonPressed?.Invoke(shipData);
    }

    public void Clear() {
        currencyController.Clear();
        healthController.Clear();
    }

    private void FireThirdShipAction() {
        if (GameInputLocker.InputLocked) {
            return;
        }

        var shipData = shipProvider.GetThirdShip();

        if (!currencyController.HasEnoughCurrency(shipData.cost)) {
            return;
        }

        currencyController.SpendCurrency(shipData.cost);

        OnShipButtonPressed?.Invoke(shipData);
    }

}