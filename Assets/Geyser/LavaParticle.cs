using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
//[RequireComponent(typeof(CircleCollider2D))]
public class LavaParticle : MonoBehaviour, IPoolable {

	public Sprite[] possibleSprites;
	private SpriteRenderer spriteRenderer;
	public bool particleMovesUpwards;
	public bool particleShrinksAtPeak;
	private bool routineStarted = false;

	public PoolEvent PoolEvent {
		get { return recycleEvent; }
		set { recycleEvent = value; }
	}
	protected Rigidbody2D rb2D;
	protected Vector3 moveDir;
	private PoolEvent recycleEvent;

	public bool dealsDamage = false;
	public int damageAmount;
	public float shrinkTime;

	private void Awake() {
		rb2D = gameObject.GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameObject.SetActive(false);
	}

	private void Start() {
		transform.position = transform.parent.position;
	}

	public void InitialiseObj(Quaternion rot, float minSpread, float maxSpread, float force, Transform spawnLoc) {
		ChangeSprite();
		transform.position = spawnLoc.position;
		transform.rotation = rot;
		if(particleMovesUpwards)
			rb2D.AddForce(Vector3.up * force + new Vector3(UnityEngine.Random.Range(minSpread, maxSpread), 0, 0));
		else 
			rb2D.AddForce(new Vector3(UnityEngine.Random.Range(minSpread, maxSpread), 0, 0) * force);
		StartCoroutine(despawnAfterTime());
	}

	public IEnumerator despawnAfterTime() {
		yield return new WaitForSeconds(4f);
		Recycle();
	}

	private void FixedUpdate() {
		if(particleShrinksAtPeak && !routineStarted && rb2D.velocity.y < 0)
			StartCoroutine(ShrinkParticle());
	}

	private IEnumerator ShrinkParticle() {
		float t = 0;
		float originalScaleX = transform.localScale.x;
		float originalScaleY = transform.localScale.y;
		routineStarted = true;
		while(true) {
			yield return null;
			float numX = Mathf.Lerp(originalScaleX, 0f, t / shrinkTime);
			float numY = Mathf.Lerp(originalScaleY, 0f, t / shrinkTime);
			transform.localScale = new Vector3(numX, numY, 1);
			t += Time.deltaTime;

			if(t > shrinkTime || transform.localScale.x <= 0) {
				break;
			}
		}
		transform.localScale = new Vector3(originalScaleX, originalScaleY, 1);
		Recycle();
		routineStarted = false;
	}

	public void ChangeSprite() {
		int num = UnityEngine.Random.Range(0, possibleSprites.Length);
		spriteRenderer.sprite = possibleSprites[num];
		spriteRenderer.sprite.texture.Apply();

		//Vector2 s = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
		//gameObject.GetComponent<CircleCollider2D>().radius = s.x;
		//gameObject.GetComponent<CircleCollider2D>().offset = new Vector2((s.x / 2), 0);

		//Makes sprite half transparent if the particle deals no damage.
		if(dealsDamage)
			spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
		else
			spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

	}

	public void Recycle() {
		if(recycleEvent != null)
			recycleEvent(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D col) {
		if(col.transform.tag == "LavaParticle")
			return;
		if(dealsDamage)
			if(col.transform.GetComponent<IDamagable>() != null)
				col.transform.GetComponent<IDamagable>().ApplyDamage(damageAmount);
		transform.position = transform.parent.position;
		Recycle();
	}

}