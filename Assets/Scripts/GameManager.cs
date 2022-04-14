using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public Minion[] masterMinionList;
    public int loadoutSize;
    private Minion[] loadout;

    public static GameManager getInstance() {
        return instance;
    }

    private void Awake() {
        if (instance != null && instance != this) {
            // defensive programming, in case multiple singletons spawned (cleanup from previous game not happened)
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        // start everything as null so the loadout selector 
        // can recognize the empty slots
        loadout = new Minion[loadoutSize];
        for (int i = 0; i < loadoutSize; ++i) {
            loadout[i] = null;
        }
    }

    public Minion[] getMasterMinionList() {
        return masterMinionList;
    }

    public int getLoadoutSize() {
        return loadoutSize;
    }
    public Minion[] getLoadout() {
        return loadout;
    }
    public void setLoadout(Minion[] load) {
        loadout = load;
    }

    // create a worldmanager object for the given level
    // change the scene to a level scene and tell worldmanager "go"
    public void changeScene(string level) {
        SceneManager.LoadScene(level);
    }

    public void printMessage(string message) {
        Debug.Log(message);
    }
}
