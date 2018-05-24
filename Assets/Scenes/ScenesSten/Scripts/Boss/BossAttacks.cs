using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour {

    [SerializeField]
    private List<GameObject> BossProjectiles = new List<GameObject>();

    public float startRockslide;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Update() {
        //FSM
    }
}
