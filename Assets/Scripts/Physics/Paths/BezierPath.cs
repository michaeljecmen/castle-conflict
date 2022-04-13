using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * Given a p0 p1 p2 and p3, generate a Bezier curve and 
 * provide access to the location of an object along
 * the curve at time t from [0, 1].
 */
public class BezierPath : Path {
    public Vector3 p0 = new Vector3(-8, 3, 0);
    public Vector3 p1 = new Vector3(-7, -4, 0);
    public Vector3 p2 = new Vector3(7, -4, 0);
    public Vector3 p3 = new Vector3(8, -3, 0);

    private float length;

    void Start() {
        // only do the length calculations once
        // from https://stackoverflow.com/questions/29438398/cheap-way-of-calculating-cubic-bezier-length
        float chord = Vector3.Distance(p3, p0);
        float cont_net = Vector3.Distance(p0, p1) + Vector3.Distance(p2, p1) + Vector3.Distance(p3, p2);
        length = (cont_net + chord) / 2;
    }

    // returns the total path length for this curve
    public override float getLength() {
        return length;
    }

    // pass in t in range [0, 1] and get point along curve
    public override Vector3 getPos(float t) {     
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
