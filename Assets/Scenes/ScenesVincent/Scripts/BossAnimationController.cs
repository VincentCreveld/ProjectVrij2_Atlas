using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour {

    public Animator anim;

    public void AnimationUnchain() {
        anim.SetTrigger("Unchain");
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
        }
    public void AnimationTentacleSwipe() {
        anim.SetTrigger("Swipe");
        }
    }
