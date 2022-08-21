
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class HealthPipView : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector appearDirector;

    [SerializeField]
    private PlayableDirector disappearDirector;

    [SerializeField]
    private Image image;

    public void PlayAppearDirector() {
        disappearDirector.Stop();
        appearDirector.Play();
    }

    public void PlayDisappearDirector() {
        appearDirector.Stop();
        disappearDirector.Play();
    }

    public void Reset() {
        transform.localScale = Vector3.one;
        image.color = Color.black;
    }
}
