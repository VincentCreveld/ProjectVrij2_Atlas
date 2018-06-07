using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealpackSpawner : MonoBehaviour {

	public GameObject healPackPrefab;
	public Transform spawnLocation;
	private GameObject spawnedHealpack;
	private bool coroutineRunning = false;
	public float spawnDelayAfterPickup;

	public void FixedUpdate() {
		if(!coroutineRunning && !IsHealpackSpawned())
			StartCoroutine(SpawnHealpack());
	}

	private IEnumerator SpawnHealpack() {
		Debug.Log("Starting routine");
		coroutineRunning = true;
		yield return new WaitForSeconds(spawnDelayAfterPickup);
		spawnedHealpack = Instantiate(healPackPrefab, spawnLocation.position, Quaternion.identity);
		spawnedHealpack.transform.parent = gameObject.transform;
		coroutineRunning = false;
	}

	private bool IsHealpackSpawned() {
		return spawnedHealpack == null ? false : true;
	}
}
