using UnityEngine;
using System.Collections;

public class MoveNumberHandler : MonoBehaviour {

	public GameObject moveNumberZero, moveNumberOne, moveNumberTwo, moveNumberThree, moveNumberFour, moveNumberFive, moveNumberSix, moveNumberSeven, moveNumberEight, moveNumberNine;
	public GameObject blueRowDestructionStar, greenRowDestructionStar, orangeRowDestructionStar, purpleRowDestructionStar, redRowDestructionStar, whiteRowDestructionStar;
	public GameObject plusFive;
	Vector3 twoDigitsFirstDigitPosition, twoDigitsSecondDigitPosition, oneDigitPosition, rowDestructionPosition;
	GameObject firstDigit, secondDigit, instantiatedRowDestructionStar;
	int moveNumberTotal, timeCount;
	bool launchRowDestructionStars, finishedLaunching;
	float cooldown, timestamp;


	void Awake () {
		twoDigitsFirstDigitPosition = new Vector3 (2.197f, -2.899f, -30);
		twoDigitsSecondDigitPosition = new Vector3 (2.476f, -2.899f, -30);
		oneDigitPosition = new Vector3 (2.3365f, -2.899f, -30);
		rowDestructionPosition = new Vector3 (2.3365f, -2.899f, -29);
		cooldown = .5f;
		timestamp = Time.time;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (launchRowDestructionStars && Time.time > timestamp + cooldown && moveNumberTotal > 0) {
			timestamp = Time.time;
			instantiatedRowDestructionStar = (GameObject)Instantiate (GetRandomRowDestructionStar (), rowDestructionPosition, Quaternion.identity);
			PaidPowerTracker.AddPowerToList (instantiatedRowDestructionStar);
			SubtractOneFromMoveNumber ();
		} else if (launchRowDestructionStars && moveNumberTotal <= 0) {
			finishedLaunching = true;
		}
	}

	public void SetMoveNumbers (int moveNumberTotal) {
		this.moveNumberTotal = moveNumberTotal;
		if (moveNumberTotal / 10 != 0) {
			firstDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal / 10), twoDigitsFirstDigitPosition, Quaternion.identity);
			secondDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal % 10), twoDigitsSecondDigitPosition, Quaternion.identity);
		} else {
			firstDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal), oneDigitPosition, Quaternion.identity);
		}
	}

	public void SubtractOneFromMoveNumber () {
		moveNumberTotal--;
		if (moveNumberTotal == 0) {
			GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ().PauseTouch ();
		}
		Destroy (firstDigit);
		Destroy (secondDigit);
		Debug.Log ("moveNumberTotal / 10 = " + moveNumberTotal / 10);

		if (moveNumberTotal / 10 != 0) {
			firstDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal / 10), twoDigitsFirstDigitPosition, Quaternion.identity);
			secondDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal % 10), twoDigitsSecondDigitPosition, Quaternion.identity);
		} else {
			firstDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal), oneDigitPosition, Quaternion.identity);
		}
	}

	public void AddFiveToMoveNumber () {
		moveNumberTotal += 5;
		Destroy (firstDigit);
		Destroy (secondDigit);

		if (moveNumberTotal / 10 != 0) {
			Instantiate (plusFive, new Vector3 ((twoDigitsFirstDigitPosition.x + twoDigitsSecondDigitPosition.x) / 2, twoDigitsFirstDigitPosition.y, twoDigitsFirstDigitPosition.z - .1f), Quaternion.identity);
			firstDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal / 10), twoDigitsFirstDigitPosition, Quaternion.identity);
			secondDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal % 10), twoDigitsSecondDigitPosition, Quaternion.identity);
		} else {
			Instantiate (plusFive, new Vector3 (oneDigitPosition.x, oneDigitPosition.y, oneDigitPosition.z - .1f), Quaternion.identity);
			firstDigit = (GameObject)Instantiate (GetNumberGameObject (moveNumberTotal), oneDigitPosition, Quaternion.identity);
		}
	}

	public int GetMoveNumber () {
		return moveNumberTotal;
	}

	public void StartLaunchingRowDestructionStars () {
		launchRowDestructionStars = true;
	}

	public bool GetFinishedLaunching () {
		return finishedLaunching;
	}

	GameObject GetRandomRowDestructionStar () {
		switch (Random.Range (0, 6)) {
		case 0: return blueRowDestructionStar;
		case 1: return greenRowDestructionStar;
		case 2: return orangeRowDestructionStar;
		case 3: return purpleRowDestructionStar;
		case 4: return redRowDestructionStar;
		case 5: return whiteRowDestructionStar;
		}
		return null;
	}

	public GameObject GetNumberGameObject (int number) {
		switch (number) {
		case 0: return moveNumberZero;
		case 1: return moveNumberOne;
		case 2: return moveNumberTwo;
		case 3: return moveNumberThree;
		case 4: return moveNumberFour;
		case 5: return moveNumberFive;
		case 6: return moveNumberSix;
		case 7: return moveNumberSeven;
		case 8: return moveNumberEight;
		case 9: return moveNumberNine;
		}
		return null;
	}
}
