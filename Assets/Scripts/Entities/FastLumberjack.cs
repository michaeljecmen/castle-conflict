using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastLumberjack : Lumberjack {
    // fast lumberjacks gain twice the value of the resource
    public override int getResourceValue(Resource resource) {
        return resource.value * 2;
    }
}
