using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHumanPlayer : HumanPlayer {
    void Update() {
        if (!live) {
            return;
        }

        // TODO see same todo from lefthumanplayer
        if (Input.GetKeyDown(KeyCode.J)) {
            WorldManager.getInstance().rightTower.spawnMinion(loadout[0]);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            WorldManager.getInstance().rightTower.spawnMinion(loadout[1]);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            WorldManager.getInstance().rightTower.spawnMinion(loadout[2]);
        }
    }
}
