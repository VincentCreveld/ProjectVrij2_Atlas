using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour {

	public bool moveWithKey = true;
	public int sceneToMoveTo = 0;
	public string inputKeyToReactTo = "";
	public GameObject fadeOutPlane;

	private void Update() {
		if(moveWithKey) {
			if(inputKeyToReactTo.Length < 1 || inputKeyToReactTo == "") {
				if(Input.anyKeyDown)
					ChangeScene();
			}
			else {
				if(Input.GetKeyDown(inputKeyToReactTo))
					ChangeScene();
			}
		}
	}

	public void ChangeScene() {
		StartCoroutine(EndGame(sceneToMoveTo));
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
