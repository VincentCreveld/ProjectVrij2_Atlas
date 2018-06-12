using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour {

    public Transform PlayerPos;

    [Header("Specs")]
    [SerializeField]
    private float geyserActiveTime = 5;
    [SerializeField]
    private float geyserAnticipationTime;
    public float dmg;
    public float knockback;

    [Header("Positions")]
    [SerializeField]
    private List<Transform> geyserPositions = new List<Transform>();
    private Transform closestTransform;

	[Header("Timer")]
    [SerializeField]
    private float interval = 10;
    private float currentTime;

	private Camera cam;

	[Header("Feedback")]
	public float shotCameraShakeForce = 0.1f;
	public float shotCameraDuration = 0.05f;

	#region GeyserCode
	public GameObject lavaParticle;
	public static ObjectPool<LavaParticle> objectPool;
	public int maxParticles;
	public Transform particleHost;
	public float minSpeed, maxSpeed, minSpread, maxSpread;

	[ContextMenu("SpewLava")]
	public void SpewLava(Transform spawnPos) {
		particleHost.position = spawnPos.position;
		StartCoroutine(SpewLavaOverTime(spawnPos));
	}

	[ContextMenu("ExplodeLava")]
	public void LavaExplosion(Transform spawnPos) {
		particleHost.position = spawnPos.position;
		for(int i = 0; i < maxParticles / 2; i++) {
			float randomSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
			objectPool.GetObj().InitialiseObj(GunFunctions.calcSpreadShot(transform.position, -minSpread, maxSpread), minSpread, maxSpread, randomSpeed, spawnPos);
		}
	}

	public IEnumerator SpewLavaOverTime(Transform spawnPos) {
		float t = 0;
		particleHost.position = spawnPos.position;
		while(true) {
			yield return null;
			float randomSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
			objectPool.GetObj().InitialiseObj(GunFunctions.calcSpreadShot(transform.position, -minSpread, maxSpread), minSpread, maxSpread, randomSpeed, spawnPos);
			t += Time.deltaTime;
			if(t > geyserActiveTime) {
				break;
			}
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		if(objectPool == null)
			objectPool = new ObjectPool<LavaParticle>(maxParticles, particleHost, lavaParticle);
		cam = Camera.main;
		ActivateGeyser();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime < interval) {
            currentTime += Time.deltaTime;
        }else {
            ActivateGeyser();
            currentTime = 0;
        }
	}

    [ContextMenu("Spawn Geyser")]
    public void ActivateGeyser() {
        StartCoroutine(GeyserExplode());
        }

    [ContextMenu("Calc closest")]
    private Transform closestToPlayer() {

        float currentMinDist =  1000000000;//holds a value to start of, but must always be big
        float currentEquation;

        for (int i = 0; i < geyserPositions.Count; i++) {
            if((currentEquation = Vector2.Distance(PlayerPos.position, geyserPositions[i].position)) < currentMinDist) {
                closestTransform = geyserPositions[i];
                currentMinDist = currentEquation;
            }
        }
        return closestTransform;
    }

    private IEnumerator GeyserExplode() {
        closestTransform = closestToPlayer();
		if(closestTransform.GetChild(0) != null)
			closestTransform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(geyserAnticipationTime);
        //Explode

        //Wouter BossSkillGeyser

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Boss/BossSkillGeyser", this.gameObject);
        if (closestTransform.GetChild(0) != null)
			closestTransform.GetChild(0).gameObject.SetActive(false);
		StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shotCameraShakeForce, shotCameraDuration));
		StartCoroutine(SpewLavaOverTime(closestTransform.parent));
        yield return new WaitForSeconds(geyserActiveTime);
    }

	

}
