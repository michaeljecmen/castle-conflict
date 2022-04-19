using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHumanPlayer : HumanPlayer {
    public override void onLoaded() {
        base.onLoaded();
        // button spawning should happen here -- AIs don't need buttons
        for (int i = 0; i < loadout.Length; i++) {
            GameObject button = Instantiate(spawnButtonPrefab.gameObject, spawnButtonPrefab.gameObject.transform.position, Quaternion.identity);
            SpawnButton sb = button.GetComponent<SpawnButton>();
            sb.initialize(loadout[i]);
            sb.setPosition(i);
        }
    }

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
