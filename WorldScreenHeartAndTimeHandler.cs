using UnityEngine;
using System.Collections;

public class WorldScreenHeartAndTimeHandler : MonoBehaviour {

	public GameObject heart, timesSymbol, heartNumberZero, heartNumberOne, heartNumberTwo, heartNumberThree, heartNumberFour, heartNumberFive, colon;
	public GameObject timeNumberZero, timeNumberOne, timeNumberTwo, timeNumberThree, timeNumberFour, timeNumberFive, timeNumberSix, timeNumberSeven, timeNumberEight, timeNumberNine;
	GameObject instantiatedHeart, instantiatedHeartNumber, instantiatedColon, firstDigit, secondDigit, thirdDigit, fourthDigit;
	int heartNumber;
	Vector3 heartNumberPosition;
	System.DateTime timeRemaining;
	long elapsedTicks;
	System.TimeSpan elapsedSpan;
	float cooldown, timeStamp;
	Vector3 firstDigitPosition, secondDigitPosition, thirdDigitPosition, fourthDigitPosition;
	bool stillZero, timerStarted;

	// Use this for initialization
	void Start () {
		timeStamp = Time.time;
		cooldown = 1;

		firstDigitPosition = Camera.main.ViewportToWorldPoint (new Vector3 (.04f, .92f, 149f));
		secondDigitPosition = Camera.main.ViewportToWorldPoint (new Vector3 (.0775f, .92f, 149f));
		thirdDigitPosition = Camera.main.ViewportToWorldPoint (new Vector3 (.1425f, .92f, 149f));
		fourthDigitPosition = Camera.main.ViewportToWorldPoint (new Vector3 (.18f, .92f, 149f));

		timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();

		elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
		elapsedSpan = new System.TimeSpan(elapsedTicks);
		int minutesDividedByThirty = (elapsedSpan.Minutes + (elapsedSpan.Hours * 60) + (elapsedSpan.Days * 24 * 60)) / 30;
		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber () + minutesDividedByThirty);
		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddMinutesToDateTime (30 * minutesDividedByThirty);
		//Debug.Log ("MinutesDividedBy30 = " + minutesDividedByThirty);

		Instantiate (heart, Camera.main.ViewportToWorldPoint (new Vector3 (.05f, .97f, 149f)), Quaternion.identity);
		Instantiate (timesSymbol, Camera.main.ViewportToWorldPoint (new Vector3 (.11f, .96f, 149f)), Quaternion.identity);	
		heartNumberPosition = Camera.main.ViewportToWorldPoint (new Vector3 (.16f, .9675f, 149f));
		instantiatedColon = (GameObject)Instantiate (colon, Camera.main.ViewportToWorldPoint (new Vector3 (.11f, .92f, 149f)), Quaternion.identity);
		heartNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber ();
		//Debug.Log ("heartNumber = " + heartNumber);
		InstantiateHeartNumber ();

		int minutes, seconds;

		if (heartNumber == 5) {
			minutes = 30;
			seconds = 0;

		}
		else {
			timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
			elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
			elapsedSpan = new System.TimeSpan(elapsedTicks);
			
			minutes = 29 - elapsedSpan.Minutes;
			seconds = 59 - elapsedSpan.Seconds;
			timerStarted = true;
		}
		//Debug.Log ("minutes = " + minutes);
		firstDigit = (GameObject)Instantiate (GetTimeNumber (minutes / 10), Camera.main.ViewportToWorldPoint (new Vector3 (.04f, .92f, 149f)), Quaternion.identity);
		secondDigit = (GameObject)Instantiate (GetTimeNumber (minutes % 10), Camera.main.ViewportToWorldPoint (new Vector3 (.0775f, .92f, 149f)), Quaternion.identity);
		thirdDigit = (GameObject)Instantiate (GetTimeNumber (seconds / 10), Camera.main.ViewportToWorldPoint (new Vector3 (.1425f, .92f, 149f)), Quaternion.identity);
		fourthDigit = (GameObject)Instantiate (GetTimeNumber (seconds % 10), Camera.main.ViewportToWorldPoint (new Vector3 (.18f, .92f, 149f)), Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
		if (timerStarted && Time.time > timeStamp + cooldown) {
			DecreaseFourthTimeDigit ();
			timeStamp = Time.time;
		}
	}

