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

    // trees will spawn in a range up to X units out from the center
    public float treeSpawnRadius;
    public float treeSpawnHeight;

    // max and min amount of time between tree spawns
    public float minTreeSpawnTime = 2.0F;
    public float maxTreeSpawnTime = 5.0F;

    // will be set in start
    private float timestampOfNextTreeSpawn;

    // per-object members
    private Hashtable leftTeamUnits = new Hashtable();
    private Hashtable rightTeamUnits = new Hashtable();

    // TODO find better solutiona
    public GameObject leftSoldier;
    public GameObject rightSoldier;
    public GameObject leftDog;
    public GameObject rightDog;
    public GameObject leftRG;
    public GameObject rightRG;

    // the resource to spawn
    public GameObject resource;

    // public functions
    public void registerUnit(Entity unit) {
        if (unit.team == Entity.LEFT_TEAM) {
            leftTeamUnits.Add(unit.gameObject.GetInstanceID(), unit);
        } else {
            rightTeamUnits.Add(unit.gameObject.GetInstanceID(), unit);
        }
    }
    public void destroyUnit(Entity unit) {
        if (unit.team == Entity.LEFT_TEAM) {
            leftTeamUnits.Remove(unit.gameObject.GetInstanceID());
        } else {
            rightTeamUnits.Remove(unit.gameObject.GetInstanceID());
        }
    }

    private void setNextTreeSpawnTime() {
        timestampOfNextTreeSpawn = Time.time + Random.Range(minTreeSpawnTime, maxTreeSpawnTime);
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
    void Start() {
        // pick the first tree spawn time
        setNextTreeSpawnTime();
    }

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

        // resource gatherers
        if (Input.GetKeyDown(KeyCode.A)) {
            SpawnMinion(leftRG, Entity.LEFT_TEAM);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            SpawnMinion(rightRG, Entity.RIGHT_TEAM);
        }

        // when the current tree spawn threshhold is hit, spawn and pick the time
        // for the next spawn
        if (Time.time >= timestampOfNextTreeSpawn) {
            // actually spawn the tree
            float treeX = Random.Range(-1F * treeSpawnRadius, treeSpawnRadius);
            Instantiate(resource, new Vector3(treeX, treeSpawnHeight, 0F), Quaternion.identity);

            setNextTreeSpawnTime();
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
