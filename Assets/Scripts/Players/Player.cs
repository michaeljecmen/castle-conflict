using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour, LevelLoadedListener {
    // player needs a specific loadout
    public Minion[] loadout;
    public Minion[] getLoadout() { return loadout; }
    public void setLoadout(Minion[] load) { loadout = load; }

    // players need to know when the world has loaded
    public virtual void onLoaded() {
        // set our loadout to the default if we have none
        if (loadout == null || loadout.Length == 0) {
            setLoadout(GameManager.getInstance().getDefaultLoadout());
        }
    }

    void Awake() {
        // all players register themselves with the gamemanager as loaded listeners
        GameManager.getInstance().registerListener(this); // TODO deregister this when you no longer exist (for example the greedyai will set itself as a listener from the button screen but will not exist if a diff level is chosen)
    }
}
