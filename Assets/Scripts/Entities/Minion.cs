using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{
    // the power level of this entity
    public int rank = 0;

    // movement speed in units per second.
    public float speed = 1.0F;

    // transforms to act as start and end markers for the journey.
    public Vector3 startVector;
    public Vector3 endVector;

    // time when the movement started.
    private float startTime;

    // total distance between the markers.
    private float journeyLength;

    // cost of this unit, in resources
    public int cost;

    // sets the destination from the current position to be the dest
    protected void setDestination(Vector3 dest) {
        endVector = dest;
        startVector = transform.position;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, dest);
    }
   
    // register the object with the worldmanager and set movement info
    void Start() {
        // team should already be registered at this point
        WorldManager.getInstance().registerUnit(this);

        // ensure the soldier is never seen in the middle
        transform.position = startVector;
        setDestination(endVector);
    }

    // moves the object the appropriate distance to the end
    void Update() {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startVector, endVector, fractionOfJourney);
    }
}
