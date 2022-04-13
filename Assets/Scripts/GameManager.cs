using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Minion[] masterMinionList;
    public int maxLoadoutSize;
    private int currentLoadoutSize = 0;
    public Minion[] loadout; // TODO make this private once it works
    public GameObject loadoutMinionButton;
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
        loadout = new Minion[maxLoadoutSize];
        for (int i = 0; i < maxLoadoutSize; ++i) {
            loadout[i] = null;
        }
    }

    // put in first null location
    public void addToLoadout(Minion minion) {
        for (int i = 0; i < currentLoadoutSize; ++i) {
            // TODO try with loadout[i] == minion
            if (loadout[i] == null) {
                loadout[i] = minion;
                currentLoadoutSize++;
                return;
            }
        }
    }
    
    public void removeFromLoadout(Minion minion) {
        for (int i = 0; i < currentLoadoutSize; ++i) {
            // TODO try with loadout[i] == minion
            if (loadout[i].gameObject.name == minion.gameObject.name) {
                loadout[i] = null;
                currentLoadoutSize--;
                return;
            }
        }
    }

    public Minion[] getLoadout() {
        return loadout;
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
