
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CurrencyController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currencyText;

    [SerializeField]
    private EnemyActivationTrigger enemyActivationTrigger;

    [SerializeField]
    private PlayableDirector currencyAddedDirector;

    [SerializeField]
    private PlayableDirector notEnoughCurrencyDirector;

    private PlayerScript playerScript;

    private int totalCurrency;


    // Start is called before the first frame update
    void Start()
    {
        totalCurrency = 0;
    }

    public bool HasEnoughCurrency(int cost) {

        if(totalCurrency >= cost) {
            return true;
        }

        notEnoughCurrencyDirector.Stop();
        notEnoughCurrencyDirector.time = 0;
        notEnoughCurrencyDirector.Evaluate();
        notEnoughCurrencyDirector.Play();

        return false;
    }

    public void AddCurrency(int sum) {
        currencyAddedDirector.Stop();
        currencyAddedDirector.time = 0;
        currencyAddedDirector.Evaluate();
        currencyAddedDirector.Play();

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

    public void Clear() {
        totalCurrency = 0;
        UpdateCurrencyText();
    }

    public void LinkPlayer(PlayerScript playerShip) {
        this.playerScript = playerShip;
        playerScript.OnEnergyReceived += AddCurrency;
    }


    public void Unlink() {
        playerScript.OnEnergyReceived -= AddCurrency;
    }
}
