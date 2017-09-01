using UnityEngine;
using System.Collections;

public class NoLivesCoinInstantiator : MonoBehaviour {

	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, coin, yesButton, noButton;
	GameObject[] lifeCostArray, totalCoinArray;
	GameObject instantiatedCoin, instantiatedYesButton, instantiatedNoButton;
	TransitionShadeController transitionShadeController;
	int totalCoins;
	float coinTargetY, confirmationTargetY, confirmationYesTargetX, confirmationNoTargetX;
	bool moveButtons, changedMoveButtons, bounceAgain;

	// Use this for initialization
	void Start () {
		totalCoins = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetCoinTotal ();
//		totalCoins = 3823;
		lifeCostArray = new GameObject[5];
		totalCoinArray = new GameObject[7];
		coinTargetY = -1.8f;
		confirmationTargetY = -1;
		confirmationYesTargetX = 1.84f;
		confirmationNoTargetX = 1.84f;
		lifeCostArray[0] = (GameObject)Instantiate (coin, new Vector3 (1.48f, -1f, -1), Quaternion.identity);
		lifeCostArray[1] = (GameObject)Instantiate (five, new Vector3 (1.8f, -1f, -1), Quaternion.identity);
		lifeCostArray[2] = (GameObject)Instantiate (zero, new Vector3 (1.8f + .22f, -1f, -1), Quaternion.identity);
		lifeCostArray[3] = (GameObject)Instantiate (zero, new Vector3 (1.8f + (.22f * 2), -1f, -1), Quaternion.identity);
		lifeCostArray[4] = (GameObject)Instantiate (zero, new Vector3 (1.8f + (.22f * 3), -1f, -1), Quaternion.identity);
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
		instantiatedYesButton = (GameObject) Instantiate (yesButton, new Vector3 (1.84f, -1f, -1), Quaternion.identity);
		instantiatedNoButton = (GameObject) Instantiate (noButton, new Vector3 (1.84f, -1f, -1), Quaternion.identity);
		InstantiateCoinTotal ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transitionShadeController.GetAlpha () <= 0) {
			for (int i = 0; i < 5; i++) {
				if (Mathf.Abs (coinTargetY - lifeCostArray[i].transform.position.y) > .01f) {
					lifeCostArray[i].transform.Translate (new Vector3 (0, (coinTargetY - lifeCostArray[i].transform.position.y), 0) * Time.deltaTime * (9 - i/2));
				}
			}
		}
		if (moveButtons) {
			if (Mathf.Abs (confirmationTargetY - instantiatedYesButton.transform.position.y) > .01f || Mathf.Abs (confirmationYesTargetX - instantiatedYesButton.transform.position.x) > .01f) {
				instantiatedYesButton.transform.Translate (new Vector3 (confirmationYesTargetX - instantiatedYesButton.transform.position.x, (confirmationTargetY - instantiatedYesButton.transform.position.y), 0) * Time.deltaTime * 12);
			}
			if (Mathf.Abs (confirmationTargetY - instantiatedNoButton.transform.position.y) > .01f || Mathf.Abs (confirmationNoTargetX - instantiatedNoButton.transform.position.x) > .01f) {
				instantiatedNoButton.transform.Translate (new Vector3 (confirmationNoTargetX - instantiatedNoButton.transform.position.x, (confirmationTargetY - instantiatedNoButton.transform.position.y), 0) * Time.deltaTime * 12);
			}
			if (Mathf.Abs (confirmationTargetY - instantiatedYesButton.transform.position.y) <= .01f && Mathf.Abs (confirmationYesTargetX - instantiatedYesButton.transform.position.x) <= .01f &&
			    Mathf.Abs (confirmationTargetY - instantiatedNoButton.transform.position.y) <= .01f && Mathf.Abs (confirmationNoTargetX - instantiatedNoButton.transform.position.x) <= .01f)
				moveButtons = false;
		}
		if (!instantiatedNoButton.GetComponent<Rigidbody2D>().isKinematic || !instantiatedYesButton.GetComponent<Rigidbody2D>().isKinematic) {
			if (instantiatedNoButton.transform.position.y <= -.3f) {
				instantiatedNoButton.GetComponent<Rigidbody2D>().isKinematic = true;
				instantiatedNoButton.transform.position = new Vector3 (instantiatedNoButton.transform.position.x, -.3f, instantiatedNoButton.transform.position.z);
			}
			if (instantiatedYesButton.transform.position.y <= -.3f) {
				instantiatedYesButton.GetComponent<Rigidbody2D>().isKinematic = true;
				instantiatedYesButton.transform.position = new Vector3 (instantiatedYesButton.transform.position.x, -.3f, instantiatedYesButton.transform.position.z);
			}
		}
	}

	void InstantiateCoinTotal () {
		string totalCoinString = totalCoins.ToString ();
		int coinNumberLength = totalCoinString.Length;
		for (int i = 0; i < coinNumberLength; i++) {
			totalCoinArray[i] = (GameObject)Instantiate (GetNumberFromChar (totalCoinString[i]), Camera.main.ViewportToWorldPoint (new Vector3 (.95f - ((coinNumberLength - i) * .035f), .95f, 90)), Quaternion.identity);
			totalCoinArray[i].GetComponent<ScoreNumberSizeIncreaser> ().SetGrow (false);
		}
		instantiatedCoin = (GameObject)Instantiate (coin, Camera.main.ViewportToWorldPoint (new Vector3 (.95f - ((coinNumberLength + 1) * .035f + .025f), .95f, 90)), Quaternion.identity);
		instantiatedCoin.transform.Rotate (0, Random.Range (0, 360), 0);
//		totalCoinArray[7] = (GameObject)Instantiate(GetNumberFromInt (totalCoins % 10), Camera.main.ViewportToWorldPoint (new Vector3 (.9f, .95f, 90)), Quaternion.identity);
//		if (totalCoins / 100 % 10 != 0) {

//		}
	}

	public bool GetMoveButtons () {
		return moveButtons;
	}

	public void SetTargetY (float targetY) {
		this.coinTargetY = targetY;
		if (!changedMoveButtons) {
			moveButtons = true;
			changedMoveButtons = true;
			bounceAgain = true;
		}
		confirmationTargetY = -.2f;
		confirmationYesTargetX = 1.48f;
		confirmationNoTargetX = 2.2f;
	}

	GameObject GetNumberFromChar (char digit) {
		switch (digit) {
		case '0': return zero;
		case '1': return one;
		case '2': return two;
		case '3': return three;
		case '4': return four;
		case '5': return five;
		case '6': return six;
		case '7': return seven;
		case '8': return eight;
		case '9': return nine;
		}
		return null;
	}
}
