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
	public bool disableOnDestroy;

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
		FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/BulletImpactBoss"); 
	}

	public void Die() {
		healthbar.greenBar.localScale = new Vector3(0, 1, 1);
		//healthbar.StopAllCoroutines();
		if(isDestuctable && disableOnDestroy)
			gameObject.SetActive(false);
		else if(isDestuctable)
			Destroy(gameObject);
	}

    public int returnHealth() {
        return currentHealth;
    }
}
