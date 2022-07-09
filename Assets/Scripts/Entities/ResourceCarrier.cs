using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// intermediate class for any minion that carries resources 
// and needs to turn around to bring them back to their tower
public class ResourceCarrier : Minion {
    private int carriedValue = 0;

    public bool isCarrying() {
        return carriedValue != 0;
    }
    
}
