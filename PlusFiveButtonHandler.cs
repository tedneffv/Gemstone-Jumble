using UnityEngine;
using System.Collections;
using Soomla.Store;

public class PlusFiveButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	float targetX, targetY, incrementalAngle;
	bool goHome, leaveHome, starLaunched, numbersInstantiated, numbersSentHome, rotateNumbers, touchOn;
	public GameObject coinNumberFive, coinNumberZero;
	public GameObject pressedPlusFivePowerButton, redFallingStar, greenFallingStar, purpleFallingStar, orangeFallingStar, blueFallingStar, whiteFallingStar;
	public GameObject blueFallingStarExplosion, greenFallingStarExplosion, redFallingStarExplosion, purpleFallingStarExplosion, orangeFallingStarExplosion, whiteFallingStarExplosion;
	GameObject instantiatedPressedPlusFivePowerButton, instantiatedCoinNumberOne, instantiatedCoinNumberTwo, instantiatedCoinNumberThree, instantiatedThousanthCoin, coinNumberParent, coinNumberHolder, circleObject;
	PowerButtonCurrentStates currentStates;
	CoinTotalManager coinTotalManager;
	SoundHandler soundHandler;
	RockLevelTouchHandler touchHandler;
	PowerPercentageController powerPercentageController;

	// Use this for initialization
	void Start () {
		coinTotalManager = GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ();
		currentStates = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonCurrentStates> ();
		if (currentStates.GetPlusFivePowerPressed ()) {
			float totalCircleAngle = Mathf.Rad2Deg * (2 * (Mathf.Atan (((transform.position.x)/(transform.position.x * .8930232558f)))));
			float incrementalCircleAngle = totalCircleAngle / 6;
			instantiatedPressedPlusFivePowerButton = (GameObject)Instantiate (pressedPlusFivePowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedPlusFivePowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle));
			instantiatedPressedPlusFivePowerButton.transform.parent = transform;
		}	
		incrementalAngle = GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().GetIncrementalCircleAngle ();
		circleObject = GameObject.Find ("Plus Five Circle");
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
		if (!goHome && !numbersInstantiated  && !currentStates.GetPlusFivePowerPressed () && Mathf.Abs (Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - (360 - (incrementalAngle)))) < 4f) {
			instantiatedThousanthCoin = (GameObject)Instantiate (coinNumberFive, new Vector3 (targetX, targetY, -1), Quaternion.Euler (new Vector3 (0, 0, 22.5f)));
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberOne = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -1), Quaternion.identity);
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberTwo = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -1), Quaternion.Euler (new Vector3 (0, 0, -22.5f)));
			PowerButtonHandler.increaseCoinNumber ();
			instantiatedCoinNumberThree = (GameObject)Instantiate (coinNumberZero, new Vector3 (targetX, targetY, -1), Quaternion.Euler (new Vector3 (0, 0, -45)));
			PowerButtonHandler.increaseCoinNumber ();
			if (GameObject.Find ("Plus Five Coin Holder") != null) {
				coinNumberParent = GameObject.Find ("Plus Five Coin Holder");
			}
			else {
				coinNumberParent = new GameObject ();
				coinNumberParent.transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);
				coinNumberParent.name = "Plus Five Coin Holder";
			}
			instantiatedThousanthCoin.transform.parent = coinNumberParent.transform;
			instantiatedCoinNumberOne.transform.parent = coinNumberParent.transform;
			instantiatedCoinNumberTwo.transform.parent = coinNumberParent.transform;
			instantiatedCoinNumberThree.transform.parent = coinNumberParent.transform;

			instantiatedThousanthCoin.name = "Thousanth Power Coin";
			instantiatedCoinNumberOne.name = "Power Coin One";
			instantiatedCoinNumberTwo.name = "Power Coin Two";
			instantiatedCoinNumberThree.name = "Power Coin Three";

			instantiatedThousanthCoin.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			instantiatedCoinNumberOne.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			instantiatedCoinNumberTwo.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			instantiatedCoinNumberThree.GetComponent<PowerCoinNumberMovementController> ().SetAttachedButton (gameObject);
			numbersInstantiated = true;
			numbersSentHome = false;
		}
		if (goHome && numbersInstantiated && !numbersSentHome) {
			instantiatedThousanthCoin.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			instantiatedCoinNumberOne.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			instantiatedCoinNumberTwo.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			instantiatedCoinNumberThree.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
			numbersSentHome = true;
		}
		
		if (goHome && PowerButtonHandler.GetCoinNumberCount () == 0)
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime * 8f); 
		else if (!goHome)
			circleObject.transform.rotation = Quaternion.Slerp (circleObject.transform.rotation, Quaternion.Euler (0, 0, -incrementalAngle), Time.deltaTime * 8f); 
		
		if (goHome && Mathf.Abs ((circleObject.transform.rotation.eulerAngles.z) - ((0))) < .1f) {
			Destroy (circleObject);
			Destroy (gameObject);
		}
		
		if (!currentStates.GetPlusFivePowerPressed () && touchOn && touchHandler.GetGameStarted ()) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			}
			
			if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			}
			
			if (Input.GetMouseButtonUp (0)) {
				//				if (instantiatedPressedPlusFivePowerButton != null)
				//					Destroy (instantiatedPressedPlusFivePowerButton);
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
		
		if (hit != null && hit.gameObject == gameObject && instantiatedPressedPlusFivePowerButton == null) {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedPlusFivePowerButton = (GameObject)Instantiate (pressedPlusFivePowerButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			instantiatedPressedPlusFivePowerButton.transform.parent = transform;
		}
	}
	
	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject == gameObject && (hit2 == null || hit2.gameObject != gameObject)) {
			if (instantiatedPressedPlusFivePowerButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (instantiatedPressedPlusFivePowerButton);
			}
		}
	}
	
	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);
		if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject && StoreInventory.GetItemBalance ("coin_currency_ID") < 5000) {
			soundHandler.PlayButtonClickUp ();
			GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().PauseGameRemotely ();
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateCameraOffsetScreen ();
			//Debug.Log ("Not enough coins for single star power");
			Destroy (instantiatedPressedPlusFivePowerButton);
		}
		
		else if (!currentStates.GetPlusFivePowerPressed () && hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {
			if (!GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().GetGameStarted ()) {
				Destroy (instantiatedPressedPlusFivePowerButton);
				return;
			}

			GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ().IncreaseJewelCollectorDifficultyPercentage (.1f);
			powerPercentageController.IncreasePecentage (10);
			powerPercentageController.IncreaseJewelDropPercentage (10);
			powerPercentageController.IncreaseBombColorDropPercentage (10);
			GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ().SetFirstSwapPerformed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (true);
			soundHandler.PlayButtonClickUp ();
			soundHandler.PlayPowerUpSound ();
//			GameObject tempObject;
//			tempObject = (GameObject)Instantiate (GetRandomFallingStar (), new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
//			PaidPowerTracker.AddPowerToList (tempObject);
			currentStates.SetPlusFivePowerPressed (true);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
			StoreInventory.TakeItem ("coin_currency_ID", 5000);
			if (GameObject.Find ("Jewel Collector") != null) {
				GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ().AddFiveToMoveNumber ();
			} else {
				GameObject.Find ("Level Controller").GetComponent<RockLevelBombHandler> ().AddFiveToAllBombs ();
			}
			//			coinTotalManager.UpdateCoinNumber ();
			if (instantiatedThousanthCoin != null)
				instantiatedThousanthCoin.GetComponent<PowerCoinNumberMovementController> ().MoveNumberBack (true);
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
		if (GameObject.Find ("Pressed Plus Five Power Button(Clone)") != null)
			Destroy (GameObject.Find ("Pressed Plus Five Power Button(Clone)"));
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
