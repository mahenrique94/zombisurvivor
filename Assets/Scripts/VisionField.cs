using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionField : MonoBehaviour {

    private Zombie zombie;

    private void initializeAttributes() {
        this.zombie = this.transform.parent.gameObject.GetComponent<Zombie>();
    }

    private bool playerIsCloser(Collider collider) {
        return collider.gameObject.CompareTag("Player");
    }

	void Start () {
        this.initializeAttributes();
	}

	private void OnTriggerEnter(Collider other) {
        if (this.playerIsCloser(other)) {
            this.zombie.follow();
        }
	}

	private void OnTriggerExit(Collider other) {
        if (this.playerIsCloser(other)) {
            this.zombie.unFollow();
        }
	}

}
