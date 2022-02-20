using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    // the amount of resource given by this object, if collected
    public int value;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}
    
    // if we get hit by a resource collector, then get collected
    void OnCollisionEnter2D(Collision2D collision) {
        // if hit by a resource gatherer, destroy self
        ResourceGatherer rg = collision.gameObject.GetComponent<ResourceGatherer>();
        if (rg != null) {
            Destroy(gameObject);
        }
    }
}
