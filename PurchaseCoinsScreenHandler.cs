using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class PurchaseCoinsScreenHandler : MonoBehaviour {

	public GameObject purchaseCoinsText, firstPurchaseButton, secondPurchaseButton, thirdPurchaseButton, fourthPurchaseButton, fifthPurchaseButton, sixthPurchaseButton, bottomBanner, coin;
	public GameObject zeroCoinNumber, oneCoinNumber, twoCoinNumber, threeCoinNumber, fourCoinNumber, fiveCoinNumber, sixCoinNumber, sevenCoinNumber, eightCoinNumber, nineCoinNumber, shade;
	GameObject instantiatedText, instantiatedFirstButton, instantiatedSecondButton, instantiatedThirdButton, instantiatedFourthButton, instantiatedFifthButton, instantiatedSixthButton, instantiatedBottomBanner;
	GameObject instantiatedCoin, instantiatedScreen;
	Vector3 oddFirstPosition, oddSecondPosition, oddThirdPosition, oddFourthPosition, oddFifthPosition, oddSixthPosition, oddSeventhPosition;
	Vector3 evenFirstPosition, evenSecondPosition, evenThirdPosition, evenFourthPosition, evenFifthPosition, evenSixthPosition, evenSeventhPosition, evenEighthPosition;
	bool screenInstantiated, bottomBannerUp, moveBanner;
	List<GameObject> digitList;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		digitList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (screenInstantiated && moveBanner) {
			if (bottomBannerUp && Mathf.Abs (Camera.main.transform.position.y - 5 - instantiatedBottomBanner.transform.position.y) > .01f) {
				instantiatedBottomBanner.transform.Translate (new Vector3 (0, ((GameObject.Find ("Main Camera").transform.position.y - 5) - instantiatedBottomBanner.transform.position.y), 0) * .2f);
			} 
		}
		
		else if (!screenInstantiated && instantiatedBottomBanner != null && moveBanner) {
			if (!bottomBannerUp && Mathf.Abs (Camera.main.transform.position.y - 6 - instantiatedBottomBanner.transform.position.y) > .01f) {
				instantiatedBottomBanner.transform.Translate (new Vector3 (0, ((GameObject.Find ("Main Camera").transform.position.y - 6) - instantiatedBottomBanner.transform.position.y), 0) * .2f);
			} else {
				if (GameObject.Find ("Level Controller") != null)
					instantiatedBottomBanner.transform.position = new Vector3(0, -6, -5);
				moveBanner = false;
				//Destory (instantiatedBottomBanner);;
			}
		}
	}

	public void InstantiateCameraOffsetScreen () {
		if (screenInstantiated)
			return;
		if (GameObject.Find ("Quit Button(Clone)") != null && GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().GetLeftHome ()) {
			GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().LeaveHomeGoHome ();
		}
		soundHandler.PlayWoosh ();
		screenInstantiated = true;
//		moveBanner = true;
		GameObject camera = GameObject.Find ("Main Camera");
//		instantiatedBottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, Camera.main.transform.position.y - 6, -103), Quaternion.identity);
		instantiatedBottomBanner = GameObject.Find ("Bottom Banner");
		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (true, -55);
		//Debug.Log ("bottomBanner = " + bottomBanner);
		instantiatedBottomBanner.transform.parent = GameObject.Find ("Main Camera").transform;
		UpdateCoinNumber ();
		bottomBannerUp = true;
		
//		instantiatedText = (GameObject)Instantiate (purchaseCoinsText, new Vector3 (-10, camera.transform.position.y + 4.04f, -100), Quaternion.identity);
//		instantiatedText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
//		instantiatedText.transform.parent = camera.transform;
//		
		instantiatedFirstButton = (GameObject)Instantiate (firstPurchaseButton, new Vector3 (-20, camera.transform.position.y + 3.12f, -100), Quaternion.identity);
		instantiatedFirstButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedFirstButton.transform.parent = camera.transform;
		
		instantiatedSecondButton = (GameObject)Instantiate (secondPurchaseButton, new Vector3 (-30, camera.transform.position.y + 1.92f, -100), Quaternion.identity);
		instantiatedSecondButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedSecondButton.transform.parent = camera.transform;
		
		instantiatedThirdButton = (GameObject)Instantiate (thirdPurchaseButton, new Vector3 (-40, camera.transform.position.y + .72f, -100), Quaternion.identity);
		instantiatedThirdButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedThirdButton.transform.parent = camera.transform;
		
		instantiatedFourthButton = (GameObject)Instantiate (fourthPurchaseButton, new Vector3 (-50, camera.transform.position.y - .48f, -100), Quaternion.identity);
		instantiatedFourthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedFourthButton.transform.parent = camera.transform;
		
		instantiatedFifthButton = (GameObject)Instantiate (fifthPurchaseButton, new Vector3 (-60, camera.transform.position.y - 1.68f, -100), Quaternion.identity);
		instantiatedFifthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedFifthButton.transform.parent = camera.transform;
		
		instantiatedSixthButton = (GameObject)Instantiate (sixthPurchaseButton, new Vector3 (-70, camera.transform.position.y - 2.88f, -100), Quaternion.identity);
		instantiatedSixthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		instantiatedSixthButton.transform.parent = camera.transform;

	}

	public void InstantiateScreen () {
		InstantiateCameraOffsetScreen ();
		return;

		if (GameObject.Find ("29 block") != null) {
			InstantiateCameraOffsetScreen ();
			return;
		} else if (Time.timeScale == 0) {
			instantiatedScreen = (GameObject)Instantiate (shade, new Vector3 (0, 0, -102f), Quaternion.identity);
			instantiatedScreen.transform.Translate (new Vector3 (0, 0, .5f));
			instantiatedScreen.GetComponent<PurchaseCoinsShadeController> ().DarkenShade ();
		}

		soundHandler.PlayWoosh ();

		screenInstantiated = true;
//		moveBanner = true;
//		instantiatedBottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, Camera.main.transform.position.y - 6, -103), Quaternion.identity);
		instantiatedBottomBanner = GameObject.Find ("Bottom Banner");
		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (true, -50);
	
		UpdateCoinNumber ();
		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (true, 51);

		instantiatedText = (GameObject)Instantiate (purchaseCoinsText, new Vector3 (-10, 4.04f, -103), Quaternion.identity);
		instantiatedText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedFirstButton = (GameObject)Instantiate (firstPurchaseButton, new Vector3 (-20, 2.37f, -103), Quaternion.identity);
		instantiatedFirstButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedSecondButton = (GameObject)Instantiate (secondPurchaseButton, new Vector3 (-30, 1.17f, -103), Quaternion.identity);
		instantiatedSecondButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedThirdButton = (GameObject)Instantiate (thirdPurchaseButton, new Vector3 (-40, -.03f, -103), Quaternion.identity);
		instantiatedThirdButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedFourthButton = (GameObject)Instantiate (fourthPurchaseButton, new Vector3 (-50, -1.23f, -103), Quaternion.identity);
		instantiatedFourthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedFifthButton = (GameObject)Instantiate (fifthPurchaseButton, new Vector3 (-60, -2.43f, -103), Quaternion.identity);
		instantiatedFifthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

		instantiatedSixthButton = (GameObject)Instantiate (sixthPurchaseButton, new Vector3 (-70, -3.63f, -103), Quaternion.identity);
		instantiatedSixthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);

	}

	public void UpdateCoinNumber () {

		instantiatedBottomBanner.GetComponent<BottomBannerCoinHandler> ().UpdateCoinNumber ();
		return;

//		foreach (Transform child in transform) {
//			Destroy (child.gameObject);
//		}
		if (digitList == null) {
			digitList = new List<GameObject> ();
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

	public void GetRidOfScreen () {
		if (instantiatedScreen != null) {
			instantiatedScreen.GetComponent<PurchaseCoinsShadeController> ().LightenShade ();
		}
		if (screenInstantiated) {
			screenInstantiated = false;
			if (GameObject.Find ("Level Controller") == null || GameObject.Find ("Tutorial Shade 6(Clone)") != null)
				GameObject.Find ("Bottom Banner").GetComponent<BottomBannerCoinHandler> ().Move (false, 50);
//			bottomBannerUp = false;
//			instantiatedText.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedFirstButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedSecondButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedThirdButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedFourthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedFifthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedSixthButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
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
			evenFirstPosition = new Vector3 (.11f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenSecondPosition = new Vector3 (-.11f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenThirdPosition = new Vector3 (.33f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenFourthPosition = new Vector3 (-.33f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenFifthPosition = new Vector3 (.55f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenSixthPosition = new Vector3 (-.55f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenSeventhPosition = new Vector3 (.77f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			evenEighthPosition = new Vector3 (-.77f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
		}
		else {
			oddFirstPosition = new Vector3 (0, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			oddSecondPosition = new Vector3 (-.22f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			oddThirdPosition = new Vector3 (.22f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			oddFourthPosition = new Vector3 (-.44f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			oddFifthPosition = new Vector3 (.44f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			oddSixthPosition = new Vector3 (-.66f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
			oddSeventhPosition = new Vector3 (.66f, instantiatedBottomBanner.transform.position.y + .2f, -104f);
		}
	}

	public bool GetScreenInstantiated () {
		return screenInstantiated;
	}
}
