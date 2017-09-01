using UnityEngine;
using System.Collections;

public class RockLevelController : MonoBehaviour {

	public GameObject levelSlide;
	RockLevelInstantiator instantiator;
	GameObject instantiatedTutorialShade, currentTutorialShade;
	public bool slowTime, soundOn, musicOn;
	bool timeChanged;
	int levelNumber;
	TransitionShadeController transitionShadeController;

	void Awake () {
		if (GameObject.Find ("Mountain Level One ID") != null) 
			levelNumber = 1;
		else if (GameObject.Find ("Mountain Level Two ID") != null) 
			levelNumber = 2;
		else if (GameObject.Find ("Mountain Level Three ID") != null) 
			levelNumber = 3;
		else if (GameObject.Find ("Mountain Level Four ID") != null) 
			levelNumber = 4;
		else if (GameObject.Find ("Mountain Level Five ID") != null) 
			levelNumber = 5;
		else if (GameObject.Find ("Mountain Level Six ID") != null) 
			levelNumber = 6;
		else if (GameObject.Find ("Mountain Level Seven ID") != null) 
			levelNumber = 7;
		else if (GameObject.Find ("Mountain Level Eight ID") != null) 
			levelNumber = 8;
		else if (GameObject.Find ("Mountain Level Nine ID") != null) 
			levelNumber = 9;
		else if (GameObject.Find ("Mountain Level Ten ID") != null) 
			levelNumber = 10;
		else if (GameObject.Find ("Mountain Level Eleven ID") != null) 
			levelNumber = 11;
		else if (GameObject.Find ("Mountain Level Twelve ID") != null) 
			levelNumber = 12;
		else if (GameObject.Find ("Mountain Level Thirteen ID") != null) 
			levelNumber = 13;
		else if (GameObject.Find ("Mountain Level Fourteen ID") != null) 
			levelNumber = 14;
		else if (GameObject.Find ("Mountain Level Fifteen ID") != null) 
			levelNumber = 15;
		else if (GameObject.Find ("Mountain Level Sixteen ID") != null) 
			levelNumber = 16;
		else if (GameObject.Find ("Mountain Level Seventeen ID") != null) 
			levelNumber = 17;
		else if (GameObject.Find ("Mountain Level Eighteen ID") != null) 
			levelNumber = 18;
		else if (GameObject.Find ("Mountain Level Nineteen ID") != null) 
			levelNumber = 19;
		else if (GameObject.Find ("Mountain Level Twenty ID") != null) 
			levelNumber = 20;
		else if (GameObject.Find ("Mountain Level TwentyOne ID") != null) 
			levelNumber = 21;
		else if (GameObject.Find ("Mountain Level TwentyTwo ID") != null) 
			levelNumber = 22;
		else if (GameObject.Find ("Mountain Level TwentyThree ID") != null) 
			levelNumber = 23;
		else if (GameObject.Find ("Mountain Level TwentyFour ID") != null) 
			levelNumber = 24;
		else if (GameObject.Find ("Mountain Level TwentyFive ID") != null) 
			levelNumber = 25;
		else if (GameObject.Find ("Mountain Level TwentySix ID") != null) 
			levelNumber = 26;
		else if (GameObject.Find ("Mountain Level TwentySeven ID") != null) 
			levelNumber = 27;
		else if (GameObject.Find ("Mountain Level TwentyEight ID") != null) 
			levelNumber = 28;
		else if (GameObject.Find ("Mountain Level TwentyNine ID") != null) 
			levelNumber = 29;
		else if (GameObject.Find ("Mountain Level Thirty ID") != null) 
			levelNumber = 30;
		else if (GameObject.Find ("City Level One ID") != null) 
			levelNumber = 31;
		else if (GameObject.Find ("City Level Two ID") != null)
			levelNumber = 32;
		else if (GameObject.Find ("City Level Three ID") != null)
			levelNumber = 33;
		else if (GameObject.Find ("City Level Four ID") != null) 
			levelNumber = 34;
		else if (GameObject.Find ("City Level Five ID") != null)
			levelNumber = 35;
		else if (GameObject.Find ("City Level Six ID") != null)
			levelNumber = 36;
		else if (GameObject.Find ("City Level Seven ID") != null)
			levelNumber = 37;
		else if (GameObject.Find ("City Level Eight ID") != null)
			levelNumber = 38;
		else if (GameObject.Find ("City Level Nine ID") != null) 
			levelNumber = 39;
		else if (GameObject.Find ("City Level Ten ID") != null)
			levelNumber = 40;
		else if (GameObject.Find ("City Level Eleven ID") != null)
			levelNumber = 41;
		else if (GameObject.Find ("City Level Twelve ID") != null)
			levelNumber = 42;
		else if (GameObject.Find ("City Level Thirteen ID") != null)
			levelNumber = 43;
		else if (GameObject.Find ("City Level Fourteen ID") != null)
			levelNumber = 44;
		else if (GameObject.Find ("City Level Fifteen ID") != null)
			levelNumber = 45;
		else if (GameObject.Find ("City Level Sixteen ID") != null)
			levelNumber = 46;
		else if (GameObject.Find ("City Level Seventeen ID") != null)
			levelNumber = 47;
		else if (GameObject.Find ("City Level Eighteen ID") != null)
			levelNumber = 48;
		else if (GameObject.Find ("City Level Nineteen ID") != null)
			levelNumber = 49;
		else if (GameObject.Find ("City Level Twenty ID") != null)
			levelNumber = 50;
		else if (GameObject.Find ("City Level Twenty One ID") != null)
			levelNumber = 51;
		else if (GameObject.Find ("City Level Twenty Two ID") != null)
			levelNumber = 52;
		else if (GameObject.Find ("City Level Twenty Three ID") != null)
			levelNumber = 53;
		else if (GameObject.Find ("City Level Twenty Four ID") != null)
			levelNumber = 54;
		else if (GameObject.Find ("City Level Twenty Five ID") != null)
			levelNumber = 55;
		else if (GameObject.Find ("City Level Twenty Six ID") != null)
			levelNumber = 56;
		else if (GameObject.Find ("City Level Twenty Seven ID") != null)
			levelNumber = 57;
		else if (GameObject.Find ("City Level Twenty Eight ID") != null)
			levelNumber = 58;
		else if (GameObject.Find ("City Level Twenty Nine ID") != null)
			levelNumber = 59;
		else if (GameObject.Find ("City Level Thirty ID") != null)
			levelNumber = 60;
		else if (GameObject.Find ("Cabin Level One ID") != null)
			levelNumber = 61;
		else if (GameObject.Find ("Cabin Level Two ID") != null)
			levelNumber = 62;
		else if (GameObject.Find ("Cabin Level Three ID") != null)
			levelNumber = 63;
		else if (GameObject.Find ("Cabin Level Four ID") != null)
			levelNumber = 64;
		else if (GameObject.Find ("Cabin Level Five ID") != null)
			levelNumber = 65;
		else if (GameObject.Find ("Cabin Level Six ID") != null)
			levelNumber = 66;
		else if (GameObject.Find ("Cabin Level Seven ID") != null)
			levelNumber = 67;
		else if (GameObject.Find ("Cabin Level Eight ID") != null)
			levelNumber = 68;
		else if (GameObject.Find ("Cabin Level Nine ID") != null)
			levelNumber = 69;
		else if (GameObject.Find ("Cabin Level Ten ID") != null)
			levelNumber = 70;
		else if (GameObject.Find ("Cabin Level Eleven ID") != null)
			levelNumber = 71;
		else if (GameObject.Find ("Cabin Level Twelve ID") != null)
			levelNumber = 72;
		else if (GameObject.Find ("Cabin Level Thirteen ID") != null)
			levelNumber = 73;
		else if (GameObject.Find ("Cabin Level Fourteen ID") != null)
			levelNumber = 74;
		else if (GameObject.Find ("Cabin Level Fifteen ID") != null)
			levelNumber = 75;
		else if (GameObject.Find ("Cabin Level Sixteen ID") != null)
			levelNumber = 76;
		else if (GameObject.Find ("Cabin Level Seventeen ID") != null)
			levelNumber = 77;
		else if (GameObject.Find ("Cabin Level Eighteen ID") != null)
			levelNumber = 78;
		else if (GameObject.Find ("Cabin Level Nineteen ID") != null)
			levelNumber = 79;
		else if (GameObject.Find ("Cabin Level Twenty ID") != null)
			levelNumber = 80;
		else if (GameObject.Find ("Cabin Level Twenty One ID") != null)
			levelNumber = 81;
		else if (GameObject.Find ("Cabin Level Twenty Two ID") != null)
			levelNumber = 82;
		else if (GameObject.Find ("Cabin Level Twenty Three ID") != null)
			levelNumber = 83;
		else if (GameObject.Find ("Cabin Level Twenty Four ID") != null)
			levelNumber = 84;
		else if (GameObject.Find ("Cabin Level Twenty Five ID") != null)
			levelNumber = 85;
		else if (GameObject.Find ("Cabin Level Twenty Six ID") != null)
			levelNumber = 86;
		else if (GameObject.Find ("Cabin Level Twenty Seven ID") != null)
			levelNumber = 87;
		else if (GameObject.Find ("Cabin Level Twenty Eight ID") != null)
			levelNumber = 88;
		else if (GameObject.Find ("Cabin Level Twenty Nine ID") != null)
			levelNumber = 89;
		else if (GameObject.Find ("Cabin Level Thirty ID") != null)
			levelNumber = 90;
		else if (GameObject.Find ("Launchpad Level One ID") != null)
			levelNumber = 91;
		else if (GameObject.Find ("Launchpad Level Two ID") != null) 
			levelNumber = 92;
		else if (GameObject.Find ("Launchpad Level Three ID") != null)
			levelNumber = 93; 
		else if (GameObject.Find ("Launchpad Level Four ID") != null)
			levelNumber = 94;
		else if (GameObject.Find ("Launchpad Level Five ID") != null)
			levelNumber = 95;
		else if (GameObject.Find ("Launchpad Level Six ID") != null)
			levelNumber = 96;
		else if (GameObject.Find ("Launchpad Level Seven ID") != null)
			levelNumber = 97;
		else if (GameObject.Find ("Launchpad Level Eight ID") != null)
			levelNumber = 98;
		else if (GameObject.Find ("Launchpad Level Nine ID") != null)
			levelNumber = 99;
		else if (GameObject.Find ("Launchpad Level Ten ID") != null)
			levelNumber = 100;
		else if (GameObject.Find ("Launchpad Level Eleven ID") != null)
			levelNumber = 101;
		else if (GameObject.Find ("Launchpad Level Twelve ID") != null)
			levelNumber = 102;
		else if (GameObject.Find ("Launchpad Level Thirteen ID") != null)
			levelNumber = 103;
		else if (GameObject.Find ("Launchpad Level Fourteen ID") != null)
			levelNumber = 104;
		else if (GameObject.Find ("Launchpad Level Fifteen ID") != null)
			levelNumber = 105;
		else if (GameObject.Find ("Launchpad Level Sixteen ID") != null)
			levelNumber = 106;
		else if (GameObject.Find ("Launchpad Level Seventeen ID") != null)
			levelNumber = 107;
		else if (GameObject.Find ("Launchpad Level Eighteen ID") != null)
			levelNumber = 108;
		else if (GameObject.Find ("Launchpad Level Nineteen ID") != null)
			levelNumber = 109;
		else if (GameObject.Find ("Launchpad Level Twenty ID") != null)
			levelNumber = 110;
		else if (GameObject.Find ("Launchpad Level Twenty One ID") != null)
			levelNumber = 111;
		else if (GameObject.Find ("Launchpad Level Twenty Two ID") != null)
			levelNumber = 112;
		else if (GameObject.Find ("Launchpad Level Twenty Three ID") != null)
			levelNumber = 113;
		else if (GameObject.Find ("Launchpad Level Twenty Four ID") != null)
			levelNumber = 114;
		else if (GameObject.Find ("Launchpad Level Twenty Five ID") != null)
			levelNumber = 115;
		else if (GameObject.Find ("Launchpad Level Twenty Six ID") != null)
			levelNumber = 116;
		else if (GameObject.Find ("Launchpad Level Twenty Seven ID") != null)
			levelNumber = 117;
		else if (GameObject.Find ("Launchpad Level Twenty Eight ID") != null)
			levelNumber = 118;
		else if (GameObject.Find ("Launchpad Level Twenty Nine ID") != null)
			levelNumber = 119;
		else if (GameObject.Find ("Launchpad Level Thirty ID") != null)
			levelNumber = 120;
	}
	
	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		if (levelNumber >= 1 && levelNumber < 31)
			instantiator.InstantiateMountainJewels (levelNumber - 1);
		if (levelNumber > 30 && levelNumber < 61) {
			instantiator.InstantiateCityJewels (levelNumber - 31);
		}
		if (levelNumber > 60 && levelNumber < 91)
			instantiator.InstantiateCabinJewels (levelNumber - 61);
		if (levelNumber > 90 && levelNumber < 121)
			instantiator.InstantiateLaunchpadJewels (levelNumber - 91);
		instantiatedTutorialShade = (GameObject)Instantiate (levelSlide);
		currentTutorialShade = instantiatedTutorialShade;
	}
	
	// Update is called once per frame
	void Update () {
		if (slowTime && !timeChanged) {
			Time.timeScale = .25f;
			timeChanged = true;
		} else if (!slowTime && timeChanged) {
			Time.timeScale = 1f;
			timeChanged = false;
		}
	}
	
	public GameObject GetTutorialShade () {
		return currentTutorialShade;
	}
	
	public void SetCurrentTutorialShade (GameObject shade) {
		currentTutorialShade = shade;
	}
	
	public GameObject GetCurrentTutorialShade () {
		return currentTutorialShade;
	}

	public int GetLevelNumber () {
		return levelNumber;
	}
}
