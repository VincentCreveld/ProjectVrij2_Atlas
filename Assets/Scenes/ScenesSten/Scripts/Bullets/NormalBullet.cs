using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour, IBullet {


    private float speed;
    private int dmg;
    private Vector2 dir;
    private Rigidbody2D rbody;

    [Header("Visual feedback")]
    public ParticleSystem particle;

    public void Start() {
        //Ignore bullet ignore layers
        Physics2D.IgnoreLayerCollision(9, 9, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        }

    public void SetupStats(float spd, int _dmg, Vector2 _dir) {
        rbody = this.GetComponent<Rigidbody2D>();
        this.speed = spd;
        this.dmg = _dmg;
        dir = _dir;
        rbody.AddForce(transform.right * spd);
        Destroy(this.gameObject, 5f);
        }

    public void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("hit something");
        if (col.transform.GetComponent<IDamagable>() != null) {
			col.transform.GetComponent<IDamagable>().ApplyDamage(dmg);
			}
        Destroy(this.gameObject);
        }

    public void OnDisable() {
        //Perhaps add something like explosives here
        return;
    }
}
