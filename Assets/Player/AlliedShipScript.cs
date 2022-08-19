using System;
using UnityEngine;


public class AlliedShipScript : MonoBehaviour {

    private RectTransform rectTransform => transform as RectTransform;

    private float interpolator;

    private ShipData shipData;

    private IPolarCurve polarCurve;

    private PlayerScript player;

    public void Setup(ShipData shipData, IPolarCurve polarCurve, PlayerScript player) {
        this.polarCurve = polarCurve;
        this.player = player;
        this.shipData = shipData;
    }

    private void Update() {
        interpolator += Time.smoothDeltaTime * shipData.speed;
        var position = polarCurve.CalculatePosition(interpolator);
        position = position + player.gameObject.transform.position;
        var newPosition = Vector2.Lerp(rectTransform.position, position, 0.8f);
        rectTransform.position = newPosition;
    }
}
