using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// thief steals from enemy RGs that are holding resources,
// or the tower itself
public class Thief : ResourceCarrier {
    // if a unit on the other team hits us, we take damage and they die
    void OnCollisionEnter2D(Collision2D collision) {
        // if we are carriedValue and we collide with the tower, deposit resources and destroy ourselves
        Tower tower = collision.gameObject.GetComponent<Tower>();
        if (tower != null) {
            // check which team
            if (getTeam() != tower.getTeam()) {
                DestroyEntity();
            } else if (carriedValue != 0) {
                // deposit resource and destroy ourselves
                tower.depositResource(carriedValue);
                DestroyEntity();
            }
        }

        // IMPORTANT: only check if collision should occur once we know
        // we are colliding with a unit, prevent us from not colliding with
        // our own tower when we want to deposit
        if (!shouldCollisionOccur(collision)) {
            return;
        }

        // if we got hit by an attackunit, get hit
        AttackUnit attacker = collision.gameObject.GetComponent<AttackUnit>();
        if (attacker != null) {
            DestroyEntity();
        }
    }
}
