using System;
using UnityEngine;
using UnityEngine.Playables;

public class HealthController : MonoBehaviour {

    [SerializeField]
    private HealthPipView[] healthPips;

  
    private PlayerScript player;

    [SerializeField]
    private PlayableDirector shakeDirector;

    public void LinkPlayer(PlayerScript player) {
        this.player = player;
        player.OnHealReceived += OnHeal;
        player.OnDamageTaken += OnDamage;
    }

    public void Unlink() {
        player.OnHealReceived -= OnHeal;
        player.OnDamageTaken -= OnDamage;
    }

    private void OnDamage(int currentHp, int damage) {

        shakeDirector.Stop();
        shakeDirector.time = 0;
        shakeDirector.Evaluate();
        shakeDirector.Play();

        for(int i = currentHp + damage; i > currentHp; --i) {
            healthPips[i-1].PlayDisappearDirector();
        }
    }

    private void OnHeal(int currentHp, int heal) {
        for (int i = currentHp - heal; i < currentHp; ++i) {
            healthPips[i].PlayAppearDirector();
        }
    }

    public void Clear() {
        Unlink();
    }
}
