using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    public void OnCollisionEnter2D (Collision2D col) {
		if(col.gameObject.layer == 12 && col.gameObject.tag != "player") {
            this.GetComponent<IDamagable>().ApplyDamage(30);
            Debug.Log("applied dmg");
            }

        Debug.Log("Hit me daddy");
        }
}
