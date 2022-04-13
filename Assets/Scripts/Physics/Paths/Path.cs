using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for all path objects which provides
// a common interface by which paths can be used
public abstract class Path : MonoBehaviour {
    // returns the total path length for this path
    public abstract float getLength();

    // pass in time t in range [0, 1] and get point along path
    public abstract Vector3 getPos(float t);
}
