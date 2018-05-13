using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DestroyAnimation : MonoBehaviour {

	private Animator anim;

	public void Awake() {
		anim = GetComponent<Animator>();
	}

	public void Activate() {
		anim.SetTrigger("Destroy");
	}
}
