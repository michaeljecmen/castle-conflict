using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity {
    const float OFFSCREEN = 1000f;
    private int health;
    private int resourceCount;
    public List<ResourceListener> resourceListeners = new List<ResourceListener>();
    public List<HealthListener> healthListeners = new List<HealthListener>();
    public List<MinionSpawnListener> minionSpawnListeners = new List<MinionSpawnListener>();

    // support listening to resource and health updates
    public void registerListener(HealthListener listener) {
        healthListeners.Add(listener);
    }
    public void registerListener(ResourceListener listener) {
        resourceListeners.Add(listener);
    }
    public void registerListener(MinionSpawnListener listener) {
        minionSpawnListeners.Add(listener);
    }
    public void broadcastHealth() {
        foreach (HealthListener listener in healthListeners) {
            listener.updateHealth(health);
        }
    }
    public void broadcastResource() {
        foreach (ResourceListener listener in resourceListeners) {
            listener.updateResource(resourceCount);
        }
    }
    public void broadcastMinionSpawn(Minion spawned) {
        foreach (MinionSpawnListener listener in minionSpawnListeners) {
            listener.updateMinionSpawn(spawned);
        }
    }

    public void spawnMinion(Minion prefab) {
        if (health <= 0) {
            return;
        }

        if (!withdrawResource(prefab.cost)) {
            return;
        }

        // we have enough
        GameObject minion = Instantiate(prefab.gameObject, new Vector3(OFFSCREEN, OFFSCREEN, 1), Quaternion.identity);
        if (minion == null) {
            return;
        }

        minion.GetComponent<Entity>().team = team;
        broadcastMinionSpawn(minion.GetComponent<Minion>());
    }

    // store resource in tower
    public void depositResource(int amt) {
        resourceCount += amt;
        broadcastResource();
    }

    // return whether or not you had enough resource to withdraw
    public bool withdrawResource(int amt) {
        if (resourceCount < amt) {
            return false;
        }
        resourceCount -= amt;
        broadcastResource();
        return true;
    }

    // take the specified amount of damage
    public void takeDamage(int damage) {
        health -= damage;
        broadcastHealth();
        if (health <= 0) {
            // destroy the parent container which houses us
            // but before we do, trigger the gravity of the gameover object
            WorldManager.getInstance().setGameOverGravity(true);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void grantHealth(int amount) {
        health += amount;
        broadcastHealth();
    }

    // if a unit on the other team hits us, we take damage and they die
    void OnCollisionEnter2D(Collision2D collision) {
        if (!shouldCollisionOccur(collision)) {
            return;
        }

        // resolve collision
        AttackUnit otherAttackUnit = collision.gameObject.GetComponent<AttackUnit>();
        if (otherAttackUnit != null) {
            // we take damage here
            takeDamage(otherAttackUnit.towerDamage);
        }
    }
}
