﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera playerCam;
    public GameObject misslePrefab;
    public Transform missleSpawn;
    public float missleSpeed = 40f;
    private float nextTimeToFireMissle = 0f;
    public float missleFireRate = 1f;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform shotOrigin;
    public float range = 100f;
    public float impactForce = 30f;
    private float nextTimeToFireBullet = 0f;
    public float gunFireRate = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextTimeToFireMissle) {
            nextTimeToFireMissle = Time.time + 1f / missleFireRate;
            Launch();
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireBullet) {
            nextTimeToFireBullet = Time.time + 1f / gunFireRate;
            Fire();
        }
    }


    void Launch()
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

    void Fire() {
        // play particle effect
        muzzleFlash.Stop();
        muzzleFlash.Play();
        // create variable to store raycast info
        RaycastHit hit;
        // if a raycast hits something
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range )) {
            // log what it hit
            Debug.Log(hit.transform.name);
            //if it has a rigidbody component attached
            if (hit.rigidbody != null) {
                // add some force to the rigidbody on its normal in the direction of the shot
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            // instantiate a particle effect
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // destroy the particle effect
            Destroy(impactGO, 1f);
        }
    }
}