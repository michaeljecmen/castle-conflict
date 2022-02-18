using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // static members
    private static WorldManager instance;
    public static WorldManager getInstance()
    {
        return instance;
    }

    // per-object members
    private Hashtable leftTeamUnits = new Hashtable();
    private Hashtable rightTeamUnits = new Hashtable();

    // TODO find better solution
    public GameObject leftSoldier;
    public GameObject rightSoldier;
    public GameObject leftDog;
    public GameObject rightDog;

    // public functions
    public void registerUnit(Entity unit) {
        if (unit.team) {
            leftTeamUnits.Add(unit.getId(), unit);
        } else {
            rightTeamUnits.Add(unit.getId(), unit);
        }
    }
    public void destroyUnit(Entity unit) {
        if (unit.team) {
            leftTeamUnits.Remove(unit.getId());
        } else {
            rightTeamUnits.Remove(unit.getId());
        }
    }

    // TODO need a left and right team container of units
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

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {
        // if Q pressed, spawn soldier for red
        if (Input.GetKeyDown(KeyCode.Q)) {
            SpawnMinion(leftSoldier, Entity.LEFT_TEAM);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            SpawnMinion(rightSoldier, Entity.RIGHT_TEAM);
        } 
        // TODO make it easier to register new units
        if (Input.GetKeyDown(KeyCode.Z)) {
            SpawnMinion(leftDog, Entity.LEFT_TEAM);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            SpawnMinion(rightDog, Entity.RIGHT_TEAM);
        } 
    }

    // instantiates a copy of the prefab and sets the team member variable
    private void SpawnMinion(GameObject prefab, bool team) {
        GameObject minion = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
        if (minion != null) {
            minion.GetComponent<Entity>().team = team;
        }
    }
}
