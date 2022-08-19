
using System;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{

    public event Action<int> OnCurrencyReceived; //Just received, not total balance

    private int totalCurrency;

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
        OnCurrencyReceived?.Invoke(sum);
    }

    public void SpendCurrency(int cost) {
        totalCurrency -= cost;
        if(totalCurrency < 0) {
            throw new NotSupportedException("Can't have currency negative!");
        }
    }
}
