using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float edgeMargin;

    [SerializeField]
    private Transform swarmParent;

    private RectTransform rectTransform => transform as RectTransform;

    private readonly CurveFactory curveFactory = new CurveFactory();

    private readonly List<AlliedShipScript> alliedShips = new List<AlliedShipScript>(100);

    private Vector2 parentSize;

    private void Start() {
        var parentRectTransform = transform.parent.transform as RectTransform;
        parentSize = parentRectTransform.rect.size;
    }

    public void SpawnShip(ShipData shipData, BulletPool bulletPool) {
        var shipView = Instantiate(shipData.alliedShipPrefab, swarmParent);
        var curve = curveFactory.GetRandomCurve();
        shipView.Setup(shipData, curve, this, bulletPool);
        alliedShips.Add(shipView);
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = Vector2.zero;
        if (GameInputLocker.GetKey(KeyCode.W) || GameInputLocker.GetKey(KeyCode.UpArrow)) {
            velocity += Vector2.up;
        }
        
        if (GameInputLocker.GetKey(KeyCode.S) || GameInputLocker.GetKey(KeyCode.DownArrow)) {
            velocity += Vector2.down;
        }
        
        if (GameInputLocker.GetKey(KeyCode.D) || GameInputLocker.GetKey(KeyCode.RightArrow)) {
            velocity += Vector2.right;
        }
        
        if (GameInputLocker.GetKey(KeyCode.A) || GameInputLocker.GetKey(KeyCode.LeftArrow)) {
            velocity += Vector2.left;
        }
        var newPosition = rectTransform.anchoredPosition + speed * velocity * Time.smoothDeltaTime;

        if(newPosition.y > parentSize.y / 2f - edgeMargin) {
            newPosition.y = parentSize.y / 2f - edgeMargin;
        }
        else if (newPosition.y < -parentSize.y / 2f + edgeMargin) {
            newPosition.y = -parentSize.y / 2f + edgeMargin;
        }

        if (newPosition.x > parentSize.x / 2f - edgeMargin) {
            newPosition.x = parentSize.x / 2f - edgeMargin;
        } else if (newPosition.x < -parentSize.x / 2f + edgeMargin) {
            newPosition.x = -parentSize.x / 2f + edgeMargin;
        }

        rectTransform.anchoredPosition = newPosition;
    }
}
