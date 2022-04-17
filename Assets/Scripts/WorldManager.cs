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
    
    private bool live = false;

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

    // per-object members
    private Hashtable leftTeamUnits = new Hashtable();
    private Hashtable rightTeamUnits = new Hashtable();
    public Minion leftSoldier;
    public Minion rightSoldier;
    public Minion leftDog;
    public Minion rightDog;
    public Minion leftRG;
    public Minion rightRG;

    // TOWERS
    public Tower leftTower;
    public Tower rightTower;
    public List<MinionSpawnListener> leftMinionSpawnListeners = new List<MinionSpawnListener>();
    public List<MinionSpawnListener> rightMinionSpawnListeners = new List<MinionSpawnListener>();

    // UI MEMBERS
    public ResourceCountUI leftResourceCountUI;
    public ResourceCountUI rightResourceCountUI;
    public HealthBar leftTowerHPUI;
    public HealthBar rightTowerHPUI;
    public GameObject spawnButtonPrefab;

    // used to turn the gravity of the game over bar on and off,
    // for engame screens
    public Gravity gameOverBarGravity; // TODO figure out why text and health bar not updating

    public Path groundUnitPath;

    public void setGameOverGravity(bool on) {
        gameOverBarGravity.setGravity(on);
    }

    // listener pattern code
    public void registerLeftMinionSpawnListener(MinionSpawnListener listener) {
        leftMinionSpawnListeners.Add(listener);
    }
    public void registerRightMinionSpawnListener(MinionSpawnListener listener) {
        rightMinionSpawnListeners.Add(listener);
    }
    public void updateLeftMinionSpawn(Minion spawned) {
        foreach (MinionSpawnListener listener in leftMinionSpawnListeners) {
            listener.updateMinionSpawn(spawned);
        }
    }
    public void updateRightMinionSpawn(Minion spawned) {
        foreach (MinionSpawnListener listener in rightMinionSpawnListeners) {
            listener.updateMinionSpawn(spawned);
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

    void Start() {
        // get loadout from gamemanager and create the buttons for left team
        Minion[] loadout = GameManager.getInstance().getLoadout();

        for (int i = 0; i < loadout.Length; i++) {
            GameObject button = Instantiate(spawnButtonPrefab.gameObject, spawnButtonPrefab.gameObject.transform.position, Quaternion.identity);
            SpawnButton sb = button.GetComponent<SpawnButton>();
            sb.initialize(loadout[i]);
            sb.setPosition(i);
        }

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
        // if Q pressed, spawn soldier for red
        if (Input.GetKeyDown(KeyCode.D)) {
            spawnLeft(leftSoldier);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            spawnRight(rightSoldier);
        } 

        if (Input.GetKeyDown(KeyCode.S)) {
            spawnLeft(leftDog);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            spawnRight(rightDog);
        }

        // resource gatherers
        if (Input.GetKeyDown(KeyCode.A)) {
            spawnLeft(leftRG);
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            spawnRight(rightRG);
        }

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
    
    // spawn all minions offscreen
    const float OFFSCREEN = 1000f;
    public void spawnLeft(Minion prefab) {
        if (!leftTower.withdrawResource(prefab.cost)) {
            return;
        }

        // we have enough
        GameObject minion = Instantiate(prefab.gameObject, new Vector3(OFFSCREEN, OFFSCREEN, 1), Quaternion.identity);
        if (minion == null) {
            return;
        }

        minion.GetComponent<Entity>().team = Entity.LEFT_TEAM;
        updateLeftMinionSpawn(minion.GetComponent<Minion>());
    }

    public void spawnRight(Minion prefab) {
        if (!rightTower.withdrawResource(prefab.cost)) {
            return;
        }

        // we have enough
        GameObject minion = Instantiate(prefab.gameObject, new Vector3(OFFSCREEN, OFFSCREEN, 1), Quaternion.identity);
        if (minion == null) {
            return;
        }

        minion.GetComponent<Entity>().team = Entity.RIGHT_TEAM;
        updateRightMinionSpawn(minion.GetComponent<Minion>());
    }
}
