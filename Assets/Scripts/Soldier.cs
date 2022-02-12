using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Entity
{
    // Transforms to act as start and end markers for the journey.
    public Vector3 startVector;
    public Vector3 endVector;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startVector, endVector);

        // ensure the soldier is never seen in the middle
        transform.position = startVector;
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startVector, endVector, fractionOfJourney);
    }

    // If colliding with other color, resolve
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

        // do nothing if no collider detected
        if (collision.gameObject.GetComponent<Collider2D>() == null) {
            return;
        }

        Transform otherTransform = collision.gameObject.GetComponent<Collider2D>().transform;
        if (transform.CompareTag("Blue") && otherTransform.CompareTag("Red") || transform.CompareTag("Red") && otherTransform.CompareTag("Blue")) {
            // resolve collision
            Entity otherEntity = collision.gameObject.GetComponent<Entity>();

            // here check if we got destroyed and if so, die
            // do not destroy the other guy, that's his job
            if (rank <= otherEntity.rank) {
                Debug.Log("WE DIE");
                Destroy(gameObject);
            }
        }
    }
}
