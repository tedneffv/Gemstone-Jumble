using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelScoreKeeper : MonoBehaviour {

	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, progressBarFill, progressStarOne, progressStarTwo, progressStarThree;
	Vector3 evenFirstPosition, evenSecondPosition, evenThirdPosition, evenFourthPosition, evenFifthPosition, evenSixthPosition, evenSeventhPosition, evenEightPosition;
	Vector3 oddFirstPosition, oddSecondPosition, oddThirdPosition, oddFourthPosition, oddFifthPosition, oddSixthPosition, oddSeventhPosition;
	GameObject firstDigit, secondDigit, thirdDigit, fourthDigit, fifthDigit, sixthDigit, seventhDigit, eightDigit, instantiatedCoin;
	List<GameObject> digitList;
	int score;
	int[] scoreNumbers;
	bool[] destroyCheck;
	RockLevelProgressBarController progressBar;
	RockLevelStarShooter starShooter;

	// Use this for initialization
	void Start () {
		starShooter = gameObject.GetComponent<RockLevelStarShooter> ();
		progressBar = GameObject.Find ("Level Controller").GetComponent<RockLevelProgressBarController> ();
		evenFirstPosition = new Vector3 (.11f, 4.39f, -1.1f);
		evenSecondPosition = new Vector3 (-.11f, 4.39f, -1.1f);
		evenThirdPosition = new Vector3 (.33f, 4.39f, -1.1f);
		evenFourthPosition = new Vector3 (-.33f, 4.39f, -1.1f);
		evenFifthPosition = new Vector3 (.55f, 4.39f, -1.1f);
		evenSixthPosition = new Vector3 (-.55f, 4.39f, -1.1f);
		evenSeventhPosition = new Vector3 (.77f, 4.39f, -1.1f);
		evenEightPosition = new Vector3 (-.77f, 4.39f, -1.1f);

		oddFirstPosition = new Vector3 (0, 4.39f, -1.1f);
		oddSecondPosition = new Vector3 (-.22f, 4.39f, -1.1f);
		oddThirdPosition = new Vector3 (.22f, 4.39f, -1.1f);
		oddFourthPosition = new Vector3 (-.44f, 4.39f, -1.1f);
		oddFifthPosition = new Vector3 (.44f, 4.39f, -1.1f);
		oddSixthPosition = new Vector3 (-.66f, 4.39f, -1.1f);
		oddSeventhPosition = new Vector3 (.66f, 4.39f, -1.1f);

		digitList = new List<GameObject> ();
		digitList.Add ((GameObject)Instantiate (zero, oddFirstPosition, Quaternion.identity));

//		firstDigit = (GameObject)Instantiate (zero, firstPosition, Quaternion.identity);
		score = 0;
		scoreNumbers = new int[7];
		for (int i = 0; i < 7; i++) {
			scoreNumbers[i] = 0;
		}
	}
	

	void UpdateScore () {
		string scoreString = score.ToString ();
		foreach (GameObject a in digitList) {
			Destroy (a);
		}
		digitList.Clear ();
		if (scoreString.Length == 1) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), oddFirstPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 2) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), evenSecondPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 3) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[2]), oddThirdPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 4) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), evenFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[2]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[3]), evenThirdPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 5) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), oddFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[2]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[3]), oddThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[4]), oddFifthPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 6) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), evenSixthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), evenFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[2]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[3]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[4]), evenThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[5]), evenFifthPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 7) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), oddSixthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), oddFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[2]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[3]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[4]), oddThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[5]), oddFifthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[6]), oddSeventhPosition, Quaternion.identity));
		}
		else if (scoreString.Length == 8) {
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[0]), evenEightPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[1]), evenSixthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[2]), evenFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[3]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[4]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[5]), evenThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[6]), evenFifthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (scoreString[7]), evenSeventhPosition, Quaternion.identity));
		}
		foreach (GameObject a in digitList) {
			a.GetComponent<ScoreNumberSizeIncreaser> ().SetGrow (true);
		}
	}

	GameObject GetNumberFromString (char number) {
		switch (number) {
		case '0': return zero;
		case '1': return one;
		case '2': return two;
		case '3': return three;
		case '4': return four;
		case '5': return five;
		case '6': return six;
		case '7': return seven;
		case '8': return eight;
		case '9': return nine;
		}
		return null;
	}

