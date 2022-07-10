using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public Minion[] masterMinionList;
    public int loadoutSize;
    private Minion[] defaultLoadout;
    private Player leftPlayer;
    private Player rightPlayer;
    private List<LevelLoadedListener> loadedListeners = new List<LevelLoadedListener>();

    // used for tracking who is picking from the loadout manager
    public Player currentLoadoutSelector;
    private string previousSceneName = "MainMenu";

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

        // set default loadout to the first N from the master list
        defaultLoadout = new Minion[loadoutSize];
        for (int i = 0; i < loadoutSize; ++i) {
            defaultLoadout[i] = masterMinionList[i];
        }
    }

    public Minion[] getDefaultLoadout() {
        return defaultLoadout;
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
    public void levelLoaded() {
        // instantiate the right and left player as gameobjects
        // the scene. this ensures their update and start methods
        // will be called at the right times.
        // also set their towers so they know who's who
        GameObject lp = Instantiate(leftPlayer.gameObject, new Vector3(0,0,0), Quaternion.identity);
        leftPlayer = lp.GetComponent<Player>();

        GameObject rp = Instantiate(rightPlayer.gameObject, new Vector3(0,0,0), Quaternion.identity);
        rightPlayer = rp.GetComponent<Player>();

        // now that the players are registered as listeners, let everyone know we're loaded
        foreach (LevelLoadedListener l in loadedListeners) {
            l.onLoaded();
        }
    }

    // create a worldmanager object for the given level
    // change the scene to a level scene and tell worldmanager "go"
    public void changeScene(string level) {
        // remember the previous scene name before we switch
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(level);
    }
    public void goBack() {
        changeScene(previousSceneName);
    }

    public void printMessage(string message) {
        Debug.Log(message);
    }
}
