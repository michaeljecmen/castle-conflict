using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    public int health;
    public int resourceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // store resource in tower
    public void depositResource(int amt) {
        resourceCount += amt;
    }

    // take the specified amount of damage
    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            // destroy the parent container which houses us
            Destroy(gameObject.transform.parent.gameObject);
        }
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
