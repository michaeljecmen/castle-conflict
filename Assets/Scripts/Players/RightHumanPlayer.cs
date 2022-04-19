using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHumanPlayer : HumanPlayer {
    public override void onLoaded() {
        base.onLoaded();
        for (int i = 0; i < loadout.Length; i++) {
            GameObject button = Instantiate(spawnButtonPrefab.gameObject, spawnButtonPrefab.gameObject.transform.position, Quaternion.identity);
            SpawnButton sb = button.GetComponent<SpawnButton>();
            sb.initialize(loadout[i]);

            // offset + i*(button width) is used, so pass negative i
            // to get buttons moving right to left (towards negative x)
            // instead of vice versa
            sb.setPosition(-1 *i);
        }
    }

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
