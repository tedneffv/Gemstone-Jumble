using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelProgressBarController : MonoBehaviour {

	public GameObject blueProgressBar, purpleProgressBar, greenProgressBar, blueLeftEnd, blueSquare, blueRightEnd, purpleLeftEnd, purpleSquare, purpleRightEnd, greenLeftEnd, greenSquare, greenRightEnd;
	public GameObject blueProgressStar, purpleProgressStar, greenProgressStar, fillBar, fillStar1, fillStar2, fillStar3;
	Vector3 progressBarPosition, leftEndPosition, rightEndPosition, firstSquarePosition;
	GameObject currentProgressBar, tempIncrement, starOne, starTwo, starThree;
	int threeStarTarget, progressBarIncrement, incrementNumber, progressFillNumber;
	List<GameObject> incrementList, progressFillList;
	bool blueStarFalling, greenStarFalling, purpleStarFalling, addToProgressBar;
	RockLevelController levelController;
	RockLevelScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {
		progressBarPosition = new Vector3 (-1.33f, 4, -1);
		leftEndPosition = new Vector3 (-2.605f, 4, -2);
		firstSquarePosition = new Vector3 (-2.6371f, 3.9984f, -2);
		rightEndPosition = new Vector3 (-.055f, 4, -2);
		levelController = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		scoreKeeper = GameObject.Find ("Level Controller").GetComponent<RockLevelScoreKeeper> ();
//		currentProgressBar = (GameObject)Instantiate (blueProgressBar, progressBarPosition, Quaternion.identity);
		threeStarTarget = 0;
		if (levelController.GetLevelNumber () < 31) {
			switch (levelController.GetLevelNumber ()) {
			case 1: threeStarTarget = 216100; break;
			case 2: threeStarTarget = 550300; break;
			case 3: threeStarTarget = 19100; break;
			case 4: threeStarTarget = 19100; break;
			case 5: threeStarTarget = 55100; break;
			case 6: threeStarTarget = 26100; break;
			case 7: threeStarTarget = 177800; break;
			case 8: threeStarTarget = 550300; break;
			case 9: threeStarTarget = 177800; break;
			case 10: threeStarTarget = 198375; break;
			case 11: threeStarTarget = 368100; break;
			case 12: threeStarTarget = 55100; break;
			case 13: threeStarTarget = 1675000; break;
			case 14: threeStarTarget = 77800; break;
			case 15: threeStarTarget = 198375; break;
			case 16: threeStarTarget = 19100; break;
			case 17: threeStarTarget = 177800; break;
			case 18: threeStarTarget = 177800; break;
			case 19: threeStarTarget = 19100; break;
			case 20: threeStarTarget = 713625; break;
			case 21: threeStarTarget = 368100; break;
			case 22: threeStarTarget = 368100; break;
			case 23: threeStarTarget = 108000; break;
			case 24: threeStarTarget = 951500; break;
			case 25: threeStarTarget = 103875; break;
			case 26: threeStarTarget = 264500; break;
			case 27: threeStarTarget = 2762800; break;
			case 28: threeStarTarget = 108000; break;
			case 29: threeStarTarget = 368100; break;
			case 30: threeStarTarget = 4835700; break;
			}
		}
		else if (levelController.GetLevelNumber () < 61) {
			switch (levelController.GetLevelNumber ()) {
			case 31: threeStarTarget = 19100; break;
			case 32: threeStarTarget = 45600; break;
			case 33: threeStarTarget = 264500; break;
			case 34: threeStarTarget = 310600; break;
			case 35: threeStarTarget = 1545750; break;
			case 36: threeStarTarget = 177800; break;
			case 37: threeStarTarget = 264500; break;
			case 38: threeStarTarget = 66000; break;
			case 39: threeStarTarget = 264500; break;
			case 40: threeStarTarget = 5192250; break;
			case 41: threeStarTarget = 108000; break;
			case 42: threeStarTarget = 368100; break;
			case 43: threeStarTarget = 626000; break;
			case 44: threeStarTarget = 368100; break;
			case 45: threeStarTarget = 1008450; break;
			case 46: threeStarTarget = 55100; break;
			case 47: threeStarTarget = 368100; break;
			case 48: threeStarTarget = 780300; break;
			case 49: threeStarTarget = 34000; break;
			case 50: threeStarTarget = 19575; break;
			case 51: threeStarTarget = 422000; break;
			case 52: threeStarTarget = 368100; break;
			case 53: threeStarTarget = 55100; break;
			case 54: threeStarTarget = 236600; break;
			case 55: threeStarTarget = 1545750; break;
			case 56: threeStarTarget = 780300; break;
			case 57: threeStarTarget = 177800; break;
			case 58: threeStarTarget = 264500; break;
			case 59: threeStarTarget = 780300; break;
			case 60: threeStarTarget = 2197125; break;
			}
		}
		else if (levelController.GetLevelNumber () < 91) {
			switch (levelController.GetLevelNumber ()) {
			case 61: threeStarTarget = 34000; break;
			case 62: threeStarTarget = 19100; break;
			case 63: threeStarTarget = 177800; break;
			case 64: threeStarTarget = 216100; break;
			case 65: threeStarTarget = 264500; break;
			case 66: threeStarTarget = 2061000; break;
			case 67: threeStarTarget = 550300; break;
			case 68: threeStarTarget = 3674100; break;
			case 69: threeStarTarget = 22521600; break;
			case 70: threeStarTarget = 951500; break;
			case 71: threeStarTarget = 1344600; break;
			case 72: threeStarTarget = 264500; break;
			case 73: threeStarTarget = 2929500; break;
			case 74: threeStarTarget = 2333600; break;
			case 75: threeStarTarget = 17590500; break;
			case 76: threeStarTarget = 951500; break;
			case 77: threeStarTarget = 368100; break;
			case 78: threeStarTarget = 9553500; break;
			case 79: threeStarTarget = 2929500; break;
			case 80: threeStarTarget = 27317100; break;
			case 81: threeStarTarget = 2061000; break;
			case 82: threeStarTarget = 550300; break;
			case 83: threeStarTarget = 15353100; break;
			case 84: threeStarTarget = 2122600; break;
			case 85: threeStarTarget = 16827800; break;
			case 86: threeStarTarget = 54932000; break;
			case 87: threeStarTarget = 12757800; break;
			case 88: threeStarTarget = 50216500; break;
			case 89: threeStarTarget = 4502800; break;
			case 90: threeStarTarget = 5122800; break;
			}
		}
		else if (levelController.GetLevelNumber () < 121) {
			switch (levelController.GetLevelNumber ()) {
			case 91: threeStarTarget = 1162000; break;
			case 92: threeStarTarget = 723600; break;
			case 93: threeStarTarget = 55150; break;
			case 94: threeStarTarget = 352800; break;
			case 95: threeStarTarget = 65087; break;
			case 96: threeStarTarget = 33600; break;
			case 97: threeStarTarget = 723600; break;
			case 98: threeStarTarget = 33600; break;
			case 99: threeStarTarget = 75600; break;
			case 100: threeStarTarget = 1352800; break;
			case 101: threeStarTarget = 2478000; break; 
			case 102: threeStarTarget = 61392; break;
			case 103: threeStarTarget = 2613600; break;
			case 104: threeStarTarget = 52298; break;
			case 105: threeStarTarget = 985600; break; 
			case 106: threeStarTarget = 16696000; break;
			case 107: threeStarTarget = 42651; break;
			case 108: threeStarTarget = 2433600; break;
			case 109: threeStarTarget = 70379; break;
			case 110: threeStarTarget = 13356000; break;
			case 111: threeStarTarget = 3502800; break;
			case 112: threeStarTarget = 66184; break;
			case 113: threeStarTarget = 424000; break;
			case 114: threeStarTarget = 43887; break;
			case 115: threeStarTarget = 424000; break;
			case 116: threeStarTarget = 154000; break;
			case 117: threeStarTarget = 60647; break;
			case 118: threeStarTarget = 142800; break;
			case 119: threeStarTarget = 54460; break;
			case 120: threeStarTarget = 522000; break;
			}
		}
		incrementNumber = 0;
		progressFillNumber = 0;
		progressBarIncrement = threeStarTarget / 30;
		incrementList = new List<GameObject> ();
		progressFillList = new List<GameObject> ();
		addToProgressBar = true;
	}


	public void UpdateProgressBar (int score) {
		if ((score - (progressBarIncrement * incrementNumber)) > progressBarIncrement) {
			if (addToProgressBar) {
				AddToProgressBar ();
				incrementNumber++;
			}
		}
	}

	void AddToProgressBar () {
		if (starThree != null)
			return;
		//Debug.Log ("progressFillNumber = " + progressFillNumber);
		GameObject tempProgressBar;
		switch (progressFillNumber) {
		case 0: tempProgressBar = GameObject.Find ("Progress Bar 1");
			progressFillList.Clear ();
			progressFillList.Add ((GameObject)Instantiate (fillBar, new Vector3 (tempProgressBar.transform.position.x, tempProgressBar.transform.position.y, tempProgressBar.transform.position.z - .1f), Quaternion.identity)); 
			progressFillNumber++; break;
		case 1: tempProgressBar = GameObject.Find ("Progress Bar 2");
			progressFillList.Add ((GameObject)Instantiate (fillBar, new Vector3 (tempProgressBar.transform.position.x, tempProgressBar.transform.position.y, tempProgressBar.transform.position.z - .1f), Quaternion.identity)); 
			progressFillNumber++; break;
		case 2: tempProgressBar = GameObject.Find ("Progress Bar 3");
			progressFillList.Add ((GameObject)Instantiate (fillBar, new Vector3 (tempProgressBar.transform.position.x, tempProgressBar.transform.position.y, tempProgressBar.transform.position.z - .1f), Quaternion.identity)); 
			progressFillNumber++; break;
		case 3: tempProgressBar = GameObject.Find ("Progress Bar 4");
			progressFillList.Add ((GameObject)Instantiate (fillBar, new Vector3 (tempProgressBar.transform.position.x, tempProgressBar.transform.position.y, tempProgressBar.transform.position.z - .1f), Quaternion.identity)); 
			progressFillNumber++; break;
		case 4: 
			if (GameObject.Find ("Filled Star 2(Clone)") == null) {
				progressFillNumber = 0;
			} else {
				progressFillNumber++;
			}
			if (GameObject.Find ("Filled Star 1(Clone)") == null) {
				progressBarIncrement = threeStarTarget / 15;
				//Debug.Log ("Progress Bar Increment");
				GameObject temp = GameObject.Find ("Banner Star 1");
				//Debug.Log ("Instantiating Star 1");
				starOne = (GameObject)Instantiate (fillStar1, new Vector3 (-2.38f, temp.transform.position.y, temp.transform.position.z - .1f), Quaternion.identity);
			} else if (GameObject.Find ("Filled Star 2(Clone)") == null) {
				GameObject temp = GameObject.Find ("Banner Star 2");
				//Debug.Log ("Instantiating Star 2");
				starTwo = (GameObject)Instantiate (fillStar2, new Vector3 (-2.04f, temp.transform.position.y, temp.transform.position.z - .1f), Quaternion.identity);
			} else if (GameObject.Find ("Filled Star 3(Clone)") == null) {
				GameObject temp = GameObject.Find ("Banner Star 3");
				//Debug.Log ("Instantiating Star 1");
				starThree = (GameObject)Instantiate (fillStar3, new Vector3 (temp.transform.position.x, temp.transform.position.y, temp.transform.position.z - .1f), Quaternion.identity);
			}
			foreach (GameObject a in progressFillList) {
				Destroy (a);
			} 
			break;
		case 5:
			if (GameObject.Find ("Filled Star 3(Clone)") != null) {
				addToProgressBar = false;
				return;
			} break;
		}
	}
//	public void UpdateProgressBar (int score) {
//		if (score >= ((incrementNumber * progressBarIncrement) + progressBarIncrement)) {
//			if (incrementNumber == 0) {
//				tempIncrement = (GameObject)Instantiate (blueLeftEnd, leftEndPosition, Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (0 < incrementNumber && incrementNumber < 23) {
//				tempIncrement = (GameObject)Instantiate (blueSquare, new Vector3 (firstSquarePosition.x + (incrementNumber * .1135f), firstSquarePosition.y, firstSquarePosition.z), Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (incrementNumber == 23) {
//				tempIncrement = (GameObject)Instantiate (blueRightEnd, rightEndPosition, Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (incrementNumber == 24) {
//				foreach (GameObject a in incrementList) {
//					Destroy (a);
//				}
//				starOne = (GameObject)Instantiate (blueProgressStar, new Vector3 (-2.35f, 3.65f, -2), Quaternion.identity);
//				blueStarFalling = true;
//				Destroy (currentProgressBar);
//				currentProgressBar = (GameObject) Instantiate (purpleProgressBar, progressBarPosition, Quaternion.identity);
//				tempIncrement = (GameObject) Instantiate (purpleLeftEnd, leftEndPosition, Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (24 < incrementNumber && incrementNumber < 47) {
//				tempIncrement = (GameObject)Instantiate (purpleSquare, new Vector3 (firstSquarePosition.x + ((incrementNumber - 24) * .1135f), firstSquarePosition.y, firstSquarePosition.z), Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (incrementNumber == 47) {
//				tempIncrement = (GameObject)Instantiate (purpleRightEnd, rightEndPosition, Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (incrementNumber == 48) {
//				foreach (GameObject a in incrementList) {
//					Destroy (a);
//				}
//				starTwo = (GameObject)Instantiate (purpleProgressStar, new Vector3 (-2.0145f, 3.65f, -2), Quaternion.identity);
//				purpleStarFalling = true;
//				Destroy (currentProgressBar);
//				currentProgressBar = (GameObject) Instantiate (greenProgressBar, progressBarPosition, Quaternion.identity);
//				tempIncrement = (GameObject) Instantiate (greenLeftEnd, leftEndPosition, Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (48 < incrementNumber && incrementNumber < 71) {
//				tempIncrement = (GameObject)Instantiate (greenSquare, new Vector3 (firstSquarePosition.x + ((incrementNumber - 48) * .1135f), firstSquarePosition.y, firstSquarePosition.z), Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//			}
//			else if (incrementNumber == 71) {
//				tempIncrement = (GameObject)Instantiate (greenRightEnd, rightEndPosition, Quaternion.identity);
//				incrementList.Add (tempIncrement);
//				incrementNumber++;
//				starThree = (GameObject)Instantiate (greenProgressStar, new Vector3 (-1.677f, 3.65f, -2), Quaternion.identity);
//				greenStarFalling = true;
//			}
//		}
//	}

	public int GetStarNumber () {
		if (starThree != null) {
			return 3;
		}
		else if (starTwo != null) {
			return 2;
		}
		else if (starOne != null) {
			return 1;
		}
		return 0;
	}
	
}