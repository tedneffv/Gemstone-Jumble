using UnityEngine;
using System.Collections;
using Soomla.Store;

public class SingleStarPowerButtonHandler : MonoBehaviour {
	Collider2D hit, hit2, hit3;
	float targetX, targetY, incrementalAngle;
	bool goHome, leaveHome, starLaunched, numbersInstantiated, numbersSentHome, rotateNumbers, touchOn;
	public GameObject coinNumberOne, coinNumberZero;
	public GameObject pressedSingleStarPowerButton, redFallingStar, greenFallingStar, purpleFallingStar, orangeFallingStar, blueFallingStar, whiteFallingStar;
	public GameObject blueFallingStarExplosion, greenFallingStarExplosion, redFallingStarExplosion, purpleFallingStarExplosion, orangeFallingStarExplosion, whiteFallingStarExplosion;
	GameObject instantiatedPressedSingleStarPowerButton, instantiatedCoinNumberOne, instantiatedCoinNumberTwo, instantiatedCoinNumberThree, coinNumberParent, coinNumberHolder, circleObject;
	PowerButtonCurrentStates currentStates;
	CoinTotalManager coinTotalManager;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		coinTotalManager = GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ();
		currentStates = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonCurrentStates> ();
		if (currentStates.GetSingleStarPowerPressed ()) {
			instantiatedPressedSingleStarPowerButton = (GameObject)Instantiate (pressedSingleStarPowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedSingleStarPowerButton.transform.parent = transform;
		}	
		incrementalAngle = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().GetIncrementalCircleAngle ();
		circleObject = GameObject.Find ("Single Star Circle");
		touchOn = true;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (numbersInstantiated && instantiatedCoinNumberOne == null && instantiatedCoinNumberTwo == null && instantiatedCoinNumberThree == null) {
			numbersInstantiated = false;
		}
		if (!goHome && !numbersInstantiated  && !currentStates.GetSingleStarPowerPressed () && Mathf.Abs (Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - (360 - (incrementalAngle * 5)))) < 4f) {
			instantiatedCoinNumberOne = (GameObject)Instantiate (coinNumberOne, new Vector3 (targetX, targetY, -6), Quaternion.identity);
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberTwo = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -22.5f)));
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberThree = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -45)));
			PowerButtonHandler.increaseCoinNumber ();
			if (GameObject.Find ("Single Star Coin Holder") != null) {
				coinNumberParent = GameObject.Find ("Single Star Coin Holder");
			}
			else {
				coinNumberParent = new GameObject ();
				coinNumberParent.transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);
				coinNumberParent.name = "Single Star Coin Holder";
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

		if (!currentStates.GetSingleStarPowerPressed () && touchOn) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			}
			
			if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			}
			
			if (Input.GetMouseButtonUp (0)) {
//				if (instantiatedPressedSingleStarPowerButton != null)
//					Destroy (instantiatedPressedSingleStarPowerButton);
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
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	

		if (hit != null && hit.gameObject == gameObject && instantiatedPressedSingleStarPowerButton == null) {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedSingleStarPowerButton = (GameObject)Instantiate (pressedSingleStarPowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedSingleStarPowerButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && (hit2 == null || hit2.gameObject != gameObject)) {
			if (instantiatedPressedSingleStarPowerButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (instantiatedPressedSingleStarPowerButton);
			}
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);
		if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject && StoreInventory.GetItemBalance ("coin_currency_ID") < 100) {
			soundHandler.PlayButtonClickUp ();
			GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().PauseGameRemotely ();
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateCameraOffsetScreen ();
			//Debug.Log ("Not enough coins for single star power");
			Destroy (instantiatedPressedSingleStarPowerButton);
		}

		else if (!currentStates.GetSingleStarPowerPressed () && hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {
			if (!GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().GetGameStarted ()) {
				Destroy (instantiatedPressedSingleStarPowerButton);
				return;
			}
			GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ().SetFirstSwapPerformed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (true);
			soundHandler.PlayButtonClickUp ();
			soundHandler.PlayPowerUpSound ();
			GameObject tempObject;
			tempObject = (GameObject)Instantiate (GetRandomFallingStar (), new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
			PaidPowerTracker.AddPowerToList (tempObject);
			currentStates.SetSingleStarPowerPressed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
			StoreInventory.TakeItem ("coin_currency_ID", 100);
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
		if (GameObject.Find ("Pressed Single Star Power Button(Clone)") != null)
			Destroy (GameObject.Find ("Pressed Single Star Power Button(Clone)"));
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
