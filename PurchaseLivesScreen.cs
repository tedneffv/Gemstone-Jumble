using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class PurchaseLivesScreen : MonoBehaviour {

	public GameObject heart, timesX, heartNumberFive, bottomBanner, coin, shade;
	public GameObject zeroCoinNumber, oneCoinNumber, twoCoinNumber, threeCoinNumber, fourCoinNumber, fiveCoinNumber, sixCoinNumber, sevenCoinNumber, eightCoinNumber, nineCoinNumber;
	public GameObject zero, nine, decimalPoint, dollarSign;
	public GameObject livesCostCoin, livesCostFive, livesCostZero, coinParent, checkButton, xButton;

	public GameObject purchaseLivesBanner, inviteButton, heartAndPlus;

	GameObject instantiatedLivesCostCoin, instantiatedFirstCostNumber, instantiatedSecondCostNumber, instantiatedThirdCostNumber, instantiatedFourthCostNumber, instantiatedCoinParent, instantiatedDollarSign, instantiatedDecimalPoint;
	GameObject instantiatedPurchaseLivesBanner, instantiatedHeart, instantiatedTimesX, instantiatedHeartNumberFive, instantiatedBottomBanner, instantiatedCoin, instantiatedCheckButton, instantiatedXButton, instantiatedShade;
	GameObject instantiatedInviteButton, instantiatedHeartAndPlus;
	Vector3 evenFirstPosition, evenSecondPosition, evenThirdPosition, evenFourthPosition, evenFifthPosition, evenSixthPosition, evenSeventhPosition, evenEighthPosition, bottomBannerPosition;
	Vector3 oddFirstPosition, oddSecondPosition, oddThirdPosition, oddFourthPosition, oddFifthPosition, oddSixthPosition, oddSeventhPosition;
	bool screenInstantiated, bottomBannerUp, moveBanner, shadeInstantiated, getRidOfShade, getRidOfSlide;
	SpriteRenderer spriteRenderer; 
	List<GameObject> digitList;
	SoundHandler soundHanlder;
	Color oldColor;
	
	// Use this for initialization
	void Start () {
		digitList = new List<GameObject> ();
		soundHanlder = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	void FixedUpdate () {
		if (!getRidOfSlide && shadeInstantiated && spriteRenderer != null && spriteRenderer.color.a < .7) {
			oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
		}
		else if (getRidOfShade && getRidOfSlide) {
			if (spriteRenderer.color.a <= 0) {
				getRidOfShade = false;
				getRidOfSlide = false;
				Destroy (instantiatedShade);
			} else {
				oldColor = spriteRenderer.color;
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
			}
		}
	}

	public void CameraOffsetPurchaseLivesScreen () {
		soundHanlder.PlayWoosh ();
		screenInstantiated = true;
		moveBanner = true;

		GameObject camera = GameObject.Find ("Main Camera");

		if (!shadeInstantiated && GameObject.Find ("Level Controller") == null) {
			instantiatedShade = (GameObject)Instantiate (shade, new Vector3 (camera.transform.position.x, camera.transform.position.y, -50), Quaternion.identity);
			instantiatedShade.transform.parent = camera.transform;
			spriteRenderer = instantiatedShade.GetComponent<SpriteRenderer> ();
			shadeInstantiated = true;
		}

//		instantiatedBottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, Camera.main.transform.position.y - 6, -102), Quaternion.identity);
		if (GameObject.Find ("Bottom Banner") != null)
			instantiatedBottomBanner = GameObject.Find ("Bottom Banner");
		else 
			instantiatedBottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, Camera.main.transform.position.y - 6, -50), Quaternion.identity);
		instantiatedBottomBanner.transform.parent = camera.transform;
		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (true, -51);
		UpdateCoinNumber ();
		bottomBannerUp = true;

		instantiatedPurchaseLivesBanner = (GameObject)Instantiate (purchaseLivesBanner, new Vector3 (-10, camera.transform.position.y + 1.275f, -100), Quaternion.identity);
		instantiatedPurchaseLivesBanner.transform.parent = camera.transform;

//		Debug.Log ("Instantiating Invite Button");
//		instantiatedInviteButton = (GameObject)Instantiate (inviteButton, new Vector3 (-10, camera.transform.position.y, 0), Quaternion.identity);
//		instantiatedInviteButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0.1f);
//		instantiatedInviteButton.transform.parent = camera.transform;
//
//		instantiatedHeartAndPlus = (GameObject)Instantiate (heartAndPlus, new Vector3 (-15, camera.transform.position.y - 4.95f, 0), Quaternion.identity);
//		instantiatedHeartAndPlus.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0.0f);
//		instantiatedHeartAndPlus.transform.parent = camera.transform;

//		instantiatedHeart = (GameObject)Instantiate (heart, new Vector3 (-20, camera.transform.position.y + (1.63f), -101), Quaternion.identity);
//		instantiatedHeart.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1);
//		instantiatedHeart.transform.parent = camera.transform;
//
//		instantiatedTimesX = (GameObject)Instantiate (timesX, new Vector3 (-30, camera.transform.position.y + .73f, -100), Quaternion.identity);
//		instantiatedTimesX.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.61f);
//		instantiatedTimesX.transform.parent = camera.transform;
//
//		instantiatedHeartNumberFive = (GameObject)Instantiate (heartNumberFive, new Vector3 (-40, camera.transform.position.y + 1.23f, -99), Quaternion.identity);
//		instantiatedHeartNumberFive.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.94f);
//		instantiatedHeartNumberFive.transform.parent = camera.transform;
//		
//		instantiatedLivesCostCoin = (GameObject)Instantiate (livesCostCoin, new Vector3 (-50, camera.transform.position.y + (-1f), -100), Quaternion.identity);
//		instantiatedCoinParent = (GameObject)Instantiate (coinParent, new Vector3 (-50, camera.transform.position.y + (-1f), -100), Quaternion.identity);
//		instantiatedLivesCostCoin.transform.parent = instantiatedCoinParent.transform;
//		instantiatedCoinParent.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.35f);
//		instantiatedCoinParent.transform.parent = camera.transform;
//		
//		instantiatedFirstCostNumber = (GameObject)Instantiate (livesCostFive, new Vector3 (-60, camera.transform.position.y + (-1f), -99), Quaternion.identity);
//		instantiatedFirstCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.55f);
//		instantiatedFirstCostNumber.transform.parent = camera.transform;
//		
//		instantiatedSecondCostNumber = (GameObject)Instantiate (livesCostZero, new Vector3 (-70, camera.transform.position.y + (-1f), -98), Quaternion.identity);
//		instantiatedSecondCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
//		instantiatedSecondCostNumber.transform.parent = camera.transform;
//		
//		instantiatedThirdCostNumber = (GameObject)Instantiate (livesCostZero, new Vector3 (-80, camera.transform.position.y + (-1f), -97), Quaternion.identity);
//		instantiatedThirdCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.55f);
//		instantiatedThirdCostNumber.transform.parent = camera.transform;
//		
//		instantiatedFourthCostNumber = (GameObject)Instantiate (livesCostZero, new Vector3 (-90, camera.transform.position.y + (-1), -96), Quaternion.identity);
//		instantiatedFourthCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.1f);
//		instantiatedFourthCostNumber.transform.parent = camera.transform;

//		instantiatedDollarSign = (GameObject)Instantiate (dollarSign, new Vector3 (-50, camera.transform.position.y + (-1), -100), Quaternion.identity);
//		instantiatedDollarSign.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.45f);
//		instantiatedDollarSign.transform.parent = camera.transform;
//
//		instantiatedFirstCostNumber = (GameObject)Instantiate (zero, new Vector3 (-60, camera.transform.position.y + (-1), -99), Quaternion.identity);
//		instantiatedFirstCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.58f);
//		instantiatedFirstCostNumber.transform.parent = camera.transform;
//
//		instantiatedDecimalPoint = (GameObject)Instantiate (decimalPoint, new Vector3 (-70, camera.transform.position.y + (-1.46f), -98), Quaternion.identity);
//		instantiatedDecimalPoint.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.03f);
//		instantiatedDecimalPoint.transform.parent = camera.transform;
//
//		instantiatedSecondCostNumber = (GameObject)Instantiate (nine, new Vector3 (-80, camera.transform.position.y + (-1), -97), Quaternion.identity);
//		instantiatedSecondCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.52f);
//		instantiatedSecondCostNumber.transform.parent = camera.transform;
//
//		instantiatedThirdCostNumber = (GameObject)Instantiate (nine, new Vector3 (-90, camera.transform.position.y + (-1), -96), Quaternion.identity);
//		instantiatedThirdCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.37f);
//		instantiatedThirdCostNumber.transform.parent = camera.transform;
//		
//		instantiatedCheckButton = (GameObject)Instantiate (checkButton, new Vector3 (-100, camera.transform.position.y + (-2.66f), -100), Quaternion.identity);
//		instantiatedCheckButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.25f);
//		instantiatedCheckButton.transform.parent = camera.transform;
//		
//		instantiatedXButton = (GameObject)Instantiate (xButton, new Vector3 (-110, camera.transform.position.y + (-2.66f), -99), Quaternion.identity);
//		instantiatedXButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.25f);
//		instantiatedXButton.transform.parent = camera.transform;

	}

	public void InstantiatedPurcahseLivesScreen () {
		CameraOffsetPurchaseLivesScreen ();
		return;
		soundHanlder.PlayWoosh ();
		screenInstantiated = true;
		moveBanner = true;

//		instantiatedBottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, Camera.main.transform.position.y - 6, -102), Quaternion.identity);
		instantiatedBottomBanner = GameObject.Find ("Bottom Banner");
		UpdateCoinNumber ();
		bottomBannerPosition = bottomBanner.transform.position;
		bottomBannerUp = true;

		instantiatedHeart = (GameObject)Instantiate (heart, new Vector3 (-20, 1.63f, -101), Quaternion.identity);
		instantiatedHeart.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1);

		instantiatedTimesX = (GameObject)Instantiate (timesX, new Vector3 (-30, .73f, -100), Quaternion.identity);
		instantiatedTimesX.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.61f);

		instantiatedHeartNumberFive = (GameObject)Instantiate (heartNumberFive, new Vector3 (-40, 1.23f, -99), Quaternion.identity);
		instantiatedHeartNumberFive.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.94f);

		instantiatedLivesCostCoin = (GameObject)Instantiate (livesCostCoin, new Vector3 (-50, -1f, -100), Quaternion.identity);
		instantiatedCoinParent = (GameObject)Instantiate (coinParent, new Vector3 (-50, -1f, -100), Quaternion.identity);
		instantiatedLivesCostCoin.transform.parent = instantiatedCoinParent.transform;
		instantiatedCoinParent.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.35f);

		instantiatedFirstCostNumber = (GameObject)Instantiate (livesCostFive, new Vector3 (-60, -1f, -99), Quaternion.identity);
		instantiatedFirstCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.55f);

		instantiatedSecondCostNumber = (GameObject)Instantiate (livesCostZero, new Vector3 (-70, -1f, -98), Quaternion.identity);
		instantiatedSecondCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedThirdCostNumber = (GameObject)Instantiate (livesCostZero, new Vector3 (-80, -1f, -97), Quaternion.identity);
		instantiatedThirdCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (.55f);

		instantiatedFourthCostNumber = (GameObject)Instantiate (livesCostZero, new Vector3 (-90, -1, -96), Quaternion.identity);
		instantiatedFourthCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.1f);

		instantiatedCheckButton = (GameObject)Instantiate (checkButton, new Vector3 (-100, -2.66f, -100), Quaternion.identity);
		instantiatedCheckButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.25f);

		instantiatedXButton = (GameObject)Instantiate (xButton, new Vector3 (-110, -2.66f, -99), Quaternion.identity);
		instantiatedXButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.25f);
	}

	public void GetRidOfScreen () {
		if (getRidOfShade) {
			getRidOfSlide = true;
		}
		screenInstantiated = false;
		bottomBannerUp = false;
		instantiatedPurchaseLivesBanner.GetComponent<PurchaseHeartsBannerController> ().GetRidOfScreen (true);
		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (false, 51);
//		instantiatedInviteButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedHeartAndPlus.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedHeart.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedTimesX.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedHeartNumberFive.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedDollarSign.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedDecimalPoint.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedCoinParent.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedFirstCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedSecondCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedThirdCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedFourthCostNumber.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedCheckButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedXButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
	}

	public void SetGetRidOfShade (bool getRidOfShade) {
		this.getRidOfShade = getRidOfShade;
	}

	public void UpdateCoinNumber () {

		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().UpdateCoinNumber ();
		return; 

		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
		int coinNumber = StoreInventory.GetItemBalance ("coin_currency_ID");
		string coinString = coinNumber.ToString ();
		foreach (GameObject a in digitList) {
			Destroy (a);
		}
		
		ModifyPositionVectors (coinString.Length);
		
		digitList.Clear ();
		if (coinString.Length == 1) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddFirstPosition.x - .25f, oddFirstPosition.y, oddFirstPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddFirstPosition, Quaternion.identity));
		}
		else if (coinString.Length == 2) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenSecondPosition.x - .25f, evenFirstPosition.y, evenFirstPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenFirstPosition, Quaternion.identity));
		}
		else if (coinString.Length == 3) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddSecondPosition.x - .25f, oddSecondPosition.y, oddSecondPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddThirdPosition, Quaternion.identity));
		}
		else if (coinString.Length == 4) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenFourthPosition.x - .25f, evenFourthPosition.y, evenFourthPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), evenThirdPosition, Quaternion.identity));
		}
		else if (coinString.Length == 5) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddFourthPosition.x - .25f, oddFourthPosition.y, oddFourthPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), oddThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), oddFifthPosition, Quaternion.identity));
		}
		else if (coinString.Length == 6) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenSixthPosition.x - .25f, evenSixthPosition.y, evenSixthPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenSixthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), evenThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[5]), evenFifthPosition, Quaternion.identity));
		}
		else if (coinString.Length == 7) {
			if (instantiatedCoin == null) {
				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddSixthPosition.x - .25f, oddSixthPosition.y, oddSixthPosition.z), Quaternion.identity);
			} else {
				instantiatedCoin.transform.position = (new Vector3 (oddSixthPosition.x - .25f, oddSixthPosition.y, oddSixthPosition.z));
			}
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddSixthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), oddThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[5]), oddFifthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[6]), oddSeventhPosition, Quaternion.identity));
		}
		else if (coinString.Length == 8) {
			if (instantiatedCoin == null) {
				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenEighthPosition.x - .25f, evenEighthPosition.y, evenEighthPosition.z), Quaternion.identity);
			} else {
				instantiatedCoin.transform.position = (new Vector3 (evenEighthPosition.x - .25f, evenEighthPosition.y, evenSixthPosition.z));
			}
			instantiatedCoin.transform.parent = instantiatedBottomBanner.transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenEighthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenSixthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), evenFourthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), evenFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[5]), evenThirdPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[6]), evenFifthPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[7]), evenSeventhPosition, Quaternion.identity));
		}
		foreach (GameObject a in digitList) {
			a.GetComponent<ScoreNumberSizeIncreaser> ().SetGrow (true);
			a.transform.parent = instantiatedBottomBanner.transform;
		}
	}

	GameObject GetNumberFromString (char number) {
		switch (number) {
		case '0': return zeroCoinNumber;
		case '1': return oneCoinNumber;
		case '2': return twoCoinNumber;
		case '3': return threeCoinNumber;
		case '4': return fourCoinNumber;
		case '5': return fiveCoinNumber;
		case '6': return sixCoinNumber;
		case '7': return sevenCoinNumber;
		case '8': return eightCoinNumber;
		case '9': return nineCoinNumber;
		}
		return null;
	}


	void ModifyPositionVectors (int coinLength) {
		if (coinLength % 2 == 0) {
			evenFirstPosition = new Vector3 (.11f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenSecondPosition = new Vector3 (-.11f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenThirdPosition = new Vector3 (.33f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenFourthPosition = new Vector3 (-.33f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenFifthPosition = new Vector3 (.55f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenSixthPosition = new Vector3 (-.55f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenSeventhPosition = new Vector3 (.77f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			evenEighthPosition = new Vector3 (-.77f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
		}
		else {
			oddFirstPosition = new Vector3 (0, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			oddSecondPosition = new Vector3 (-.22f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			oddThirdPosition = new Vector3 (.22f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			oddFourthPosition = new Vector3 (-.44f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			oddFifthPosition = new Vector3 (.44f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			oddSixthPosition = new Vector3 (-.66f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
			oddSeventhPosition = new Vector3 (.66f, instantiatedBottomBanner.transform.position.y + .2f, -103f);
		}
	}


	public bool GetScreenInstantiated () {
		return screenInstantiated;
	}

	public void SetShadeInstantiated (bool shadeInstantiated) {
		this.shadeInstantiated = shadeInstantiated;
	}
}
