using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// thief steals from enemy RGs that are holding resources,
// or the tower itself
public class Thief : ResourceCarrier {
    public int stealFromTower;

    // when we collide with the other tower, steal a set amount of resources
    public override void handleEnemyTowerCollision(Tower enemyTower) {
        // steal what we want to take if there's enough, otherwise
        // take as much as possible
        int take = enemyTower.getResourceCount() - stealFromTower >= 0 ? stealFromTower : enemyTower.getResourceCount();
        if (!enemyTower.withdrawResource(take)) {
            Debug.Log("WARN: tower did not yield resources to the thief");
        }

        carriedValue = take;
        turnBack();
    }

    // when we collide with an enemy resource carrier, take what they're holding (if they are)
    public override void handleEnemyResourceCarrierCollision(ResourceCarrier enemyCarrier) {
        // if we're carrying, do nothing
        if (isCarrying()) {
            return;
        }
        
        // if the enemy isn't carrying, do nothing
        if (!enemyCarrier.isCarrying()) {
            return;
        }

        // otherwise take what they've got and destroy them
        carriedValue = enemyCarrier.getCarriedValue();
        enemyCarrier.DestroyEntity();
        turnBack();
    }
}
