using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelBombHandler : MonoBehaviour {

	List<GameObject> bombList = new List<GameObject> (), tempBombList, removeBombList;
	public GameObject bombNumber0, bombNumber1, bombNumber2, bombNumber3, bombNumber4, bombNumber5, bombNumber6, bombNumber7, bombNumber8, bombNumber9;
	public GameObject bombNumber10, bombNumber11, bombNumber12, bombNumber13, bombNumber14, bombNumber15, bombNumber16, bombNumber17, bombNumber18, bombNumber19;
	public GameObject bombNumber20, bombNumber21, bombNumber22, bombNumber23, bombNumber24, bombNumber25, bombNumber26, bombNumber27, bombNumber28, bombNumber29;
	public GameObject bombNumber30, bombNumber31, bombNumber32, bombNumber33, bombNumber34, bombNumber35, bombNumber36, bombNumber37, bombNumber38, bombNumber39;
	public GameObject bombNumber40, bombNumber41, bombNumber42, bombNumber43, bombNumber44, bombNumber45, bombNumber46, bombNumber47, bombNumber48, bombNumber49;
	public GameObject bombNumber50, bombNumber51, bombNumber52, bombNumber53, bombNumber54, bombNumber55, bombNumber56, bombNumber57, bombNumber58, bombNumber59;
	public GameObject bombNumber60, bombNumber61, bombNumber62, bombNumber63, bombNumber64, bombNumber65, bombNumber66, bombNumber67, bombNumber68, bombNumber69;
	public GameObject bombNumber70, bombNumber71, bombNumber72, bombNumber73, bombNumber74, bombNumber75, bombNumber76, bombNumber77, bombNumber78, bombNumber79;
	public GameObject bombNumber80, bombNumber81, bombNumber82, bombNumber83, bombNumber84, bombNumber85, bombNumber86, bombNumber87, bombNumber88, bombNumber89;
	public GameObject bombNumber90, bombNumber91, bombNumber92, bombNumber93, bombNumber94, bombNumber95, bombNumber96, bombNumber97, bombNumber98, bombNumber99;
	public GameObject bombNumber100, bombNumber101, bombNumber102, bombNumber103, bombNumber104, bombNumber105;

	public GameObject bombCountNumber0, bombCountNumber1, bombCountNumber2, bombCountNumber3, bombCountNumber4, bombCountNumber5, bombCountNumber6, bombCountNumber7, bombCountNumber8, bombCountNumber9;
	public GameObject blueExplosion, redExplosion, greenExplosion, whiteExplosion, purpleExplosion, orangeExplosion, boulderExplosion;
	public GameObject blueBombExplosion, redBombExplosion, greenBombExplosion, whiteBombExplosion, purpleBombExplosion, orangeBombExplosion;
	public GameObject bombExplosion, levelFailedShade;
	public GameObject levelCompleteShade;
	Vector2 artilleryForce;
	Vector3 bombCounterPosition, bombCounterPositionTwo;
	GameObject tempBomb, instantiatedBomb;
	RockLevelJewelMovement jewelMovement, jewelMovementOldBomb;
	RockLevelInstantiator instantiator;
	RockLevelTouchHandler touchHandler;
	RockLevelDeleteJewels deleteJewels;
	LevelTwoBombInfo bombInfo;
	RockLevelController controller;
	RockLevelSwapJewel swapJewel;
	BlackBombMovement blackBombMovement;
	RockLevelMovementChecker movementChecker;
	SoundController soundController;

	RockLevelFourInARow fourInARow;
	RockLevelCorners corners;
	RockLevelFiveInARow fiveInARow;
	SoundHandler soundHandler;

	//	LevelThreeTutorialShadeController shadeController;
	//	LevelThreeBannerController bannerController;
	GameObject currentBombCounter, bombCounterFirstDigit, bombCounterSecondDigit, instantiatedShade;
	int bombCount, timeBombCount;
	float timeStamp, timeStamp2, cooldown, cooldown2;
	bool explodeBombs, firstBombExplosion, turnOffTouch, bombExploded, dimShade, explodeTimeBombs, timeBombExploded, launchArtillery, artilleryLaunched, tutorialLevel;
	DarkenOnInstantiaton darken;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("Mountain Level One ID") != null) {
			tutorialLevel = true;
		}

		tempBombList = new List<GameObject> ();
		removeBombList = new List<GameObject> ();
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		controller = gameObject.GetComponent<RockLevelController> ();
		swapJewel = gameObject.GetComponent<RockLevelSwapJewel> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		movementChecker = gameObject.GetComponent<RockLevelMovementChecker> ();
		soundController = gameObject.GetComponent<SoundController> ();
		fourInARow = gameObject.GetComponent<RockLevelFourInARow> ();
		corners = gameObject.GetComponent<RockLevelCorners> ();
		fiveInARow = gameObject.GetComponent<RockLevelFiveInARow> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		//		shadeController = gameObject.GetComponent<LevelThreeTutorialShadeController> ();
