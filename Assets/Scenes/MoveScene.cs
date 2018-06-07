using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour {

	public int sceneToMoveTo = 0;
	public string inputKeyToReactTo = "";

	private void Update() {
		if(inputKeyToReactTo.Length < 1 || inputKeyToReactTo == "") {
			if(Input.anyKeyDown)
				SceneManager.LoadScene(sceneToMoveTo);
		}
		else {
			if(Input.GetKeyDown(inputKeyToReactTo))
				SceneManager.LoadScene(sceneToMoveTo);
		}
	}
}
