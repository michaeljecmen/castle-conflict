using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public WorldManager currentLevel;
    public Minion[] masterMinionList;
    public Minion[] loadout;
    public static GameManager getInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // defensive programming, in case multiple singletons spawned (cleanup from previous game not happened)
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public WorldManager getCurrentLevel() {
        return currentLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLoadout(Minion[] list) {
        // TODO when the units are selected in the "choose your loadout" screen,
        // call this function before creating the level
    }
    public Minion[] getLoadout() {
        return loadout;
    }

    // TODO: this function should
    // create a worldmanager object for the given level
    // change the scene to a level scene and tell worldmanager "go"
    public void createLevel() {
        currentLevel = new WorldManager();

        // TODO change scene to "level scene"

        // at very end, start the world
        currentLevel.go();
    }
}
