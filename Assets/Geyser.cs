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

	// Use this for initialization
	void Start () {
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
                Debug.Log(currentEquation);
                closestTransform = geyserPositions[i];
                currentMinDist = currentEquation;
            }
        }
        return closestTransform;
    }

    private IEnumerator GeyserExplode() {
        closestTransform = closestToPlayer();


        closestTransform.GetComponent<Renderer>().material.color = Color.green;
        closestTransform.gameObject.SetActive(true);
        yield return new WaitForSeconds(geyserAnticipationTime);
        //Explode
        closestTransform.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(geyserActiveTime);
        closestTransform.gameObject.SetActive(false);
    }

}
