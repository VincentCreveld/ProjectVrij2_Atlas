using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healpack : MonoBehaviour, ICanHeal {

	public int healAmount = 0;

	public void HealTarget(IHealable ih) {
		ih.HealHealth(healAmount);

        //Wouter BossSkillGeyser

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Player/PlayerPickupHP", this.gameObject);
        Destroy(gameObject);
	}
}
