using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject player;
    private bool mustFollow = true;

    private void initializeAttributes()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other) {
        this.toggleMustFollow();
    }

    private void OnTriggerExit(Collider other) {
        this.toggleMustFollow();
    }

	void Start () {
        this.initializeAttributes();
	}

    private void toggleMustFollow() {
        this.mustFollow = !this.mustFollow;
        this.agent.isStopped = !this.mustFollow;
    }
	
	void Update () {
        if (mustFollow) {
            this.agent.destination = this.player.transform.position;
        }
	}

}
