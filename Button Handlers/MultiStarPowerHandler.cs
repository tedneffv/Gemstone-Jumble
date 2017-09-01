using UnityEngine;
using System.Collections;
using Soomla.Store;

public class MultiStarPowerHandler : MonoBehaviour {
	Collider2D hit, hit2, hit3;
	float targetX, targetY, timeStamp, cooldown, incrementalAngle;
	bool goHome, leaveHome, launchStars, numbersInstantiated, numbersSentHome, rotateNumbers, touchOn;
	int launchStarCount;
	public GameObject coinNumberTwo, coinNumberFive, coinNumberZero;
	public GameObject blueHomingStar, greenHomingStar, orangeHomingStar, purpleHomingStar, redHomingStar, whiteHomingStar;
	public GameObject pressedMultiStarPowerButton, redFallingStar, blueFallingStar, greenFallingStar, orangeFallingStar, whiteFallingStar, purpleFallingStar;
	public GameObject redFallingStarExplosion, blueFallingStarExplosion, greenFallingStarExplosion, orangeFallingStarExplosion, whiteFallingStarExplosion, purpleFallingStarExplosion;
	GameObject instantiatedPressedMultiStarPowerButton, instantiatedCoinNumberOne, instantiatedCoinNumberTwo, instantiatedCoinNumberThree, coinNumberParent, circleObject;
	PowerButtonCurrentStates currentStates;
	CoinTotalManager coinTotalManager;
	SoundHandler soundHandler;
	RockLevelTouchHandler touchHandler;
	PowerPercentageController powerPercentageController;
	
	// Use this for initialization
	void Start () {
		cooldown = .25f;
		timeStamp = Time.time;
		launchStarCount = 0;
		currentStates = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonCurrentStates> ();
		if (currentStates.GetMultiStarPowerPressed ()) {
			float totalCircleAngle = Mathf.Rad2Deg * (2 * (Mathf.Atan (((transform.position.x)/(transform.position.x * .8930232558f)))));
			float incrementalCircleAngle = totalCircleAngle / 6;
			instantiatedPressedMultiStarPowerButton = (GameObject)Instantiate (pressedMultiStarPowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedMultiStarPowerButton.transform.parent = transform;
			instantiatedPressedMultiStarPowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 5));
		}
		coinTotalManager = GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ();
		incrementalAngle = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().GetIncrementalCircleAngle ();
		circleObject = GameObject.Find ("Multi Star Circle");
		touchOn = true;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		powerPercentageController = GameObject.Find ("Level Controller").GetComponent<PowerPercentageController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (numbersInstantiated && instantiatedCoinNumberOne == null && instantiatedCoinNumberTwo == null && instantiatedCoinNumberThree == null) {
			numbersInstantiated = false;
		}
		if (!goHome && !numbersInstantiated && !currentStates.GetMultiStarPowerPressed () && Mathf.Abs (Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - (360 - (incrementalAngle * 5)))) < 4f) {
			instantiatedCoinNumberOne = (GameObject)Instantiate (coinNumberTwo, new Vector3 (targetX, targetY, -6), Quaternion.identity);
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberTwo = (GameObject)Instantiate (coinNumberFive, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -22.5f)));
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberThree = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -45)));
			PowerButtonHandler.increaseCoinNumber ();
			if (GameObject.Find ("Multi Star Coin Holder") != null) {
				coinNumberParent = GameObject.Find ("Multi Star Coin Holder");
			}
			else {
				coinNumberParent = new GameObject ();
				coinNumberParent.transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);
				coinNumberParent.name = "Multi Star Coin Holder";
			}

			instantiatedCoinNumberOne.transform.parent = coinNumberParent.transform;
			instantiatedCoinNumberTwo.transform.parent = coinNumberParent.transform;
			instantiatedCoinNumberThree.transform.parent = coinNumberParent.transform;

			instantiatedCoinNumberOne.name = "Power Coin One";
			instantiatedCoinNumberTwo.name = "Power Coin Two";
			instantiatedCoinNumberThree.name = "Power Coin Three";
			instantiatedCoinNumberOne.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			instantiatedCoinNumberTwo.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			instantiatedCoinNumberThree.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			numbersInstantiated = true;
			numbersSentHome = false;
		}
		if (goHome && numbersInstantiated && !numbersSentHome) {
			instantiatedCoinNumberOne.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			instantiatedCoinNumberTwo.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			instantiatedCoinNumberThree.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			numbersSentHome = true;
		}

		if (goHome && PowerButtonHandler.GetCoinNumberCount () == 0)
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime * 8f);
		else if (!goHome)
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, -incrementalAngle * 5), Time.deltaTime * 8f);
		if (goHome && Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - ((0))) < .1f) {
			Destroy (circleObject);
			Destroy (gameObject);
		}

		if (!currentStates.GetMultiStarPowerPressed () && touchOn && touchHandler.GetGameStarted ()) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			}
			
			if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			}
			
			if (Input.GetMouseButtonUp (0)) {
//				if (instantiatedPressedMultiStarPowerButton != null) 
//					Destroy (instantiatedPressedMultiStarPowerButton);
				CheckTouch3 (Input.mousePosition);
			}
		}

