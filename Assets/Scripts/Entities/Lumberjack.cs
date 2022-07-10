using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : ResourceCarrier {
    // when we collide with the other tower, destroy ourselves
    public override void handleEnemyTowerCollision(Tower enemyTower) {
        DestroyEntity();
    }

    // can be overridden by child lumberjacks to determine how many resources they get
    // by default it's the full value of the resource
    public virtual int getResourceValue(Resource resource) {
        return resource.value;
    }

    // pick up resource and take the full value therein
    // do nothing if already carrying something
    public override void handleResourceCollision(Resource resource) {
        if (isCarrying()) {
            return;
        }

        carriedValue = getResourceValue(resource);
        resource.onCollected();

        // now work back towards our tower
        turnBack();
    }
}
