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
    public float a;
    public float c;
    public float startX;
    public float stopX;
    public int integralAccuracy;
    private float xLen;

    void Start() {
        xLen = stopX - startX;

        // compute total arc length of journey
        // s = integral(a, b) over sqrt(1 + (f'(x))^2) dx

        // wolfram alpha that and we can simplify to 
        // s = (1/a) integral(a,b) over sec^3(u) du
        // where u = tan inverse(ax)
        // simplifies to (1/2a)(secutanu + ln|secu + tanu|)
        // secu = sec(tan inverse(ax)) = sqrt(a^2x^2 + 1)
        // tanu = tan(tan inverse(ax)) = ax

        // s = (1/2a)((ax)sqrt(a^2x^2 + 1) + ln|sqrt(a^2x^2 + 1) + ax|)
        // now do it from startX to stopX

        xLen = computeArcLength(a, c, startX, stopX, integralAccuracy);
    }

    public float getX(float fractionOfJourney) {
        return (xLen * fractionOfJourney) + startX;
    }

    public float getY(float x) {
        return a*x*x + c;
    }

    // estimates an arc length of a quadratic equation using the rectangle area method
    // increase grain to increase accuracy. assume b is zero for now.
    private float computeArcLength(float a, float c, float xStart, float xStop, int grain) {
        float xp, y, s, result = 0, g = (xStop - xStart) / grain;
        for (int i = 0; i < grain; i++) {
            xp = xStart + g;
            // y = (a * Math.Pow(xp, 2)) + (b * xp) + c;
            // instead of calculating integral over y = ax2 + bx + c,
            // compute over sqrt(1 + (f'(x))^2)
            y = (float) Math.Sqrt(1 + 4*a*a);

            s = g * y;
            result += s;
        }
        return result;
    }

    // TODO idea:
    // make the minion just have a direction bool (toward or away)
    // and the speed is now "amount of time to cross map"
    // then, the fraction of journey is easy -- it's just time elapsed over time to cross
    // then make a turn around function which sets dest back to home tower
    // and all we have to do is use the normalized equation for arc length
    // try parametric quadratic equation? x and y in terms of t?
    // https://www.math-only-math.com/parametric-equations-of-a-parabola.html
    // and add constant C to all ys
    // and in minion use a deltaY to offset each position y based on sprite height
}
