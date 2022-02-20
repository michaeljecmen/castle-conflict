using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGatherer : MovingObject
{
    public Vector3 towerLocation;
    private int carriedValue = 0;

    // if a unit on the other team hits us, we take damage and they die
    void OnCollisionEnter2D(Collision2D collision) {
        // resolve collision
        Resource resource = collision.gameObject.GetComponent<Resource>();

        // can only carry one at a time
        if (resource != null && carriedValue == 0) {
            carriedValue = resource.value;

            // now work back towards our tower
            setDestination(towerLocation);
        }

        // if we are carriedValue and we collide with the tower, deposit resources and destroy ourselves
        Tower tower = collision.gameObject.GetComponent<Tower>();
        if (tower != null) {
            // check which team
            if (team != tower.team) {
                DestroyEntity();
            } else if (carriedValue != 0) {
                // deposit resource and destroy ourselves
                tower.depositResource(carriedValue);
                DestroyEntity();
            }
        }
    }
}
