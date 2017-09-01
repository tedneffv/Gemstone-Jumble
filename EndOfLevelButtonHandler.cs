using System.Collections;
using UnityEngine;

public class EndOfLevelButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	GameObject tempPressedButton, tempRestartButton, levelFailedBanner, timeHolder, heart0, heart1, heart2, heart3, heart4, levelFailedHome, levelFailedRetry, wholeScreen, instantiatedNoLivesButtons,
		instantiatedNoLivesText, instantiatedNoLivesPhrase, instantiatedLivesButton, instantiatedCoinsButton, instantiatedNoLivesHomeButton, instantiatedTimeLeftPhrase;
	public GameObject pressedHomeButton, pressedRestartButton, pressedNextButton, heartExplosion, heartOutline, pressedFailedHomeButton, pressedFailedRestartButton;
	public GameObject noLivesPhrase, purchaseLivesButton, purchaseCoinsButton, noLivesGoHomeButton, timeLeftTillHeartsPhrase;
	RockLevelController levelController;
	bool restartLevel, goHome, nextLevel, levelFailed, heartExploded, darkenStarted, touchOn, noMoreLives, moveLeft, noLivesSlideInstantiated, coinsInstantiated;
	TransitionShadeController transitionShadeController;
	float timeStamp, cooldown, targetX, speed, errorDistance;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		levelController = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
		cooldown = 3;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (heartExploded  || !levelFailed) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedButton != null) {
				CheckTouch3 (Input.mousePosition);
				if (tempPressedButton != null) {
					soundHandler.PlayButtonClickUp ();
					Destroy (tempPressedButton);
				}
			}
		}

		if (levelFailed && restartLevel && transitionShadeController.GetAlpha () >= 1) {
			switch (levelController.GetLevelNumber ()) {
			case 1: Application.LoadLevel ("Mountain Level 1"); break;
			case 2: Application.LoadLevel ("Mountain Level 2"); break;
			case 3: Application.LoadLevel ("Mountain Level 3"); break;
			case 4: Application.LoadLevel ("Mountain Level 4"); break;
			case 5: Application.LoadLevel ("Mountain Level 5"); break;
			case 6: Application.LoadLevel ("Mountain Level 6"); break;
			case 7: Application.LoadLevel ("Mountain Level 7"); break;
			case 8: Application.LoadLevel ("Mountain Level 8"); break;
			case 9: Application.LoadLevel ("Mountain Level 9"); break;
			case 10: Application.LoadLevel ("Mountain Level 10"); break;
			case 11: Application.LoadLevel ("Mountain Level 11"); break;
			case 12: Application.LoadLevel ("Mountain Level 12"); break;
			case 13: Application.LoadLevel ("Mountain Level 13"); break;
			case 14: Application.LoadLevel ("Mountain Level 14"); break;
			case 15: Application.LoadLevel ("Mountain Level 15"); break;
			case 16: Application.LoadLevel ("Mountain Level 16"); break;
			case 17: Application.LoadLevel ("Mountain Level 17"); break;
			case 18: Application.LoadLevel ("Mountain Level 18"); break;
			case 19: Application.LoadLevel ("Mountain Level 19"); break;
			case 20: Application.LoadLevel ("Mountain Level 20"); break;
			case 21: Application.LoadLevel ("Mountain Level 21"); break;
			case 22: Application.LoadLevel ("Mountain Level 22"); break;
			case 23: Application.LoadLevel ("Mountain Level 23"); break;
			case 24: Application.LoadLevel ("Mountain Level 24"); break;
			case 25: Application.LoadLevel ("Mountain Level 25"); break;
			case 26: Application.LoadLevel ("Mountain Level 26"); break;
			case 27: Application.LoadLevel ("Mountain Level 27"); break;
			case 28: Application.LoadLevel ("Mountain Level 28"); break;
			case 29: Application.LoadLevel ("Mountain Level 29"); break;
			case 30: Application.LoadLevel ("Mountain Level 30"); break;

			case 31: Application.LoadLevel ("City Level 1"); break;
			case 32: Application.LoadLevel ("City Level 2"); break;
			case 33: Application.LoadLevel ("City Level 3"); break;
			case 34: Application.LoadLevel ("City Level 4"); break;
			case 35: Application.LoadLevel ("City Level 5"); break;
			case 36: Application.LoadLevel ("City Level 6"); break;
			case 37: Application.LoadLevel ("City Level 7"); break;
			case 38: Application.LoadLevel ("City Level 8"); break;
			case 39: Application.LoadLevel ("City Level 9"); break;
			case 40: Application.LoadLevel ("City Level 10"); break;
			case 41: Application.LoadLevel ("City Level 11"); break;
			case 42: Application.LoadLevel ("City Level 12"); break;
			case 43: Application.LoadLevel ("City Level 13"); break;
			case 44: Application.LoadLevel ("City Level 14"); break;
			case 45: Application.LoadLevel ("City Level 15"); break;
			case 46: Application.LoadLevel ("City Level 16"); break;
			case 47: Application.LoadLevel ("City Level 17"); break;
			case 48: Application.LoadLevel ("City Level 18"); break;
			case 49: Application.LoadLevel ("City Level 19"); break;
			case 50: Application.LoadLevel ("City Level 20"); break;
			case 51: Application.LoadLevel ("City Level 21"); break;
			case 52: Application.LoadLevel ("City Level 22"); break;
			case 53: Application.LoadLevel ("City Level 23"); break;
			case 54: Application.LoadLevel ("City Level 24"); break;
			case 55: Application.LoadLevel ("City Level 25"); break;
			case 56: Application.LoadLevel ("City Level 26"); break;
			case 57: Application.LoadLevel ("City Level 27"); break;
			case 58: Application.LoadLevel ("City Level 28"); break;
			case 59: Application.LoadLevel ("City Level 29"); break;
			case 60: Application.LoadLevel ("City Level 30"); break;

			case 61: Application.LoadLevel ("Cabin Level 1"); break;
			case 62: Application.LoadLevel ("Cabin Level 2"); break;
			case 63: Application.LoadLevel ("Cabin Level 3"); break;
			case 64: Application.LoadLevel ("Cabin Level 4"); break;
			case 65: Application.LoadLevel ("Cabin Level 5"); break;
			case 66: Application.LoadLevel ("Cabin Level 6"); break;
			case 67: Application.LoadLevel ("Cabin Level 7"); break;
			case 68: Application.LoadLevel ("Cabin Level 8"); break;
			case 69: Application.LoadLevel ("Cabin Level 9"); break;
			case 70: Application.LoadLevel ("Cabin Level 10"); break;
			case 71: Application.LoadLevel ("Cabin Level 11"); break;
			case 72: Application.LoadLevel ("Cabin Level 12"); break;
			case 73: Application.LoadLevel ("Cabin Level 13"); break;
			case 74: Application.LoadLevel ("Cabin Level 14"); break;
			case 75: Application.LoadLevel ("Cabin Level 15"); break;
			case 76: Application.LoadLevel ("Cabin Level 16"); break;
			case 77: Application.LoadLevel ("Cabin Level 17"); break;
			case 78: Application.LoadLevel ("Cabin Level 18"); break;
			case 79: Application.LoadLevel ("Cabin Level 19"); break;
			case 80: Application.LoadLevel ("Cabin Level 20"); break;
			case 81: Application.LoadLevel ("Cabin Level 21"); break;
			case 82: Application.LoadLevel ("Cabin Level 22"); break;
			case 83: Application.LoadLevel ("Cabin Level 23"); break;
			case 84: Application.LoadLevel ("Cabin Level 24"); break;
			case 85: Application.LoadLevel ("Cabin Level 25"); break;
			case 86: Application.LoadLevel ("Cabin Level 26"); break;
			case 87: Application.LoadLevel ("Cabin Level 27"); break;
			case 88: Application.LoadLevel ("Cabin Level 28"); break;
			case 89: Application.LoadLevel ("Cabin Level 29"); break;
			case 90: Application.LoadLevel ("Cabin Level 30"); break;

			case 91: Application.LoadLevel ("Launchpad Level 1"); break;
			case 92: Application.LoadLevel ("Launchpad Level 2"); break;
			case 93: Application.LoadLevel ("Launchpad Level 3"); break;
			case 94: Application.LoadLevel ("Launchpad Level 4"); break;
			case 95: Application.LoadLevel ("Launchpad Level 5"); break;
			case 96: Application.LoadLevel ("Launchpad Level 6"); break;
			case 97: Application.LoadLevel ("Launchpad Level 7"); break;
			case 98: Application.LoadLevel ("Launchpad Level 8"); break;
			case 99: Application.LoadLevel ("Launchpad Level 9"); break;
			case 100: Application.LoadLevel ("Launchpad Level 10"); break;
			case 101: Application.LoadLevel ("Launchpad Level 11"); break;
			case 102: Application.LoadLevel ("Launchpad Level 12"); break;
			case 103: Application.LoadLevel ("Launchpad Level 13"); break;
			case 104: Application.LoadLevel ("Launchpad Level 14"); break;
			case 105: Application.LoadLevel ("Launchpad Level 15"); break;
			case 106: Application.LoadLevel ("Launchpad Level 16"); break;
			case 107: Application.LoadLevel ("Launchpad Level 17"); break;
			case 108: Application.LoadLevel ("Launchpad Level 18"); break;
			case 109: Application.LoadLevel ("Launchpad Level 19"); break;
			case 110: Application.LoadLevel ("Launchpad Level 20"); break;
			case 111: Application.LoadLevel ("Launchpad Level 21"); break;
			case 112: Application.LoadLevel ("Launchpad Level 22"); break;
			case 113: Application.LoadLevel ("Launchpad Level 23"); break;
			case 114: Application.LoadLevel ("Launchpad Level 24"); break;
			case 115: Application.LoadLevel ("Launchpad Level 25"); break;
			case 116: Application.LoadLevel ("Launchpad Level 26"); break;
			case 117: Application.LoadLevel ("Launchpad Level 27"); break;
			case 118: Application.LoadLevel ("Launchpad Level 28"); break;
			case 119: Application.LoadLevel ("Launchpad Level 29"); break;
			case 120: Application.LoadLevel ("Launchpad Level 30"); break;
			}
		}
		else if (!levelFailed && restartLevel && transitionShadeController.GetAlpha () >= 1) {
			switch (levelController.GetLevelNumber ()) {
			case 1: Application.LoadLevel ("Mountain Level 1"); break;
			case 2: Application.LoadLevel ("Mountain Level 2"); break;
			case 3: Application.LoadLevel ("Mountain Level 3"); break;
			case 4: Application.LoadLevel ("Mountain Level 4"); break;
			case 5: Application.LoadLevel ("Mountain Level 5"); break;
			case 6: Application.LoadLevel ("Mountain Level 6"); break;
			case 7: Application.LoadLevel ("Mountain Level 7"); break;
			case 8: Application.LoadLevel ("Mountain Level 8"); break;
			case 9: Application.LoadLevel ("Mountain Level 9"); break;
			case 10: Application.LoadLevel ("Mountain Level 10"); break;
			case 11: Application.LoadLevel ("Mountain Level 11"); break;
			case 12: Application.LoadLevel ("Mountain Level 12"); break;
			case 13: Application.LoadLevel ("Mountain Level 13"); break;
			case 14: Application.LoadLevel ("Mountain Level 14"); break;
			case 15: Application.LoadLevel ("Mountain Level 15"); break;
			case 16: Application.LoadLevel ("Mountain Level 16"); break;
			case 17: Application.LoadLevel ("Mountain Level 17"); break;
			case 18: Application.LoadLevel ("Mountain Level 18"); break;
			case 19: Application.LoadLevel ("Mountain Level 19"); break;
			case 20: Application.LoadLevel ("Mountain Level 20"); break;
			case 21: Application.LoadLevel ("Mountain Level 21"); break;
			case 22: Application.LoadLevel ("Mountain Level 22"); break;
			case 23: Application.LoadLevel ("Mountain Level 23"); break;
			case 24: Application.LoadLevel ("Mountain Level 24"); break;
			case 25: Application.LoadLevel ("Mountain Level 25"); break;
			case 26: Application.LoadLevel ("Mountain Level 26"); break;
			case 27: Application.LoadLevel ("Mountain Level 27"); break;
			case 28: Application.LoadLevel ("Mountain Level 28"); break;
			case 29: Application.LoadLevel ("Mountain Level 29"); break;
			case 30: Application.LoadLevel ("Mountain Level 30"); break;

			case 31: Application.LoadLevel ("City Level 1"); break;
			case 32: Application.LoadLevel ("City Level 2"); break;
			case 33: Application.LoadLevel ("City Level 3"); break;
			case 34: Application.LoadLevel ("City Level 4"); break;
			case 35: Application.LoadLevel ("City Level 5"); break;
			case 36: Application.LoadLevel ("City Level 6"); break;
			case 37: Application.LoadLevel ("City Level 7"); break;
			case 38: Application.LoadLevel ("City Level 8"); break;
			case 39: Application.LoadLevel ("City Level 9"); break;
			case 40: Application.LoadLevel ("City Level 10"); break;
			case 41: Application.LoadLevel ("City Level 11"); break;
			case 42: Application.LoadLevel ("City Level 12"); break;
			case 43: Application.LoadLevel ("City Level 13"); break;
			case 44: Application.LoadLevel ("City Level 14"); break;
			case 45: Application.LoadLevel ("City Level 15"); break;
			case 46: Application.LoadLevel ("City Level 16"); break;
			case 47: Application.LoadLevel ("City Level 17"); break;
			case 48: Application.LoadLevel ("City Level 18"); break;
			case 49: Application.LoadLevel ("City Level 19"); break;
			case 50: Application.LoadLevel ("City Level 20"); break;
			case 51: Application.LoadLevel ("City Level 21"); break;
			case 52: Application.LoadLevel ("City Level 22"); break;
			case 53: Application.LoadLevel ("City Level 23"); break;
			case 54: Application.LoadLevel ("City Level 24"); break;
			case 55: Application.LoadLevel ("City Level 25"); break;
			case 56: Application.LoadLevel ("City Level 26"); break;
			case 57: Application.LoadLevel ("City Level 27"); break;
			case 58: Application.LoadLevel ("City Level 28"); break;
			case 59: Application.LoadLevel ("City Level 29"); break;
			case 60: Application.LoadLevel ("City Level 30"); break;

			case 61: Application.LoadLevel ("Cabin Level 1"); break;
			case 62: Application.LoadLevel ("Cabin Level 2"); break;
			case 63: Application.LoadLevel ("Cabin Level 3"); break;
			case 64: Application.LoadLevel ("Cabin Level 4"); break;
			case 65: Application.LoadLevel ("Cabin Level 5"); break;
			case 66: Application.LoadLevel ("Cabin Level 6"); break;
			case 67: Application.LoadLevel ("Cabin Level 7"); break;
			case 68: Application.LoadLevel ("Cabin Level 8"); break;
			case 69: Application.LoadLevel ("Cabin Level 9"); break;
			case 70: Application.LoadLevel ("Cabin Level 10"); break;
			case 71: Application.LoadLevel ("Cabin Level 11"); break;
			case 72: Application.LoadLevel ("Cabin Level 12"); break;
			case 73: Application.LoadLevel ("Cabin Level 13"); break;
			case 74: Application.LoadLevel ("Cabin Level 14"); break;
			case 75: Application.LoadLevel ("Cabin Level 15"); break;
			case 76: Application.LoadLevel ("Cabin Level 16"); break;
			case 77: Application.LoadLevel ("Cabin Level 17"); break;
			case 78: Application.LoadLevel ("Cabin Level 18"); break;
			case 79: Application.LoadLevel ("Cabin Level 19"); break;
			case 80: Application.LoadLevel ("Cabin Level 20"); break;
			case 81: Application.LoadLevel ("Cabin Level 21"); break;
			case 82: Application.LoadLevel ("Cabin Level 22"); break;
			case 83: Application.LoadLevel ("Cabin Level 23"); break;
			case 84: Application.LoadLevel ("Cabin Level 24"); break;
			case 85: Application.LoadLevel ("Cabin Level 25"); break;
			case 86: Application.LoadLevel ("Cabin Level 26"); break;
			case 87: Application.LoadLevel ("Cabin Level 27"); break;
			case 88: Application.LoadLevel ("Cabin Level 28"); break;
			case 89: Application.LoadLevel ("Cabin Level 29"); break;
			case 90: Application.LoadLevel ("Cabin Level 30"); break;

			case 91: Application.LoadLevel ("Launchpad Level 1"); break;
			case 92: Application.LoadLevel ("Launchpad Level 2"); break;
			case 93: Application.LoadLevel ("Launchpad Level 3"); break;
			case 94: Application.LoadLevel ("Launchpad Level 4"); break;
			case 95: Application.LoadLevel ("Launchpad Level 5"); break;
			case 96: Application.LoadLevel ("Launchpad Level 6"); break;
			case 97: Application.LoadLevel ("Launchpad Level 7"); break;
			case 98: Application.LoadLevel ("Launchpad Level 8"); break;
			case 99: Application.LoadLevel ("Launchpad Level 9"); break;
			case 100: Application.LoadLevel ("Launchpad Level 10"); break;
			case 101: Application.LoadLevel ("Launchpad Level 11"); break;
			case 102: Application.LoadLevel ("Launchpad Level 12"); break;
			case 103: Application.LoadLevel ("Launchpad Level 13"); break;
			case 104: Application.LoadLevel ("Launchpad Level 14"); break;
			case 105: Application.LoadLevel ("Launchpad Level 15"); break;
			case 106: Application.LoadLevel ("Launchpad Level 16"); break;
			case 107: Application.LoadLevel ("Launchpad Level 17"); break;
			case 108: Application.LoadLevel ("Launchpad Level 18"); break;
			case 109: Application.LoadLevel ("Launchpad Level 19"); break;
			case 110: Application.LoadLevel ("Launchpad Level 20"); break;
			case 111: Application.LoadLevel ("Launchpad Level 21"); break;
			case 112: Application.LoadLevel ("Launchpad Level 22"); break;
			case 113: Application.LoadLevel ("Launchpad Level 23"); break;
			case 114: Application.LoadLevel ("Launchpad Level 24"); break;
			case 115: Application.LoadLevel ("Launchpad Level 25"); break;
			case 116: Application.LoadLevel ("Launchpad Level 26"); break;
			case 117: Application.LoadLevel ("Launchpad Level 27"); break;
			case 118: Application.LoadLevel ("Launchpad Level 28"); break;
			case 119: Application.LoadLevel ("Launchpad Level 29"); break;
			case 120: Application.LoadLevel ("Launchpad Level 30"); break;


			}
		}
		else if (goHome && transitionShadeController.GetAlpha () >= 1) {
			Application.LoadLevel ("New Level Map");
		}
		else if (nextLevel && transitionShadeController.GetAlpha () >= 1) {
			switch (levelController.GetLevelNumber ()) {
			case 1: Application.LoadLevel ("Mountain Level 2"); break;
			case 2: Application.LoadLevel ("Mountain Level 3"); break;
			case 3: Application.LoadLevel ("Mountain Level 4"); break;
			case 4: Application.LoadLevel ("Mountain Level 5"); break;
			case 5: Application.LoadLevel ("Mountain Level 6"); break; 
			case 6: Application.LoadLevel ("Mountain Level 7"); break;
			case 7: Application.LoadLevel ("Mountain Level 8"); break;
			case 8: Application.LoadLevel ("Mountain Level 9"); break; 
			case 9: Application.LoadLevel ("Mountain Level 10"); break; 
			case 10: Application.LoadLevel ("Mountain Level 11"); break; 
			case 11: Application.LoadLevel ("Mountain Level 12"); break;
			case 12: Application.LoadLevel ("Mountain Level 13"); break; 
			case 13: Application.LoadLevel ("Mountain Level 14"); break;
			case 14: Application.LoadLevel ("Mountain Level 15"); break; 
			case 15: Application.LoadLevel ("Mountain Level 16"); break;
			case 16: Application.LoadLevel ("Mountain Level 17"); break; 
			case 17: Application.LoadLevel ("Mountain Level 18"); break;
			case 18: Application.LoadLevel ("Mountain Level 19"); break; 
			case 19: Application.LoadLevel ("Mountain Level 20"); break;
			case 20: Application.LoadLevel ("Mountain Level 21"); break; 
			case 21: Application.LoadLevel ("Mountain Level 22"); break;
			case 22: Application.LoadLevel ("Mountain Level 23"); break; 
			case 23: Application.LoadLevel ("Mountain Level 24"); break;
			case 24: Application.LoadLevel ("Mountain Level 25"); break; 
			case 25: Application.LoadLevel ("Mountain Level 26"); break;
			case 26: Application.LoadLevel ("Mountain Level 27"); break; 
			case 27: Application.LoadLevel ("Mountain Level 28"); break;
			case 28: Application.LoadLevel ("Mountain Level 29"); break; 
			case 29: Application.LoadLevel ("Mountain Level 30"); break;

			case 30: Application.LoadLevel ("City Level 1"); break;
			case 31: Application.LoadLevel ("City Level 2"); break;
			case 32: Application.LoadLevel ("City Level 3"); break;
			case 33: Application.LoadLevel ("City Level 4"); break;
			case 34: Application.LoadLevel ("City Level 5"); break;
			case 35: Application.LoadLevel ("City Level 6"); break;
			case 36: Application.LoadLevel ("City Level 7"); break;
			case 37: Application.LoadLevel ("City Level 8"); break;
			case 38: Application.LoadLevel ("City Level 9"); break;
			case 39: Application.LoadLevel ("City Level 10"); break;
			case 40: Application.LoadLevel ("City Level 11"); break;
			case 41: Application.LoadLevel ("City Level 12"); break;
			case 42: Application.LoadLevel ("City Level 13"); break;
			case 43: Application.LoadLevel ("City Level 14"); break;
			case 44: Application.LoadLevel ("City Level 15"); break;
			case 45: Application.LoadLevel ("City Level 16"); break;
			case 46: Application.LoadLevel ("City Level 17"); break;
			case 47: Application.LoadLevel ("City Level 18"); break;
			case 48: Application.LoadLevel ("City Level 19"); break;
			case 49: Application.LoadLevel ("City Level 20"); break;
			case 50: Application.LoadLevel ("City Level 21"); break;
			case 51: Application.LoadLevel ("City Level 22"); break;
			case 52: Application.LoadLevel ("City Level 23"); break;
			case 53: Application.LoadLevel ("City Level 24"); break;
			case 54: Application.LoadLevel ("City Level 25"); break;
			case 55: Application.LoadLevel ("City Level 26"); break;
			case 56: Application.LoadLevel ("City Level 27"); break;
			case 57: Application.LoadLevel ("City Level 28"); break;
			case 58: Application.LoadLevel ("City Level 29"); break;
			case 59: Application.LoadLevel ("City Level 30"); break;

			case 60: Application.LoadLevel ("Cabin Level 1"); break;
			case 61: Application.LoadLevel ("Cabin Level 2"); break;
			case 62: Application.LoadLevel ("Cabin Level 3"); break;
			case 63: Application.LoadLevel ("Cabin Level 4"); break;
			case 64: Application.LoadLevel ("Cabin Level 5"); break;
			case 65: Application.LoadLevel ("Cabin Level 6"); break;
			case 66: Application.LoadLevel ("Cabin Level 7"); break;
			case 67: Application.LoadLevel ("Cabin Level 8"); break;
			case 68: Application.LoadLevel ("Cabin Level 9"); break;
			case 69: Application.LoadLevel ("Cabin Level 10"); break;
			case 70: Application.LoadLevel ("Cabin Level 11"); break;
			case 71: Application.LoadLevel ("Cabin Level 12"); break;
			case 72: Application.LoadLevel ("Cabin Level 13"); break;
			case 73: Application.LoadLevel ("Cabin Level 14"); break;
			case 74: Application.LoadLevel ("Cabin Level 15"); break;
			case 75: Application.LoadLevel ("Cabin Level 16"); break;
			case 76: Application.LoadLevel ("Cabin Level 17"); break;
			case 77: Application.LoadLevel ("Cabin Level 18"); break;
			case 78: Application.LoadLevel ("Cabin Level 19"); break;
			case 79: Application.LoadLevel ("Cabin Level 20"); break;
			case 80: Application.LoadLevel ("Cabin Level 21"); break;
			case 81: Application.LoadLevel ("Cabin Level 22"); break;
			case 82: Application.LoadLevel ("Cabin Level 23"); break;
			case 83: Application.LoadLevel ("Cabin Level 24"); break;
			case 84: Application.LoadLevel ("Cabin Level 25"); break;
			case 85: Application.LoadLevel ("Cabin Level 26"); break;
			case 86: Application.LoadLevel ("Cabin Level 27"); break;
			case 87: Application.LoadLevel ("Cabin Level 28"); break;
			case 88: Application.LoadLevel ("Cabin Level 29"); break;
			case 89: Application.LoadLevel ("Cabin Level 30"); break;

			case 90: Application.LoadLevel ("Launchpad Level 1"); break;
			case 91: Application.LoadLevel ("Launchpad Level 2"); break;
			case 92: Application.LoadLevel ("Launchpad Level 3"); break;
			case 93: Application.LoadLevel ("Launchpad Level 4"); break;
			case 94: Application.LoadLevel ("Launchpad Level 5"); break;
			case 95: Application.LoadLevel ("Launchpad Level 6"); break;
			case 96: Application.LoadLevel ("Launchpad Level 7"); break;
			case 97: Application.LoadLevel ("Launchpad Level 8"); break;
			case 98: Application.LoadLevel ("Launchpad Level 9"); break;
			case 99: Application.LoadLevel ("Launchpad Level 10"); break;
			case 100: Application.LoadLevel ("Launchpad Level 11"); break;
			case 101: Application.LoadLevel ("Launchpad Level 12"); break;
			case 102: Application.LoadLevel ("Launchpad Level 13"); break;
			case 103: Application.LoadLevel ("Launchpad Level 14"); break;
			case 104: Application.LoadLevel ("Launchpad Level 15"); break;
			case 105: Application.LoadLevel ("Launchpad Level 16"); break;
			case 106: Application.LoadLevel ("Launchpad Level 17"); break;
			case 107: Application.LoadLevel ("Launchpad Level 18"); break;
			case 108: Application.LoadLevel ("Launchpad Level 19"); break;
			case 109: Application.LoadLevel ("Launchpad Level 20"); break;
			case 110: Application.LoadLevel ("Launchpad Level 21"); break;
			case 111: Application.LoadLevel ("Launchpad Level 22"); break;
			case 112: Application.LoadLevel ("Launchpad Level 23"); break;
			case 113: Application.LoadLevel ("Launchpad Level 24"); break;
			case 114: Application.LoadLevel ("Launchpad Level 25"); break;
			case 115: Application.LoadLevel ("Launchpad Level 26"); break;
			case 116: Application.LoadLevel ("Launchpad Level 27"); break;
			case 117: Application.LoadLevel ("Launchpad Level 28"); break;
			case 118: Application.LoadLevel ("Launchpad Level 29"); break;
			case 119: Application.LoadLevel ("Launchpad Level 30"); break;
			case 120: Application.LoadLevel ("New Level Map"); break;
			}
		}
		else if (noMoreLives) {
			if (Mathf.Abs (targetX - wholeScreen.transform.position.x) > errorDistance) {
				wholeScreen.transform.Translate (new Vector3 (targetX - wholeScreen.transform.position.x, 0, 0) * Time.deltaTime * speed);
//				if (instantiatedNoLivesPhrase != null && instantiatedLivesButton != null && instantiatedCoinsButton != null && instantiatedNoLivesHomeButton != null && instantiatedTimeLeftPhrase != null) {
//					instantiatedNoLivesPhrase.transform.Translate (new Vector3 (0 - instantiatedNoLivesPhrase.transform.position.x, 0, 0) * Time.deltaTime * 10);
//					instantiatedLivesButton.transform.Translate (new Vector3 (0 - instantiatedLivesButton.transform.position.x, 0, 0) * Time.deltaTime * 9);
//					instantiatedCoinsButton.transform.Translate (new Vector3 (0 - instantiatedCoinsButton.transform.position.x, 0, 0) * Time.deltaTime * 8);
//					instantiatedNoLivesHomeButton.transform.Translate (new Vector3 (0 - instantiatedNoLivesHomeButton.transform.position.x, 0, 0) * Time.deltaTime * 7);
//					instantiatedTimeLeftPhrase.transform.Translate (new Vector3 (0 - instantiatedTimeLeftPhrase.transform.position.x, 0, 0) * Time.deltaTime * 6);
//				}
				if (!moveLeft && Mathf.Abs (targetX - wholeScreen.transform.position.x) < errorDistance) {
					Destroy (wholeScreen);
				}
			} else {
				if (moveLeft) {
					targetX = 10;
					speed = 20;
					errorDistance = .001f;
					moveLeft = false;
					if (!noLivesSlideInstantiated) {
						instantiatedNoLivesPhrase = (GameObject)Instantiate (noLivesPhrase, new Vector3 (-11, 3, -60), Quaternion.identity);
						instantiatedNoLivesPhrase.GetComponent<NoLivesScreenMovement> ().SetTargetX (0);
						instantiatedNoLivesPhrase.GetComponent<NoLivesScreenMovement> ().SetMovementSpeed (10f);

						instantiatedLivesButton = (GameObject)Instantiate (purchaseLivesButton, new Vector3 (-21, .5f, -60), Quaternion.identity);
						instantiatedLivesButton.GetComponent<NoLivesScreenMovement> ().SetTargetX (0);
						instantiatedLivesButton.GetComponent<NoLivesScreenMovement> ().SetMovementSpeed (10f);

						instantiatedCoinsButton = (GameObject)Instantiate (purchaseCoinsButton, new Vector3 (-31, -1, -60), Quaternion.identity);
						instantiatedCoinsButton.GetComponent<NoLivesScreenMovement> ().SetTargetX (0);
						instantiatedCoinsButton.GetComponent<NoLivesScreenMovement> ().SetMovementSpeed (10f);

						instantiatedNoLivesHomeButton = (GameObject)Instantiate (noLivesGoHomeButton, new Vector3 (-41, -2.5f, -60), Quaternion.identity);
						instantiatedNoLivesHomeButton.GetComponent<NoLivesScreenMovement> ().SetTargetX (0);
						instantiatedNoLivesHomeButton.GetComponent<NoLivesScreenMovement> ().SetMovementSpeed (10f);

						instantiatedTimeLeftPhrase = (GameObject)Instantiate (timeLeftTillHeartsPhrase, new Vector3 (-51, -3.75f, -60), Quaternion.identity);
						instantiatedTimeLeftPhrase.GetComponent<NoLivesScreenMovement> ().SetTargetX (0);
						instantiatedTimeLeftPhrase.GetComponent<NoLivesScreenMovement> ().SetMovementSpeed (10f);
						noLivesSlideInstantiated = true;
					}
				}
				else if (wholeScreen.transform.position.x > 9)
					Destroy (wholeScreen);

			}


		}
