using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossController : MonoBehaviour {
    [SerializeField]
    private BossAnimationController bossAnim;

	[Header("Boss stats")]
	private BreakableObject hp;
    private BossAttacks bAttacks;
    [SerializeField]
    private int currentHp;

    private float heavyDmg;
    private float heavyCooldown;

    private float medDmg;
    private float medCooldown;

    private float lightDmg;
    private float lightCooldown;

    private float HeavyTimer = 0;
    private float MedTimer = 0;
    private float lightTimer = 0;

    private bool canHeavyAttack = false;
    private bool canMedAttack = false;
    private bool canLightAttack = false;

    private bool canMove = true;

    [Header("Specifics")]
	[SerializeField]
	private int currentStage = 1;

	[Header("References")]
	public GameObject player;//Add player to keep track of distance between this and player

	[FMODUnity.EventRef]
	public string MusicGameplay = "event:/Music/MusicGameplayNonLineair";
	public FMOD.Studio.EventInstance MusicGameplayNonLineair;
	public FMOD.Studio.ParameterInstance StageParameter;

	private void Awake()
	{
		MusicGameplayNonLineair = FMODUnity.RuntimeManager.CreateInstance(MusicGameplay);
		MusicGameplayNonLineair.getParameter("Stage", out StageParameter);
        bossAnim = this.GetComponent<BossAnimationController>();
	}

	void Start() {
		hp = this.GetComponent<BreakableObject>();
        bAttacks = this.GetComponent<BossAttacks>();
        SetStats();

		//wouter StartMusic
		MusicGameplayNonLineair.start();

	}

	void FixedUpdate () {

        //Cooldown timers
        if (HeavyTimer < heavyCooldown) {
            HeavyTimer += Time.deltaTime;
            canHeavyAttack = false;
        }else {
            canHeavyAttack = true;
        }
        if (MedTimer < medCooldown) {
            MedTimer += Time.deltaTime;
            canMedAttack = false;
        }else {
            canMedAttack = true;
        }
        if (lightTimer < lightCooldown) {
            lightTimer += Time.deltaTime;
            canLightAttack = false;
        }else {
            canLightAttack = false;
        }

        //FSM
        ManageStage(currentStage);
	}

	public void ManageStage(int stage) {
		switch (currentStage) {
		case 1:
			Stage1();
			break;
		case 2:
			Stage2();
			break;
		default:
			Debug.Log("wrong stage given");
			break;
		}
	}

	public int SetBossStage(int stage) {
        SetStats();
		return currentStage = stage;
	}

	public void Stage1() {

        if (CurrentHp() < hp.maxHealth * 0.6f) {
            //60%
            //Debug.Log("60% or less");
            bossAnim.AnimationUnchain();
            SetBossStage(2);

            //Audio
            StageParameter.setValue(2);
            //End Audio
        }

        else if (CurrentHp() < hp.maxHealth * 0.8f) {
            //80%
            //Debug.Log("80% or less");
            if (canHeavyAttack) {
                //Heavy attack
                CallHeavyAttack();
            }

            if (canMedAttack) {
                //Medium Attack
                CallMedAttack();
            }

            if (canLightAttack) {
                //Light attack
                CallLightAttack();
            }
        } 

        else {
            //100%
            //Debug.Log("100% or less");

            if (canMedAttack) {
                //Medium Attack
                CallMedAttack();
            }

            if (canLightAttack) {
                //Light attack
                CallLightAttack();

                //Start geysers continouous
            }

            //Audio
            StageParameter.setValue(1);
            //End Audio

        }
	}

	public void Stage2() {

        if (CurrentHp() < hp.maxHealth * 0.15f) {
            //15%
            //Debug.Log("15% or less");

            if (canHeavyAttack) {
                //Heavy attack
                CallHeavyAttack();
            }

            if (canMedAttack) {
                CallMedAttack();
                }


            if (canLightAttack) {
                //Light attack
                CallLightAttack();

            }

            //Audio
            StageParameter.setValue(4);
            //End Audio
        }

        else if (CurrentHp() < hp.maxHealth * 0.25f) {
            //25%
            //Debug.Log("25% or less");

            if (canHeavyAttack) {
                //Heavy attack
                CallHeavyAttack();
                }

            if (canMedAttack) {
                //Medium Attack
                CallMedAttack();
            }

            if (canLightAttack) {
                //Light attack
                CallLightAttack();
            }

            //Audio
            StageParameter.setValue(3);
            //End Audio
        }
        else if (CurrentHp() < hp.maxHealth * 0.6f) {
            //Debug.Log("60% or less");

            if (canHeavyAttack) {
                //Heavy attack
                CallHeavyAttack();
            }

            if (canMedAttack) {
                CallMedAttack();
                }
                //Medium Attack

                

            if (canLightAttack) {
                //Light attack
                CallLightAttack();
            }
        }

        else {
            //Debug.Log("Dead");
        }
        //Move
        //if(!bAttacks.currentlyAttacking)
            //MoveTowardsPlayer();

	}


    #region Call Attacks
    public void CallHeavyAttack() {
        bAttacks.HeavyAttack(currentStage);
        HeavyTimer = 0;
        canHeavyAttack = false;
        }

    public void CallMedAttack() {
        if (currentStage == 2) {
            if (!bAttacks.currentlyAttacking) {
                bAttacks.SetupMedVariables(player.transform.position);
                bAttacks.MediumAttack();
                bossAnim.AnimationBodyslamBegin();
                bossAnim.AnimationBodyslamPeak();
                bossAnim.AnimationBodyslamLand();
                Debug.Log("Setup");
                }

            else {
                bAttacks.MediumAttack();
                }

            if (!bAttacks.MediumAttack()) {
                MedTimer = 0;
                canMedAttack = false;
                }
            }
        else {
            bAttacks.LavaPlume();
            MedTimer = 0;
            canMedAttack = false;
            }
        }
    public void CallLightAttack() { bossAnim.AnimationTentacleSwipe(); lightTimer = 0; canLightAttack = false; }
    #endregion

    public void MoveTowardsPlayer() {
        if (Vector2.Distance(this.transform.position, player.transform.position) > 15) {
            this.transform.position = Vector2.MoveTowards(new Vector2(this.transform.position.x, 0), new Vector2(player.transform.position.x, 0), 0.01f);
            }
        }
    #region manage stats

    public int CurrentHp(){
		return hp.returnHealth();
	}

    private void SetStats() {
        heavyDmg = bAttacks.heavyDmg;
        heavyCooldown = bAttacks.heavyCooldown;

        medDmg = bAttacks.medDmg;
        medCooldown = bAttacks.medCooldown;

        lightDmg = bAttacks.lightDmg;
        lightCooldown = bAttacks.lightCooldown;

    }
    #endregion
    }


//Maybe add scripts for each attack, call the attack, this then returns a boolean?
