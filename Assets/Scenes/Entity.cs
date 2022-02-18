using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for all living objects, has rank
public class Entity : MonoBehaviour
{
    // constants for the Entity class
    public const bool LEFT_TEAM = true;
    public const bool RIGHT_TEAM = false;

    // the power level of this entity
    public int rank = 0;

    // movement speed in units per second.
    public float speed = 1.0F;

    // ID field for each unit, guaranteed to be unique
    private static uint idMaster = 0; // TODO might need a way to reset this
    private uint id = 0;

    // denotes which team the unit is on
    public bool team = true;
    
    // Start is called before the first frame update
    void Start() {
        id = idMaster++;
        
        // team should already be registered at this point
        // TODO confirm this
        WorldManager.getInstance().registerUnit(this);
    }

    // Update is called once per frame
    void Update() {}

    public uint getId() { return id; }
}
