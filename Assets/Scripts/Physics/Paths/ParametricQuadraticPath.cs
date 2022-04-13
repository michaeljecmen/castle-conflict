using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricQuadraticPath : Path {
    // returns the total path length for this path
    public override float getLength() {
        return 0.0f;
    }

    // pass in time t in range [0, 1] and get point along path
    public override Vector3 getPos(float t) {
        return new Vector3(0,0,0);
    }
}
