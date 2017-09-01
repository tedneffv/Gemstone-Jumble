using UnityEngine;
using System.Collections;

public class LevelFailedScreenHandler : MonoBehaviour {

	public GameObject levelFailedBanner, heart, timesSymbol, zero, one, two, three, four, five, retryButton, homeButton, heartExplosion, colon, removeAdsButton;
	public GameObject countdown0, countdown1, countdown2, countdown3, countdown4, countdown5, countdown6, countdown7, countdown8, countdown9;
	GameObject instantiatedLevelFailedBanner, instantiatedHeart, instantiatedTimesSymbol, instantiatedNumber, instantiatedRetryButton, instantiatedHomeButton, instantiatedRemoveAdsButton;
	GameObject firstTimeDigit, secondTimeDigit, thirdTimeDigit, fourthTimeDigit, instantiatedColon;
	System.DateTime timeRemaining;
	int minutesDividedByThirty, heartNumber;
	bool timerStarted, numberExploded, screenInstantiated;
	float timeStamp, cooldown;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade(false);
		cooldown = 1;
		timeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerStarted && Time.time >= timeStamp + cooldown) {
			DecreaseFourthTimeDigitByOne ();
			timeStamp = Time.time;
		}
	}

	public void InstantiateFinalScreen () {
		soundHandler.PlayWoosh ();
		instantiatedLevelFailedBanner = (GameObject)Instantiate (levelFailedBanner, new Vector3 (-10, 2.3f, -101), Quaternion.identity);
		instantiatedLevelFailedBanner.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0.08f);

//		instantiatedHeart = (GameObject)Instantiate (heart, new Vector3 (-20, 1.63f, -101), Quaternion.identity);
//		instantiatedHeart.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1);

//		instantiatedTimesSymbol = (GameObject)Instantiate (timesSymbol, new Vector3 (-30, .73f, -100), Quaternion.identity);
//		instantiatedTimesSymbol.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.61f);

//		timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
//		long elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
//		System.TimeSpan elapsedSpan = new System.TimeSpan(elapsedTicks);
//		minutesDividedByThirty = (elapsedSpan.Minutes + (elapsedSpan.Hours * 60) + (elapsedSpan.Days * 24 * 60)) / 30;
//		//Debug.Log ("minutesDividedByThirty = " + minutesDividedByThirty);
//		heartNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber ();
//		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (heartNumber - 1);
//		heartNumber = (int)Mathf.Clamp ((heartNumber + minutesDividedByThirty), 0, 5);
//		//Debug.Log ("minutesDividedByThirty = " + minutesDividedByThirty);
//		if (minutesDividedByThirty > 0)
//			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddMinutesToDateTime (minutesDividedByThirty * 30);
//		//Debug.Log ("heartNumber = " + heartNumber);
//		instantiatedNumber = (GameObject)Instantiate (GetNumberFromInt (heartNumber), new Vector3 (-40, 1.23f, -99), Quaternion.identity);
//		instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.94f);
//		instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetExplodeNumber (true);

//		if (heartNumber == 5) {
//			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartLostDateAndTime (System.DateTime.Now.ToUniversalTime ());
//			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (4);
//			firstTimeDigit = (GameObject)Instantiate (countdown3, new Vector3 (-50f, -1.03f, -100), Quaternion.identity);
//			firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.6f);
//
//			secondTimeDigit = (GameObject)Instantiate (countdown0, new Vector3 (-60f, -1.03f, -99), Quaternion.identity);
//			secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.64f);
//
//			thirdTimeDigit = (GameObject)Instantiate (countdown0, new Vector3 (-70f, -1.03f, -98), Quaternion.identity);
//			thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.64f);
//
//			fourthTimeDigit = (GameObject)Instantiate (countdown0, new Vector3 (-80f, -1.03f, -97), Quaternion.identity);
//			fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.6f);
//		}

//		else {
//			timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
//			elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
//			elapsedSpan = new System.TimeSpan(elapsedTicks);
//
//			int minutes = 29 - elapsedSpan.Minutes;
//			int seconds = 59 - elapsedSpan.Seconds;
//
//			firstTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes / 10), new Vector3 (-50f, -1.03f, -100), Quaternion.identity);
//			firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.6f);
//
//			secondTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes % 10), new Vector3 (-60f, -1.03f, -99), Quaternion.identity);
//			secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.64f);
//
//			thirdTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds / 10), new Vector3 (-70f, -1.03f, -98), Quaternion.identity);
//			thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.64f);
//
//			fourthTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds % 10), new Vector3 (-80f, -1.03f, -97), Quaternion.identity);
//			fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.6f);
//		}

