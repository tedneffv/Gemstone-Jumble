using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class BottomBannerCoinHandler : MonoBehaviour {

	public GameObject zeroCoinNumber, oneCoinNumber, twoCoinNumber, threeCoinNumber, fourCoinNumber, fiveCoinNumber, sixCoinNumber, sevenCoinNumber, eightCoinNumber, nineCoinNumber, coin;
	Vector3 evenFirstPosition, evenSecondPosition, evenThirdPosition, evenFourthPosition, evenFifthPosition, evenSixthPosition, evenSeventhPosition, evenEighthPosition;
	Vector3 oddFirstPosition, oddSecondPosition, oddThirdPosition, oddFourthPosition, oddFifthPosition, oddSixthPosition, oddSeventhPosition;
	Quaternion coinRotation;
	GameObject bottomBanner, instantiatedCoin; 
	GameManagerScript gameManagerScript;
	PowerButtonHandler powerButtonHandler;
	GameObject[] bottomBannerList;
	List<GameObject> digitList;
	int coinNumber;
	bool move;
	float targetY;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start () {
		if (GameObject.Find ("Power Button(Clone)") != null)
			powerButtonHandler = GameObject.Find ("Power Button(Clone)").GetComponent<PowerButtonHandler> ();
		transform.parent = GameObject.Find ("Main Camera").transform;
		//Debug.Log ("BottomBannerCoinHandler Start Method");
		digitList = new List<GameObject> ();
		
		gameManagerScript = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ();
//		coinNumber = StoreInventory.GetItemBalance ("coin_currency_ID");
//		UpdateCoinNumber ();
	}

	void Update () {
		if (move) {
			transform.Translate (new Vector3 (0, (targetY - transform.position.y), 0) * Time.deltaTime * 6);
			if (Mathf.Abs (targetY - transform.position.y) < .001f) {
				move = false;
				transform.position = new Vector3 (transform.position.x, targetY, transform.position.z);
			}
		} 
	}

	public void Move (bool up, float z) {
		transform.position = new Vector3 (transform.position.x, transform.position.y, z);
		if (up)
			targetY = Camera.main.transform.position.y - 5;
		else 
			targetY = Camera.main.transform.position.y - 6;
		move = true;
	}

	public void MoveBannerDownCoroutine () {
		StartCoroutine (MoveBannerDownHelper ());
	}

	IEnumerator MoveBannerDownHelper () {
		yield return new WaitForSeconds (2);
		Move (false, transform.position.z);
	}

	public void UpdateCoinNumber () {
//		foreach (Transform child in transform) {
//			Destroy (child.gameObject);
//		}
		if (digitList == null) {
			digitList = new List<GameObject> ();
		}
		string coinString = StoreInventory.GetItemBalance ("coin_currency_ID").ToString ();
		foreach (GameObject a in digitList) {
			//Debug.Log ("Destroying " + a);
			Destroy (a);
		}
		
		ModifyPositionVectors (coinString.Length);
		digitList.Clear ();
		if (coinString.Length == 1) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddFirstPosition.x - .25f, oddFirstPosition.y, oddFirstPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddFirstPosition, Quaternion.identity));
		}
		else if (coinString.Length == 2) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenSecondPosition.x - .25f, evenFirstPosition.y, evenFirstPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), evenSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), evenFirstPosition, Quaternion.identity));
		}
		else if (coinString.Length == 3) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (oddSecondPosition.x - .25f, oddSecondPosition.y, oddSecondPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = transform;
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[0]), oddSecondPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[1]), oddFirstPosition, Quaternion.identity));
			digitList.Add ((GameObject)Instantiate (GetNumberFromString (coinString[2]), oddThirdPosition, Quaternion.identity));
		}
		else if (coinString.Length == 4) {
			if (instantiatedCoin != null) {
				Destroy (instantiatedCoin);
			}
			instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (evenFourthPosition.x - .25f, evenFourthPosition.y, evenFourthPosition.z), Quaternion.identity);
			instantiatedCoin.transform.parent = transform;
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
			instantiatedCoin.transform.parent = transform;
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
			instantiatedCoin.transform.parent = transform;
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
			instantiatedCoin.transform.parent = transform;
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
			instantiatedCoin.transform.parent = transform;
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
			a.transform.parent = transform;
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
			evenFirstPosition = new Vector3 (.11f, transform.position.y + .2f, transform.position.z - .1f);
			evenSecondPosition = new Vector3 (-.11f, transform.position.y + .2f, transform.position.z - .1f);
			evenThirdPosition = new Vector3 (.33f, transform.position.y + .2f, transform.position.z - .1f);
			evenFourthPosition = new Vector3 (-.33f, transform.position.y + .2f, transform.position.z - .1f);
			evenFifthPosition = new Vector3 (.55f, transform.position.y + .2f, transform.position.z - .1f);
			evenSixthPosition = new Vector3 (-.55f, transform.position.y + .2f, transform.position.z - .1f);
			evenSeventhPosition = new Vector3 (.77f, transform.position.y + .2f, transform.position.z - .1f);
			evenEighthPosition = new Vector3 (-.77f, transform.position.y + .2f, transform.position.z - .1f);
		}
		else {
			oddFirstPosition = new Vector3 (0, transform.position.y + .2f, transform.position.z - .1f);
			oddSecondPosition = new Vector3 (-.22f, transform.position.y + .2f, transform.position.z - .1f);
			oddThirdPosition = new Vector3 (.22f, transform.position.y + .2f, transform.position.z - .1f);
			oddFourthPosition = new Vector3 (-.44f, transform.position.y + .2f, transform.position.z - .1f);
			oddFifthPosition = new Vector3 (.44f, transform.position.y + .2f, transform.position.z - .1f);
			oddSixthPosition = new Vector3 (-.66f, transform.position.y + .2f, transform.position.z - .1f);
			oddSeventhPosition = new Vector3 (.66f, transform.position.y + .2f, transform.position.z - .1f);
		}
	}

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
