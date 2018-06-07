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
		Physics2D.IgnoreLayerCollision(11, 12, true);
		yield return new WaitForSeconds(invulnerabilityUptime);
		Physics2D.IgnoreLayerCollision(11, 12, false);
		isDamagable = true;
	}
}
