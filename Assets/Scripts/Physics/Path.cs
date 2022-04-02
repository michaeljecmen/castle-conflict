using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Paths units take are represented by a function and a float value from 0 to 1.
 * Paths are functions with endpoints, and the fraction of the journey plus
 * a path function yields an exact position. Paths are per-level state.
 */
public class Path {
    public Path() {

    }
    public float getX(float percentOfJourney) {
        return 0.0f; // TODO
    }

    public float getYDelta(float percentOfJourney) {
        return 0.0f;
    }   
}
