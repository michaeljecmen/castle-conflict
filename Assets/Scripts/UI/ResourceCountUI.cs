using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCountUI : MonoBehaviour, ResourceListener {
    public bool team;
    void Start() {
        Tower t = (team == Entity.LEFT_TEAM) ? WorldManager.getInstance().leftTower : WorldManager.getInstance().rightTower;
        t.registerListener(this);
        t.broadcastResource();
    }

    public void updateResource(int amount) {
        Text txt = GetComponent<Text>();
        txt.text = amount.ToString();
    }
}
