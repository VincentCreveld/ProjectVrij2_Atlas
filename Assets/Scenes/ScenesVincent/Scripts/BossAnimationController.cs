using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour {

    public Animator anim;

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
    }
