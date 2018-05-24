using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    private SpriteRenderer currSprite;
    private Color currSpriteColor;

	// Use this for initialization
	void Start () {
        currSprite = this.GetComponent<SpriteRenderer>();
        currSpriteColor = this.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
        currSpriteColor.r = (255 + Mathf.Sin(Time.deltaTime));
	}
}
