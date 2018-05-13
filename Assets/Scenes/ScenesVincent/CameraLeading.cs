using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLeading : MonoBehaviour {

	public Transform target;
	[SerializeField]
	private float controllerDeadzone = 0.19f;

	public float LeadOffset;
	public float moveSpeed;

	private float curXdistance = 0;
	private float prevXDistance = 0;
	private float curYdistance = 0;
	private float prevYDistance = 0;


	public void FixedUpdate() {
		curXdistance = target.position.x - transform.position.x;
		curYdistance = target.position.y - transform.position.y;

		if(Input.GetAxis("Horizontal") > controllerDeadzone)
			LeadRight();
		else if(Input.GetAxis("Horizontal") < -controllerDeadzone)
			LeadLeft();
		else
			Recentre();
		RecentreY();
		prevXDistance = curXdistance;
	}

	public void RecentreY() {
		transform.position = new Vector3(transform.position.x,Mathf.Lerp(transform.position.y, target.transform.position.y, Time.deltaTime * moveSpeed * 1.5f), transform.position.z);
	}
	public void LeadLeft() {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x - LeadOffset, Time.deltaTime * moveSpeed), transform.position.y, transform.position.z);
	}
	public void LeadRight() {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x + LeadOffset, Time.deltaTime * moveSpeed), transform.position.y, transform.position.z);
	}
	public void Recentre() {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x, Time.deltaTime * moveSpeed ), transform.position.y, transform.position.z);
	}
}
