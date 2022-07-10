using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bombers are unique in that they destroy themselves upon contact with another unit
public class Bomber : AttackUnit {
    public override void onCollidedWithMinion(Minion other) {
        DestroyEntity();
    }
}
