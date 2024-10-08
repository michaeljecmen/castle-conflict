using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, HealthListener {
    public bool team;
    void Start() {
        Tower t = (team == Entity.LEFT_TEAM) ? WorldManager.getInstance().leftTower : WorldManager.getInstance().rightTower;
        t.registerListener(this);
        t.broadcastHealth();
    }

    // draw bar as a fraction of max possible HP
    public void updateHealth(int hp) {
        Slider s = GetComponent<Slider>();
        s.value = (float) hp / WorldManager.getInstance().maxTowerHealth;
    }
}
