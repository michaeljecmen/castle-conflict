using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    // the amount of resource given by this object, if collected
    public int value;
    public Vector3 startScale;
    public Vector3 endScale;
    public float timeToGrow;
    private float startTime;

    void Start() {
        startTime = Time.time;
        transform.localScale = startScale;
    }

    void Update() {
        float elapsed = Time.time - startTime;
        if (elapsed <= timeToGrow) {
            float fractionOfGrowth = elapsed / timeToGrow;
            transform.localScale = Vector3.Lerp(startScale, endScale, fractionOfGrowth);
        }
    }
    
    // if we get hit by a resource collector, then get collected
    public void onCollected() {
        Destroy(gameObject);
    }
}
