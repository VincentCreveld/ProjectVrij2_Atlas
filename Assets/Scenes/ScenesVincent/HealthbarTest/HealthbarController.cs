using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour {

	public RectTransform greenBar, yellowBar;
	private int maxHealth;
	private float currentHealth;
	public float lerpDuration;

	private float greenBarX;

	public void Initialise(int maxHealth) {
		this.maxHealth = maxHealth;
		currentHealth = maxHealth;
	}
	
	public void TakeDamage(int amt) {
		currentHealth -= amt;
		greenBar.localScale = new Vector3(Mathf.Clamp(ExtensionFunctions.Map((float)currentHealth, 0f, (float)maxHealth, 0f, 1f), 0f, 1f), greenBar.localScale.y, greenBar.localScale.z);
		greenBarX = greenBar.localScale.x;
		StopCoroutine(LerpYellowBar());
		if(gameObject.activeSelf)
			StartCoroutine(LerpYellowBar());

        //Wouter PlayerTakesDmg
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerTakesDmg");
    }

	public void HealDamage(int amt) {
		currentHealth += amt;
		greenBar.localScale = new Vector3(Mathf.Clamp(ExtensionFunctions.Map((float)currentHealth, 0f, (float)maxHealth, 0f, 1f), 0f, 1f), greenBar.localScale.y, greenBar.localScale.z);
		greenBarX = greenBar.localScale.x;
		StopCoroutine(LerpYellowBar());
		if(gameObject.activeSelf)
			StartCoroutine(LerpYellowBar());
	}

	public IEnumerator LerpYellowBar() {
		float t = 0;
		yield return new WaitForSeconds(0.25f);
		while(true) {
			yield return null;
			float tempfloat = Mathf.Lerp(yellowBar.localScale.x, greenBarX, t / lerpDuration);
			yellowBar.localScale = new Vector3(Mathf.Clamp(tempfloat, 0f, 1f), yellowBar.localScale.y, yellowBar.localScale.z);
			t += Time.deltaTime;
			if(t > lerpDuration) {
				break;
			}
		}
	}

	//MOET VERANDERD
	public IEnumerator LerpGreenBar() {
		float t = 0;
		yield return new WaitForSeconds(0.25f);
		while(true) {
			yield return null;
			float tempfloat = Mathf.Lerp(yellowBar.localScale.x, greenBarX, t / lerpDuration);
			yellowBar.localScale = new Vector3(Mathf.Clamp(tempfloat, 0f, 1f), yellowBar.localScale.y, yellowBar.localScale.z);
			t += Time.deltaTime;
			if(t > lerpDuration) {
				break;
			}
		}
	}

	private void OnDisable() {
		StopAllCoroutines();
	}
}
