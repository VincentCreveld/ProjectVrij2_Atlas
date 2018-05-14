using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamagable {

	[Header("HealthBar specs")]
	public int maxHealth;
	private int currentHealth;
	public HealthbarController healthbar;

	[Header("OnDestruction specs")]
	public DestroyAnimation DestAnim;
	public bool isDestuctable;

	public void Awake() {
		currentHealth = maxHealth;
		healthbar.Initialise(maxHealth);
	}

	public void ApplyDamage(int damage) {
		currentHealth -= damage;
		healthbar.TakeDamage(damage);
		if(currentHealth <= 0)
			Die();
	}

	public void Die() {
		healthbar.StopAllCoroutines();
		if(isDestuctable)
			gameObject.SetActive(false);
	}
}
