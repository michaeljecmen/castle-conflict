using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{
    // the power level of this entity
    public int rank = 0;

    // movement speed in units per second.
    public float speed = 1.0F;
    // based on speed and path length, the time it should take to cross the map
    private float timeToCross;

    // time when the movement started.
    private float startTime;

    // time when the unit turned around
    private float turnTime;

    // whether or not this unit is going forward or backward
    // TODO put in intermediate class bidirectionalmover
    // same with turnBack(), only specific minions will need these
    private bool movingForward = true;

    // TODO put in Ydelta member so we can normalize how things appear on our curve

    // cost of this unit, in resources
    public int cost;

    /*  if we turned around, we're going to be at 1-fraction
     *  on the next frame. we need to make sure our elapsed
     *  measurement has changed so that our previous fraction
     *  equals the current invocation's 1-fraction
     *  prevFraction = (Time.time - startTime) / timeToCross
     *
     *  prevFraction = 1 - ((Time.time - X) / timeToCross)
     *  solve for X
     *
     *  ((Time.time - X) / timeToCross) = 1 - prevFraction
     *  Time.time - X = (1 - prevFraction) * timeToCross
     *  Time.time - (1 - prevFraction) * timeToCross  = X
     *
     *  simplify
     *  Time.time - (1 - ((Time.time - startTime) / timeToCross)) * timeToCross = X
     *  Time.time - (timeToCross - (Time.time - startTime)) = X
     *  Time.time - (timeToCross + startTime - Time.time) = X
     *  2*Time.time - timeToCross - startTime = X
     *   
     *  check our work: set startTime equal to X and see if prevFraction = 1-fraction
     *  prevFraction = (Time.time - startTime) / timeToCross
     *  1 - fraction = 1 - ((Time.time - X) / timeToCross)
     *  1 - fraction = 1 - ((Time.time - (2*Time.time - timeToCross - startTime)) / timeToCross)
     *  1 - fraction = 1 - ((-Time.time + timeToCross + startTime) / timeToCross)
     *  1 - fraction = (timeToCross/timeToCross) - ((-Time.time + timeToCross + startTime) / timeToCross)
     *  1 - fraction = (Time.time - startTime) / timeToCross
     *  1 - fraction == prevFraction
     *  nice!
     *
     *  so set startTime equal to the value which will give us the correct
     *  1-fraction on the next update call
     */
    protected void turnBack() {
        // TODO flip the sprite here too
        movingForward = false;
        turnTime = Time.time;
        startTime = 2*Time.time - timeToCross - startTime;
    }
   
    // register the object with the worldmanager and set movement info
    void Start() {
        // team should already be registered at this point
        WorldManager.getInstance().registerUnit(this);

        // set our time to cross
        timeToCross = WorldManager.getInstance().groundUnitPath.getLength() / speed;

        // init startTime and start moving
        startTime = Time.time;
    }

    // moves the object the appropriate distance to the end
    void Update() {
        // to make the math easier, assume moving RIGHT until the end of this function
        // when the direction will be accounted for
        float elapsed = Time.time - startTime;

        float fractionOfJourney = elapsed / timeToCross;

        bool movingRight = (team == Entity.LEFT_TEAM) == movingForward;
        if (!movingRight) {
            fractionOfJourney = 1-fractionOfJourney;
        }

        // set our position as a fraction of the distance between the markers
        transform.position = WorldManager.getInstance().groundUnitPath.getPos(fractionOfJourney);
    }
}
