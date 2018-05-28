using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour {

    [SerializeField]
    private List<GameObject> BossProjectiles = new List<GameObject>();

    [Header("Heavy attack specs")]
    public float heavyDmg;
    public float heavyCooldown;
    public GameObject rockslide;

    [Header("Medium attack specs")]
    public float medDmg;
    public float medCooldown;

    [Header("Light attack specs")]
    public float lightDmg;
    public float lightCooldown;
    public GameObject geysers;

    public void HeavyAttack() {
        Debug.Log("heavy");
    }

    public void MediumAttack() {
        Debug.Log("Medium");
    }

    public void LightAttack() {
        Debug.Log("Light");
    }
}
