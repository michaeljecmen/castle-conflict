using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * Paths units take are represented by a function and a float value from 0 to 1.
 * Paths are functions with endpoints, and the fraction of the journey plus
 * a path function yields an exact position. Paths are per-level state.
 * 
 * Takes in a function y = ax^2 + bx + c, and a range of x values to consider.
 * For now, b is fixed at 0 because that makes the math actually doable.
 */
public class Path : MonoBehaviour {
    // public float a;
    // public float c;
    // public float startX;
    // public float stopX;
    // public int integralAccuracy;
    // private float xLen;

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    private float length;

    void Start() {
        // only do the length calculations once
        // from https://stackoverflow.com/questions/29438398/cheap-way-of-calculating-cubic-bezier-length
        float chord = Vector3.Distance(p3, p0);
        float cont_net = Vector3.Distance(p0, p1) + Vector3.Distance(p2, p1) + Vector3.Distance(p3, p2);
        length = (cont_net + chord) / 2;
    }

    // returns the total path length for this curve
    public float getLength() {
        return length;
    }

    // pass in t in range [0, 1] and get point along curve
    public Vector3 getPos(float t) {     
        float r = 1f - t;
        float f0 = r * r * r;
        float f1 = r * r * t * 3;
        float f2 = r * t * t * 3;
        float f3 = t * t * t;
         return new Vector3(
            f0*p0.x + f1*p1.x + f2*p2.x + f3*p3.x,
            f0*p0.y + f1*p1.y + f2*p2.y + f3*p3.y,
            f0*p0.z + f1*p1.z + f2*p2.z + f3*p3.z
        );
    }
}
