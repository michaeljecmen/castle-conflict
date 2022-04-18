using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHumanPlayer : HumanPlayer {
    void Update() {
        if (!live) {
            return;
        }

        // TODO add a keybinding thing (in inspector)
        if (Input.GetKeyDown(KeyCode.A)) {
            WorldManager.getInstance().leftTower.spawnMinion(loadout[0]);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            WorldManager.getInstance().leftTower.spawnMinion(loadout[1]);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            WorldManager.getInstance().leftTower.spawnMinion(loadout[2]);
        }
    }
}
