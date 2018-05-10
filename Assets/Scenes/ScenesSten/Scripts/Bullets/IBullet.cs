using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet {

    void SetupStats(float spd, int _dmg, Vector2 _dir);
    void OnCollisionEnter2D(Collision2D col);
    void OnDisable();
    }
