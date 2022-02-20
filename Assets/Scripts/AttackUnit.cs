using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnit : MovingObject
{
    // damage we deal to the enemy tower if we hit
    public int towerDamage;

    // If colliding with other other team's entities
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shouldCollisionOccur(collision)) {
            return;
        }
        
        // resolve collision
        MovingObject otherMO = collision.gameObject.GetComponent<MovingObject>();
        if (otherMO != null) {
            // here check if we got destroyed and if so, die
            // do not destroy the other guy, that's his job
            if (rank <= otherMO.rank) {
                DestroyEntity();
            }
        }

        // when colliding with the enemy tower, attack units should die
        Tower otherTower = collision.gameObject.GetComponent<Tower>();
        if (otherTower != null) {
            DestroyEntity();
        }
    }
}
