using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerande : MonoBehaviour {

    public GameObject GerandePrefab;
    public Transform GerandeSpawn;
    public float GerandeSpeed = 40f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grenade();
        }
    }


    void Grenade()
    {
        // Create the missle from the missle Prefab
        var Gerande = (GameObject)Instantiate(
            GerandePrefab,
            GerandeSpawn.position,
            GerandeSpawn.rotation);

        // Add velocity to the missle
        Gerande.GetComponent<Rigidbody>().velocity = Gerande.transform.forward * GerandeSpeed;

        // Destroy the missle after 2 seconds
        Destroy(Gerande, 2.0f);
    }
}
