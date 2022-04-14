using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MinionButtonGenerator {   
    // called when button clicked
    public void spawnMinionForLeftTeam() {
        WorldManager.getInstance().spawnLeft(prefab);
    }
}
