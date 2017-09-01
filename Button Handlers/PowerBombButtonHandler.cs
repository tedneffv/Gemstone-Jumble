using UnityEngine;
using System.Collections;
using Soomla.Store;

public class PowerBombButtonHandler : MonoBehaviour {
	Collider2D hit, hit2, hit3;
	float targetX, targetY, incrementalAngle;
	bool goHome, leaveHome, numbersInstantiated, numbersSentHome, rotateNumbers, touchOn;
	public GameObject coinNumberFive, coinNumberZero;
	public GameObject blueHomingBomb, greenHomingbomb, orangeHomingBomb, purpleHomingBomb, redHomingBomb, whiteHomingBomb;
	public GameObject pressedBombPowerButton, redFallingBomb, blueFallingBomb, greenFallingBomb, orangeFallingBomb, purpleFallingBomb, whiteFallingBomb;
	public GameObject  redFallingBombExplosion, blueFallingBombExplosion, greenFallingBombExplosion, orangeFallingBombExplosion, purpleFallingBombExplosion, whiteFallingBombExplosion;
	GameObject instantiatedPressedBombPowerButton, instantiatedCoinNumberOne, instantiatedCoinNumberTwo, instantiatedCoinNumberThree, coinNumberParent, circleObject;
	PowerButtonCurrentStates currentStates;
	CoinTotalManager coinTotalManager;
	SoundHandler soundHandler;
	RockLevelTouchHandler touchHandler;
	PowerPercentageController powerPercentageController;

	// Use this for initialization
	void Start () {
		currentStates = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonCurrentStates> ();
		if (currentStates.GetBombPowerPressed ()) {
			float totalCircleAngle = Mathf.Rad2Deg * (2 * (Mathf.Atan (((transform.position.x)/(transform.position.x * .8930232558f)))));
			float incrementalCircleAngle = totalCircleAngle / 6;
			instantiatedPressedBombPowerButton = (GameObject)Instantiate (pressedBombPowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedBombPowerButton.transform.parent = transform;
			instantiatedPressedBombPowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 4));
		}
		coinTotalManager = GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ();
		incrementalAngle = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().GetIncrementalCircleAngle ();
		circleObject = GameObject.Find ("Bomb Power Circle");
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
		if (!goHome && !numbersInstantiated && !currentStates.GetBombPowerPressed () && Mathf.Abs (Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - (360 - (incrementalAngle * 4)))) < 4f) {
			instantiatedCoinNumberOne = (GameObject)Instantiate (coinNumberFive, new Vector3 (targetX, targetY, -6), Quaternion.identity);
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberTwo = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -22.5f)));
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberThree = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -45)));
			PowerButtonHandler.increaseCoinNumber ();

			if (GameObject.Find ("Power Bomb Coin Handler") != null) {
				coinNumberParent = GameObject.Find ("Power Bomb Coin Handler");
			}
			else {
				coinNumberParent = new GameObject ();
				coinNumberParent.transform.position = gameObject.transform.position;
				coinNumberParent.name = "Power Bomb Coin Handler";
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
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, -incrementalAngle * 4), Time.deltaTime * 8f);

		if (goHome && Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - ((0))) < .1f) {
			Destroy (circleObject);
			Destroy (gameObject);
		}

		if (!currentStates.GetBombPowerPressed () && touchOn && touchHandler.GetGameStarted ()) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			}
			
			if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			}
			
			if (Input.GetMouseButtonUp (0)) {
//				if (instantiatedPressedBombPowerButton != null)
//					Destroy (instantiatedPressedBombPowerButton);
				CheckTouch3 (Input.mousePosition);
				
			}
		}

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
			instantiatedPressedBombPowerButton = (GameObject)Instantiate (pressedBombPowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedBombPowerButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && (hit2 == null || hit2.gameObject != gameObject)) {
			if (instantiatedPressedBombPowerButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (instantiatedPressedBombPowerButton);
			}
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);
		if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject && StoreInventory.GetItemBalance ("coin_currency_ID") < 500) {
			soundHandler.PlayButtonClickUp ();
			GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().PauseGameRemotely ();
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateCameraOffsetScreen ();
			//Debug.Log ("Not enough coins for bomb power");
			Destroy (instantiatedPressedBombPowerButton);
		}

		else if (!currentStates.GetBombPowerPressed () && hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {

			if (!GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().GetGameStarted ()) {
				Destroy (instantiatedPressedBombPowerButton);
				return;
			}

			GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ().IncreaseJewelCollectorDifficultyPercentage (.05f);
			powerPercentageController.IncreasePecentage (10);
			powerPercentageController.IncreaseJewelDropPercentage (10);
			powerPercentageController.IncreaseBombColorDropPercentage (10);
			GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ().SetFirstSwapPerformed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (true);
			soundHandler.PlayButtonClickUp ();
			soundHandler.PlayPowerUpSound ();
			GameObject tempObject;
			tempObject = (GameObject)Instantiate (GetRandomFallingBomb (), new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
			PaidPowerTracker.AddPowerToList (tempObject);
//			tempObject.GetComponent<HomingBombMovementController> ().SetTargetRowAndCol (Random.Range (1, 8), Random.Range (1, 8));
			currentStates.SetBombPowerPressed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
			StoreInventory.TakeItem ("coin_currency_ID", 500);
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

	void OnDestroy () {
		if (GameObject.Find ("Pressed Bomb Power Button(Clone)") != null) {
			Destroy (GameObject.Find ("Pressed Bomb Power Button(Clone)"));
		}
	}

	GameObject GetRandomHomingBomb () {
		switch (Random.Range (0, 6)) {
		case 0: return blueHomingBomb;
		case 1: return greenHomingbomb;
		case 2: return orangeHomingBomb;
		case 3: return purpleHomingBomb;
		case 4: return redHomingBomb;
		case 5: return whiteHomingBomb;
		}
		return null;
	}

	GameObject GetRandomFallingBomb () {
		switch (Random.Range (0, 6)) {
		case 0: return blueFallingBomb;
		case 1: return greenFallingBomb;
		case 2: return orangeFallingBomb;
		case 3: return purpleFallingBomb;
		case 4: return redFallingBomb;
		case 5: return whiteFallingBomb;
		}
		return null;
	}

	GameObject GetRandomFallingExplosionBomb () {
		switch (Random.Range (0, 6)) {
		case 0: Instantiate (redFallingBombExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0));
			return redFallingBomb;
		case 1: Instantiate (blueFallingBombExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0));
			return blueFallingBomb;
		case 2: Instantiate (greenFallingBombExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0)); 
			return greenFallingBomb;
		case 3: Instantiate (whiteFallingBombExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0)); 
			return whiteFallingBomb;
		case 4: Instantiate (orangeFallingBombExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0)); 
			return orangeFallingBomb;
		case 5: Instantiate (purpleFallingBombExplosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.Euler (-90, 0, 0));
			return purpleFallingBomb;
		}
		return null;
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
