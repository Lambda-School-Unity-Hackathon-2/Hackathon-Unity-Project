using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject enemiesPrefab;
	public GameObject[] respawns;
	public GameObject[] spawnPoints;

	[SerializeField]
	private GameObject enemies;
	public static int enemyCount = 0;
	public int maxEnemies = 10;

	// Use this for initialization
	void Start () {
		if (respawns == null)
				respawns = GameObject.FindGameObjectsWithTag("Respawn");

		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

		foreach (GameObject spawnPoint in spawnPoints)
		{
				Instantiate(enemiesPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
		}
		StartCoroutine(Spawn());

	}

	// void LateUpdate() {
	// 	StartCoroutine(Spawn());
	// }

	private IEnumerator Spawn() {
		respawns = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log("there are " + respawns.Length + "enemies");
		if(respawns.Length < maxEnemies) {
				Debug.Log(respawns.Length);
				yield return new WaitForSeconds(5);
				int randomNumber = Random.Range(0, spawnPoints.Length);
				Instantiate(enemies, spawnPoints[randomNumber].transform.position, spawnPoints[randomNumber].transform.rotation);
		}
		StartCoroutine(Spawn());

	}
}
