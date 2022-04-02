using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for all living objects
public class Entity : MonoBehaviour
{
    // constants for the Entity class
    public const bool LEFT_TEAM = true;
    public const bool RIGHT_TEAM = false;

    // denotes which team the unit is on
    public bool team = LEFT_TEAM;

    // child classes call this when they collide with an object
    // returns true iff on other team and has collider component
    protected bool shouldCollisionOccur(Collision2D collision) {
        // do nothing if no collider detected
        if (collision.gameObject.GetComponent<Collider2D>() == null) {
            return false;
        }
        
        // if unit is on same team or has no collider, do nothing
        Entity otherEntity = collision.gameObject.GetComponent<Entity>();
        if (otherEntity == null || team == otherEntity.team) {
            return false;
        }

        return true;
    }

    // destroys the entity and deregisters it from the worldobject
    protected void DestroyEntity() {
        GameManager.getInstance().getCurrentLevel().destroyUnit(this);
        Destroy(gameObject);
    }
}
