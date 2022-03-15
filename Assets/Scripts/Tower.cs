using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    private int health;
    private int resourceCount;

    public int getResourceCount() {
        return resourceCount;
    }

    // store resource in tower
    public void depositResource(int amt) {
        resourceCount += amt;
        WorldManager.getInstance().updateResourceUI(team, resourceCount);
    }

    // return whether or not you had enough resource to withdraw
    public bool withdrawResource(int amt) {
        if (resourceCount < amt) {
            return false;
        }
        resourceCount -= amt;
        WorldManager.getInstance().updateResourceUI(team, resourceCount);
        return true;
    }

    // take the specified amount of damage
    public void takeDamage(int damage) {
        health -= damage;
        WorldManager.getInstance().updateTowerHPUI(team, health);
        if (health <= 0) {
            // destroy the parent container which houses us
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void grantHealth(int amount) {
        health += amount;
        WorldManager.getInstance().updateTowerHPUI(team, health);
    }

    // if a unit on the other team hits us, we take damage and they die
    void OnCollisionEnter2D(Collision2D collision) {
        if (!shouldCollisionOccur(collision)) {
            return;
        }

        // resolve collision
        AttackUnit otherAttackUnit = collision.gameObject.GetComponent<AttackUnit>();
        if (otherAttackUnit != null) {
            // we take damage here
            takeDamage(otherAttackUnit.towerDamage);
        }

        // TODO when different unit types hit do diff things (e.g. thief)
    }
}
