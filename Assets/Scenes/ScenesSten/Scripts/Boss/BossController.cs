using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossController : MonoBehaviour {


	[Header("Boss stats")]
	[SerializeField]
	private BreakableObject hp;
	private int currentHp;
	//Add all cooldown(get from child)

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



	}

	void Start() {
		hp = this.GetComponent<BreakableObject>();

		//wouter StartMusic
		MusicGameplayNonLineair.start();

	}
	void Update () {

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
		return currentStage = stage;
	}

	public void Stage1() {

		if (CurrentHp() < hp.maxHealth * 0.6) {
			//Stage 1.1
			//Hier iets wat op 60% hp moet gebeuren
			StageParameter.setValue(2);

			Debug.Log("1.1");
		}else {
			//60 - 100%
			StageParameter.setValue(1);
		}

		if (CurrentHp() < hp.maxHealth * 0.2)
		{
			//stage 2.1
			//Hier iets wat op 20% hp moet gebeuren
			StageParameter.setValue(3);
		}
		else
		{
			//hier alles boven 20% hp
		}
		//voor testen even hier moet uiteindelijk (if bossdeath)
		if (CurrentHp() < hp.maxHealth * 0.05)
		{
			//stage 3
			//Hier iets wat op 0% hp moet gebeuren
			//voor test even hier moet naar stage 2
			StageParameter.setValue(4);
		}
		else
		{
			//hier alles boven 20% hp
		}
	}

	public void Stage2() {

		if(CurrentHp() < hp.maxHealth*0.2) {
			//stage 2.1
			//Hier iets wat op 20% hp moet gebeuren
			StageParameter.setValue(3);
		}
		else {
			//hier alles boven 20% hp
		}
	}




	public int CurrentHp(){
		return hp.returnHealth();
	}
}


//Maybe add scripts for each attack, call the attack, this then returns a boolean?
