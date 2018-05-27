using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour, IDamagable  {

    public void ApplyDamage(int damage) {
        this.gameObject.SetActive(false);
        //explode particles
    }
}
