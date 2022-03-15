using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // static members
    private static WorldManager instance;
    public static WorldManager getInstance() // TODO disable copying/moving
    {
        return instance;
    }

    public int startingTowerHealth;
    public int startingResourceCount;
    public float timeBetweenResourceIncrements;
    public int resourceIncrement;
    private float timestampOfNextResourceIncrement;

    // the resource to spawn
    public GameObject resource;

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

    // TODO find better solution
    public Minion leftSoldier;
    public Minion rightSoldier;
    public Minion leftDog;
    public Minion rightDog;
    public Minion leftRG;
    public Minion rightRG;

    // TOWERS
    public Tower leftTower;
    public Tower rightTower;

    // UI MEMBERS
    public TextUI leftResourceCountUI;
    public TextUI rightResourceCountUI;
    public TextUI leftTowerHPUI;
    public TextUI rightTowerHPUI;

    public void updateResourceUI(bool team, int amt) { // TODO fix switching functions
        if (team) {
            leftResourceCountUI.updateDisplay(amt);
        } else {
            rightResourceCountUI.updateDisplay(amt);
        }
    }

     public void updateTowerHPUI(bool team, int hp) {
        if (team) {
            leftTowerHPUI.updateDisplay(hp);
        } else {
            rightTowerHPUI.updateDisplay(hp);
        }
    }

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

    private void setNextResourceIncrementTime() {
        timestampOfNextResourceIncrement = Time.time + timeBetweenResourceIncrements;
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
        setNextResourceIncrementTime();

        // start towers off with starting resources
        leftTower.depositResource(startingResourceCount);
        rightTower.depositResource(startingResourceCount);

        // start towers off with the correct amount of health
        leftTower.grantHealth(startingTowerHealth);
        rightTower.grantHealth(startingTowerHealth);
    }

    // Update is called once per frame
    void Update() {
        // if Q pressed, spawn soldier for red
        if (Input.GetKeyDown(KeyCode.D)) {
            SpawnLeft(leftSoldier);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            SpawnRight(rightSoldier);
        } 
        // TODO make it easier to register new units as a dev
        if (Input.GetKeyDown(KeyCode.S)) {
            SpawnLeft(leftDog);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            SpawnRight(rightDog);
        }

        // resource gatherers
        if (Input.GetKeyDown(KeyCode.A)) {
            SpawnLeft(leftRG);
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            SpawnRight(rightRG);
        }

        // when the current tree spawn threshhold is hit, spawn and pick the time
        // for the next spawn
        if (Time.time >= timestampOfNextTreeSpawn) {
            // actually spawn the tree
            float treeX = Random.Range(-1F * treeSpawnRadius, treeSpawnRadius);
            Instantiate(resource, new Vector3(treeX, treeSpawnHeight, 0F), Quaternion.identity);

            setNextTreeSpawnTime();
        }

        // every so often, give each tower some resources
        if (Time.time >= timestampOfNextResourceIncrement) {
            leftTower.depositResource(resourceIncrement);
            rightTower.depositResource(resourceIncrement);
            
            setNextResourceIncrementTime();
        }
    }

    private void SpawnLeft(Minion prefab) {
        if (leftTower.withdrawResource(prefab.cost)) {
            SpawnMinion(prefab, Entity.LEFT_TEAM);
        }
    }

    private void SpawnRight(Minion prefab) {
        if (rightTower.withdrawResource(prefab.cost)) {
            SpawnMinion(prefab, Entity.RIGHT_TEAM);
        }
    }

    // instantiates a copy of the prefab and sets the team member variable
    private void SpawnMinion(Minion prefab, bool team) {
        GameObject minion = Instantiate(prefab.gameObject, prefab.gameObject.transform.position, Quaternion.identity);
        if (minion != null) {
            minion.GetComponent<Entity>().team = team;
        }
    }
}
