﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Rigidbody2D rBody;
    public float force = 10;
	// Use this for initialization
	void Start () {
        rBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rBody.velocity = new Vector2(1,(0 *0)) * force;
        Destroy(this.gameObject, 5f);
    }
}
