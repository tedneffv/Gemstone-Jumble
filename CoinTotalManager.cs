using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class CoinTotalManager : MonoBehaviour {

	public GameObject zeroCoinNumber, oneCoinNumber, twoCoinNumber, threeCoinNumber, fourCoinNumber, fiveCoinNumber, sixCoinNumber, sevenCoinNumber, eightCoinNumber, nineCoinNumber, coin;
	Vector3 evenFirstPosition, evenSecondPosition, evenThirdPosition, evenFourthPosition, evenFifthPosition, evenSixthPosition, evenSeventhPosition, evenEighthPosition;
	Vector3 oddFirstPosition, oddSecondPosition, oddThirdPosition, oddFourthPosition, oddFifthPosition, oddSixthPosition, oddSeventhPosition;
	Quaternion coinRotation;
	GameObject bottomBanner, instantiatedCoin;
	GameManagerScript gameManagerScript;
	GameObject[] bottomBannerList;
	List<GameObject> digitList;
	int coinNumber;

	// Use this for initialization
	void Start () {
		digitList = new List<GameObject> ();

		gameManagerScript = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ();
//		coinNumber = StoreInventory.GetItemBalance ("coin_currency_ID");
//		//bottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, -6, -2), Quaternion.identity);
	}
//	void UpdateCoinNumber () {
//		bool stillZero = true;
//		int digitNumber = 0;
//		DestroyAllDigits ();
//		if ((coinNumber / 1000000) != 0) {
//			SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (coinNumber / 1000000), GetDigitPosition (digitNumber), Quaternion.identity));
//			digitNumber++;
//			stillZero = false;
//		}
//		if ((((coinNumber % 1000000) / 100000) == 0 && !stillZero) || ((coinNumber % 1000000) / 100000) != 0) {
//			SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (((coinNumber % 1000000) / 100000)), GetDigitPosition (digitNumber), Quaternion.identity));
//			digitNumber++;
//			stillZero = false;
//		}
//		if ((((coinNumber % 100000) / 10000) == 0 && !stillZero) || ((coinNumber % 100000) / 10000) != 0) {
//			SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (((coinNumber % 100000) / 10000)), GetDigitPosition (digitNumber), Quaternion.identity));
//			digitNumber++;
//			stillZero = false;
//		}
//		if ((((coinNumber % 10000) / 1000) == 0 && !stillZero) || ((coinNumber % 10000) / 1000) != 0) {
//			SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (((coinNumber % 10000) / 1000)), GetDigitPosition (digitNumber), Quaternion.identity));
//			digitNumber++;
//			stillZero = false;
//		}
//		if ((((coinNumber % 1000) / 100) == 0 && !stillZero) || ((coinNumber % 1000) / 100) != 0) {
//			SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (((coinNumber % 1000) / 100)), GetDigitPosition (digitNumber), Quaternion.identity));
//			digitNumber++;
//			stillZero = false;
//		}
//		if (((((coinNumber % 100) / 10) == 0 && !stillZero) || ((coinNumber % 100) / 10) != 0)) {
//			SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (((coinNumber % 100) / 10)), GetDigitPosition (digitNumber), Quaternion.identity));
//			digitNumber++;
//			stillZero = false;
//		}
//		SetCorrectDigit (digitNumber, (GameObject)Instantiate (GetNumberFromInt (coinNumber % 10), GetDigitPosition (digitNumber), Quaternion.identity));
//		digitNumber++;
//		stillZero = false;
//		gameManagerScript.SetCoinTotal (coinNumber);
//	}

	public void UpdateCoinNumber () {
		coinNumber = StoreInventory.GetItemBalance ("coin_currency_ID");
		bottomBanner = GameObject.FindGameObjectWithTag ("Bottom Banner");
		//Debug.Log ("Bottom Banner = " + bottomBanner);
		bottomBanner.GetComponent<BottomBannerCoinHandler> ().UpdateCoinNumber ();
//		string coinString = StoreInventory.GetItemBalance ("coin_currency_ID").ToString ();
//		foreach (GameObject a in digitList) {
//			Destroy (a);
//		}
//		//Debug.Log ("Coin total = " + coinString);
//
//		bottomBannerList = GameObject.FindGameObjectsWithTag ("Bottom Banner");
//		for (int i = 0; i < bottomBannerList.Length; i++) {
//
//			bottomBanner = bottomBannerList[i];
//			ModifyPositionVectors (coinString.Length);
//			digitList.Clear ();
//			if (coinString.Length == 1) {
//				if (instantiatedCoin != null) {
//					Destroy (instantiatedCoin);
//				}
//				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddFirstPosition.x - .25f, oddFirstPosition.y, oddFirstPosition.z), Quaternion.identity);
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddFirstPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 2) {
//				if (instantiatedCoin != null) {
//					Destroy (instantiatedCoin);
//				}
//				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenSecondPosition.x - .25f, evenFirstPosition.y, evenFirstPosition.z), Quaternion.identity);
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenFirstPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 3) {
//				if (instantiatedCoin != null) {
//					Destroy (instantiatedCoin);
//				}
//				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddSecondPosition.x - .25f, oddSecondPosition.y, oddSecondPosition.z), Quaternion.identity);
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddFirstPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddThirdPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 4) {
//				if (instantiatedCoin != null) {
//					Destroy (instantiatedCoin);
//				}
//				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenFourthPosition.x - .25f, evenFourthPosition.y, evenFourthPosition.z), Quaternion.identity);
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenFourthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), evenFirstPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), evenThirdPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 5) {
//				if (instantiatedCoin != null) {
//					Destroy (instantiatedCoin);
//				}
//				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddFourthPosition.x - .25f, oddFourthPosition.y, oddFourthPosition.z), Quaternion.identity);
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddFourthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddFirstPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), oddThirdPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), oddFifthPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 6) {
//				if (instantiatedCoin != null) {
//					Destroy (instantiatedCoin);
//				}
//				instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenSixthPosition.x - .25f, evenSixthPosition.y, evenSixthPosition.z), Quaternion.identity);
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenSixthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenFourthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), evenSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), evenFirstPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), evenThirdPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[5]), evenFifthPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 7) {
//				if (instantiatedCoin == null) {
//					instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddSixthPosition.x - .25f, oddSixthPosition.y, oddSixthPosition.z), Quaternion.identity);
//				} else {
//					instantiatedCoin.transform.position = (new Vector3 (oddSixthPosition.x - .25f, oddSixthPosition.y, oddSixthPosition.z));
//				}
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddSixthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddFourthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), oddFirstPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), oddThirdPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[5]), oddFifthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[6]), oddSeventhPosition, Quaternion.identity));
//			}
//			else if (coinString.Length == 8) {
//				if (instantiatedCoin == null) {
//					instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenEighthPosition.x - .25f, evenEighthPosition.y, evenEighthPosition.z), Quaternion.identity);
//				} else {
//					instantiatedCoin.transform.position = (new Vector3 (evenEighthPosition.x - .25f, evenEighthPosition.y, evenSixthPosition.z));
//				}
//				instantiatedCoin.transform.parent = bottomBanner.transform;
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenEighthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenSixthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), evenFourthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[3]), evenSecondPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[4]), evenFirstPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[5]), evenThirdPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[6]), evenFifthPosition, Quaternion.identity));
//				digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[7]), evenSeventhPosition, Quaternion.identity));
//			}
//			foreach (GameObject a in digitList) {
//				a.GetComponent<ScoreNumberSizeIncreaser> ().SetGrow (true);
//				a.transform.parent = bottomBanner.transform;
//			}
//		}
//		if (GameObject.Find ("$.99 Button(Clone)") != null) {
//			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().UpdateCoinNumber ();
//		}
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
			evenFirstPosition = new Vector3 (.11f, bottomBanner.transform.position.y + .2f, -103f);
			evenSecondPosition = new Vector3 (-.11f, bottomBanner.transform.position.y + .2f, -103f);
			evenThirdPosition = new Vector3 (.33f, bottomBanner.transform.position.y + .2f, -103f);
			evenFourthPosition = new Vector3 (-.33f, bottomBanner.transform.position.y + .2f, -103f);
			evenFifthPosition = new Vector3 (.55f, bottomBanner.transform.position.y + .2f, -103f);
			evenSixthPosition = new Vector3 (-.55f, bottomBanner.transform.position.y + .2f, -103f);
			evenSeventhPosition = new Vector3 (.77f, bottomBanner.transform.position.y + .2f, -103f);
			evenEighthPosition = new Vector3 (-.77f, bottomBanner.transform.position.y + .2f, -103f);
		}
		else {
			oddFirstPosition = new Vector3 (0, bottomBanner.transform.position.y + .2f, -103f);
			oddSecondPosition = new Vector3 (-.22f, bottomBanner.transform.position.y + .2f, -103f);
			oddThirdPosition = new Vector3 (.22f, bottomBanner.transform.position.y + .2f, -103f);
			oddFourthPosition = new Vector3 (-.44f, bottomBanner.transform.position.y + .2f, -103f);
			oddFifthPosition = new Vector3 (.44f, bottomBanner.transform.position.y + .2f, -103f);
			oddSixthPosition = new Vector3 (-.66f, bottomBanner.transform.position.y + .2f, -103f);
			oddSeventhPosition = new Vector3 (.66f, bottomBanner.transform.position.y + .2f, -103f);
		}
	}

