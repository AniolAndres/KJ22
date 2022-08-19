using System;
using UnityEngine;


public class LevelController : MonoBehaviour {

    public event Action OnLevelComplete;

    public event Action OnLevelFailed;

    private float scrollSpeed;



    public void OnDestroy() {
        
    }

    public void OnStart() {
        
    }

    private void Update() {
        transform.localPosition += Vector3.left * Time.smoothDeltaTime * scrollSpeed;
    }

    public void SetScrollSpeed(float levelScrollSpeed) {
        scrollSpeed = levelScrollSpeed;
    }
}