//		instantiatedColon = (GameObject)Instantiate (colon, new Vector3 (-65, -1.03f, -100), Quaternion.identity);
//		instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedRetryButton = (GameObject)Instantiate (retryButton, new Vector3 (-20, 0.51f, -101), Quaternion.identity);
		instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedHomeButton = (GameObject)Instantiate (homeButton, new Vector3 (-30, -0.83f, -101), Quaternion.identity);
		instantiatedHomeButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		if (!PlayerPrefs.HasKey ("removeAds")) {
			instantiatedRemoveAdsButton = (GameObject)Instantiate (removeAdsButton, new Vector3 (-40, -2.4f, -101), Quaternion.identity) as GameObject;
			instantiatedRemoveAdsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		}

//		timerStarted = true;
		screenInstantiated = true;
	}

//	void OnApplicationFocus (bool focusStatus) {
//		if (focusStatus && screenInstantiated) {
//			timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
//			long elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
//			System.TimeSpan elapsedSpan = new System.TimeSpan (elapsedTicks);
//			minutesDividedByThirty = (elapsedSpan.Minutes + (elapsedSpan.Hours * 60) + (elapsedSpan.Days * 24 * 60)) / 30;
//			if (minutesDividedByThirty > 0) {
//				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddMinutesToDateTime (minutesDividedByThirty * 30);
//				timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
//				elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
//				elapsedSpan = new System.TimeSpan (elapsedTicks);
//				heartNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber ();
//				heartNumber = (int)Mathf.Clamp ((heartNumber + minutesDividedByThirty), 0, 5);
//				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (heartNumber);
//				instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//				instantiatedNumber = (GameObject)Instantiate (GetNumberFromInt (heartNumber), new Vector3 (-40, 1.23f, -99), Quaternion.identity);
//				instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.94f);
//			}
//
//			int minutes, seconds;
//			if (heartNumber == 5) {
//				minutes = 30;
//				seconds = 0;
//				timerStarted = false;
//			} else {
//				minutes = 29 - elapsedSpan.Minutes;
//				seconds = 59 - elapsedSpan.Seconds;
//			}
//
//			if (GetNameOfInstantiatedNumber (minutes / 10) != firstTimeDigit.name) {
//				firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//				firstTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes / 10), new Vector3 (-10f, -1.03f, -100), Quaternion.identity);
//				firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.6f);
//			}
//
//			if (GetNameOfInstantiatedNumber (minutes % 10) != secondTimeDigit.name) {
//				secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//				secondTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes % 10), new Vector3 (-10, -1.03f, -99), Quaternion.identity);
//				secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.64f);
//			}
//
//			if (GetNameOfInstantiatedNumber (seconds / 10) != thirdTimeDigit.name) {
//				thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//				thirdTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds / 10), new Vector3 (-10, -1.03f, -98), Quaternion.identity);
//				thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.64f);
//			}
//
//			if (GetNameOfInstantiatedNumber (seconds % 10) != fourthTimeDigit.name) {
//				fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//				fourthTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds % 10), new Vector3 (-10, -1.03f, -97), Quaternion.identity);
//				fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.6f);
//			}
//		}
//	}

	public void GetRidOfScreen () {
		screenInstantiated = false;
		timerStarted = false;
		instantiatedLevelFailedBanner.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedHeart.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedTimesSymbol.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedHomeButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
	}

	public bool GetScreenInstantiated () {
		return screenInstantiated;
	}

	string GetNameOfInstantiatedNumber (int number) {
		switch (number) {
		case 0: return "Countdown Number Zero(Clone)";
		case 1: return "Countdown Number One(Clone)";
		case 2: return "Countdown Number Two(Clone)";
		case 3: return "Countdown Number Three(Clone)";
		case 4: return "Countdown Number Four(Clone)";
		case 5: return "Countdown Number Five(Clone)";
		case 6: return "Countdown Number Six(Clone)"; 
		case 7: return "Countdown Number Seven(Clone)";
		case 8: return "Countdown Number Eight(Clone)";
		case 9: return "Countdown Number Nine(Clone)";
		}
		return null;
	}

	GameObject GetNumberFromInt (int number) {
		switch (number) {
		case 0: return zero;
		case 1: return one;
		case 2: return two;
		case 3: return three;
		case 4: return four;
		case 5: return five;
		}
		return null;
	}

	GameObject GetCountdownNumber (int number) {
		switch (number) {
		case 0: return countdown0;
		case 1: return countdown1;
		case 2: return countdown2;
		case 3: return countdown3;
		case 4: return countdown4;
		case 5: return countdown5;
		case 6: return countdown6;
		case 7: return countdown7;
		case 8: return countdown8;
		case 9: return countdown9;
		}
		return null;
	}

	public void ExplodeNumber () {
		soundHandler.PlayJewelBreak ();
		Instantiate (heartExplosion, new Vector3 (instantiatedNumber.transform.position.x, instantiatedNumber.transform.position.y, instantiatedNumber.transform.position.z - 1), Quaternion.Euler (180, 0, 0));
		Destroy (instantiatedNumber);
		heartNumber--;
		instantiatedNumber = (GameObject)Instantiate (GetNumberFromInt (heartNumber), new Vector3 (-40, 1.23f, -99), Quaternion.identity);
		instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.94f);
		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (heartNumber);
		timerStarted = true;
		numberExploded = true;
	}

	public bool NumberExploded () {
		return numberExploded;
	}

	void DecreaseFourthTimeDigitByOne () {
		GameObject tempObject = null;
		switch (fourthTimeDigit.name) {
		case "Countdown Number Zero(Clone)": tempObject = countdown9; 
			if (firstTimeDigit.name == "Countdown Number Zero(Clone)" && secondTimeDigit.name == "Countdown Number Zero(Clone)" && thirdTimeDigit.name == "Countdown Number Zero(Clone)") {
				heartNumber++;
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (heartNumber);
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddMinutesToDateTime (30);
				instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				instantiatedNumber = (GameObject)Instantiate (GetNumberFromInt (heartNumber), new Vector3 (-40, 1.23f, -99), Quaternion.identity);
				instantiatedNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.94f);
				if (heartNumber == 5) {
					Vector3 tempVector2 = firstTimeDigit.transform.position;
					Destroy (firstTimeDigit);
					firstTimeDigit = (GameObject)Instantiate (countdown3, tempVector2, Quaternion.identity);
					firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.6f);
					timerStarted = false;
					return;
				} else {
					DecreaseThirdTimeDigitByOne ();
				}
			} else {
				DecreaseThirdTimeDigitByOne (); 
			}
			break;

		case "Countdown Number One(Clone)": tempObject = countdown0; break;
		case "Countdown Number Two(Clone)": tempObject = countdown1; break;
		case "Countdown Number Three(Clone)": tempObject = countdown2; break;
		case "Countdown Number Four(Clone)": tempObject = countdown3; break;
		case "Countdown Number Five(Clone)": tempObject = countdown4; break;
		case "Countdown Number Six(Clone)": tempObject = countdown5; break;
		case "Countdown Number Seven(Clone)": tempObject = countdown6; break;
		case "Countdown Number Eight(Clone)": tempObject = countdown7; break;
		case "Countdown Number Nine(Clone)": tempObject = countdown8; break;
		}
		Vector3 tempVector = fourthTimeDigit.transform.position;
		Destroy (fourthTimeDigit);
		fourthTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.6f);
	}

	void DecreaseThirdTimeDigitByOne () {
		GameObject tempObject = null;
		switch (thirdTimeDigit.name) {
		case "Countdown Number Zero(Clone)": tempObject = countdown5; DecreaseSecondTimeDigitByOne (); break;
		case "Countdown Number One(Clone)": tempObject = countdown0; break;
		case "Countdown Number Two(Clone)": tempObject = countdown1; break;
		case "Countdown Number Three(Clone)": tempObject = countdown2; break;
		case "Countdown Number Four(Clone)": tempObject = countdown3; break;
		case "Countdown Number Five(Clone)": tempObject = countdown4; break;
		}
		Vector3 tempVector = thirdTimeDigit.transform.position;
		Destroy (thirdTimeDigit);
		thirdTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.64f);
	}

	void DecreaseSecondTimeDigitByOne () {
		GameObject tempObject = null;
		switch (secondTimeDigit.name) {
		case "Countdown Number Zero(Clone)": tempObject = countdown9; DecreaseFirstTimeDigitByOne (); break;
		case "Countdown Number One(Clone)": tempObject = countdown0; break;
		case "Countdown Number Two(Clone)": tempObject = countdown1; break;
		case "Countdown Number Three(Clone)": tempObject = countdown2; break;
		case "Countdown Number Four(Clone)": tempObject = countdown3; break;
		case "Countdown Number Five(Clone)": tempObject = countdown4; break;
		case "Countdown Number Six(Clone)": tempObject = countdown5; break;
		case "Countdown Number Seven(Clone)": tempObject = countdown6; break;
		case "Countdown Number Eight(Clone)": tempObject = countdown7; break;
		case "Countdown Number Nine(Clone)": tempObject = countdown8; break;
		}
		Vector3 tempVector = secondTimeDigit.transform.position;
		Destroy (secondTimeDigit);
		secondTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.64f);
	}

	void DecreaseFirstTimeDigitByOne () {
		GameObject tempObject = null;
		switch (firstTimeDigit.name) {
		case "Countdown Number Zero(Clone)": tempObject = countdown2; heartNumber++; break;
		case "Countdown Number One(Clone)": tempObject = countdown0; break;
		case "Countdown Number Two(Clone)": tempObject = countdown1; break;
		case "Countdown Number Three(Clone)": tempObject = countdown2; break;
		}
		Vector3 tempVector = firstTimeDigit.transform.position;
		Destroy (firstTimeDigit);
		firstTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.6f);
	}
}
