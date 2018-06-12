using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamagable, IHealable {

	[Header("Player Stats")]
	public int maxHealth;
	private int currentHealth;
	public float invulnerabilityUptime = 0f;
	private bool isDamagable = true;
	public SpriteRenderer playerSprite;

	[Header("Player Healthbar")]
	public HealthbarController healthbar;

	public void Awake() {
		currentHealth = maxHealth;
		healthbar.Initialise(maxHealth);
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if(collision.GetComponent<ICanHeal>() != null)
			collision.GetComponent<ICanHeal>().HealTarget(this);
	}

	public void ApplyDamage(int damage) {
		if(isDamagable == false)
			return;
		currentHealth -= damage;
		healthbar.TakeDamage(damage);
		if(currentHealth <= 0)
			Die();
		StartCoroutine(Invulnerability());
		//Wouter BulletImpactBoss
		//FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/BulletImpactBoss");
	}

	public void HealHealth(int amt) {
		int temp = 0;
		if(currentHealth + amt > maxHealth)
			temp = maxHealth - currentHealth;
		else
			temp = amt;

		healthbar.HealHealth(temp);
	}

	public void Die() {
		healthbar.greenBar.localScale = new Vector3(0, 1, 1);
		//healthbar.StopAllCoroutines();
		gameObject.SetActive(false);
		SceneManager.LoadScene(2);
	}

	public IEnumerator Invulnerability() {
		Debug.Log("Don't take dmg");
		isDamagable = false;
		playerSprite.color = new Vector4(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f);
		Physics2D.IgnoreLayerCollision(11, 12, true);
		Physics2D.IgnoreLayerCollision(11, 13, true);
		float t = 0;
		while(true) {
			Debug.Log("reached");
			playerSprite.color = new Vector4(playerSprite.color.r, 0, 0, playerSprite.color.a);
			yield return new WaitForSeconds(0.125f);
			playerSprite.color = new Vector4(playerSprite.color.r, 1, 1, playerSprite.color.a);
			yield return new WaitForSeconds(0.125f);
			t += 0.25f;
			Debug.Log(t);
			if(t > invulnerabilityUptime) {
				break;
			}
		}
		playerSprite.color = new Vector4(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
		Physics2D.IgnoreLayerCollision(11, 12, false);
		Physics2D.IgnoreLayerCollision(11, 13, false);

		isDamagable = true;
	}

	public void InvokeBlinkSprite() {
		StartCoroutine(BlinkSprite());
	}

	public IEnumerator BlinkSprite() {
		Debug.Log("reached");
		playerSprite.color = new Vector4(playerSprite.color.r, 0, 0, playerSprite.color.a);
		yield return new WaitForSeconds(0.125f);
		playerSprite.color = new Vector4(playerSprite.color.r, 1, 1, playerSprite.color.a);
		yield return new WaitForSeconds(0.125f);
	}
}
