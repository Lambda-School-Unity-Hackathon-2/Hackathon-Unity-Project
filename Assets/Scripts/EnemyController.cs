using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyController : MonoBehaviour {

    public Transform[] points;
		public Transform player;
		public int startingHealth = 100;
		private int currentHealth;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
				currentHealth = startingHealth;
        GotoNextPoint();
    }


    void GotoNextPoint() {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }


    void Update () {
			if(currentHealth <= 0){
				carDeath();
			}
			if(Vector3.Distance(transform.position, player.position) > 40){
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
			} else {
				agent.destination = player.position;
			}
		}

		void carDeath() {
			gameObject.SetActive(false);
		}
}
