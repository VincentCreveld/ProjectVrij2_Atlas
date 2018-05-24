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
	private int currentWeapon;
	public GameObject[] weapons;
    [SerializeField]
    private float attackSpeed = 1f;
    [SerializeField]
    private float attackSpeedSpec = 5f;
    private float timer = 0;
    private float timerSpec = 0;
    private bool canShoot = true;
    private bool canShootSpec = true;

    //Everything camera
    private Camera cam;

    // Use this for initialization
    void Start () {
		//set variables
		foreach(GameObject go in weapons) {
			go.SetActive(false);
		}
		currentGun.SetActive(true);
		currentWeapon = 0;
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
            if (canShootSpec) {
                Special();
                canShootSpec = false;
                timerSpec = 0;
            }
        }

		CheckWeaponSwitch();

		currentGun.transform.rotation = GunFunctions.CalcPlayerToMouseAngle(transform.position);

		//Timers
		if (timer < attackSpeed) {
            timer += Time.deltaTime;
			currentGun.GetComponent<IGun>().DisableCDNormalLight();
			//UI Inactive
		}
        else {
            canShoot = true;
			currentGun.GetComponent<IGun>().EnableCDNormalLight();
			//UI active
		}

        if (timerSpec < attackSpeedSpec) {
            timerSpec += Time.deltaTime;
			currentGun.GetComponent<IGun>().DisableCDSpecialLight();
            //UI Inactive
        }else {
            canShootSpec = true;
			currentGun.GetComponent<IGun>().EnableCDSpecialLight();
			//UI active
		}
	}

	public void CheckWeaponSwitch() {
		int desiredWeapon = currentWeapon;
		if(Input.GetButtonDown("SwapWeapon")) {
			if(desiredWeapon < weapons.Length - 1)
				desiredWeapon++;
			else
				desiredWeapon = 0;
		}
		if(desiredWeapon != currentWeapon) {
			currentGun.SetActive(false);
			currentGun = weapons[desiredWeapon];
			currentGun.SetActive(true);
			currentWeapon = desiredWeapon;
			PickupGun();
		}
	}

    public void PickupGun() {
        //pick up a gun and set our variables
        if (!currentGun)
            return;
        currentGun.GetComponent<IGun>().PickupGun(this.transform);
        attackSpeed = currentGun.GetComponent<IGun>().ReturnAttackSpeed();
        attackSpeedSpec = currentGun.GetComponent<IGun>().ReturnAttackSpeedSpec();
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
