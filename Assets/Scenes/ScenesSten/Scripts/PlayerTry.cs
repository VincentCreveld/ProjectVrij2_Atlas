using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTry : MonoBehaviour {

	public Transform ShootPos;

    //Player stats
    [SerializeField]
    private float moveSpeed = 5;
    private float jumpForce = 10;
    private static float health;//maybe static?
    private Rigidbody2D rBody;

    //Player actions
    public GameObject bulletPrefab;
    [SerializeField]
    private float attackSpeed = 1f;
    [SerializeField]
    private float timer = 0;
    private bool canShoot = true;
    [SerializeField]
    private float shotForce;
    [SerializeField]
    private float shotCameraShakeForce;
    [SerializeField]
    private float shotCameraDuration;

    //Feedback
    [SerializeField]
    private ParticleSystem shotParticle;
    [SerializeField]
    private ParticleSystem movementTrails;

    //Everything camera
    private Camera cam;

    // Use this for initialization
    void Start () {
        //set variables
        cam = Camera.main;
        rBody = this.GetComponent<Rigidbody2D>();
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

        if(timer < attackSpeed) {
            timer += Time.deltaTime;
        }else {
            canShoot = true;
        }
	}

    public virtual void Shoot() {
        //Bullet spawn
        GameObject go = Instantiate(bulletPrefab, ShootPos.position, Quaternion.identity);
		go.GetComponent<BulletScript>().moveDir = Mathf.Sign(rBody.velocity.x);
        //Player knockback
        KnockBack(new Vector2(-1, 0), shotForce);
        //Camerashake
        StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shotCameraShakeForce, shotCameraDuration));
    }

    public Vector2 KnockBack(Vector2 dir,float force) {
        rBody.AddForce(dir * force);
        return transform.position;
    }
}