//	void SetCorrectDigit (int digitNumber, GameObject instantiatedDigit) {
//		switch (digitNumber) {
//		case 0: firstDigit = instantiatedDigit; break;
//		case 1: secondDigit = instantiatedDigit; break;
//		case 2: thirdDigit = instantiatedDigit; break;
//		case 3: fourthDigit = instantiatedDigit; break;
//		case 4: fifthDigit = instantiatedDigit; break;
//		case 5: sixthDigit = instantiatedDigit; break;
//		case 6: seventhDigit = instantiatedDigit; break;
//		}
//	}
//
//	GameObject GetCorrectDigit (int digitNumber) {
//		switch (digitNumber) {
//		case 0: return firstDigit;
//		case 1: return secondDigit;
//		case 2: return thirdDigit;
//		case 3: return fourthDigit;
//		case 4: return fifthDigit;
//		case 5: return sixthDigit;
//		case 6: return seventhDigit;
//		}
//		return null;
//	}

//	Vector3 GetDigitPosition (int digitNumber) {
//		switch (digitNumber) {
//		case 0: return firstPosition;
//		case 1: return secondPosition;
//		case 2: return thirdPosition;
//		case 3: return fourthPosition;
//		case 4: return fifthPosition;
//		case 5: return sixthPosition;
//		case 6: return seventhPosition;
//		}
//		return Vector3.zero;
//	}

	void DestroyAllDigits () {
		if (eightDigit != null) {
			Destroy (eightDigit);
		}
		if (seventhDigit != null)  {
			Destroy (seventhDigit);
		}
		if (sixthDigit != null) {
			Destroy (sixthDigit);
		}
		if (fifthDigit != null) {
			Destroy (fifthDigit);
		}
		if (fourthDigit != null) {
			Destroy (fourthDigit);
		}
		if (thirdDigit != null) {
			Destroy (thirdDigit);
		}
		if (secondDigit != null) {
			Destroy (secondDigit);
		}
		if (firstDigit != null) {
			Destroy (firstDigit);
		}
	}

	public void IncreaseScoreByOneJewel () {
		score += (100 + Random.Range (0, 10));
		UpdateScore ();
		progressBar.UpdateProgressBar (score);
	}

	public void IncreaseScoreByBombNumberStar (int bombNumber) {
		score += ((bombNumber * 100) + Random.Range (0, 10));
//		score += (3000 + Random.Range (0, 10));
		progressBar.UpdateProgressBar (score);
	}

	public void IncreaseScoreByFallingBomb (int bombNumber) {
		for (int i = 0; i < 5; i++) {
			score += ((bombNumber * 100) + Random.Range (0, 10));
			bombNumber--;
		}
		progressBar.UpdateProgressBar (score);
	}

	public void IncreaseScoreByRowDestructionStar (int bombNumber) {
		for (int i = 0; i < 10; i++) {
			score += ((bombNumber * 100) + Random.Range (0, 10));
			bombNumber--;
		}
		progressBar.UpdateProgressBar (score);
	}

	GameObject GetNumberFromInt (int scoreNumber) {
		switch (scoreNumber) {
		case 0: return zero;
		case 1: return one;
		case 2: return two;
		case 3: return three;
		case 4: return four;
		case 5: return five;
		case 6: return six;
		case 7: return seven;
		case 8: return eight;
		case 9: return nine;
		}
		return null;
	}

	public int GetScore () {
		return score;
	}
}
