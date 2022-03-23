using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public void setGravity(bool on) { 
        Rigidbody2D rb = gameObject.transform.GetComponent<Rigidbody2D>();
        if (rb != null && on) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } else {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
