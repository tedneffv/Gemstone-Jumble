using UnityEngine;
using System.Collections;

public class NoMoreLivesScreenHandler : MonoBehaviour {

	public GameObject noMoreLivesText, purchaseLivesButton, purchaseCoinsButton, returnHomeButton, playLevelButton, retryButton, timeLeftText, heart;
	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, colon, shade;
	GameObject instantiatedNoMoreLivesText, instantiatedPurchaseLivesButton, instantiatedPurchaseCoinsButton, instantiatedReturnHomeButton, instantiatedRetryButton, instantiatedTimeLeftText;
	GameObject instantiatedPlayLevelButton;
	GameObject firstTimeDigit, secondTimeDigit, instantiatedColon, thirdTimeDigit, fourthTimeDigit, instantiatedHeart, instantiatedShade;
	bool timerStarted, screenInstantiated, shadeInstantiated, destroyShade;
	int heartNumber; 
	string levelToPlay;
	float timeStamp, cooldown;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		if (GameObject.Find ("Movement Controller") != null)
			GameObject.Find ("Movement Controller").GetComponent<TheMountainMovementController> ().SetOkayToExit (false);
		cooldown = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape) && shadeInstantiated && screenInstantiated && GameObject.Find ("29 block") != null) {
			GameObject.Find ("Movement Controller").GetComponent<TheMountainMovementController> ().SetOkayToExit (true);
			screenInstantiated = false;
			shadeInstantiated = false;
			GetRidOfScreen ();
			destroyShade = true;
			GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().GetRidOfScreen ();
		}


		if (timerStarted && Time.time > timeStamp + cooldown) {
			DecreaseFourthTimeDigitByOne ();
			timeStamp = Time.time;
		}
	
		if (shadeInstantiated && spriteRenderer != null && spriteRenderer.color.a < .7) {
			oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
		}
		else if (destroyShade && !shadeInstantiated) {
			if (spriteRenderer.color.a > 0) {
				oldColor = spriteRenderer.color;
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
			} else {
				destroyShade = false;
				Destroy (instantiatedShade);
			}
		}

	}

	public void CameraOffsetNoLivesScreen () {
		soundHandler.PlayWoosh ();
		heartNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber ();
		GameObject camera = GameObject.Find ("Main Camera");

		Debug.Log ("shadeInstantiated = " + shadeInstantiated);

//		if (!shadeInstantiated) {
//			instantiatedShade = (GameObject)Instantiate (shade, new Vector3 (camera.transform.position.x, camera.transform.position.y, -50), Quaternion.identity);
//			instantiatedShade.transform.parent = camera.transform;
//			spriteRenderer = instantiatedShade.GetComponent<SpriteRenderer> ();
//			shadeInstantiated = true;
//		}

//		instantiatedNoMoreLivesText = (GameObject)Instantiate (noMoreLivesText, new Vector3 (-10, camera.transform.position.y + 3f, -100), Quaternion.identity);
//		instantiatedNoMoreLivesText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
//		instantiatedNoMoreLivesText.transform.parent = camera.transform;

		instantiatedPurchaseLivesButton = (GameObject)Instantiate (purchaseLivesButton, new Vector3 (-20, camera.transform.position.y + 2.25f, -100), Quaternion.identity);
		instantiatedPurchaseLivesButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedPurchaseLivesButton.transform.parent = camera.transform;
		if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber () > 0) {
			instantiatedPurchaseLivesButton.GetComponent<NoLivesButtonHandler> ().SetTouchOn (false);
			SpriteRenderer temp = instantiatedPurchaseLivesButton.GetComponent<SpriteRenderer> ();
			temp.color = new Color (temp.color.r, temp.color.g, temp.color.b, .5f);
		}

		instantiatedPurchaseCoinsButton = (GameObject)Instantiate (purchaseCoinsButton, new Vector3 (-30, camera.transform.position.y + (.75f), -100), Quaternion.identity);
		instantiatedPurchaseCoinsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedPurchaseCoinsButton.transform.parent = camera.transform;

		instantiatedReturnHomeButton = (GameObject)Instantiate (returnHomeButton, new Vector3 (-40, camera.transform.position.y + (-.75f), -100), Quaternion.identity);
		instantiatedReturnHomeButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedReturnHomeButton.transform.parent = camera.transform;

		if (heartNumber == 5) {
			if (GameObject.Find ("0 block") != null) {
				instantiatedPlayLevelButton = (GameObject)Instantiate (playLevelButton, new Vector3 (-50, GameObject.Find ("Main Camera").transform.position.y - 2.25f, -100), Quaternion.identity);
				instantiatedPlayLevelButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
				instantiatedPlayLevelButton.transform.parent = GameObject.Find ("Main Camera").transform;
			} else {
				instantiatedRetryButton = (GameObject)Instantiate (retryButton, new Vector3 (-50, GameObject.Find ("Main Camera").transform.position.y + (-2.25f), -100), Quaternion.identity);
				instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
				instantiatedRetryButton.transform.parent = GameObject.Find ("Main Camera").transform;
			}
			return;
		}

		System.DateTime timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
		long elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
		System.TimeSpan elapsedSpan = new System.TimeSpan(elapsedTicks);
		
		int minutes = 29 - elapsedSpan.Minutes;
		int seconds = 59 - elapsedSpan.Seconds;
		
		if (minutes >= 0) {
			
			instantiatedTimeLeftText = (GameObject)Instantiate (timeLeftText, new Vector3 (-50, camera.transform.position.y + (-2f), -100), Quaternion.identity);
			instantiatedTimeLeftText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			instantiatedTimeLeftText.transform.parent = camera.transform;
			
			firstTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes / 10), new Vector3 (-60, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
			firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.16f);
			firstTimeDigit.transform.parent = camera.transform;
			
			secondTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes % 10), new Vector3 (-70, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
			secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.41f);
			secondTimeDigit.transform.parent = camera.transform;
			
			instantiatedColon = (GameObject)Instantiate (colon, new Vector3 (-80, camera.transform.position.y + (-2.56f), -100), Quaternion.identity);
			instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.6f);
			instantiatedColon.transform.parent = camera.transform;
			
			thirdTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds / 10), new Vector3 (-90, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
			thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.8f);
			thirdTimeDigit.transform.parent = camera.transform;
			
			fourthTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds % 10), new Vector3 (-100, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
			fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (2.05f);
			fourthTimeDigit.transform.parent = camera.transform;
			
			timerStarted = true;
			timeStamp = Time.time;
		}
		screenInstantiated = true;
	}

	public void InstantiateNoLivesScreen () {
		CameraOffsetNoLivesScreen ();
		return;
		soundHandler.PlayWoosh ();
		if (GameObject.Find ("29 block") != null) {
			CameraOffsetNoLivesScreen ();
			return;
		}

		heartNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber ();

		instantiatedNoMoreLivesText = (GameObject)Instantiate (noMoreLivesText, new Vector3 (-10, 3f, -100), Quaternion.identity);
		instantiatedNoMoreLivesText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		
		instantiatedPurchaseLivesButton = (GameObject)Instantiate (purchaseLivesButton, new Vector3 (-20, .5f, -100), Quaternion.identity);
		instantiatedPurchaseLivesButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		
		instantiatedPurchaseCoinsButton = (GameObject)Instantiate (purchaseCoinsButton, new Vector3 (-30, -1f, -100), Quaternion.identity);
		instantiatedPurchaseCoinsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		
		instantiatedReturnHomeButton = (GameObject)Instantiate (returnHomeButton, new Vector3 (-40, -2.5f, -100), Quaternion.identity);
		instantiatedReturnHomeButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		if (heartNumber == 0) {
			
			System.DateTime timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
			long elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
			System.TimeSpan elapsedSpan = new System.TimeSpan(elapsedTicks);
			
			int minutes = 29 - elapsedSpan.Minutes;
			int seconds = 59 - elapsedSpan.Seconds;
			
			if (minutes >= 0) {
				
				instantiatedTimeLeftText = (GameObject)Instantiate (timeLeftText, new Vector3 (-50, -3.75f, -100), Quaternion.identity);
				instantiatedTimeLeftText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
				
				firstTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes / 10), new Vector3 (-60, -4.3f, -100), Quaternion.identity);
				firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.16f);
				
				secondTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes % 10), new Vector3 (-70, -4.3f, -100), Quaternion.identity);
				secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.41f);
				
				instantiatedColon = (GameObject)Instantiate (colon, new Vector3 (-80, -4.31f, -100), Quaternion.identity);
				instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.6f);
				
				thirdTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds / 10), new Vector3 (-90, -4.3f, -100), Quaternion.identity);
				thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.8f);
				
				fourthTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds % 10), new Vector3 (-100, -4.3f, -100), Quaternion.identity);
				fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (2.05f);
				
				timerStarted = true;
				timeStamp = Time.time;
			}

		} else {
			if (GameObject.Find ("0 block") != null) {
				timerStarted = false;
				instantiatedPlayLevelButton = (GameObject)Instantiate (playLevelButton, new Vector3 (-50, -4, -100), Quaternion.identity);
				instantiatedPlayLevelButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			} else {
				instantiatedRetryButton = (GameObject)Instantiate (retryButton, new Vector3 (-50, -4, -100), Quaternion.identity);
				instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			}
		}
		screenInstantiated = true;
	}

	public void GetRidOfScreen () {
		screenInstantiated = false;
		timerStarted = false;
//		instantiatedNoMoreLivesText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedPurchaseLivesButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedPurchaseCoinsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedReturnHomeButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		if (instantiatedTimeLeftText != null) {
			instantiatedTimeLeftText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		}
		if (instantiatedRetryButton != null) {
			instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		}
		if (instantiatedPlayLevelButton != null) {
			instantiatedPlayLevelButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		}
	}

	void OnApplicationFocus (bool focusStatus) {
		if (screenInstantiated && focusStatus && instantiatedRetryButton == null) {
			GameObject camera = GameObject.Find ("Main Camera");
			System.DateTime timeRemaining = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartLostDateAndTime ();
			long elapsedTicks = System.DateTime.Now.ToUniversalTime ().Ticks - timeRemaining.Ticks;
			System.TimeSpan elapsedSpan = new System.TimeSpan(elapsedTicks);
			
			int minutes = 29 - elapsedSpan.Minutes;
			int seconds = 59 - elapsedSpan.Seconds;

			if (minutes >= 0) {
				if (GetNameOfInstantiatedNumber (minutes / 10) != firstTimeDigit.name) {
					firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
					firstTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes / 10), new Vector3 (-10, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
					firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.16f);
					firstTimeDigit.transform.parent = camera.transform;
				}
				
				if (GetNameOfInstantiatedNumber (minutes % 10) != secondTimeDigit.name) {
					secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
					secondTimeDigit = (GameObject)Instantiate (GetCountdownNumber (minutes % 10), new Vector3 (-20, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
					secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.41f);
					secondTimeDigit.transform.parent = camera.transform;
				}
				
				if (GetNameOfInstantiatedNumber (seconds / 10) != thirdTimeDigit.name) {
					thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
					thirdTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds / 10), new Vector3 (-30, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
					thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.8f);
					thirdTimeDigit.transform.parent = camera.transform;
				}
				
				if (GetNameOfInstantiatedNumber (seconds % 10) != fourthTimeDigit.name) {
					fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
					fourthTimeDigit = (GameObject)Instantiate (GetCountdownNumber (seconds % 10), new Vector3 (-40, camera.transform.position.y + (-2.55f), -100), Quaternion.identity);
					fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (2.05f);
					fourthTimeDigit.transform.parent = camera.transform;
				}
			}
			else if (GameObject.Find ("0 block") != null && instantiatedPlayLevelButton == null) {
				timerStarted = false;
				instantiatedTimeLeftText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);

				instantiatedPlayLevelButton = (GameObject)Instantiate (playLevelButton, new Vector3 (-50, camera.transform.position.y - 2.25f, -100), Quaternion.identity);
				instantiatedPlayLevelButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
				instantiatedPlayLevelButton.transform.parent = camera.transform;
			}
			else if (instantiatedRetryButton == null) {
				timerStarted = false;
				instantiatedTimeLeftText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);

				instantiatedRetryButton = (GameObject)Instantiate (retryButton, new Vector3 (-50, camera.transform.position.y + (-2.25f), -100), Quaternion.identity);
				instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
				instantiatedRetryButton.transform.parent = camera.transform;
			}

		}
	}

	string GetNameOfInstantiatedNumber (int number) {
		switch (number) {
		case 0: return "No Lives Number Zero(Clone)";
		case 1: return "No Lives Number One(Clone)";
		case 2: return "No Lives Number Two(Clone)";
		case 3: return "No Lives Number Three(Clone)";
		case 4: return "No Lives Number Four(Clone)";
		case 5: return "No Lives Number Five(Clone)";
		case 6: return "No Lives Number Six(Clone)";
		case 7: return "No Lives Number Seven(Clone)"; 
		case 8: return "No Lives Number Eight(Clone)";
		case 9: return "No Lives Number Nine(Clone)";
		}
		return null;
	}

	GameObject GetCountdownNumber (int number) {
		switch (number) {
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

	void DecreaseFourthTimeDigitByOne () {
		GameObject tempObject = null;
		switch (fourthTimeDigit.name) {
		case "No Lives Number Zero(Clone)": tempObject = nine; 
			if (firstTimeDigit.name == "No Lives Number Zero(Clone)" && secondTimeDigit.name == "No Lives Number Zero(Clone)" && thirdTimeDigit.name == "No Lives Number Zero(Clone)") {
				heartNumber++;
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (heartNumber);
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddMinutesToDateTime (30);
				instantiatedTimeLeftText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				instantiatedColon.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
				fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);

				if (GameObject.Find ("0 block") != null) {
					instantiatedPlayLevelButton = (GameObject)Instantiate (playLevelButton, new Vector3 (-10, GameObject.Find ("Main Camera").transform.position.y - 2.25f, -100), Quaternion.identity);
					instantiatedPlayLevelButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
					instantiatedPlayLevelButton.transform.parent = GameObject.Find ("Main Camera").transform;
				} else {
					instantiatedRetryButton = (GameObject)Instantiate (retryButton, new Vector3 (-10, GameObject.Find ("Main Camera").transform.position.y + (-2.25f), -100), Quaternion.identity);
					instantiatedRetryButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
					instantiatedRetryButton.transform.parent = GameObject.Find ("Main Camera").transform;
				}

				timerStarted = false;
				return;
			} else {
				DecreaseThirdTimeDigitByOne (); 
			}
			break;
			
		case "No Lives Number One(Clone)": tempObject = zero; break;
		case "No Lives Number Two(Clone)": tempObject = one; break;
		case "No Lives Number Three(Clone)": tempObject = two; break;
		case "No Lives Number Four(Clone)": tempObject = three; break;
		case "No Lives Number Five(Clone)": tempObject = four; break;
		case "No Lives Number Six(Clone)": tempObject = five; break;
		case "No Lives Number Seven(Clone)": tempObject = six; break;
		case "No Lives Number Eight(Clone)": tempObject = seven; break;
		case "No Lives Number Nine(Clone)": tempObject = eight; break;
		}
		Vector3 tempVector = fourthTimeDigit.transform.position;
		Destroy (fourthTimeDigit);
		fourthTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		fourthTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (2.05f);
		fourthTimeDigit.transform.parent = GameObject.Find ("Main Camera").transform;
	}
	
	void DecreaseThirdTimeDigitByOne () {
		GameObject tempObject = null;
		switch (thirdTimeDigit.name) {
		case "No Lives Number Zero(Clone)": tempObject = five; DecreaseSecondTimeDigitByOne (); break;
		case "No Lives Number One(Clone)": tempObject = zero; break;
		case "No Lives Number Two(Clone)": tempObject = one; break;
		case "No Lives Number Three(Clone)": tempObject = two; break;
		case "No Lives Number Four(Clone)": tempObject = three; break;
		case "No Lives Number Five(Clone)": tempObject = four; break;
		}
		Vector3 tempVector = thirdTimeDigit.transform.position;
		Destroy (thirdTimeDigit);
		thirdTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		thirdTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.8f);
		thirdTimeDigit.transform.parent = GameObject.Find ("Main Camera").transform;
	}
	
	void DecreaseSecondTimeDigitByOne () {
		GameObject tempObject = null;
		switch (secondTimeDigit.name) {
		case "No Lives Number Zero(Clone)": tempObject = nine; DecreaseFirstTimeDigitByOne (); break;
		case "No Lives Number One(Clone)": tempObject = zero; break;
		case "No Lives Number Two(Clone)": tempObject = one; break;
		case "No Lives Number Three(Clone)": tempObject = two; break;
		case "No Lives Number Four(Clone)": tempObject = three; break;
		case "No Lives Number Five(Clone)": tempObject = four; break;
		case "No Lives Number Six(Clone)": tempObject = five; break;
		case "No Lives Number Seven(Clone)": tempObject = six; break;
		case "No Lives Number Eight(Clone)": tempObject = seven; break;
		case "No Lives Number Nine(Clone)": tempObject = eight; break;
		}
		Vector3 tempVector = secondTimeDigit.transform.position;
		Destroy (secondTimeDigit);
		secondTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		secondTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.41f);
		secondTimeDigit.transform.parent = GameObject.Find ("Main Camera").transform;
	}
	
	void DecreaseFirstTimeDigitByOne () {
		GameObject tempObject = null;
		switch (firstTimeDigit.name) {
		case "No Lives Number Zero(Clone)": tempObject = two; heartNumber++; break;
		case "No Lives Number One(Clone)": tempObject = zero; break;
		case "No Lives Number Two(Clone)": tempObject = one; break;
		case "No Lives Number Three(Clone)": tempObject = two; break;
		}
		Vector3 tempVector = firstTimeDigit.transform.position;
		Destroy (firstTimeDigit);
		firstTimeDigit = (GameObject)Instantiate (tempObject, tempVector, Quaternion.identity);
		firstTimeDigit.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.16f);
		firstTimeDigit.transform.parent = GameObject.Find ("Main Camera").transform;
	}

	public bool GetScreenInstantiated () {
		return screenInstantiated;
	}

	public void SetLevelToPlay (string level) {
		this.levelToPlay = level;
	}

	public string GetLevelToPlay () {
		return levelToPlay;
	}

	public void SetShadeInstantiated (bool shadeInstantiated) {
		this.shadeInstantiated = shadeInstantiated;
	}
}
