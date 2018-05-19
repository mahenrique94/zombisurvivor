using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {

    private const string ANIMATION_REALOAD = "reload";
    private const string ANIMATION_SHOT = "fire";

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int bullets;
    [SerializeField]
    private int bulletsByComb;
    private Text bulletsUI;
    private AudioSource bulletsSoundTrack;
    private GameObject cam;
    private bool isReloading = false;
    [SerializeField]
    private int munition;
    private Text munitionUI;

    private bool animationIsRunning(string name) {
        return this.animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    private void animationRunOnce(string name) {
        this.animator.CrossFadeInFixedTime(name, 0.1f);
    }

	private void FixedUpdate() {
        this.isReloading = this.animationIsRunning(ANIMATION_REALOAD);
	}

	private bool hasBulletsToReload(int bulletsToReload) {
        return bulletsToReload <= this.munition;
    }

    private bool hasBulletsToShot() {
        return this.bullets > 0;
    }

    private bool hittedAZombie(RaycastHit hitted) {
        return hitted.transform.CompareTag("Zombie");
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
                this.realodingAnimation();
            }
        }
    }

    private void realodingAnimation() {
        if (this.animationIsRunning(ANIMATION_REALOAD)) {
            return;
        }
        this.animationRunOnce(ANIMATION_REALOAD);
    }

    private void shot() {
        if (this.shotted()) {
            if (this.hasBulletsToShot() && !this.isReloading) {
                RaycastHit hitted;
                this.bulletsSoundTrack.Play();
                this.animationRunOnce(ANIMATION_SHOT);
				if (Physics.Raycast(this.cam.transform.position, this.cam.transform.forward, out hitted)) {
                    Debug.Log(hitted.transform.name);
                    if (this.hittedAZombie(hitted)) {
                        hitted.transform.gameObject.GetComponent<Zombie>().takeDamage(25);
                    }
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
