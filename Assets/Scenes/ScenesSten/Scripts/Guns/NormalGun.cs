using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : IGun {

    public GameObject bulletPrefab;
    [Header("Gun Specs")]
    public int specialBulletAmount = 5;
    public int minSpread = 20;
    public int maxSpread = 20;
    public float attackSpead = 0.75f;
    public int shotForce = 50;

    [Header("Bullet Specs")]
    public int bulletDmg = 5;
    public int bulletSpeed = 500;

    [Header("Feedback")]
    public float shotCameraShakeForce = 0.1f;
    public float shotCameraDuration = 0.05f;


    //store the player
    private Transform player;
    private Camera cam;

    public override void PickupGun(Transform _player) {
        player = _player;
        this.transform.parent = player.transform;
        cam = Camera.main;
        }

    public override float ReturnForce() {
        return shotForce;
        }
    public override float ReturnAttackSpeed() {
        return attackSpead;
        }
    public override void DropGun() {
        this.transform.parent = null;
        }

    public override void Reload() {
        throw new NotImplementedException();
        }

    //Normal attack
    public override void Shoot() {
            if (!bulletPrefab)
                return;

                Quaternion rot = GunFunctions.CalcPlayerToMouseAngle(this.transform.position);
                GameObject go = Instantiate(bulletPrefab, transform.position, rot);
                go.GetComponent<IBullet>().SetupStats(bulletSpeed, bulletDmg, new Vector2(1, 0));
                //Feedback
                GunFunctions.KnockBack(player.GetComponent<Rigidbody2D>(), new Vector2(-1, 0), shotForce);
                StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shotCameraShakeForce, shotCameraDuration));

		//Wouter PlasmaGunShot
		FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/PlasmaGunShot"); 
        }

    //Special attack
    public override IEnumerator Special() {
        for (int i = 0; i < specialBulletAmount; i++) {
            Quaternion rot = GunFunctions.calcSpreadShot(this.transform.position, -minSpread, maxSpread);
            GameObject go = Instantiate(bulletPrefab, transform.position, rot);
            go.GetComponent<IBullet>().SetupStats(bulletSpeed, bulletDmg, new Vector2(1, 0));
            //Feedback
            GunFunctions.KnockBack(player.GetComponent<Rigidbody2D>(), transform.forward *-1, shotForce);
            StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shotCameraShakeForce, shotCameraDuration + 0.2f));

			//Wouter HeavyGunShot
			FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/HeavyGunShot");
            }
        yield return null;
        }

    //Used to load bullets
    public override void LoadBullets(GameObject bullet, int bulletDmg) {
        bulletPrefab = bullet;

        }
    }
