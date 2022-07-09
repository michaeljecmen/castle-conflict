using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for all living objects
// every living object has a spriterenderer
public class Entity : MonoBehaviour {
    // constants for the Entity class
    public const bool LEFT_TEAM = true;
    public const bool RIGHT_TEAM = false;

    // denotes which team the unit is on
    public bool team = LEFT_TEAM;
    public Sprite leftSprite;
    public Sprite rightSprite;

    // sets the internal bool and updates the actual sprite to be displayed
    public void setTeam(bool team_) {
        team = team_;
        GetComponent<SpriteRenderer>().sprite = team == LEFT_TEAM ? leftSprite : rightSprite;
    }
    public bool getTeam() {
        return team;
    }

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

    // destroys the entity and do any cleanup work
    protected void DestroyEntity() {
        Destroy(gameObject);
    }
}
