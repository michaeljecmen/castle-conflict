using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player {
    // need to spawn buttons for human players
    public GameObject spawnButtonPrefab;
    protected bool live = false;

    public override void onLoaded() {
        base.onLoaded();
        live = true;
    }
}
