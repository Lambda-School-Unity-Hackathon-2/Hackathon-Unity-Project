using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;

	public Camera playerCam;
	public GameObject bulletOrigin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}
		
	}

	void Shoot () {
		RaycastHit hit;
		if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range)) {
			Debug.Log(hit.transform.name);
			Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green);
		}

	}
}
