using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player {
    // need to spawn buttons for human players
    public GameObject spawnButtonPrefab;
    protected bool live = false;

    public override void onLoaded() {
        Debug.Log("HUMANPLAYER LOADED");
        base.onLoaded();
        live = true;

        // button spawning should happen here -- AIs don't need buttons
        for (int i = 0; i < loadout.Length; i++) {
            GameObject button = Instantiate(spawnButtonPrefab.gameObject, spawnButtonPrefab.gameObject.transform.position, Quaternion.identity);
            SpawnButton sb = button.GetComponent<SpawnButton>();
            sb.initialize(loadout[i]);
            sb.setPosition(i);
        }
    }
}
