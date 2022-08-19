using System;
using UnityEngine;


public class LevelController : MonoBehaviour {

    public event Action OnLevelComplete;

    public event Action OnLevelFailed;

    public void OnDestroy() {
        
    }

    public void OnStart() {
        
    }
}
