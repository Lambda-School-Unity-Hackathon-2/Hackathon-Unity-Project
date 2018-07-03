using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyController : MonoBehaviour {

    public GameObject[] waypoints;
		public Transform player;
		public int startingHealth = 100;
		private int currentHealth;
    private int waypointIndex;
    private NavMeshAgent agent;


    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
				currentHealth = startingHealth;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointIndex = Random.Range(0, waypoints.Length);
        GotoNextPoint();
    }


    void GotoNextPoint() {
        waypointIndex = Random.Range(0, waypoints.Length);
        agent.destination = waypoints[waypointIndex].transform.position;
    }


    void Update () {
			if(currentHealth <= 0){
				carDeath();
			}
			if(Vector3.Distance(transform.position, player.position) > 40){
        if (!agent.pathPending && agent.remainingDistance < 1.0f)
            GotoNextPoint();
			} else {
				agent.destination = player.position;
			}
		}

		void carDeath() {
			gameObject.SetActive(false);
		}
}