//	void SetCorrectDigit (int digitNumber, GameObject instantiatedDigit) {
//		switch (digitNumber) {
//		case 0: firstDigit = instantiatedDigit; break;
//		case 1: secondDigit = instantiatedDigit; break;
//		case 2: thirdDigit = instantiatedDigit; break;
//		case 3: fourthDigit = instantiatedDigit; break;
//		case 4: fifthDigit = instantiatedDigit; break;
//		case 5: sixthDigit = instantiatedDigit; break;
//		case 6: seventhDigit = instantiatedDigit; break;
//		}
//	}
//	
//	GameObject GetCorrectDigit (int digitNumber) {
//		switch (digitNumber) {
//		case 0: return firstDigit;
//		case 1: return secondDigit;
//		case 2: return thirdDigit;
//		case 3: return fourthDigit;
//		case 4: return fifthDigit;
//		case 5: return sixthDigit;
//		case 6: return seventhDigit;
//		}
//		return null;
//	}
//	
//	Vector3 GetDigitPosition (int digitNumber) {
//		switch (digitNumber) {
//		case 0: return firstPosition;
//		case 1: return secondPosition;
//		case 2: return thirdPosition;
//		case 3: return fourthPosition;
//		case 4: return fifthPosition;
//		case 5: return sixthPosition;
//		case 6: return seventhPosition;
//		}
//		return Vector3.zero;
//	}
//	
//	void DestroyAllDigits () {
//		if (seventhDigit != null)  {
//			Destroy (seventhDigit);
//		}
//		if (sixthDigit != null) {
//			Destroy (sixthDigit);
//		}
//		if (fifthDigit != null) {
//			Destroy (fifthDigit);
//		}
//		if (fourthDigit != null) {
//			Destroy (fourthDigit);
//		}
//		if (thirdDigit != null) {
//			Destroy (thirdDigit);
//		}
//		if (secondDigit != null) {
//			Destroy (secondDigit);
//		}
//		if (firstDigit != null) {
//			Destroy (firstDigit);
//		}
//	}

	GameObject GetNumberFromInt (int scoreNumber) {
		switch (scoreNumber) {
		case 0: return zeroCoinNumber;
		case 1: return oneCoinNumber;
		case 2: return twoCoinNumber;
		case 3: return threeCoinNumber;
		case 4: return fourCoinNumber;
		case 5: return fiveCoinNumber;
		case 6: return sixCoinNumber;
		case 7: return sevenCoinNumber;
		case 8: return eightCoinNumber;
		case 9: return nineCoinNumber;
		}
		return null;
	}

	public int GetCoinNumber () {
		return coinNumber;
	}

	public void SubtractCoins (int coinsToSubtract) {
		this.coinNumber -= coinsToSubtract;
		UpdateCoinNumber ();
	}

	public void SetBottomBanner (GameObject bottomBanner) {
		this.bottomBanner = bottomBanner;
	}

	public void SetCoinNumber (int coins) {
		this.coinNumber = coins;
		UpdateCoinNumber ();
	}

	public void AddCoins (int coinsToAdd) {
		this.coinNumber += coinsToAdd;
		UpdateCoinNumber ();
	}
}
