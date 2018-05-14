using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTry : MonoBehaviour {

	public Transform ShootPos;

    //Player stats
    private static float health;//maybe static?
    private Rigidbody2D rBody;

    //Player actions
    [Header("Player actions")]
    public GameObject currentGun;
    [SerializeField]
    private float attackSpeed = 1f;
    private float timer = 0;
    private bool canShoot = true;

    //Everything camera
    private Camera cam;

    // Use this for initialization
    void Start () {
        //set variables
        cam = Camera.main;
        rBody = this.GetComponent<Rigidbody2D>();
        PickupGun();
	}

	// Update is called once per frame
	void Update () {
        //movement
       if (Input.GetButton("Fire1")) {
            //Shoot() or interact
            if (canShoot) {
                Shoot();
                canShoot = false;
                timer = 0;
            }
        }

        if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.U)) {
            if (canShoot) {
                Special();
                canShoot = false;
                timer = 0;
                }
            }

        if(timer < attackSpeed) {
            timer += Time.deltaTime;
        }else {
            canShoot = true;
        }
	}

    public void PickupGun() {
        //pick up a gun and set our variables
        if (!currentGun)
            return;
        currentGun.GetComponent<IGun>().PickupGun(this.transform);
        attackSpeed = currentGun.GetComponent<IGun>().ReturnAttackSpeed();
        }

    public void Shoot() {
        //Call gun
        currentGun.GetComponent<IGun>().Shoot();        
    }
    
    public void Special() {
        StartCoroutine(currentGun.GetComponent<IGun>().Special());
        }


    public Vector2 KnockBack(Vector2 dir,float force) {
        rBody.AddForce(dir * force);
        return transform.position;
    }
}
