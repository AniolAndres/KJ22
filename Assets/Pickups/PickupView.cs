
using System;
using UnityEngine;

public enum PickupType {
    Health,
    Energy
}


public class PickupView : MonoBehaviour
{
    [SerializeField]
    private PickupType type;

    [SerializeField]
    private int amount;

    private bool used;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (used) {
            return;
        }

        var playerScript = collision.gameObject.GetComponent<PlayerScript>();
        if(playerScript == null) {
            return;
        }

        switch (type) {
            case PickupType.Energy:
                playerScript.GiveEnergy(amount);
                break;
            case PickupType.Health:
                playerScript.Heal(amount);
                break;
            default:
                throw new ArgumentOutOfRangeException("Pkcup type invalid!");
        }


        used = true;
        gameObject.SetActive(false);

    }

}
