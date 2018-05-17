using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSTest : MonoBehaviour {

	public LayerMask rayMask;
	public float explosionRadius;

	public Transform target;

	[ContextMenu("CheckLOS")]
	public void CheckLine() {
		CheckLOS(target);
	}

	public void CheckLOS(Transform t) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, (t.position - transform.position).normalized, explosionRadius, rayMask);
		if(hit.transform == t) {
			Debug.Log("Hit: " + hit.transform.name + " correctly.");
		}
		else
			Debug.Log("Hit: " + hit.transform.name + ". Not correct.");
	}
}