	void OnApplicationFocus (bool focusStatus) {
		if (focusStatus) {
			GameManagerScript gameManagerScript = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ();

			GameObject camera = GameObject.Find ("Main Camera");
			System.DateTime timeRemaining = gameManagerScript.GetHeartLostDateAndTime ();
			long elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
			System.TimeSpan elapsedSpan = new System.TimeSpan(elapsedTicks);

			int minutesDividedByThiry = elapsedSpan.Minutes / 30;
			if (minutesDividedByThiry > 0) {
				gameManagerScript.SetHeartNumber (gameManagerScript.GetHeartNumber () + minutesDividedByThiry);
				gameManagerScript.AddMinutesToDateTime (minutesDividedByThiry * 30);
			}

			if (gameManagerScript.GetHeartNumber () == 5) {
				timerStarted = false;
				heartNumber = gameManagerScript.GetHeartNumber ();
				InstantiateHeartNumber ();

				Destroy (firstDigit);
				Destroy (secondDigit);
				Destroy (thirdDigit);
				Destroy (fourthDigit);

				firstDigit = (GameObject)Instantiate (timeNumberThree, firstDigitPosition, Quaternion.identity);
				secondDigit = (GameObject)Instantiate (timeNumberZero, secondDigitPosition, Quaternion.identity);
				thirdDigit = (GameObject)Instantiate (timeNumberZero, thirdDigitPosition, Quaternion.identity);
				fourthDigit = (GameObject)Instantiate (timeNumberZero, fourthDigitPosition, Quaternion.identity);
			}
 			
			else {
				heartNumber = gameManagerScript.GetHeartNumber ();
				InstantiateHeartNumber ();

				timeRemaining = gameManagerScript.GetHeartLostDateAndTime ();
				elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
				elapsedSpan = new System.TimeSpan (elapsedTicks);

				int minutes = 29 - elapsedSpan.Minutes;
				int seconds = 59 - elapsedSpan.Seconds;

				Destroy (firstDigit);
				Destroy (secondDigit);
				Destroy (thirdDigit);
				Destroy (fourthDigit);

				firstDigit = (GameObject)Instantiate (GetTimeNumber (minutes / 10), firstDigitPosition, Quaternion.identity);
				secondDigit = (GameObject)Instantiate (GetTimeNumber (minutes % 10), secondDigitPosition, Quaternion.identity);
				thirdDigit = (GameObject)Instantiate (GetTimeNumber (seconds / 10), thirdDigitPosition, Quaternion.identity);
				fourthDigit = (GameObject)Instantiate (GetTimeNumber (seconds % 10), fourthDigitPosition, Quaternion.identity);
			}
						
		}
	}


	void DecreaseFourthTimeDigit () {
		stillZero = false;
		Destroy (fourthDigit);
		switch (fourthDigit.name) {
		case "Time Number Zero(Clone)": 
			fourthDigit = (GameObject)Instantiate (timeNumberNine, fourthDigitPosition, Quaternion.identity);
			stillZero = true;
			DecreaseThirdTimeDigit (); break;
		case "Time Number One(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberZero, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Two(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberOne, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Three(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberTwo, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Four(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberThree, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Five(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberFour, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Six(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberFive, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Seven(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberSix, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Eight(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberSeven, fourthDigitPosition, Quaternion.identity); break;
		case "Time Number Nine(Clone)": fourthDigit = (GameObject)Instantiate (timeNumberEight, fourthDigitPosition, Quaternion.identity); break;
		}
	}

	void DecreaseThirdTimeDigit () {
		Destroy (thirdDigit);
		switch (thirdDigit.name) {
		case "Time Number Zero(Clone)": 
			thirdDigit = (GameObject)Instantiate (timeNumberFive, thirdDigitPosition, Quaternion.identity);
			DecreaseSecondTimeDigit (); break;
		case "Time Number One(Clone)": stillZero = false; thirdDigit = (GameObject)Instantiate (timeNumberZero, thirdDigitPosition, Quaternion.identity); break;
		case "Time Number Two(Clone)": stillZero = false; thirdDigit = (GameObject)Instantiate (timeNumberOne, thirdDigitPosition, Quaternion.identity); break;
		case "Time Number Three(Clone)": stillZero = false; thirdDigit = (GameObject)Instantiate (timeNumberTwo, thirdDigitPosition, Quaternion.identity); break;
		case "Time Number Four(Clone)": stillZero = false; thirdDigit = (GameObject)Instantiate (timeNumberThree, thirdDigitPosition, Quaternion.identity); break;
		case "Time Number Five(Clone)": stillZero = false; thirdDigit = (GameObject)Instantiate (timeNumberFour, thirdDigitPosition, Quaternion.identity); break;
		}
	}

