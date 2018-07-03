using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyController : MonoBehaviour {

    public GameObject[] waypoints;
    public GameObject missiles;
		private GameObject player;
		public int startingHealth = 100;
		private int currentHealth;
    private int waypointIndex;
    private NavMeshAgent agent;

    public GameObject misslePrefab;
    public Transform missleSpawn;
    public float missleSpeed = 40f;
    private float nextTimeToFireMissle = 0f;
    public float missleFireRate = 1f;


    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
				currentHealth = startingHealth;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointIndex = Random.Range(0, waypoints.Length);
        player = GameObject.FindWithTag("Player");
        GotoNextPoint();
    }


    void GotoNextPoint() {
        waypointIndex = Random.Range(0, waypoints.Length);
        agent.destination = waypoints[waypointIndex].transform.position;
    }


    void Update () {
			if(currentHealth <= 0){
				carDeath();
			} else {
  			if(Vector3.Distance(transform.position, player.transform.position) > 40){
          if (!agent.pathPending && agent.remainingDistance < 1.0f)
              GotoNextPoint();
  			} else {
    				agent.destination = player.transform.position;
            if (Time.time >= nextTimeToFireMissle) {
                nextTimeToFireMissle = Time.time + 1f / missleFireRate;
                Launch();
            }
  			 }
      }
		}

		void carDeath() {
			Destroy(gameObject);
		}

    void OnCollisionEnter(Collision col) {
      if (col.gameObject.name == "Missile(Clone)") {
        currentHealth -= 50;
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
}
