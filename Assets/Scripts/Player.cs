using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject minion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector2.left);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector2.right);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector2.up);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector2.down);

        // if e pressed, spawn prefab
        if (Input.GetKeyDown(KeyCode.E)) {
            SpawnMinion();
        }
    }

    private void SpawnMinion()
    {
        Instantiate(minion, minion.transform.position, Quaternion.identity);
    }
}
