using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutElement : MonoBehaviour {

	private GameObject fadeOutPlane;

	private void Awake() {
		Geyser.objectPool = null;
	}

	private void Start() {	
	fadeOutPlane = gameObject;
		StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn() {
		float t = 0;
		float dur = 2f;
		fadeOutPlane.SetActive(true);
		while(true) {
			yield return null;
			float tempfloat = Mathf.Lerp(1f, 0, t / dur);
			fadeOutPlane.GetComponent<Image>().color = new Vector4(0f, 0f, 0f, tempfloat);
			t += Time.deltaTime;
			if(t > dur) {
				break;
			}
		}
		gameObject.SetActive(false);
	}
}
