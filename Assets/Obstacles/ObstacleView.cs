using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private int damage;

    private float rotationDirection;


    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f, rotationSpeed * rotationDirection * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var playerScript = collision.gameObject.GetComponent<PlayerScript>();
        if(playerScript != null) {
            playerScript.TakeDamage(damage);
        }
    }
}
