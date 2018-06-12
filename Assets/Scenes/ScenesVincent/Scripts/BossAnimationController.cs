using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAnimationController : MonoBehaviour {

    public Animator anim;
	public GameObject endGameScreen;

	public void AnimationUnchain() {
        anim.SetTrigger("Unchain");
        //Wouter BossUnchain

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Boss/BossUnchain", this.gameObject);
    }

    public void AnimationBodyslamBegin() {
        anim.SetTrigger("Bodyslam");
        }
    public void AnimationBodyslamPeak() {
        anim.SetTrigger("Peak");
        }
    public void AnimationBodyslamLand() {
        anim.SetTrigger("Land");
        }
    public void AnimationTentacleSlam() {
        anim.SetTrigger("Slam");
        //Wouter BossSkillSmash

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Boss/BossSkillSmash", this.gameObject);
    }
    public void AnimationTentacleSwipe() {
        anim.SetTrigger("Swipe");
        //Wouter BossSkillSweep

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Boss/BossSkillSweep", this.gameObject);
    }

	public void AnimationLavaPlume() {
		anim.SetTrigger("Plume");
	}

	public void AnimationDie() {
		anim.SetTrigger("Die");
		if(GetComponent<BossController>()!= null)
			GetComponent<BossController>().enabled = false;
		if(GetComponent<BossAttacks>() != null)
			GetComponent<BossAttacks>().enabled = false;
		if(GetComponent<Collider2D>() != null)
			GetComponent<Collider2D>().enabled = false;
		endGameScreen.SetActive(true);
	}
}
