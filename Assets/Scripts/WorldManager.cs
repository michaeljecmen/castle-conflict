using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private static WorldManager instance;
    public static WorldManager getInstance() {
        return instance;
    }
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public int maxTowerHealth;
    public int startingResourceCount;
    public float timeBetweenResourceIncrements;
    public int resourceIncrement;
    private float timestampOfNextResourceIncrement;

    // the resource to spawn
    public GameObject resource;

    // trees will spawn in a range up to X % out from the center
    public float treeSpawnRadius;

    // max and min amount of time between tree spawns
    public float minTreeSpawnTime = 2.0F;
    public float maxTreeSpawnTime = 5.0F;

    // will be set in start
    private float timestampOfNextTreeSpawn;

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
    public ResourceCountUI leftResourceCountUI;
    public ResourceCountUI rightResourceCountUI;
    public HealthBar leftTowerHPUI;
    public HealthBar rightTowerHPUI;

    // used to turn the gravity of the game over bar on and off,
    // for engame screens
    public Gravity gameOverBarGravity;

    public Path groundUnitPath;

    public void setGameOverGravity(bool on) {
        gameOverBarGravity.setGravity(on);
    }

    private void setNextTreeSpawnTime() {
        timestampOfNextTreeSpawn = Time.time + Random.Range(minTreeSpawnTime, maxTreeSpawnTime);
    }

    private void setNextResourceIncrementTime() {
        timestampOfNextResourceIncrement = Time.time + timeBetweenResourceIncrements;
    }

    void Start() {
        // level has loaded, broadcast it to the players
        GameManager.getInstance().levelLoaded();

        // pick the first tree spawn time
        setNextTreeSpawnTime();
        setNextResourceIncrementTime();

        // start towers off with starting resources
        leftTower.depositResource(startingResourceCount);
        rightTower.depositResource(startingResourceCount);

        // start towers off with the correct amount of health
        leftTower.grantHealth(maxTowerHealth);
        rightTower.grantHealth(maxTowerHealth);
    }

    // Update is called once per frame
    void Update() {
        // when the current tree spawn threshhold is hit, spawn and pick the time
        // for the next spawn
        if (Time.time >= timestampOfNextTreeSpawn) {
            // actually spawn the tree as a range of percents out from 50
            float t = Random.Range(.5f - treeSpawnRadius, treeSpawnRadius + .5f);

            // need to spawn the tree with the root touching the ground, so need height for that
            Vector3 treePos = groundUnitPath.getPos(t);
            Instantiate(resource, new Vector3(treePos.x, treePos.y, treePos.z), Quaternion.identity);

            setNextTreeSpawnTime();
        }

        // every so often, give each tower some resources
        if (Time.time >= timestampOfNextResourceIncrement) {
            leftTower.depositResource(resourceIncrement);
            rightTower.depositResource(resourceIncrement);
            
            setNextResourceIncrementTime();
        }
    }
}
