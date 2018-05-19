using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float attackInterval;
    [SerializeField]
    private float damage;
    private int life = 100;
    private bool mustFollow = false;
    private bool mustAttack = false;
    private GameObject player;

    private void died() {
        if (this.life <= 0) {
            this.animator.SetBool("died", true);
            Destroy(gameObject, 3f);
        }
    }

    public void follow() {
        this.toggleMustFollow();
    }

    private void initializeAttributes()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other) {
        this.toggleMustFollow();
        StartCoroutine(this.attack());
    }

    private void OnTriggerExit(Collider other) {
        this.toggleMustFollow();
        StopCoroutine(this.attack());
    }

	void Start () {
        this.initializeAttributes();
	}

    public void takeDamage(int damage) {
        this.life -= damage;
    }

    private void toggleMustFollow() {
        this.mustFollow = !this.mustFollow;
        this.mustAttack = this.mustFollow;
        this.agent.isStopped = this.mustFollow;
    }

    public void unFollow() {
        this.toggleMustFollow();
    }
	
	void Update () {
        if (mustFollow) {
            this.agent.destination = this.player.transform.position;
        }
        this.died();
	}

    IEnumerator attack() {
        Player player = this.player.GetComponent<Player>();
        while (this.mustAttack) {
            player.takeAttack(this.damage);
            yield return new WaitForSeconds(this.attackInterval);
        }
    }

}
