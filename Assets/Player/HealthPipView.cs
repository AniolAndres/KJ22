
using UnityEngine;
using UnityEngine.Playables;

public class HealthPipView : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector appearDirector;

    [SerializeField]
    private PlayableDirector disappearDirector;

    public void PlayAppearDirector() {
        disappearDirector.Stop();
        appearDirector.Play();
    }

    public void PlayDisappearDirector() {
        appearDirector.Stop();
        disappearDirector.Play();
    }
}
