using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {

    [SerializeField]
    private int bullets;
    [SerializeField]
    private int bulletsByComb;
    private Text bulletsUI;
    private AudioSource bulletsSoundTrack;
    private GameObject cam;
    [SerializeField]
    private int munition;
    private Text munitionUI;

    private bool hasBulletsToReload(int bulletsToReload) {
        return bulletsToReload <= this.munition;
    }

    private bool hasBulletsToShot() {
        return this.bullets > 0;
    }

    private void initializeAttributes() {
        this.bulletsUI = GameObject.FindGameObjectWithTag("BulletsUI").GetComponent<Text>();
        this.bulletsSoundTrack = this.GetComponent<AudioSource>();
        this.munitionUI = GameObject.FindGameObjectWithTag("MunitionUI").GetComponent<Text>();
        this.cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private bool itCanReload() {
        return this.bullets < this.bulletsByComb;
    }

    private void reload() {
        if (this.reloaded()) {
            this.reloading();
        }
    }

    private bool reloaded() {
        return Input.GetKeyDown(KeyCode.R);
    }

    private void reloading() {
        if (this.itCanReload()) {
            int bulletsToReload = this.bulletsByComb - this.bullets;
            if (this.hasBulletsToReload(bulletsToReload)) {
                this.bullets += bulletsToReload;
                this.munition -= bulletsToReload;
            }
        }
    }

    private void shot() {
        if (this.shotted()) {
            if (this.hasBulletsToShot()) {
                RaycastHit hitted;
                this.bulletsSoundTrack.Play();
				if (Physics.Raycast(this.cam.transform.position, this.cam.transform.forward, out hitted)) {
					if (hitted.transform.gameObject.GetComponent<Rigidbody>()) {
						Rigidbody rb = hitted.transform.gameObject.GetComponent<Rigidbody>();
						rb.AddExplosionForce(250f, hitted.point, 10);
					}
				}
                this.bullets--;
            } else {
                this.reloading();
            }
        }
        this.updateUI();
    }

    private bool shotted() {
        return Input.GetMouseButtonDown(0);
    }

	void Start () {
        this.initializeAttributes();
        this.updateUI();
	}
	
	void Update () {
        this.reload();
        this.shot();
	}

    private void updateUI() {
        this.bulletsUI.text = this.bullets.ToString();
        this.munitionUI.text = this.munition.ToString();
    }

}
