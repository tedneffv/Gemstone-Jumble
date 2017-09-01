using UnityEngine;
using System.Collections;
using Soomla.Store;

public class RowDestructionButtonHandler : MonoBehaviour {
	Collider2D hit, hit2, hit3;
	float targetX, targetY, incrementalAngle;
	bool goHome, leaveHome, numbersInstantiated, numbersSentHome, rotateNumbers, touchOn;
	public GameObject coinNumberSeven, coinNumberFive, coinNumberZero;
	public GameObject pressedRowDestructionButton, redRowDestructionStar, blueRowDestructionStar, greenRowDestructionStar, orangeRowDestructionStar, purpleRowDestructionStar, whiteRowDestructionStar;
	GameObject instantiatedPressedRowDestructionButton, instantiatedCoinNumberOne, instantiatedCoinNumberTwo, instantiatedCoinNumberThree, coinNumberParent, circleObject;
	PowerButtonCurrentStates currentStates;
	CoinTotalManager coinTotalManager;
	SoundHandler soundHandler; 
	RockLevelTouchHandler touchHandler;
	PowerPercentageController powerPercentageController;

	// Use this for initialization
	void Start () {
		currentStates = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonCurrentStates> ();
		if (currentStates.GetRowDestructionPowerPressed ()) {
			float totalCircleAngle = Mathf.Rad2Deg * (2 * (Mathf.Atan (((transform.position.x)/(transform.position.x * .8930232558f)))));
			float incrementalCircleAngle = totalCircleAngle / 6;
			instantiatedPressedRowDestructionButton = (GameObject) Instantiate (pressedRowDestructionButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedRowDestructionButton.transform.parent = transform;
			instantiatedPressedRowDestructionButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 3));
		}
		coinTotalManager = GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ();
		incrementalAngle = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().GetIncrementalCircleAngle ();
		circleObject = GameObject.Find ("Row Destruction Circle");
		touchOn = true;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> (); 
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler > ();
		powerPercentageController = GameObject.Find ("Level Controller").GetComponent<PowerPercentageController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (numbersInstantiated && instantiatedCoinNumberOne == null && instantiatedCoinNumberTwo == null && instantiatedCoinNumberThree == null) {
			numbersInstantiated = false;
		}
		if (!goHome && !numbersInstantiated && !currentStates.GetRowDestructionPowerPressed () && Mathf.Abs (Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - (360 - (incrementalAngle * 3)))) < 4f) {
			instantiatedCoinNumberOne = (GameObject)Instantiate (coinNumberSeven, new Vector3 (targetX, targetY, -6), Quaternion.identity);
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberTwo = (GameObject)Instantiate (coinNumberFive, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -22.5f)));
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberThree = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -6), Quaternion.Euler (new Vector3 (0, 0, -45)));
			PowerButtonHandler.increaseCoinNumber ();

			if (GameObject.Find ("Row Destruction Coin Holder") != null) {
				coinNumberParent = GameObject.Find ("Row Destruction Coin Holder");
			}
			else {
				coinNumberParent = new GameObject ();
				coinNumberParent.transform.position = gameObject.transform.position;
				coinNumberParent.name = "Row Destruction Coin Holder";
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
//
		if (goHome && PowerButtonHandler.GetCoinNumberCount () == 0)
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime * 8f);
		else if (!goHome)
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, -incrementalAngle * 3), Time.deltaTime * 8f);
		if (goHome && Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - ((0))) < .1f) {
			Destroy (circleObject);
			Destroy (gameObject);
		}

		if (!currentStates.GetRowDestructionPowerPressed () && touchOn && touchHandler.GetGameStarted ()) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			}
			
			if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			}
			
			if (Input.GetMouseButtonUp (0)) {
				//			if (instantiatedPressedRowDestructionButton != null) {
				//				Destroy (instantiatedPressedRowDestructionButton);
				//			}
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
			instantiatedPressedRowDestructionButton = (GameObject)Instantiate (pressedRowDestructionButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedRowDestructionButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && (hit2 == null || hit2.gameObject != gameObject)) {
			if (instantiatedPressedRowDestructionButton) {
				soundHandler.PlayButtonClickUp ();
				Destroy (instantiatedPressedRowDestructionButton);
			}
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject && StoreInventory.GetItemBalance ("coin_currency_ID") < 750) {
			soundHandler.PlayButtonClickUp ();
			GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().PauseGameRemotely ();
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateCameraOffsetScreen ();
			//Debug.Log ("Not enough coins for row destruction power");
			Destroy (instantiatedPressedRowDestructionButton);
		}

		else if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {
			if (!GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().GetGameStarted ()) {
				Destroy (instantiatedPressedRowDestructionButton);
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
			tempObject = (GameObject)Instantiate (GetRandomRowDestructionStar (), new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
			PaidPowerTracker.AddPowerToList (tempObject);
			currentStates.SetRowDestructionPowerPressed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
			StoreInventory.TakeItem ("coin_currency_ID", 750);
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

	GameObject GetRandomRowDestructionStar () {
		switch (Random.Range (0, 6)) {
		case 0: return redRowDestructionStar;
		case 1: return blueRowDestructionStar;
		case 2: return greenRowDestructionStar;
		case 3: return purpleRowDestructionStar;
		case 4: return whiteRowDestructionStar;
		case 5: return orangeRowDestructionStar;
		}
		return null;
	}

	void OnDestroy () {
		if (GameObject.Find ("Pressed Row Destruction Button(Clone)") != null)
			Destroy (GameObject.Find ("Pressed Row Destruction Button(Clone)"));
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
