
using System;
using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currencyText;

    [SerializeField]
    private EnemyActivationTrigger enemyActivationTrigger;

    private int totalCurrency;

    private void OnEnable() {
        enemyActivationTrigger.OnCurrencyReceived += AddCurrency;
    }

    private void OnDisable() {
        enemyActivationTrigger.OnCurrencyReceived -= AddCurrency;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalCurrency = 0;
    }

    public bool HasEnoughCurrency(int cost) {
        return totalCurrency >= cost;
    }

    public void AddCurrency(int sum) {
        totalCurrency += sum;
        UpdateCurrencyText();
    }

    public void SpendCurrency(int cost) {
        totalCurrency -= cost;
        if(totalCurrency < 0) {
            throw new NotSupportedException("Can't have currency negative!");
        }
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText() {
        currencyText.text = totalCurrency.ToString();
    }
}
