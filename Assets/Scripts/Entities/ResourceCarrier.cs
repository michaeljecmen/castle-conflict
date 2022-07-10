using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// intermediate class for any minion that carries resources 
// and needs to turn around to bring them back to their tower
public class ResourceCarrier : Minion {
    protected int carriedValue = 0;
    public int getCarriedValue() {
        return carriedValue;
    }
    public bool isCarrying() {
        return carriedValue != 0;
    }

    // fat interface functions for children to override if needed
    // allows us to reuse the basic oncollisionenter template logic from this class
    public virtual void handleEnemyTowerCollision(Tower enemyTower) {}
    public virtual void handleEnemyResourceCarrierCollision(ResourceCarrier enemyCarrier) {}
    public virtual void handleResourceCollision(Resource resource) {}
    
    // if a unit on the other team hits us, we take damage and die
    // if we collide with their tower, do something (specified by the child class)
    // if we collide with our tower, deposit the resource we've collected
    void OnCollisionEnter2D(Collision2D collision) {
        // can only carry one at a time
        Resource resource = collision.gameObject.GetComponent<Resource>();
        if (resource != null) {
            handleResourceCollision(resource);
            return;
        }

        // if we are carriedValue and we collide with the tower, deposit resources and destroy ourselves
        Tower tower = collision.gameObject.GetComponent<Tower>();
        if (tower != null) {
            // check which team
            if (getTeam() != tower.getTeam()) {
                handleEnemyTowerCollision(tower);
                return;
            } 
            
            if (isCarrying()) {
                // deposit resource and destroy ourselves
                tower.depositResource(carriedValue);
                DestroyEntity();
                return;
            }
        }

        // IMPORTANT: only check if collision should occur once we know
        // we are colliding with a unit, prevent us from not colliding with
        // our own tower when we want to deposit
        if (!shouldCollisionOccur(collision)) {
            return;
        }

        // if we got hit by an attackunit, die
        AttackUnit attacker = collision.gameObject.GetComponent<AttackUnit>();
        if (attacker != null) {
            DestroyEntity();
            return;
        }

        ResourceCarrier carrier = collision.gameObject.GetComponent<ResourceCarrier>();
        if (carrier != null) {
            handleEnemyResourceCarrierCollision(carrier);
            return;
        }
    }
}
