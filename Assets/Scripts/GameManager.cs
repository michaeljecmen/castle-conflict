using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public Minion[] masterMinionList;
    public int loadoutSize;
    public Minion[] defaultLoadout;
    private Player leftPlayer;
    private Player rightPlayer;
    private List<LevelLoadedListener> loadedListeners = new List<LevelLoadedListener>();

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

    public Minion[] getMasterMinionList() {
        return masterMinionList;
    }

    public int getLoadoutSize() {
        return loadoutSize;
    }

    public Player getRightPlayer() {
        return rightPlayer;
    }
    public Player getLeftPlayer() {
        return leftPlayer;
    }
    public void setRightPlayer(Player player) {
        rightPlayer = player;
    }
    public void setLeftPlayer(Player player) {
        leftPlayer = player;
    }

    // for level loaded alerts
    public void registerListener(LevelLoadedListener listener) {
        loadedListeners.Add(listener);
    }
    public void broadcastLevelLoaded() {
        foreach (LevelLoadedListener l in loadedListeners) {
            l.onLoaded();
        }
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
