using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private AudioSource source;
    public AudioClip machineGun;
    public AudioClip grenadeLauncher;

    public Camera playerCam;
    public GameObject missilePrefab;
    public Transform missileSpawn;
    public float missileSpeed = 40f;
    private float nextTimeToFireMissile = 0f;
    public float missileFireRate = 1f;

    public GameObject grenadePrefab;
    public Transform grenadeSpawn;
    public float grenadeSpeed = 20f;
    private float nextTimeToFireGrenade = 0f;
    public float grenadeFireRate = 1f;
    public int grenadeAmmo = 5;
    bool reloading = false;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform shotOrigin;
    public float range = 100f;
    public float impactForce = 30f;
    private float nextTimeToFireBullet = 0f;
    public float gunFireRate = 5f;

    void Awake () {
		source = GetComponent<AudioSource>();
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextTimeToFireMissile) {
            nextTimeToFireMissile = Time.time + 1f / missileFireRate;
            Launch();
        }
        if (Input.GetKey(KeyCode.E) && Time.time >= nextTimeToFireGrenade) {
            nextTimeToFireGrenade = Time.time + 1f / grenadeFireRate;
            if (grenadeAmmo > 0) {
            GrenadeLaunch();
            grenadeAmmo--;
            }
            if (reloading == false) {
                reloading = true;
                Invoke("Reload", 2);  
            }
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireBullet) {
            nextTimeToFireBullet = Time.time + 1f / gunFireRate;
            Fire();
        }
    }

    void Reload() {
        grenadeAmmo = 5;
        reloading = false;
    }


    void Launch()
    {
        // Create the missile from the missile Prefab
        var missile = (GameObject)Instantiate(
            missilePrefab,
            missileSpawn.position,
            missileSpawn.rotation);

        // Add velocity to the missile
        missile.GetComponent<Rigidbody>().velocity = missile.transform.forward * missileSpeed;

        // Destroy the missile after 4 seconds
        Destroy(missile, 4.0f);
    }
    void GrenadeLaunch()
    {
        source.PlayOneShot(grenadeLauncher);
        // Create the missile from the missile Prefab
        var grenade = (GameObject)Instantiate(
            grenadePrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);

        // Add velocity to the grenade
        grenade.GetComponent<Rigidbody>().velocity = grenade.transform.forward * grenadeSpeed;

        // Destroy the grenade after 5 seconds
        Destroy(grenade, 2.1f);
    }

    void Fire() {
        source.PlayOneShot(machineGun);
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