using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

	public GameObject fadeOutPlane;

	public void Update() {
		if(Input.GetKeyDown("r")) {
			StartCoroutine(EndGame(1));
		}
		if(Input.GetKeyDown("t")) {
			StartCoroutine(EndGame(3));
		}
	}


	private IEnumerator EndGame(int num) {
		float t = 0;
		float dur = 2f;
		fadeOutPlane.SetActive(true);
		while(true) {
			yield return null;
			float tempfloat = Mathf.Lerp(0, 1f, t / dur);
			fadeOutPlane.GetComponent<Image>().color = new Vector4(0f, 0f, 0f, tempfloat);
			t += Time.deltaTime;
			if(t > dur) {
				break;
			}
		}
		SceneManager.LoadScene(num);
	}
}