	void DecreaseSecondTimeDigit () {
		Destroy (secondDigit);
		switch (secondDigit.name) {
		case "Time Number Zero(Clone)": 
			secondDigit = (GameObject)Instantiate (timeNumberNine, secondDigitPosition, Quaternion.identity);
			DecreaseFirstTimeDigit (); break;
		case "Time Number One(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberZero, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Two(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberOne, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Three(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberTwo, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Four(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberThree, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Five(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberFour, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Six(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberFive, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Seven(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberSix, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Eight(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberSeven, secondDigitPosition, Quaternion.identity); break;
		case "Time Number Nine(Clone)": stillZero = false; secondDigit = (GameObject)Instantiate (timeNumberEight, secondDigitPosition, Quaternion.identity); break;
		}
	}

	void DecreaseFirstTimeDigit () {
		Destroy (firstDigit);
		switch (firstDigit.name) {
		case "Time Number Zero(Clone)": 
			if (stillZero) {
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (heartNumber + 1);
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddMinutesToDateTime (30);
				heartNumber++;
				InstantiateHeartNumber ();
				if (heartNumber == 5) {
					Destroy (firstDigit);
					Destroy (secondDigit);
					Destroy (thirdDigit);
					Destroy (fourthDigit);
					firstDigit = (GameObject)Instantiate (timeNumberThree, firstDigitPosition, Quaternion.identity); 
					secondDigit = (GameObject)Instantiate (timeNumberZero, secondDigitPosition, Quaternion.identity);
					thirdDigit = (GameObject)Instantiate (timeNumberZero, thirdDigitPosition, Quaternion.identity);
					fourthDigit = (GameObject)Instantiate (timeNumberZero, fourthDigitPosition, Quaternion.identity);
					timerStarted = false;
					return;
				}
			}
			firstDigit = (GameObject)Instantiate (timeNumberTwo, firstDigitPosition, Quaternion.identity); break;
		case "Time Number One(Clone)": firstDigit = (GameObject)Instantiate (timeNumberZero, firstDigitPosition, Quaternion.identity); break;
		case "Time Number Two(Clone)": firstDigit = (GameObject)Instantiate (timeNumberOne, firstDigitPosition, Quaternion.identity); break;
		}
	}

	void InstantiateHeartNumber () {
		if (instantiatedHeartNumber != null) {
			Destroy (instantiatedHeartNumber);
		}
		switch (heartNumber) {
		case 0: instantiatedHeartNumber = (GameObject)Instantiate (heartNumberZero, heartNumberPosition, Quaternion.identity); break;
		case 1: instantiatedHeartNumber = (GameObject)Instantiate (heartNumberOne, heartNumberPosition, Quaternion.identity); break;
		case 2: instantiatedHeartNumber = (GameObject)Instantiate (heartNumberTwo, heartNumberPosition, Quaternion.identity); break;
		case 3: instantiatedHeartNumber = (GameObject)Instantiate (heartNumberThree, heartNumberPosition, Quaternion.identity); break;
		case 4: instantiatedHeartNumber = (GameObject)Instantiate (heartNumberFour, heartNumberPosition, Quaternion.identity); break;
		case 5: instantiatedHeartNumber = (GameObject)Instantiate (heartNumberFive, heartNumberPosition, Quaternion.identity); break;
		}
	}

	GameObject GetTimeNumber (int number) {
		switch (number) {
		case 0: return timeNumberZero;
		case 1: return timeNumberOne;
		case 2: return timeNumberTwo;
		case 3: return timeNumberThree;
		case 4: return timeNumberFour;
		case 5: return timeNumberFive;
		case 6: return timeNumberSix;
		case 7: return timeNumberSeven;
		case 8: return timeNumberEight;
		case 9: return timeNumberNine;
		}
		return null;
	}
}
