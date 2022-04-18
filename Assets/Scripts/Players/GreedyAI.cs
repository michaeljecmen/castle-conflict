using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyAI : Player {
    public float spawnCooldown;
    private float timestampOfNextMinionSpawn;
    private System.Random rand = new System.Random();

    // cost -> to list of minions with that cost
    private SortedDictionary<int, List<Minion>> loadoutByCost;

    class GreedyAIResourceListener : ResourceListener { // TODO don't spawn when dead
        GreedyAI parent;
        public GreedyAIResourceListener(GreedyAI parent_) {
            parent = parent_;
        }
        public void updateResource(int amount) {
            parent.tryToSpawnMinion(amount);
        }
    }

    public override void onLoaded() {
        Debug.Log("GREEDY AI LOADED");

        // costs sorted in reverse order
        loadoutByCost = new SortedDictionary<int, List<Minion>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (Minion m in loadout) {
            if (!loadoutByCost.ContainsKey(m.cost)) {
                loadoutByCost.Add(m.cost, new List<Minion>());
            }
            loadoutByCost[m.cost].Add(m);
        }

        // we are ready to spawn immediately
        timestampOfNextMinionSpawn = Time.time;

        // set up listener
        GreedyAIResourceListener listener = new GreedyAIResourceListener(this);
        WorldManager.getInstance().rightTower.registerListener(listener);
    }

    void tryToSpawnMinion(int amount) {
        // every time our resource count changes, spawn a minion unless we're on cooldown
        if (Time.time >= timestampOfNextMinionSpawn) {
            // pick minion to spawn
            foreach (KeyValuePair<int, List<Minion>> entry in loadoutByCost) {
                // if we can afford to buy this bucket of minions
                if (amount >= entry.Key) {
                    List<Minion> bucket = entry.Value;

                    // pick one at random and spawn it
                    int index = rand.Next(bucket.Count);
                    WorldManager.getInstance().rightTower.spawnMinion(bucket[index]);

                    // update our cooldown
                    timestampOfNextMinionSpawn = Time.time + spawnCooldown;
                    Debug.Log("ts: " + timestampOfNextMinionSpawn);
                    Debug.Log("time: " + Time.time); // TODO fix

                    // leave the loop once we choose one
                    return;
                }
            }
        }
    }
}