//		else if (noMoreLives && transitionShadeController.GetAlpha () >= 1) {
//			Application.LoadLevel ("No More Lives");
//		}
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
	
	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		if (hit != null && hit.gameObject.name == "Level Complete Home Button(Clone)" && tempPressedButton == null) {
			soundHandler.PlayButtonClickDown ();
			tempPressedButton = (GameObject)Instantiate(pressedHomeButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
		else if (hit != null && hit.gameObject.name == "Level Failed Home Button(Clone)" && tempPressedButton == null) {
			soundHandler.PlayButtonClickDown ();
			tempPressedButton = (GameObject)Instantiate (pressedFailedHomeButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
		else if (hit != null && hit.gameObject.name == "Level Complete Next Button(Clone)" && tempPressedButton == null) {
			soundHandler.PlayButtonClickDown ();
			tempPressedButton = (GameObject)Instantiate(pressedNextButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
		else if (hit != null && hit.gameObject.name == "Level Complete Restart Button(Clone)" && tempPressedButton == null) {
			soundHandler.PlayButtonClickDown ();
			tempPressedButton = (GameObject)Instantiate(pressedRestartButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
		else if (hit != null && hit.gameObject.name == "Level Failed Retry Button(Clone)" && tempPressedButton == null) {
			soundHandler.PlayButtonClickDown ();
			tempPressedButton = (GameObject)Instantiate(pressedFailedRestartButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}

	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && (hit.gameObject.name == "Level Complete Home Button(Clone)" || hit.gameObject.name == "Level Complete Next Button(Clone)" || hit.gameObject.name == "Level Complete Restart Button(Clone)") && 
		    (hit2 == null || (hit2.gameObject.name != "Level Complete Home Button(Clone)" && hit2.gameObject.name != "Level Complete Next Button(Clone)" && hit2.gameObject.name != "Level Complete Restart Button(Clone)"))) {
			if (tempPressedButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedButton);
			}
		}
	}

	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit3 != null && hit3.gameObject.name == "Level Complete Home Button(Clone)" || hit3.gameObject.name == "Level Failed Home Button(Clone)") {
			//Debug.Log ("Home Button Pressed");
			transitionShadeController.DarkenShade ();
			goHome = true;
		}
		else if (hit3 != null && hit3.gameObject.name == "Level Complete Restart Button(Clone)") {
			transitionShadeController.DarkenShade ();
			tempRestartButton = hit3.gameObject;
			restartLevel = true;
		}
		else if (hit3 != null && hit3.gameObject.name == "Level Failed Retry Button(Clone)" && GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber () > 0) {
			transitionShadeController.DarkenShade ();
			tempRestartButton = hit3.gameObject;
			restartLevel = true;
		}
		else if (hit3 != null && hit3.gameObject.name == "Level Failed Retry Button(Clone)" && GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber () == 0) {
			targetX = -1;
			moveLeft = true;
			speed = 12;
			errorDistance = .05f;
			wholeScreen = new GameObject ();
			wholeScreen.name = "Whole Screen";

			levelFailedBanner = GameObject.Find ("Level Failed Banner(Clone)");
			levelFailedBanner.transform.parent = wholeScreen.transform;
			Destroy (levelFailedBanner.GetComponent<SlideInFromLeft> ());

			if (GameObject.Find ("Heart 0") != null) 
				heart0 = GameObject.Find ("Heart 0");
			else 
				heart0 = GameObject.Find ("Heart Outline 0");
			heart0.transform.parent = wholeScreen.transform;
			Destroy (heart0.GetComponent<SlideInFromLeft> ());

			if (GameObject.Find ("Heart 1") != null) 
				heart1 = GameObject.Find ("Heart 1");
			else 
				heart1 = GameObject.Find ("Heart Outline 1");
			heart1.transform.parent = wholeScreen.transform;
			Destroy (heart1.GetComponent<SlideInFromLeft> ());

			if (GameObject.Find ("Heart 2") != null) 
				heart2 = GameObject.Find ("Heart 2");
			else 
				heart2 = GameObject.Find ("Heart Outline 2");
			heart2.transform.parent = wholeScreen.transform;
			Destroy (heart2.GetComponent<SlideInFromLeft> ());

			if (GameObject.Find ("Heart 3") != null) 
				heart3 = GameObject.Find ("Heart 3");
			else 
				heart3 = GameObject.Find ("Heart Outline 3");
			heart3.transform.parent = wholeScreen.transform;
			Destroy (heart3.GetComponent<SlideInFromLeft> ());

			if (GameObject.Find ("Heart 4") != null) 
				heart4 = GameObject.Find ("Heart 4");
			else 
				heart4 = GameObject.Find ("Heart Outline 4");
			heart4.transform.parent = wholeScreen.transform;
			Destroy (heart4.GetComponent<SlideInFromLeft> ());

			levelFailedHome = GameObject.Find ("Level Failed Home Button(Clone)");
			levelFailedHome.transform.parent = wholeScreen.transform;
			Destroy (levelFailedHome.GetComponent<SlideInFromLeft> ());

			transform.parent = wholeScreen.transform;
			Destroy (gameObject.GetComponent<SlideInFromLeft> ());

			gameObject.GetComponent<LifeTimerHandler> ().StopTimer ();
			timeHolder = GameObject.Find ("Time Holder");
			//Debug.Log ("Time Holder = " + timeHolder);
			timeHolder.transform.parent = wholeScreen.transform;

			foreach (Transform child in timeHolder.transform) {
				child.GetComponent<SlideInFromLeft> ().SetTargetX (10);
				Destroy (child.GetComponent<SlideInFromLeft> ());
			}

			noMoreLives = true;
		}
		else if (hit3 != null && hit3.gameObject.name == "Level Complete Next Button(Clone)") {
			transitionShadeController.DarkenShade ();
			nextLevel = true;
		}
	}

	public void SetLevelFailed (bool levelFailed) {
		this.levelFailed = levelFailed;
	}

	public bool GetLevelFailed () {
		return levelFailed;
	}

	public void ExplodeHearts () {
		GameObject tempHeartOutline;
		if (GameObject.Find ("Heart 4") != null) {
			tempHeartOutline = (GameObject)Instantiate (heartOutline, GameObject.Find ("Heart 4").transform.position, Quaternion.identity);
			tempHeartOutline.name = "Heart Outline 4";
			tempHeartOutline.GetComponent<SlideInFromLeft> ().SetTargetX (-2.04f + (4 * 1.02f));
			Instantiate (heartExplosion, GameObject.Find ("Heart 4").transform.position, Quaternion.Euler (180, 0, 0));
			Destroy (GameObject.Find ("Heart 4"));
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (4);
		} else if (GameObject.Find ("Heart 3") != null) {
			tempHeartOutline = (GameObject)Instantiate (heartOutline, GameObject.Find ("Heart 3").transform.position, Quaternion.identity);
			tempHeartOutline.name = "Heart Outline 3";
			tempHeartOutline.GetComponent<SlideInFromLeft> ().SetTargetX (-2.04f + (3 * 1.02f));
			Instantiate (heartExplosion, GameObject.Find ("Heart 3").transform.position, Quaternion.Euler (180, 0, 0));
			Destroy (GameObject.Find ("Heart 3"));
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (3);
		} else if (GameObject.Find ("Heart 2") != null) {
			tempHeartOutline = (GameObject)Instantiate (heartOutline, GameObject.Find ("Heart 2").transform.position, Quaternion.identity);
			tempHeartOutline.name = "Heart Outline 2";
			tempHeartOutline.GetComponent<SlideInFromLeft> ().SetTargetX (-2.04f + (2 * 1.02f));
			Instantiate (heartExplosion, GameObject.Find ("Heart 2").transform.position, Quaternion.Euler (180, 0, 0));
			Destroy (GameObject.Find ("Heart 2"));
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (2);
		} else if (GameObject.Find ("Heart 1") != null) {
			tempHeartOutline = (GameObject)Instantiate (heartOutline, GameObject.Find ("Heart 1").transform.position, Quaternion.identity);
			tempHeartOutline.name = "Heart Outline 1";
			tempHeartOutline.GetComponent<SlideInFromLeft> ().SetTargetX (-2.04f + (1 * 1.02f));
			Instantiate (heartExplosion, GameObject.Find ("Heart 1").transform.position, Quaternion.Euler (180, 0, 0));
			Destroy (GameObject.Find ("Heart 1"));
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (1);
		} else if (GameObject.Find ("Heart 0") != null) {
			tempHeartOutline = (GameObject)Instantiate (heartOutline, GameObject.Find ("Heart 0").transform.position, Quaternion.identity);
			tempHeartOutline.name = "Heart Outline 0";
			tempHeartOutline.GetComponent<SlideInFromLeft> ().SetTargetX (-2.04f);
			Instantiate (heartExplosion, GameObject.Find ("Heart 0").transform.position, Quaternion.Euler (180, 0, 0));
			Destroy (GameObject.Find ("Heart 0"));
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (0);
		}
		heartExploded = true;
	}
}