//		bombCounterPosition = new Vector3 (2.21f, 4.39f, -1);
		bombCounterPosition = new Vector3 (2.31f, 4.45f, -1.1f);
		bombCounterPositionTwo = new Vector3 (2.48f, 4.45f, -2.38f);
		artilleryForce = new Vector2 (0, 1000);
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		firstBombExplosion = true;
//			bannerController = gameObject.GetComponent<LevelThreeBannerController> ();
		timeStamp = Time.time;
		cooldown2 = 1f;
		cooldown = .15f;
		timeBombCount = 0;
		//Debug.Log ("controller.GetLevelNumber () = " + controller.GetLevelNumber ());
		switch (controller.GetLevelNumber ()) {
		case 1: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 2: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 3: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 4: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 5: bombCount = 2; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPosition, Quaternion.identity); break;
		case 6: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 7: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 8: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 9: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 10: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 11: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 12: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 13: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 14: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 15: bombCount = 2; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPosition, Quaternion.identity); break;
		case 16: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 17: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 18: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 19: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 20: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 21: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 22: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 23: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 24: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 25: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 26: bombCount = 10; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPositionTwo, Quaternion.identity); break; 
		case 27: bombCount = 10; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPositionTwo, Quaternion.identity); break;
		case 28: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 29: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 30: bombCount = 12; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPositionTwo, Quaternion.identity); break;
		case 31: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 32: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 33: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 34: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 35: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 36: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 37: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 38: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 39: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 40: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 41: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 42: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 43: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 44: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 45: bombCount = 6; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPosition, Quaternion.identity); break;
		case 46: bombCount = 6; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPosition, Quaternion.identity); break;
		case 47: bombCount = 8; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber8, bombCounterPosition, Quaternion.identity); break;
		case 48: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 49: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 50: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 51: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 52: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 53: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 54: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 55: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 56: bombCount = 6; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPosition, Quaternion.identity); break;
		case 57: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 58: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 59: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 60: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 61: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 62: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 63: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 64: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 65: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 66: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 67: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 68: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 69: bombCount = 17; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber7, bombCounterPositionTwo, Quaternion.identity); break;
		case 70: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 71: bombCount = 12; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPositionTwo, Quaternion.identity); break;
		case 72: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 73: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 74: bombCount = 8; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber8, bombCounterPosition, Quaternion.identity); break;
		case 75: bombCount = 15; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPositionTwo, Quaternion.identity); break;
		case 76: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 77: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 78: bombCount = 12; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity);
			bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPositionTwo, Quaternion.identity); break;
		case 79: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 80: bombCount = 25; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPosition, Quaternion.identity);
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPositionTwo, Quaternion.identity); break;
		case 81: bombCount = 6; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPosition, Quaternion.identity); break;
		case 82: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber5, bombCounterPosition, Quaternion.identity); break;
		case 83: bombCount = 12; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPositionTwo, Quaternion.identity); break;
		case 84: bombCount = 7; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber7, bombCounterPosition, Quaternion.identity); break;
		case 85: bombCount = 16; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPositionTwo, Quaternion.identity); break;
		case 86: bombCount = 32; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity);
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPositionTwo, Quaternion.identity); break;
		case 87: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 88: bombCount = 17; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); 
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber7, bombCounterPositionTwo, Quaternion.identity); break;
		case 89: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 90: bombCount = 16; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity);
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPositionTwo, Quaternion.identity); break;
		case 91: bombCount = 5; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber8, bombCounterPosition, Quaternion.identity); break;
		case 92: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 93: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 94: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 95: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 96: bombCount = 2; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPosition, Quaternion.identity); break;
		case 97: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 98: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 99: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 100: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 101: bombCount = 21; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPosition, Quaternion.identity);
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPositionTwo, Quaternion.identity); break;
		case 102: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 103: bombCount = 6; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPosition, Quaternion.identity); break;
		case 104: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity); break;
		case 105: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 106: bombCount = 27; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity);
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber6, bombCounterPositionTwo, Quaternion.identity); break;
		case 107: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 108: bombCount = 8; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber8, bombCounterPosition, Quaternion.identity); break;
		case 109: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 110: bombCount = 18; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity);
			bombCounterSecondDigit = (GameObject)Instantiate (bombCountNumber8, bombCounterPosition, Quaternion.identity); break;
		case 111: bombCount = 9; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber9, bombCounterPosition, Quaternion.identity); break;
		case 112: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 113: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 114: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 115: bombCount = 4; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber4, bombCounterPosition, Quaternion.identity); break;
		case 116: bombCount = 2; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber2, bombCounterPosition, Quaternion.identity); break;
		case 117: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 118: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		case 119: bombCount = 1; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber0, bombCounterPosition, Quaternion.identity); break;
		case 120: bombCount = 3; bombCounterFirstDigit = (GameObject)Instantiate (bombCountNumber3, bombCounterPosition, Quaternion.identity); break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (explodeBombs && Time.time > timeStamp + cooldown) {
			if (GameObject.Find ("Time Bomb Identification") != null)
				GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted (false);
			GameObject.Find ("Level Controller").GetComponent<RockLevelMatchAssistant> ().SetGameStarted (false);
			GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().SetGameStarted (false);
			ExplodeBomb ();
		}
		else if (explodeTimeBombs && Time.time > timeStamp + cooldown) {
			ExplodeTimeBombs ();
		}
		if (turnOffTouch) {
			TurnOffTouchHandler ();
			turnOffTouch = false;
		}
		if (dimShade && Time.time > timeStamp2 + cooldown2) {
			deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
			deleteJewels.SetStopFindingMatches (true);
			instantiatedShade = (GameObject)Instantiate (levelFailedShade);
			darken = instantiatedShade.GetComponent<DarkenOnInstantiaton> ();
			darken.SetLevelComplete (false);
			dimShade = false;
		}
		if (launchArtillery && Time.time > timeStamp + cooldown) {
			cooldown = .25f;
			if (timeBombCount <= 0) {
				launchArtillery = false;
				artilleryLaunched = true;
				return;
			}
			blackBombMovement = currentBombCounter.GetComponent<BlackBombMovement> ();
			blackBombMovement.LaunchBomb ();
			currentBombCounter.GetComponent<Rigidbody2D>().isKinematic = false;
//			currentBombCounter.rigidbody2D.AddForce (artilleryForce);
//			currentBombCounter = (GameObject)Instantiate (GetBlackBombMinusOne (currentBombCounter), bombCounterPosition, Quaternion.identity);
			timeBombCount--;
			timeStamp = Time.time;
		}

		else if (artilleryLaunched && timeBombCount == 0 && Time.time > timeStamp + cooldown2 && movementChecker.GetGridStatic ()) {
			artilleryLaunched = false;
			deleteJewels.SetStopFindingMatches (true);
			instantiatedShade = (GameObject)Instantiate (levelCompleteShade);
			darken = instantiatedShade.GetComponent<DarkenOnInstantiaton> ();
			darken.SetLevelComplete (true);
		}
	}

	public bool ZeroBombInList () {
		foreach (GameObject a in bombList) {
			for (int i = 0; i < a.transform.childCount; i++) {
				if (a.transform.GetChild (i).tag == "Bomb Number") {
					if (a.transform.GetChild (i).GetComponent<BombNumberHandler> ().GetBombNumber () == 0) {
						return true;
					}
					break;
				}
			}
		}
		return false;
	}

	public void AddBombToList (GameObject bomb) {
		bombList.Add (bomb);
	}

	public void DecreaseAllBombsInList () {
		Vector3 bombPosition = Vector3.zero;
		foreach (GameObject a in bombList) {
			if (a != null) {
				GameObject tempNumber = null, newTempNumber;
				if (a.transform.childCount > 0) {
					for (int i = 0; i < a.transform.childCount; i++) {
						if (a.transform.GetChild (i).tag == "Bomb Number") {
							tempNumber = a.transform.GetChild (i).gameObject;
							bombPosition = a.transform.GetChild (i).transform.position;
							break;
						}
					}
				}

				if (GetNextBombNumber (tempNumber) == null) {
					//Debug.Log ("GetNextBombNumber is returning null");
					return;
				}
//				if (a.tag == "Boulder" && a.transform.childCount > 1)
//					newTempNumber = (GameObject)Instantiate (GetNextBombNumber (tempNumber), new Vector3 (a.transform.position.x + .03f, a.transform.position.y - .1f, a.transform.position.z -.1f), Quaternion.identity);
//				else 
//					newTempNumber = (GameObject)Instantiate (GetNextBombNumber (tempNumber), new Vector3 (a.transform.position.x, a.transform.position.y, a.transform.position.z - .1f), Quaternion.identity);
				newTempNumber = (GameObject)Instantiate (GetNextBombNumber (tempNumber), bombPosition, Quaternion.identity);
				newTempNumber.transform.parent = a.transform;
				Destroy (tempNumber);
			}

//			if (jewelMovement.GetBounced ()) {
//				switch (a.tag) {
//				case "Red Bomb": tempBomb = GetRedBombMinusOne (a); break;
//				case "Blue Bomb": tempBomb = GetBlueBombMinusOne (a); break;
//				case "Green Bomb": tempBomb = GetGreenBombMinusOne (a); break;
//				case "White Bomb": tempBomb = GetWhiteBombMinusOne (a); break;
//				case "Purple Bomb": tempBomb = GetPurpleBombMinusOne (a); break;
//				case "Orange Bomb": tempBomb = GetOrangeBombMinusOne (a); break;
//				} 
//				instantiatedBomb = (GameObject)Instantiate (tempBomb, a.transform.position, Quaternion.identity);
//				instantiatedBomb.rigidbody2D.velocity = a.rigidbody2D.velocity;
//				jewelMovement = instantiatedBomb.GetComponent<RockLevelJewelMovement> ();
//				bombInfo = instantiatedBomb.GetComponent<LevelTwoBombInfo> ();
//				jewelMovement.SetBounced (true);
//				jewelMovementOldBomb = a.GetComponent<RockLevelJewelMovement> ();
//				jewelMovement.SetRow (jewelMovementOldBomb.GetRow ());
//				jewelMovement.SetCol (jewelMovementOldBomb.GetCol ());
//				instantiatedBomb.layer = a.layer;
//				tempBombList.Add (instantiatedBomb);
//				jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
//				instantiator.SetJewelGridGameObject (jewelMovement.GetRow (), jewelMovement.GetCol (), instantiatedBomb);
//				removeBombList.Add (a);
//				fourInARow.SwapIfContainedInList (a, instantiatedBomb);
//				corners.SwapIfInDeleteArrays (a, instantiatedBomb);
//				fiveInARow.SwapIfInChildStarList (a, instantiatedBomb);
//			}
		}
//		foreach (GameObject a in tempBombList) {
//			bombList.Add (a);
//		}
//		foreach (GameObject a in removeBombList) {
//			bombList.Remove (a);
//			Destroy (a);
//		}
//		tempBombList.Clear ();
//		removeBombList.Clear ();
	}

	public bool BombIsBeingSwapped () {
		if (swapJewel.GetSwapStart ())
			return true;
		foreach (GameObject a in bombList) {
			if (swapJewel.IsASwapJewel (a))
				return true;
		}
		return false;
	}

	public void AddFiveToAllBombs () {
		foreach (GameObject a in bombList) {
			AddFiveToAllBombsHelper (a);
		}
	}

	public GameObject GetTheClosestBomb (int col) {
		for (int i = 0; i < 9; i++) {
			if (IsBombMinusBoulder (instantiator.GetJewelGridGameObject (i, col))) {
				return (instantiator.GetJewelGridGameObject (i, col));	
			}
			if (col - 1 >= 0 && IsBombMinusBoulder (instantiator.GetJewelGridGameObject (i, col - 1))) {
				return (instantiator.GetJewelGridGameObject (i, col - 1));
			}
			if (col + 1 < 9 && IsBombMinusBoulder (instantiator.GetJewelGridGameObject (i, col + 1))) {
				return (instantiator.GetJewelGridGameObject (i, col + 1));
			}
			if (col - 2 >= 0 && IsBombMinusBoulder (instantiator.GetJewelGridGameObject (i , col - 2))) {
				return (instantiator.GetJewelGridGameObject (i, col - 2));
			}
			if (col + 2 < 9 && IsBombMinusBoulder (instantiator.GetJewelGridGameObject (i, col + 2))) {
				return (instantiator.GetJewelGridGameObject (i, col + 2));
			}
		}
		return null;
	}

	void AddFiveToAllBombsHelper (GameObject bomb) {
		int bombNumber = 0;
		GameObject tempBombNumber = null, newTempBombNumber = null;
		for (int i = 0; i < bomb.transform.childCount; i++) {
			if (bomb.transform.GetChild (i).tag == "Bomb Number") {
				bombNumber = bomb.transform.GetChild (i).GetComponent<BombNumberHandler> ().GetBombNumber ();
				tempBombNumber = bomb.transform.GetChild (i).gameObject;
				break;
			}
		}

		switch (bombNumber) {
		case 1: newTempBombNumber = (GameObject)Instantiate (bombNumber6, tempBombNumber.transform.position, Quaternion.identity); break;
		case 2: newTempBombNumber = (GameObject)Instantiate (bombNumber7, tempBombNumber.transform.position, Quaternion.identity); break;
		case 3: newTempBombNumber = (GameObject)Instantiate (bombNumber8, tempBombNumber.transform.position, Quaternion.identity); break;
		case 4: newTempBombNumber = (GameObject)Instantiate (bombNumber9, tempBombNumber.transform.position, Quaternion.identity); break;
		case 5: newTempBombNumber = (GameObject)Instantiate (bombNumber10, tempBombNumber.transform.position, Quaternion.identity); break;
		case 6: newTempBombNumber = (GameObject)Instantiate (bombNumber11, tempBombNumber.transform.position, Quaternion.identity); break;
		case 7: newTempBombNumber = (GameObject)Instantiate (bombNumber12, tempBombNumber.transform.position, Quaternion.identity); break;
		case 8: newTempBombNumber = (GameObject)Instantiate (bombNumber13, tempBombNumber.transform.position, Quaternion.identity); break;
		case 9: newTempBombNumber = (GameObject)Instantiate (bombNumber14, tempBombNumber.transform.position, Quaternion.identity); break;

		case 10: newTempBombNumber = (GameObject)Instantiate (bombNumber15, tempBombNumber.transform.position, Quaternion.identity); break;
		case 11: newTempBombNumber = (GameObject)Instantiate (bombNumber16, tempBombNumber.transform.position, Quaternion.identity); break;
		case 12: newTempBombNumber = (GameObject)Instantiate (bombNumber17, tempBombNumber.transform.position, Quaternion.identity); break;
		case 13: newTempBombNumber = (GameObject)Instantiate (bombNumber18, tempBombNumber.transform.position, Quaternion.identity); break;
		case 14: newTempBombNumber = (GameObject)Instantiate (bombNumber19, tempBombNumber.transform.position, Quaternion.identity); break;
		case 15: newTempBombNumber = (GameObject)Instantiate (bombNumber20, tempBombNumber.transform.position, Quaternion.identity); break;
		case 16: newTempBombNumber = (GameObject)Instantiate (bombNumber21, tempBombNumber.transform.position, Quaternion.identity); break;
		case 17: newTempBombNumber = (GameObject)Instantiate (bombNumber22, tempBombNumber.transform.position, Quaternion.identity); break;
		case 18: newTempBombNumber = (GameObject)Instantiate (bombNumber23, tempBombNumber.transform.position, Quaternion.identity); break;
		case 19: newTempBombNumber = (GameObject)Instantiate (bombNumber24, tempBombNumber.transform.position, Quaternion.identity); break;

		case 20: newTempBombNumber = (GameObject)Instantiate (bombNumber25, tempBombNumber.transform.position, Quaternion.identity); break;
		case 21: newTempBombNumber = (GameObject)Instantiate (bombNumber26, tempBombNumber.transform.position, Quaternion.identity); break;
		case 22: newTempBombNumber = (GameObject)Instantiate (bombNumber27, tempBombNumber.transform.position, Quaternion.identity); break;
		case 23: newTempBombNumber = (GameObject)Instantiate (bombNumber28, tempBombNumber.transform.position, Quaternion.identity); break;
		case 24: newTempBombNumber = (GameObject)Instantiate (bombNumber29, tempBombNumber.transform.position, Quaternion.identity); break;
		case 25: newTempBombNumber = (GameObject)Instantiate (bombNumber30, tempBombNumber.transform.position, Quaternion.identity); break;
		case 26: newTempBombNumber = (GameObject)Instantiate (bombNumber31, tempBombNumber.transform.position, Quaternion.identity); break;
		case 27: newTempBombNumber = (GameObject)Instantiate (bombNumber32, tempBombNumber.transform.position, Quaternion.identity); break;
		case 28: newTempBombNumber = (GameObject)Instantiate (bombNumber33, tempBombNumber.transform.position, Quaternion.identity); break;
		case 29: newTempBombNumber = (GameObject)Instantiate (bombNumber34, tempBombNumber.transform.position, Quaternion.identity); break;

		case 30: newTempBombNumber = (GameObject)Instantiate (bombNumber35, tempBombNumber.transform.position, Quaternion.identity); break;
		case 31: newTempBombNumber = (GameObject)Instantiate (bombNumber36, tempBombNumber.transform.position, Quaternion.identity); break;
		case 32: newTempBombNumber = (GameObject)Instantiate (bombNumber37, tempBombNumber.transform.position, Quaternion.identity); break;
		case 33: newTempBombNumber = (GameObject)Instantiate (bombNumber38, tempBombNumber.transform.position, Quaternion.identity); break;
		case 34: newTempBombNumber = (GameObject)Instantiate (bombNumber39, tempBombNumber.transform.position, Quaternion.identity); break;
		case 35: newTempBombNumber = (GameObject)Instantiate (bombNumber40, tempBombNumber.transform.position, Quaternion.identity); break;
		case 36: newTempBombNumber = (GameObject)Instantiate (bombNumber41, tempBombNumber.transform.position, Quaternion.identity); break;
		case 37: newTempBombNumber = (GameObject)Instantiate (bombNumber42, tempBombNumber.transform.position, Quaternion.identity); break;
		case 38: newTempBombNumber = (GameObject)Instantiate (bombNumber43, tempBombNumber.transform.position, Quaternion.identity); break;
		case 39: newTempBombNumber = (GameObject)Instantiate (bombNumber44, tempBombNumber.transform.position, Quaternion.identity); break;

		case 40: newTempBombNumber = (GameObject)Instantiate (bombNumber45, tempBombNumber.transform.position, Quaternion.identity); break;
		case 41: newTempBombNumber = (GameObject)Instantiate (bombNumber46, tempBombNumber.transform.position, Quaternion.identity); break;
		case 42: newTempBombNumber = (GameObject)Instantiate (bombNumber47, tempBombNumber.transform.position, Quaternion.identity); break;
		case 43: newTempBombNumber = (GameObject)Instantiate (bombNumber48, tempBombNumber.transform.position, Quaternion.identity); break;
		case 44: newTempBombNumber = (GameObject)Instantiate (bombNumber49, tempBombNumber.transform.position, Quaternion.identity); break;
		case 45: newTempBombNumber = (GameObject)Instantiate (bombNumber50, tempBombNumber.transform.position, Quaternion.identity); break;
		case 46: newTempBombNumber = (GameObject)Instantiate (bombNumber51, tempBombNumber.transform.position, Quaternion.identity); break;
		case 47: newTempBombNumber = (GameObject)Instantiate (bombNumber52, tempBombNumber.transform.position, Quaternion.identity); break;
		case 48: newTempBombNumber = (GameObject)Instantiate (bombNumber53, tempBombNumber.transform.position, Quaternion.identity); break;
		case 49: newTempBombNumber = (GameObject)Instantiate (bombNumber54, tempBombNumber.transform.position, Quaternion.identity); break;

		case 50: newTempBombNumber = (GameObject)Instantiate (bombNumber55, tempBombNumber.transform.position, Quaternion.identity); break;
		case 51: newTempBombNumber = (GameObject)Instantiate (bombNumber56, tempBombNumber.transform.position, Quaternion.identity); break;
		case 52: newTempBombNumber = (GameObject)Instantiate (bombNumber57, tempBombNumber.transform.position, Quaternion.identity); break;
		case 53: newTempBombNumber = (GameObject)Instantiate (bombNumber58, tempBombNumber.transform.position, Quaternion.identity); break;
		case 54: newTempBombNumber = (GameObject)Instantiate (bombNumber59, tempBombNumber.transform.position, Quaternion.identity); break;
		case 55: newTempBombNumber = (GameObject)Instantiate (bombNumber60, tempBombNumber.transform.position, Quaternion.identity); break;
		case 56: newTempBombNumber = (GameObject)Instantiate (bombNumber61, tempBombNumber.transform.position, Quaternion.identity); break;
		case 57: newTempBombNumber = (GameObject)Instantiate (bombNumber62, tempBombNumber.transform.position, Quaternion.identity); break;
		case 58: newTempBombNumber = (GameObject)Instantiate (bombNumber63, tempBombNumber.transform.position, Quaternion.identity); break;
		case 59: newTempBombNumber = (GameObject)Instantiate (bombNumber64, tempBombNumber.transform.position, Quaternion.identity); break;

		case 60: newTempBombNumber = (GameObject)Instantiate (bombNumber65, tempBombNumber.transform.position, Quaternion.identity); break;
		case 61: newTempBombNumber = (GameObject)Instantiate (bombNumber66, tempBombNumber.transform.position, Quaternion.identity); break;
		case 62: newTempBombNumber = (GameObject)Instantiate (bombNumber67, tempBombNumber.transform.position, Quaternion.identity); break;
		case 63: newTempBombNumber = (GameObject)Instantiate (bombNumber68, tempBombNumber.transform.position, Quaternion.identity); break;
		case 64: newTempBombNumber = (GameObject)Instantiate (bombNumber69, tempBombNumber.transform.position, Quaternion.identity); break;
		case 65: newTempBombNumber = (GameObject)Instantiate (bombNumber70, tempBombNumber.transform.position, Quaternion.identity); break;
		case 66: newTempBombNumber = (GameObject)Instantiate (bombNumber71, tempBombNumber.transform.position, Quaternion.identity); break;
		case 67: newTempBombNumber = (GameObject)Instantiate (bombNumber72, tempBombNumber.transform.position, Quaternion.identity); break;
		case 68: newTempBombNumber = (GameObject)Instantiate (bombNumber73, tempBombNumber.transform.position, Quaternion.identity); break;
		case 69: newTempBombNumber = (GameObject)Instantiate (bombNumber74, tempBombNumber.transform.position, Quaternion.identity); break;

		case 70: newTempBombNumber = (GameObject)Instantiate (bombNumber75, tempBombNumber.transform.position, Quaternion.identity); break;
		case 71: newTempBombNumber = (GameObject)Instantiate (bombNumber76, tempBombNumber.transform.position, Quaternion.identity); break;
		case 72: newTempBombNumber = (GameObject)Instantiate (bombNumber77, tempBombNumber.transform.position, Quaternion.identity); break;
		case 73: newTempBombNumber = (GameObject)Instantiate (bombNumber78, tempBombNumber.transform.position, Quaternion.identity); break;
		case 74: newTempBombNumber = (GameObject)Instantiate (bombNumber79, tempBombNumber.transform.position, Quaternion.identity); break;
		case 75: newTempBombNumber = (GameObject)Instantiate (bombNumber80, tempBombNumber.transform.position, Quaternion.identity); break;
		case 76: newTempBombNumber = (GameObject)Instantiate (bombNumber81, tempBombNumber.transform.position, Quaternion.identity); break;
		case 77: newTempBombNumber = (GameObject)Instantiate (bombNumber82, tempBombNumber.transform.position, Quaternion.identity); break;
		case 78: newTempBombNumber = (GameObject)Instantiate (bombNumber83, tempBombNumber.transform.position, Quaternion.identity); break;
		case 79: newTempBombNumber = (GameObject)Instantiate (bombNumber84, tempBombNumber.transform.position, Quaternion.identity); break;
		
		case 80: newTempBombNumber = (GameObject)Instantiate (bombNumber85, tempBombNumber.transform.position, Quaternion.identity); break;
		case 81: newTempBombNumber = (GameObject)Instantiate (bombNumber86, tempBombNumber.transform.position, Quaternion.identity); break;
		case 82: newTempBombNumber = (GameObject)Instantiate (bombNumber87, tempBombNumber.transform.position, Quaternion.identity); break;
		case 83: newTempBombNumber = (GameObject)Instantiate (bombNumber88, tempBombNumber.transform.position, Quaternion.identity); break;
		case 84: newTempBombNumber = (GameObject)Instantiate (bombNumber89, tempBombNumber.transform.position, Quaternion.identity); break;
		case 85: newTempBombNumber = (GameObject)Instantiate (bombNumber90, tempBombNumber.transform.position, Quaternion.identity); break;
		case 86: newTempBombNumber = (GameObject)Instantiate (bombNumber91, tempBombNumber.transform.position, Quaternion.identity); break;
		case 87: newTempBombNumber = (GameObject)Instantiate (bombNumber92, tempBombNumber.transform.position, Quaternion.identity); break;
		case 88: newTempBombNumber = (GameObject)Instantiate (bombNumber93, tempBombNumber.transform.position, Quaternion.identity); break;
		case 89: newTempBombNumber = (GameObject)Instantiate (bombNumber94, tempBombNumber.transform.position, Quaternion.identity); break;

		case 90: newTempBombNumber = (GameObject)Instantiate (bombNumber95, tempBombNumber.transform.position, Quaternion.identity); break;
		case 91: newTempBombNumber = (GameObject)Instantiate (bombNumber96, tempBombNumber.transform.position, Quaternion.identity); break;
		case 92: newTempBombNumber = (GameObject)Instantiate (bombNumber97, tempBombNumber.transform.position, Quaternion.identity); break;
		case 93: newTempBombNumber = (GameObject)Instantiate (bombNumber98, tempBombNumber.transform.position, Quaternion.identity); break;
		case 94: newTempBombNumber = (GameObject)Instantiate (bombNumber99, tempBombNumber.transform.position, Quaternion.identity); break;
		case 95: newTempBombNumber = (GameObject)Instantiate (bombNumber100, tempBombNumber.transform.position, Quaternion.identity); break;
		case 96: newTempBombNumber = (GameObject)Instantiate (bombNumber101, tempBombNumber.transform.position, Quaternion.identity); break;
		case 97: newTempBombNumber = (GameObject)Instantiate (bombNumber102, tempBombNumber.transform.position, Quaternion.identity); break;
		case 98: newTempBombNumber = (GameObject)Instantiate (bombNumber103, tempBombNumber.transform.position, Quaternion.identity); break;
		case 99: newTempBombNumber = (GameObject)Instantiate (bombNumber104, tempBombNumber.transform.position, Quaternion.identity); break;
		case 100: newTempBombNumber = (GameObject)Instantiate (bombNumber105,tempBombNumber.transform.position, Quaternion.identity); break;
		}
		tempBombNumber.GetComponent<BombNumberHandler> ().InstantiatePlusFive ();
		Destroy (tempBombNumber);
		newTempBombNumber.transform.parent = bomb.transform;
	}

	public void DecreaseBombCounterByOne () {
		--bombCount;
		if (bombCount == 0) {
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (false);
			GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().SetTouchOn (false);
			
			if (GameObject.Find ("Jewel Swap Button(Clone)") != null) 
				GameObject.Find ("Jewel Swap Button(Clone)").GetComponent<JewelSwapHandler> ().SetTouchOn (false);
			if (GameObject.Find ("Row Destruction Button(Clone)") != null) 
				GameObject.Find ("Row Destruction Button(Clone)").GetComponent<RowDestructionButtonHandler> ().SetTouchOn (false);
			if (GameObject.Find ("Bomb Power Button(Clone)") != null)
				GameObject.Find ("Bomb Power Button(Clone)").GetComponent<PowerBombButtonHandler> ().SetTouchOn (false);
			if (GameObject.Find ("Multi Star Power Button(Clone)") != null)
				GameObject.Find ("Multi Star Power Button(Clone)").GetComponent<MultiStarPowerHandler> ().SetTouchOn (false);
			if (GameObject.Find ("Single Star Power Button(Clone)") != null)
				GameObject.Find ("Single Star Power Button(Clone)").GetComponent<SingleStarPowerButtonHandler> ().SetTouchOn (false);
		}
		string bombNumber = bombCount.ToString ();
		if (bombCounterFirstDigit != null)
			Destroy (bombCounterFirstDigit);
		if (bombCounterSecondDigit != null) {
			Destroy (bombCounterSecondDigit);
			if (bombCount / 10 > 0) {
				bombCounterSecondDigit = (GameObject)Instantiate (GetBombCountNumber (bombNumber[1]), bombCounterPositionTwo, Quaternion.identity);
			}
		}
		bombCounterFirstDigit = (GameObject)Instantiate (GetBombCountNumber (bombNumber[0]), bombCounterPosition, Quaternion.identity);
//		instantiatedBomb = (GameObject)Instantiate (GetBlackBombMinusOne (currentBombCounter), bombCounterPosition, Quaternion.identity);
//		Destroy (currentBombCounter);
//		currentBombCounter = instantiatedBomb;
//		bombCount--;
	}

	GameObject GetBombCountNumber (char number) {
		switch (number) {
		case '0': return bombCountNumber0;
		case '1': return bombCountNumber1;
		case '2': return bombCountNumber2;
		case '3': return bombCountNumber3;
		case '4': return bombCountNumber4;
		case '5': return bombCountNumber5;
		case '6': return bombCountNumber6;
		case '7': return bombCountNumber7;
		case '8': return bombCountNumber8;
		case '9': return bombCountNumber9;
		}
		return null;
	}

	public GameObject GetRandomBomb () {
		int randomBombNumber = Random.Range (0, bombList.Count);
		int randomBombCountNumber = 0;
		foreach (GameObject a in bombList) {
			if (randomBombCountNumber == randomBombNumber)
				return a;
			randomBombCountNumber++; 
		}
		return null;
	}

	public void IncreaseBombCounterByOne () {
		if (timeBombCount == 0) {
			currentBombCounter = (GameObject) Instantiate (bombCountNumber1, bombCounterPosition, Quaternion.identity);
		}
		else {
//			tempBomb = (GameObject) Instantiate (GetBlackBombPlusOne (currentBombCounter), bombCounterPosition, Quaternion.identity);
			Destroy (currentBombCounter);
			currentBombCounter = tempBomb;
		}
		timeBombCount++;
	}

	GameObject GetNextBombNumber (GameObject currentBombNumber) {
		if (currentBombNumber == null)
			return null;
		switch (currentBombNumber.name) {
		case "Bomb Number 105(Clone)": return bombNumber104;
		case "Bomb Number 104(Clone)": return bombNumber103;
		case "Bomb Number 103(Clone)": return bombNumber102;
		case "Bomb Number 102(Clone)": return bombNumber101;
		case "Bomb Number 101(Clone)": return bombNumber100;

		case "Bomb Number 100(Clone)":  return bombNumber99;
		case "Bomb Number 99(Clone)":  return bombNumber98;
		case "Bomb Number 98(Clone)":  return bombNumber97;
		case "Bomb Number 97(Clone)":  return bombNumber96;
		case "Bomb Number 96(Clone)":  return bombNumber95;
		case "Bomb Number 95(Clone)":  return bombNumber94;
		case "Bomb Number 94(Clone)":  return bombNumber93;
		case "Bomb Number 93(Clone)":  return bombNumber92;
		case "Bomb Number 92(Clone)":  return bombNumber91;
		case "Bomb Number 91(Clone)":  return bombNumber90;
			
		case "Bomb Number 90(Clone)":  return bombNumber89;
		case "Bomb Number 89(Clone)":  return bombNumber88;
		case "Bomb Number 88(Clone)":  return bombNumber87;
		case "Bomb Number 87(Clone)":  return bombNumber86;
		case "Bomb Number 86(Clone)":  return bombNumber85;
		case "Bomb Number 85(Clone)":  return bombNumber84;
		case "Bomb Number 84(Clone)":  return bombNumber83;
		case "Bomb Number 83(Clone)":  return bombNumber82;
		case "Bomb Number 82(Clone)":  return bombNumber81;
		case "Bomb Number 81(Clone)":  return bombNumber80;
			
		case "Bomb Number 80(Clone)":  return bombNumber79;
		case "Bomb Number 79(Clone)":  return bombNumber78;
		case "Bomb Number 78(Clone)":  return bombNumber77;
		case "Bomb Number 77(Clone)":  return bombNumber76;
		case "Bomb Number 76(Clone)":  return bombNumber75;
		case "Bomb Number 75(Clone)":  return bombNumber74;
		case "Bomb Number 74(Clone)":  return bombNumber73;
		case "Bomb Number 73(Clone)":  return bombNumber72;
		case "Bomb Number 72(Clone)":  return bombNumber71;
		case "Bomb Number 71(Clone)":  return bombNumber70;
			
		case "Bomb Number 70(Clone)":  return bombNumber69;
		case "Bomb Number 69(Clone)":  return bombNumber68;
		case "Bomb Number 68(Clone)":  return bombNumber67;
		case "Bomb Number 67(Clone)":  return bombNumber66;
		case "Bomb Number 66(Clone)":  return bombNumber65;
		case "Bomb Number 65(Clone)":  return bombNumber64;
		case "Bomb Number 64(Clone)":  return bombNumber63;
		case "Bomb Number 63(Clone)":  return bombNumber62;
		case "Bomb Number 62(Clone)":  return bombNumber61;
		case "Bomb Number 61(Clone)":  return bombNumber60;
			
		case "Bomb Number 60(Clone)":  return bombNumber59;
		case "Bomb Number 59(Clone)":  return bombNumber58;
		case "Bomb Number 58(Clone)":  return bombNumber57;
		case "Bomb Number 57(Clone)":  return bombNumber56;
		case "Bomb Number 56(Clone)":  return bombNumber55;
		case "Bomb Number 55(Clone)":  return bombNumber54;
		case "Bomb Number 54(Clone)":  return bombNumber53;
		case "Bomb Number 53(Clone)":  return bombNumber52;
		case "Bomb Number 52(Clone)":  return bombNumber51;
		case "Bomb Number 51(Clone)":  return bombNumber50;
			
		case "Bomb Number 50(Clone)":  return bombNumber49;
		case "Bomb Number 49(Clone)":  return bombNumber48;
		case "Bomb Number 48(Clone)":  return bombNumber47;
		case "Bomb Number 47(Clone)":  return bombNumber46;
		case "Bomb Number 46(Clone)":  return bombNumber45;
		case "Bomb Number 45(Clone)":  return bombNumber44;
		case "Bomb Number 44(Clone)":  return bombNumber43;
		case "Bomb Number 43(Clone)":  return bombNumber42;
		case "Bomb Number 42(Clone)":  return bombNumber41;
		case "Bomb Number 41(Clone)":  return bombNumber40;

		case "Bomb Number 40(Clone)":  return bombNumber39;
		case "Bomb Number 39(Clone)":  return bombNumber38;
		case "Bomb Number 38(Clone)":  return bombNumber37;
		case "Bomb Number 37(Clone)":  return bombNumber36;
		case "Bomb Number 36(Clone)":  return bombNumber35;
		case "Bomb Number 35(Clone)":  return bombNumber34;
		case "Bomb Number 34(Clone)":  return bombNumber33;
		case "Bomb Number 33(Clone)":  return bombNumber32;
		case "Bomb Number 32(Clone)":  return bombNumber31;
		case "Bomb Number 31(Clone)":  return bombNumber30;

		case "Bomb Number 30(Clone)":  return bombNumber29;
		case "Bomb Number 29(Clone)":  return bombNumber28;
		case "Bomb Number 28(Clone)":  return bombNumber27;
		case "Bomb Number 27(Clone)":  return bombNumber26;
		case "Bomb Number 26(Clone)":  return bombNumber25;
		case "Bomb Number 25(Clone)":  return bombNumber24;
		case "Bomb Number 24(Clone)":  return bombNumber23;
		case "Bomb Number 23(Clone)":  return bombNumber22;
		case "Bomb Number 22(Clone)":  return bombNumber21;
		case "Bomb Number 21(Clone)":  return bombNumber20;

		case "Bomb Number 20(Clone)":  return bombNumber19;
		case "Bomb Number 19(Clone)":  return bombNumber18;
		case "Bomb Number 18(Clone)":  return bombNumber17;
		case "Bomb Number 17(Clone)":  return bombNumber16;
		case "Bomb Number 16(Clone)":  return bombNumber15;
		case "Bomb Number 15(Clone)":  return bombNumber14;
		case "Bomb Number 14(Clone)":  return bombNumber13;
		case "Bomb Number 13(Clone)":  return bombNumber12;
		case "Bomb Number 12(Clone)":  return bombNumber11;
		case "Bomb Number 11(Clone)":  return bombNumber10;

		case "Bomb Number 10(Clone)":  return bombNumber9;
		case "Bomb Number 9(Clone)":  return bombNumber8;
		case "Bomb Number 8(Clone)":  return bombNumber7;
		case "Bomb Number 7(Clone)":  return bombNumber6;
		case "Bomb Number 6(Clone)":  return bombNumber5;
		case "Bomb Number 5(Clone)":  return bombNumber4;
		case "Bomb Number 4(Clone)": 
			if (GameObject.Find ("Time Bomb Identification") != null)
				soundHandler.PlayAlarm (); 
			return bombNumber3;
		case "Bomb Number 3(Clone)": 
			if (GameObject.Find ("Time Bomb Identification") != null) 
				soundHandler.PlayAlarm (); 
			return bombNumber2;
		case "Bomb Number 2(Clone)": 
			if (GameObject.Find ("Time Bomb Identification") != null) 
				soundHandler.PlayAlarm (); 
			return bombNumber1;
		case "Bomb Number 1(Clone)": 
			if (GameObject.Find ("Time Bomb Identification") != null) {
				soundHandler.PlayAlarm (); 
			}
			turnOffTouch = true;
			return bombNumber0;
		}
		return null;
	}


	public bool BombListContains (GameObject bomb) {
		return bombList.Contains (bomb);
	}
	
	public void RemoveFromBombList (GameObject bomb) {
		bombList.Remove (bomb);
	}
	
	private void TurnOffTouchHandler () {
		touchHandler.SetGameStarted (false);  
		if (GameObject.Find ("Power Up Button(Clone)") != null)
			GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().SetTouchOn (false);

		if (GameObject.Find ("Jewel Swap Button(Clone)") != null)
			GameObject.Find ("Jewel Swap Button(Clone)").GetComponent<JewelSwapHandler> ().SetTouchOn (false);

		if (GameObject.Find ("Row Destruction Button(Clone)") != null)
			GameObject.Find ("Row Destruction Button(Clone)").GetComponent<RowDestructionButtonHandler> ().SetTouchOn (false);

		if (GameObject.Find ("Bomb Power Button(Clone)") != null)
			GameObject.Find ("Bomb Power Button(Clone)").GetComponent<PowerBombButtonHandler> ().SetTouchOn (false);

		if (GameObject.Find ("Multi Star Power Button(Clone)") != null)
			GameObject.Find ("Multi Star Power Button(Clone)").GetComponent<MultiStarPowerHandler> ().SetTouchOn (false);

		if (GameObject.Find ("Single Star Power Button(Clone)") != null)
			GameObject.Find ("Single Star Power Button(Clone)").GetComponent<SingleStarPowerButtonHandler> ().SetTouchOn (false);
	}
	
	public int GetCurrentBombCount () {
		return bombCount;
	}

	public bool ZeroBombsStillExist () {
		foreach (GameObject a in bombList) {
			if (a != null) {
				if (a.transform.childCount > 0) {
					for (int i = 0; i < a.transform.childCount; i++) {
						if (a.transform.GetChild(i) != null && a.transform.GetChild (i).tag == "Bomb Number") {
							int bombNumber = a.transform.GetChild (i).GetComponent<BombNumberHandler> ().GetBombNumber ();
							if (a != null && GameObject.Find ("Time Bomb Identifiaction") == null && bombNumber == 0) {
								explodeBombs = true;
								break;
							} else if (a != null && GameObject.Find ("Time Bomb Identifiaction") != null && bombNumber == 0) {
								GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().SetGameStarted (false);
								GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetBombsExploding (true);
								explodeBombs = true;
								return true;
							}
						}
					}
				}
//				else if (a.transform.GetChild (1) != null && a.transform.GetChild (1).tag == "Bomb Number") {
//					int bombNumber = a.transform.GetChild (1).GetComponent<BombNumberHandler> ().GetBombNumber ();
//					if (a != null && GameObject.Find ("Time Bomb Identification") == null && bombNumber == 0) {
//						explodeBombs = true;
//						return true;
//					} else if (a != null && GameObject.Find ("Time Bomb Identification") != null && bombNumber == 0) {
////						GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted (false);
//						GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().SetGameStarted (false);
//						GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetBombsExploding (true);
//						explodeBombs = true;
//						return true;
//					}
//				}
			}
		}
		return false;
	}
	
	public void ExplodeBomb () {
//		if (GameObject.Find ("Time Bomb Identification") != null) 
//			GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().StartCountdown (false);
		bombExploded = true;
		TurnOffTouchHandler ();
		while (bombList.Count > 0) {
			soundHandler.PlayExplosion ();
			GameObject a = bombList[Random.Range (0, bombList.Count)];
			while (a == null) 
				a = bombList[Random.Range (0, bombList.Count)];
			if (a != null && firstBombExplosion) {
				for (int i = 0; i < a.transform.childCount; i++) {
					if (a.transform.GetChild (i).GetComponent<BombNumberHandler> () != null && a.transform.GetChild (i).GetComponent<BombNumberHandler> ().GetBombNumber () == 0) {
						Instantiate (bombExplosion, new Vector3 (a.transform.position.x, a.transform.position.y, -20), Quaternion.Euler (180, 0, 0));
						DestroySurroundingJewels (a);
						bombList.Remove (a);
						Destroy (a);
						timeStamp = Time.time;
						firstBombExplosion = false;
						if (bombList.Count == 0) {
							explodeBombs = false;
							dimShade = true;
							timeStamp2 = Time.time;
						}
						return;
					}
				}
			} else if (!firstBombExplosion && a != null) {
				Instantiate (bombExplosion, new Vector3 (a.transform.position.x, a.transform.position.y, -20), Quaternion.Euler (180, 0, 0)); 
				DestroySurroundingJewels (a);
				bombList.Remove (a);
				Destroy (a);
				timeStamp = Time.time;
				if (bombList.Count == 0) {
					explodeBombs = false;
					dimShade = true;
					timeStamp2 = Time.time;
				}
				return;
			}
		}
	}

	public void ExplodeTimeBombs () {
		soundController.SetGameEnded (true);
		timeBombExploded = true;
		TurnOffTouchHandler ();
		while (bombList.Count > 0) {
			GameObject a = bombList[Random.Range (0, bombList.Count)];
			while (a == null) 
				a = bombList[Random.Range (0, bombList.Count)];
			if (a != null && firstBombExplosion /*&& (a.name == "Blue Bomb 0(Clone)" || a.name == "Green Bomb 0(Clone)" || a.name == "Red Bomb 0(Clone)" || a.name == "White Bomb 0(Clone)" || a.name == "Purple Bomb 0(Clone)" || a.name == "Orange Bomb 0(Clone)")*/) {
				for (int i = 0; i < a.transform.childCount; i++) {
					if (a.transform.GetChild (i).GetComponent<BombNumberHandler> ().GetBombNumber () == 0) {
						Instantiate (bombExplosion, new Vector3 (a.transform.position.x, a.transform.position.y, -20), Quaternion.Euler (180, 0, 0));
						SendSurroundingJewelsToDeleteClass (a);
						bombList.Remove (a);
						Destroy (a);
						timeStamp = Time.time;
						firstBombExplosion = false;
		//				if (bombList.Count == 0) {
		//					explodeBombs = false;
		//					dimShade = true;
		//					timeStamp2 = Time.time;
		//				}
						return;
					}
				}
			} else if (!firstBombExplosion && a != null) {
				Instantiate (bombExplosion, new Vector3 (a.transform.position.x, a.transform.position.y, -20), Quaternion.Euler (180, 0, 0));
				SendSurroundingJewelsToDeleteClass (a);
				bombList.Remove (a);
				Destroy (a);
				timeStamp = Time.time;
//				if (bombList.Count == 0) {
//					explodeBombs = false;
//					dimShade = true;
//					timeStamp2 = Time.time;
//				}
				return;
			}
		}
	}

	public void LaunchArtillery () {
		timeStamp = Time.time;
		cooldown = .5f;
		launchArtillery = true;
	}

	void SendSurroundingJewelsToDeleteClass (GameObject bomb) {
		HashSet<GameObject> deleteList = new HashSet<GameObject> ();
		jewelMovement = bomb.GetComponent<RockLevelJewelMovement> ();
		int row = jewelMovement.GetRow ();
		int col = jewelMovement.GetCol ();
		GameObject tempJewel = null;
		deleteList.Add (bomb);
		if (row - 1 >= 0) { 
			tempJewel = instantiator.GetJewelGridGameObject (row - 1, col);
			if (!IsBomb (tempJewel)) {
				deleteList.Add (tempJewel);
			}
			if (col + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (row - 1, col + 1);
				if (!IsBomb (tempJewel)) {
					deleteList.Add (tempJewel);
				}
			}
			if (col - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (row - 1, col - 1);
				if (!IsBomb (tempJewel)) {
					deleteList.Add (tempJewel);
				}
			}
		}
		if (row + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (row + 1, col);
			if (!IsBomb (tempJewel)) {
				deleteList.Add (tempJewel);
			}
			if (col + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (row + 1, col + 1);
				if (!IsBomb (tempJewel)) {
					deleteList.Add (tempJewel);
				}
			}
			if (col - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (row + 1, col - 1);
				if (!IsBomb (tempJewel)) {
					deleteList.Add (tempJewel);
				}
			}
		}
		
		if (col + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (row, col + 1);
			if (!IsBomb (tempJewel)) {
				deleteList.Add (tempJewel);
			}
		}
		if (col - 1 >= 0)
			tempJewel = instantiator.GetJewelGridGameObject (row, col - 1);
		if (!IsBomb (tempJewel)) {
			deleteList.Add (tempJewel);
		}
		deleteJewels.DeleteAllJewelsInList (deleteList, true);
	}

	void DestroySurroundingJewels (GameObject bomb) {
		jewelMovement = bomb.GetComponent<RockLevelJewelMovement> ();
		int row = jewelMovement.GetRow ();
		int col = jewelMovement.GetCol ();
		GameObject tempJewel = null;
		if (row - 1 >= 0) { 
			tempJewel = instantiator.GetJewelGridGameObject (row - 1, col);
			if (IsBomb (tempJewel)) 
				bombList.Remove (tempJewel);
			InstantiateExplosions (tempJewel);
			Destroy (tempJewel);
			if (col + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (row - 1, col + 1);
				if (IsBomb (tempJewel)) 
					bombList.Remove (tempJewel);
				InstantiateExplosions (tempJewel);
				Destroy (tempJewel);
			}
			if (col - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (row - 1, col - 1);
				if (IsBomb (tempJewel)) 
					bombList.Remove (tempJewel);
				InstantiateExplosions (tempJewel);
				Destroy (tempJewel);
			}
		}
		if (row + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (row + 1, col);
			if (IsBomb (tempJewel)) 
				bombList.Remove (tempJewel);
			InstantiateExplosions (tempJewel);
			Destroy (tempJewel);
			if (col + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (row + 1, col + 1);
				if (IsBomb (tempJewel)) 
					bombList.Remove (tempJewel);
				InstantiateExplosions (tempJewel);
				Destroy (tempJewel);
			}
			if (col - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (row + 1, col - 1);
				if (IsBomb (tempJewel)) 
					bombList.Remove (tempJewel);
				InstantiateExplosions (tempJewel);
				Destroy (tempJewel);
			}
		}
		
		if (col + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (row, col + 1);
			if (IsBomb (tempJewel)) 
				bombList.Remove (tempJewel);
			InstantiateExplosions (tempJewel);
			Destroy (tempJewel);
		}
		if (col - 1 >= 0)
			tempJewel = instantiator.GetJewelGridGameObject (row, col - 1);
		if (IsBomb (tempJewel)) 
			bombList.Remove (tempJewel);
		InstantiateExplosions (tempJewel);
		Destroy (tempJewel);
	}
	
	public void InstantiateExplosions (GameObject jewel) {
		if (jewel == null)
			return;
		switch (jewel.tag) {
		case "Blue Bomb":
		case "Blue Block": Instantiate (blueExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Green Bomb": 
		case "Green Block": Instantiate (greenExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Purple Bomb":
		case "Purple Block": Instantiate (purpleExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Orange Bomb":
		case "Yellow Block": Instantiate (orangeExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "White Bomb":
		case "White Block": Instantiate (whiteExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Red Bomb":
		case "Red Block": Instantiate (redExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Steel Block":
		case "Boulder": Instantiate (boulderExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		}
	}
	
	bool IsBomb (GameObject jewel) {
		if (jewel != null && (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "White Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "Purple Bomb"))
			return true;
		if (jewel != null && jewel.tag == "Boulder" && (jewel.transform.childCount > 0)) {
			for (int i = 0; i < jewel.transform.childCount; i++) {
				if (jewel.transform.GetChild (i).tag == "Bomb Number")
					return true;
			}
		}
		return false;
	}

	bool IsBombMinusBoulder (GameObject jewel) {
		if (jewel != null && (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "White Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "Purple Bomb"))
			return true;
		return false;
	}
	
	public bool BombListHasZeros () {
		foreach (GameObject a in bombList) {
			BombNumberHandler temp = a.GetComponentInChildren<BombNumberHandler> ();
			if (temp.GetBombNumber () == 0)
				return true;
		}
		return false;
	}

	public bool TimeBombIsExploded () {
		return timeBombExploded;
	}

	public int GetTimeBombCount () {
		return timeBombCount;
	}
		
	public bool BombIsExploded () {
		//Debug.Log ("bombExploded" + bombExploded);
		return bombExploded;
	}
	
	public string GetBombCounterName () {
		return currentBombCounter.name;
	}

	public int GetBombCount () {
		return bombCount;
	}

	public int GetBombListCount () {
		return bombList.Count;
	}
}
