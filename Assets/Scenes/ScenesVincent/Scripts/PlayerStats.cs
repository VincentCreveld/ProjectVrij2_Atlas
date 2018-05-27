using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable {

	[Header("Player Stats")]
	public int maxHealth;
	private int currentHealth;

	[Header("Player Healthbar")]
	public HealthbarController healthbar;

	public void Awake() {
		currentHealth = maxHealth;
		healthbar.Initialise(maxHealth);
	}

	public void ApplyDamage(int damage) {
		currentHealth -= damage;
		healthbar.TakeDamage(damage);
		if(currentHealth <= 0)
			Die();

		//Wouter BulletImpactBoss
		//FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/BulletImpactBoss");
	}

	public void Die() {
		healthbar.greenBar.localScale = new Vector3(0, 1, 1);
		//healthbar.StopAllCoroutines();
		gameObject.SetActive(false);
	}
}
