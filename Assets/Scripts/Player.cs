using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Slider healthBar;
    private float life = 1;

    private void initializeAttributes() {
        this.healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        this.healthBar.value = this.life;
    }

	void Start () {
        this.initializeAttributes();
	}

    public void takeAttack(float attack) {
        this.life -= attack;
    }
	
	void Update () {
        this.updateLife();
	}

    private void updateLife() {
        this.healthBar.value = this.life;
    }

}
