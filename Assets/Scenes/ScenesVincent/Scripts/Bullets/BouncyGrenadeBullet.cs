using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BouncyGrenadeBullet : MonoBehaviour, IBullet {

	private Camera cam;
	private float speed;
	private int dmg;
	private Vector2 dir;
	private Rigidbody2D rbody;

	[Header("Bounce/explosive specs")]
	public int maxBounces;
	public float fuseTime;
	private int currentBounces = 0;
	public float gravityMod;
	public float explosionRadius;
	public int explosionDamage;
	public LayerMask rayMask;
	public GameObject explosion;

	[Header("Visual feedback")]
	public ParticleSystem particle;

	[Header("Feedback")]
	public float shotCameraShakeForce = 0.1f;
	public float shotCameraDuration = 0.05f;

	public void Start() {
		//Ignore bullet ignore layers
		cam = Camera.main;
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
		
		StartCoroutine(FuseTimer());
	}

	public void OnCollisionEnter2D(Collision2D col) {
		Debug.Log("hit something");
		//Debug.DrawLine(transform.localPosition, transform.localPosition + (Vector3.right * explosionRadius), Color.cyan, 5f);
		if(col.transform.GetComponent<IDamagable>() != null) {
			Explode();
		}else if(currentBounces < maxBounces) {
			currentBounces++;
		}else if(currentBounces >= maxBounces) {
			Explode();
		}
	}

	public IEnumerator FuseTimer() {
		yield return new WaitForSeconds(fuseTime);
		Explode();
	}

	[ContextMenu("Explode")]
	public void Explode() {
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

		foreach(Collider2D col in hitColliders) {
			if(col.transform.GetComponent<IDamagable>() != null) {
				if(CheckLOS(col.transform)) {
					col.transform.GetComponent<IDamagable>().ApplyDamage(explosionDamage);
				}
			}
		}
		GameObject GO = Instantiate(explosion, transform.position, Quaternion.identity);
		GO.transform.localScale = new Vector3(explosionRadius / transform.localScale.x, explosionRadius / transform.localScale.y, explosionRadius );
		//Debug.DrawLine(transform.position,transform.position + (Vector3.right * explosionRadius), Color.cyan, 5f);
		Destroy(GO, 0.5f);
		StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shotCameraShakeForce, shotCameraDuration));
		Destroy(gameObject);
	}

	public bool CheckLOS(Transform t) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, (t.position - transform.position).normalized, explosionRadius, rayMask);
		if(hit.transform == t) {
			return true;
		}
		else
			return false;
	}

	public void OnDisable() {
		//Perhaps add something like explosives here
		return;
	}

	public void OnDestroy() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/HeavyGunShot");
		StopAllCoroutines();
	}
}