//		if (launchStars && Time.time >= timeStamp + cooldown) {
//			soundHandler.PlayPowerUpSound ();
//			GameObject tempStar = (GameObject)Instantiate (GetRandomHomingStar (), new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
//			PaidPowerTracker.AddPowerToList (tempStar);
//			launchStarCount--;
//			if (launchStarCount <= 0) {
//				launchStars = false;
//				launchStarCount = 0;
//			}
//			timeStamp = Time.time;
//		}

		if (rotateNumbers) {
			coinNumberParent.transform.Rotate (new Vector3 (0, 0, -180) * Time.deltaTime * 2);
			if (instantiatedCoinNumberOne == null && instantiatedCoinNumberTwo == null && instantiatedCoinNumberThree == null) {
				Destroy (coinNumberParent);
				rotateNumbers = false;
			}
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject) {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedMultiStarPowerButton = (GameObject)Instantiate (pressedMultiStarPowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedMultiStarPowerButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && (hit2 == null || hit2.gameObject != gameObject)) {
			if (instantiatedPressedMultiStarPowerButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (instantiatedPressedMultiStarPowerButton);
			}
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject && StoreInventory.GetItemBalance ("coin_currency_ID") < 250) {
			soundHandler.PlayButtonClickUp ();
			GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().PauseGameRemotely ();
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateCameraOffsetScreen ();
			//Debug.Log ("Not enough scratch for multi star power");
			Destroy (instantiatedPressedMultiStarPowerButton);
		}

		else if (!currentStates.GetMultiStarPowerPressed () && hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {
			if (!GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().GetGameStarted ()) {
					Destroy (instantiatedPressedMultiStarPowerButton);
					return;
			}

			GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ().IncreaseJewelCollectorDifficultyPercentage (.05f);
			powerPercentageController.IncreasePecentage (10);
			powerPercentageController.IncreaseJewelDropPercentage (10);
			powerPercentageController.IncreaseBombColorDropPercentage (10);
			GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ().SetFirstSwapPerformed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (true);
			soundHandler.PlayButtonClickUp ();
//			launchStars = true;
//			launchStarCount += 5;
			GameObject temp;
			for (int i = 0; i < 5; i++) {
				temp = (GameObject)Instantiate (GetRandomHomingStar (), new Vector3 (transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity);
				temp.GetComponent<RockLevelHomingStarMovement> ().SetRow (Random.Range (0, 9));
				temp.GetComponent<RockLevelHomingStarMovement> ().SetCol (Random.Range (0, 9));
			}
			currentStates.SetMultiStarPowerPressed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
			StoreInventory.TakeItem ("coin_currency_ID", 250);
//			coinTotalManager.UpdateCoinNumber ();
			if (instantiatedCoinNumberOne != null)
				instantiatedCoinNumberOne.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			if (instantiatedCoinNumberTwo != null)
				instantiatedCoinNumberTwo.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			if (instantiatedCoinNumberThree != null)
				instantiatedCoinNumberThree.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			numbersSentHome = true;
		}

	}

	GameObject GetRandomHomingStar () {
		switch (Random.Range (0, 6)) {
		case 0: return blueHomingStar;
		case 1: return greenHomingStar;
		case 2: return orangeHomingStar;
		case 3: return purpleHomingStar;
		case 4: return redHomingStar;
		case 5: return whiteHomingStar;
		}
		return null;
	}

	GameObject GetRandomFallingStar () {
		switch (Random.Range (0, 6)) {
		case 0: Instantiate (redFallingStarExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0));
			return redFallingStar;
		case 1: Instantiate (blueFallingStarExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0));
			return blueFallingStar;
		case 2: Instantiate (greenFallingStarExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0)); 
			return greenFallingStar;
		case 3: Instantiate (whiteFallingStarExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0)); 
			return whiteFallingStar;
		case 4: Instantiate (orangeFallingStarExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0)); 
			return orangeFallingStar;
		case 5: Instantiate (purpleFallingStarExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0));
			return purpleFallingStar;
		}
		return null;
	}

	void OnDestroy () {
		if (GameObject.Find ("Pressed Multi Star Power Button(Clone)") != null)
			Destroy (GameObject.Find ("Pressed Multi Star Power Button(Clone)"));
	}

	public void SetTarget (float targetX, float targetY) {
		this.targetX = targetX;
		this.targetY = targetY;
	}
	
	public void GoHome () {
		goHome = true;
		leaveHome = false;
	}
	
	public void LeaveHome () {
		leaveHome = true;
		goHome = false;
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
