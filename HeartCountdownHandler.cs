using UnityEngine;
using System.Collections;

public class HeartCountdownHandler : MonoBehaviour {

	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, heartExplosion, heart;
	float timeStamp, cooldown, totalCooldown, totalTimeStamp;
	int currentTimeMinutes, currentTimeSeconds, firstDigitInt, secondDigitInt, heartNumber;
	GameObject fourthDigit, thirdDigit, secondDigit, firstDigit;
	Vector3 fourthDigitPosition, thirdDigitPosition, secondDigitPosition, firstDigitPosition;
	bool destroyFifthHeart, destroyFourthHeart, destroyThirdHeart, destroySecondHeart, destroyFirstHeart, instantiateTime, darkenShade;
	GameManagerScript gameManagerScript;
	TransitionShadeController transitionShadeController;

	// Use this for initialization
	void Start () {
		heartNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber ();
		if (heartNumber >= 1) {
			Instantiate (heart, new Vector3 (-2.1f, 0, 0), Quaternion.identity).name = "Heart 5";
		}
		if (heartNumber >= 2) {
			Instantiate (heart, new Vector3 (-1.16f, 0, 0), Quaternion.identity).name = "Heart 4";
		}
		if (heartNumber >= 3) {
			Instantiate (heart, new Vector3 (-.22f, 0, 0), Quaternion.identity).name = "Heart 3";
		}
		if (heartNumber >= 4) {
			Instantiate (heart, new Vector3 (.72f, 0, 0), Quaternion.identity).name = "Heart 2";
		}
		if (heartNumber >= 5) {
			Instantiate (heart, new Vector3 (1.66f, 0, 0), Quaternion.identity).name = "Heart 1";
		}
		fourthDigitPosition = new Vector3 (-1.113162f, .9f, -1f);
		thirdDigitPosition = new Vector3 (-.483161f, .9f, -1f);
		secondDigitPosition = new Vector3 (.3768382f, .9f, -1f);
		firstDigitPosition = new Vector3 (1.016838f, .9f, -1f);
		if (GameObject.Find ("Heart 1") != null) {
			currentTimeMinutes = 30;
			currentTimeSeconds = 0;
			destroyFirstHeart = true;
		}
		else if ("Heart 2" != null) {
			destroySecondHeart = true;
		}
		else if ("Heart 3" != null) {
			destroyThirdHeart = true;
		}
		else if ("Heart 4" != null) {
			destroyFourthHeart = true;
		}
		else if ("Heart 5" != null) {
			destroyFifthHeart = true;
		}
		timeStamp = Time.time;
		totalTimeStamp = Time.time;
		cooldown = 1;
		totalCooldown = 5;
		gameManagerScript = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ();
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!destroyFirstHeart && !instantiateTime && Time.time >= timeStamp + cooldown) {
			DecreaseSecondByOne ();
		}

		if (destroyFirstHeart && Time.time >= timeStamp + cooldown) {
			Destroy (GameObject.Find ("Heart 1"));
			Instantiate (heartExplosion, GameObject.Find ("Heart 1").transform.position, Quaternion.Euler (180, 0, 0));
			timeStamp = Time.time;
			destroyFirstHeart = false;
			instantiateTime = true;
		}

		else if (destroySecondHeart && Time.time >= timeStamp + cooldown) {
			Destroy (GameObject.Find ("Heart 2"));
			Instantiate (heartExplosion, GameObject.Find ("Heart 2").transform.position, Quaternion.Euler (180, 0, 0));
			timeStamp = Time.time;
			destroySecondHeart = true;
		}

