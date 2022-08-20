using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUiView : MonoBehaviour
{
    [SerializeField]
    private AddShipButtonView FirstShipButton;

    [SerializeField]
    private AddShipButtonView SecondShipButton;

    [SerializeField]
    private AddShipButtonView ThirdShipButton;

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
        FirstShipButton.OnButtonClicked += FireFirstShipAction;
        SecondShipButton.OnButtonClicked += FireSecondShipAction;
        ThirdShipButton.OnButtonClicked += FireThirdShipAction;
    }

    private void OnDisable() {
        FirstShipButton.OnButtonClicked -= FireFirstShipAction;
        SecondShipButton.OnButtonClicked -= FireSecondShipAction;
        ThirdShipButton.OnButtonClicked -= FireThirdShipAction;
    }

    public void Setup() {
        FirstShipButton.Setup(shipProvider.GetFirstShip());
        SecondShipButton.Setup(shipProvider.GetSecondShip());
        ThirdShipButton.Setup(shipProvider.GetThirdShip());
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

    public void LinkPlayer(PlayerScript playerShip) {
        healthController.LinkPlayer(playerShip);
    }

    public void UnlinkPlayer() {
        healthController.Unlink();
    }
}
