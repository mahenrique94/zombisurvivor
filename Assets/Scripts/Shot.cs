using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    private GameObject cam;

	void Start () {
        this.cam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void Update () {
        this.shot();
	}

    void shot() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitted;
            if (Physics.Raycast(this.cam.transform.position, this.cam.transform.forward, out hitted)) {
                if (hitted.transform.gameObject.GetComponent<Rigidbody>()) {
                    Rigidbody rb = hitted.transform.gameObject.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(250f, hitted.point, 10);
                }
            }
        }
    }

}
