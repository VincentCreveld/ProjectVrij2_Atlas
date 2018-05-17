using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketLancherBullet : MonoBehaviour, IBullet {

	private float speed;
	private int dmg;
	private Vector2 dir;
	private Rigidbody2D rbody;

	[Header("Bounce/explosive specs")]
	public float minSpeed;
	public float maxSpeed;
	public float speedUpTime;
	public float fuseTime;
	public float gravityMod;
	public float explosionRadius;
	public int explosionDamage;
	public LayerMask rayMask;
	public GameObject explosion;

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
		transform.rotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, transform.rotation.z));
		StartCoroutine(FuseTimer());
		StartCoroutine(AccelrateRocket());
	}

	public IEnumerator AccelrateRocket() {
		float t = 0, v;
		while(true) {
			yield return null;
			v = Mathf.Lerp(minSpeed, maxSpeed, t/speedUpTime);
			t += Time.deltaTime;
			rbody.AddForce(transform.forward);
			if(t > speedUpTime) {
				break;
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D col) {
		Debug.Log("hit something");
		Explode();
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
		GO.transform.localScale *= explosionRadius * 2;
		Destroy(GO, 0.5f);
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