		if (instantiateTime && Time.time >= timeStamp + cooldown) {
			fourthDigit = (GameObject)Instantiate (three, fourthDigitPosition, Quaternion.identity);
			thirdDigit = (GameObject)Instantiate (zero, thirdDigitPosition, Quaternion.identity);
			secondDigit = (GameObject)Instantiate (zero, secondDigitPosition, Quaternion.identity);
			firstDigit = (GameObject)Instantiate (zero, firstDigitPosition, Quaternion.identity);
			instantiateTime = false;
			timeStamp = Time.time;
		}
		if (Time.time > totalTimeStamp + totalCooldown && !darkenShade) {
			transitionShadeController.DarkenShade ();
			darkenShade = true;
		}
		if (Time.time > totalTimeStamp + cooldown && darkenShade && transitionShadeController.GetAlpha () >= 1) {
			Application.LoadLevel (gameManagerScript.GetRestartName ());
		}
	}

	void DecreaseSecondByOne () {
		if (currentTimeSeconds == 0) {
			currentTimeSeconds = 60;
			DecreaseMinuteByOne ();
		}
		currentTimeSeconds--;
		secondDigitInt = currentTimeSeconds / 10;
		firstDigitInt = currentTimeSeconds % 10;
		Destroy (firstDigit);
		Destroy (secondDigit);
		switch (firstDigitInt) {
		case 0: firstDigit = (GameObject)Instantiate (zero, firstDigitPosition, Quaternion.identity); break;
		case 1: firstDigit = (GameObject)Instantiate (one, firstDigitPosition, Quaternion.identity); break;
		case 2: firstDigit = (GameObject)Instantiate (two, firstDigitPosition, Quaternion.identity); break;
		case 3: firstDigit = (GameObject)Instantiate (three, firstDigitPosition, Quaternion.identity); break;
		case 4: firstDigit = (GameObject)Instantiate (four, firstDigitPosition, Quaternion.identity); break;
		case 5: firstDigit = (GameObject)Instantiate (five, firstDigitPosition, Quaternion.identity); break;
		case 6: firstDigit = (GameObject)Instantiate (six, firstDigitPosition, Quaternion.identity); break;
		case 7: firstDigit = (GameObject)Instantiate (seven, firstDigitPosition, Quaternion.identity); break;
		case 8: firstDigit = (GameObject)Instantiate (eight, firstDigitPosition, Quaternion.identity); break;
		case 9: firstDigit = (GameObject)Instantiate (nine, firstDigitPosition, Quaternion.identity); break;
		}

		switch (secondDigitInt) {
		case 0: secondDigit = (GameObject)Instantiate (zero, secondDigitPosition, Quaternion.identity); break;
		case 1: secondDigit = (GameObject)Instantiate (one, secondDigitPosition, Quaternion.identity); break;
		case 2: secondDigit = (GameObject)Instantiate (two, secondDigitPosition, Quaternion.identity); break;
		case 3: secondDigit = (GameObject)Instantiate (three, secondDigitPosition, Quaternion.identity); break;
		case 4: secondDigit = (GameObject)Instantiate (four, secondDigitPosition, Quaternion.identity); break;
		case 5: secondDigit = (GameObject)Instantiate (five, secondDigitPosition, Quaternion.identity); break;
		case 6: secondDigit = (GameObject)Instantiate (six, secondDigitPosition, Quaternion.identity); break;
		case 7: secondDigit = (GameObject)Instantiate (seven, secondDigitPosition, Quaternion.identity); break;
		case 8: secondDigit = (GameObject)Instantiate (eight, secondDigitPosition, Quaternion.identity); break;
		case 9: secondDigit = (GameObject)Instantiate (nine, secondDigitPosition, Quaternion.identity); break;
		}
		timeStamp = Time.time;
	}

	void DecreaseMinuteByOne () {
		if (currentTimeMinutes == 0) {
			currentTimeMinutes = 30;
		}
		currentTimeMinutes--;
		int secondMinuteInt = currentTimeMinutes / 10;
		int firstMinuteInt = currentTimeMinutes % 10;
		Destroy (thirdDigit);
		Destroy (fourthDigit);

		switch (firstMinuteInt) {
		case 0: thirdDigit = (GameObject)Instantiate (zero, thirdDigitPosition, Quaternion.identity); break;
		case 1: thirdDigit = (GameObject)Instantiate (one, thirdDigitPosition, Quaternion.identity); break;
		case 2: thirdDigit = (GameObject)Instantiate (two, thirdDigitPosition, Quaternion.identity); break;
		case 3: thirdDigit = (GameObject)Instantiate (three, thirdDigitPosition, Quaternion.identity); break;
		case 4: thirdDigit = (GameObject)Instantiate (four, thirdDigitPosition, Quaternion.identity); break;
		case 5: thirdDigit = (GameObject)Instantiate (five, thirdDigitPosition, Quaternion.identity); break;
		case 6: thirdDigit = (GameObject)Instantiate (six, thirdDigitPosition, Quaternion.identity); break;
		case 7: thirdDigit = (GameObject)Instantiate (seven, thirdDigitPosition, Quaternion.identity); break;
		case 8: thirdDigit = (GameObject)Instantiate (eight, thirdDigitPosition, Quaternion.identity); break;
		case 9: thirdDigit = (GameObject)Instantiate (nine, thirdDigitPosition, Quaternion.identity); break;
		}
		
		switch (secondMinuteInt) {
		case 0: fourthDigit = (GameObject)Instantiate (zero, fourthDigitPosition, Quaternion.identity); break;
		case 1: fourthDigit = (GameObject)Instantiate (one, fourthDigitPosition, Quaternion.identity); break;
		case 2: fourthDigit = (GameObject)Instantiate (two, fourthDigitPosition, Quaternion.identity); break;
		case 3: fourthDigit = (GameObject)Instantiate (three, fourthDigitPosition, Quaternion.identity); break;
		case 4: fourthDigit = (GameObject)Instantiate (four, fourthDigitPosition, Quaternion.identity); break;
		case 5: fourthDigit = (GameObject)Instantiate (five, fourthDigitPosition, Quaternion.identity); break;
		case 6: fourthDigit = (GameObject)Instantiate (six, fourthDigitPosition, Quaternion.identity); break;
		case 7: fourthDigit = (GameObject)Instantiate (seven, fourthDigitPosition, Quaternion.identity); break;
		case 8: fourthDigit = (GameObject)Instantiate (eight, fourthDigitPosition, Quaternion.identity); break;
		case 9: fourthDigit = (GameObject)Instantiate (nine, fourthDigitPosition, Quaternion.identity); break;
		}

	}
}
