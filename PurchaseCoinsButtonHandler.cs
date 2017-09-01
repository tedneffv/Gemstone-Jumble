using UnityEngine;
using System.Collections;

public class PurchaseCoinsButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedFirstButton, pressedSecondButton, pressedThirdButton, pressedFourthButton, pressedFifthButton, pressedSixthButton;
	GameObject instantiatedPressedButton;
	Vector3 firstPress, secondPress;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		} else if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		} else if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		} else if (Input.GetKeyUp (KeyCode.Escape) && name == "$.99 Button(Clone)" && GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetScreenInstantiated ()) {
			if (Time.timeScale == 0) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
			}
			else if (GameObject.Find ("Settings Button") != null && GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().GetMusicButton () == null &&
				    GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().GetSoundButton () == null && GameObject.Find ("But Coins Button") == null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
				GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().InstantiateSettingsScreen ();
			} else if (GameObject.Find ("Settings Button") == null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
			}
		}

	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		firstPress = wp;
		if (hit != null) {
			if (hit.gameObject.name == "$.99 Button(Clone)" && name == "$.99 Button(Clone)") {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedButton = (GameObject)Instantiate (pressedFirstButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
				instantiatedPressedButton.transform.parent = hit.gameObject.transform;
			}
			else if (hit.gameObject.name == "$2.99 Button(Clone)" && name == "$2.99 Button(Clone)") {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedButton = (GameObject)Instantiate (pressedSecondButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
				instantiatedPressedButton.transform.parent = hit.gameObject.transform;
			}
			else if (hit.gameObject.name == "$4.99 Button(Clone)" && name == "$4.99 Button(Clone)") {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedButton = (GameObject)Instantiate (pressedThirdButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
				instantiatedPressedButton.transform.parent = hit.gameObject.transform;
			}
			else if (hit.gameObject.name == "$9.99 Button(Clone)" && name == "$9.99 Button(Clone)") {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedButton = (GameObject)Instantiate (pressedFourthButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
				instantiatedPressedButton.transform.parent = hit.gameObject.transform;
			}
			else if (hit.gameObject.name == "$19.99 Button(Clone)" && name == "$19.99 Button(Clone)") {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedButton = (GameObject)Instantiate (pressedFifthButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
				instantiatedPressedButton.transform.parent = hit.gameObject.transform;
			}
			else if (hit.gameObject.name == "$49.99 Button(Clone)" && name == "$49.99 Button(Clone)") {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedButton = (GameObject)Instantiate (pressedSixthButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
				instantiatedPressedButton.transform.parent = hit.gameObject.transform;
			}
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		secondPress = wp;
		if (instantiatedPressedButton != null) {
			if (hit2 == null || hit2.gameObject.name != name) {
				soundHandler.PlayButtonClickUp ();
				Destroy (instantiatedPressedButton);
			}
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit3.gameObject.name == "$.99 Button(Clone)" && instantiatedPressedButton != null && name == "$.99 Button(Clone)") {
			StoreAssets.BuyFiveThousandCoinPack ();
		}

		else if (hit3 != null && hit3.gameObject.name == "$2.99 Button(Clone)" && instantiatedPressedButton != null && name == "$2.99 Button(Clone)") {
			StoreAssets.BuyTwentyThousandCoinPack ();
		}

		else if (hit3 != null && hit3.gameObject.name == "$4.99 Button(Clone)" && instantiatedPressedButton != null && name == "$4.99 Button(Clone)") {
			StoreAssets.BuyFiftyThousandCoinPack ();
		}

		else if (hit3 != null && hit3.gameObject.name == "$9.99 Button(Clone)" && instantiatedPressedButton != null && name == "$9.99 Button(Clone)") {
			StoreAssets.BuyOneHundredTwentyFiveThousandCoinPack ();
		}

		else if (hit3 != null && hit3.gameObject.name == "$19.99 Button(Clone)" && instantiatedPressedButton != null && name == "$19.99 Button(Clone)") {
			StoreAssets.BuyThreeHundredThousandCoinPack ();
		}

		else if (hit3 != null && hit3.gameObject.name == "$49.99 Button(Clone)" && instantiatedPressedButton != null && name == "$49.99 Button(Clone)") {
			StoreAssets.BuyOneMillionCoinPack ();
		}



		if (instantiatedPressedButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedButton);
		}
	}
}
