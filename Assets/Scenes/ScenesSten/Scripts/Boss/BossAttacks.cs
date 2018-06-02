using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour {

    [SerializeField]
    private List<GameObject> bossProjectiles = new List<GameObject>();

    [Header("Heavy attack specs")]
    public float heavyDmg;
    public float heavyCooldown;
    public GameObject rockslide;

    [Header("Medium attack specs")]
    public float medDmg;
    public float medCooldown;
    private float jumpHeight = 20;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool phase1 = true;


    [Header("Light attack specs")]
    public float lightDmg;
    public float lightCooldown;
    public GameObject geysers;

    public bool currentlyAttacking = false;

    public void HeavyAttack(int state) {
        //Audio

        //End Audio

        //Technical
        if (state != 1) {
            //smash
            }

        StartCoroutine(rockslide.GetComponent<Rockslide>().Earthquake(true));

        Debug.Log("heavy");
        }

    public void SetupMedVariables(Vector2 playerPos) {
        startPos = this.transform.position;
        endPos = playerPos;
        currentlyAttacking = true;
        phase1 = true;
        Debug.Log("Med setup");
        }

    //Body Slam
    public bool MediumAttack() {


        //Audio;

        //End Audio

        //Technical
        float steps = 0.3f;
        Debug.Log("med attack");

        if (!phase1 && (int)transform.position.y <= 1) {
            transform.position = new Vector2(this.transform.position.x, 0);
            currentlyAttacking = false;
            return false;
            }

        if ((int)this.transform.position.y >= jumpHeight - 5) {
            phase1 = false;
            }

        if (phase1) {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(endPos.x, endPos.y + jumpHeight+5), steps);
            currentlyAttacking = true;
            }
        else {
            steps *= 1.1f;
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(endPos.x, 0), steps);
            }
        return true;
        }

    public bool LavaPlume() {

        //start particle and animation

        Debug.Log("PLUME");

        return true;
        }

    public void LightAttack() {

        //Animation

        //Audio

        //End Audio

        //Technical
        Debug.Log("Light");
        }
    
    public void SetStatsV2() {
        //Change cooldowns and dmg'es

        }
    }


