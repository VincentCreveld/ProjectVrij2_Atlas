using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healpack : MonoBehaviour, ICanHeal {

	public int healAmount = 0;

	public void HealTarget(IHealable ih) {
		ih.HealHealth(healAmount);
		Destroy(gameObject);
	}
}
