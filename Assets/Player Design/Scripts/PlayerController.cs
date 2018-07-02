using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject misslePrefab;
    public Transform missleSpawn;
    public float missleSpeed = 40f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }


    void Fire()
    {
        // Create the missle from the missle Prefab
        var missle = (GameObject)Instantiate(
            misslePrefab,
            missleSpawn.position,
            missleSpawn.rotation);

        // Add velocity to the missle
        missle.GetComponent<Rigidbody>().velocity = missle.transform.forward * missleSpeed;

        // Destroy the missle after 2 seconds
        Destroy(missle, 2.0f);
    }
}