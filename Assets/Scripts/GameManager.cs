using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Minion[] masterMinionList;
    public Minion[] loadout;
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

    public void setLoadout(Minion[] list) {
        // TODO when the units are selected in the "choose your loadout" screen,
        // call this function before creating the level
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
