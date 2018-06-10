using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour, IDamagable  {

    public void ApplyDamage(int damage) {
        this.gameObject.SetActive(false);
        //explode particles
        //Wouter TumblingRocks
       // FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/BossSkillRockslideTumblingRocks");
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Boss/BossSkillRockslideTumblingRocks", this.gameObject);
    }

    public void TakeDamage() {
        //let the player take damage too
        }
}
