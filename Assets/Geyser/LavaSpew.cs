using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpew : MonoBehaviour {

	public GameObject lavaParticle;
	public static ObjectPool<LavaParticle> objectPool;
	public int maxParticles;
	public Transform particleHost;
	public Transform spawnPos;
	public float minSpeed, maxSpeed, minSpread, maxSpread;
	public float spewTime = 3f;

	private void Awake() {
		if(objectPool == null)
			objectPool = new ObjectPool<LavaParticle>(maxParticles, particleHost, lavaParticle);
	}

	[ContextMenu("SpewLava")]
	public void SpewLava() {
		particleHost.position = spawnPos.position;
		StartCoroutine(SpewLavaOverTime());
	}

	[ContextMenu("ExplodeLava")]
	public void LavaExplosion() {
		particleHost.position = spawnPos.position;
		for(int i = 0; i < maxParticles/2; i++) {
			float randomSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
			objectPool.GetObj().InitialiseObj(GunFunctions.calcSpreadShot(transform.position, -minSpread, maxSpread), minSpread, maxSpread, randomSpeed, spawnPos);
		}
	}

	public IEnumerator SpewLavaOverTime() {
		float t = 0;
		while(true) {
			yield return null;
			float randomSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
			objectPool.GetObj().InitialiseObj(GunFunctions.calcSpreadShot(transform.position, -minSpread, maxSpread),minSpread, maxSpread, randomSpeed, spawnPos);
			t += Time.deltaTime;
			if(t > spewTime) {
				break;
			}
		}
	}
}