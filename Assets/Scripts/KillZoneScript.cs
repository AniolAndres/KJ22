using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        var enemyScript = collision.gameObject.GetComponent<EnemyShipScript>();
        if (enemyScript != null) {
            Destroy(enemyScript.gameObject);

        }
    }
}
