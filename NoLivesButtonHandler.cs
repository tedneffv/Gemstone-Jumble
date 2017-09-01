using UnityEngine;
using System.Collections;

public class NoLivesButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedPurchaseCoinsButton, pressedPurchaseLivesButton, pressedPlayLevelButton;
	GameObject instantiatedPressedPurchaseCoinsButton, instantiatedPressedPurchaseLivesButton, instantiatedPressedPlayLevelButton;
	string levelToPlay;
	TransitionShadeController transitionShadeController;
	bool loadLevel, touchOn;
	SoundHandler soundHandler;

	void Awake () {
		touchOn = true;
	}

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (touchOn || gameObject.name != "Purchase Lives Button(Clone)") {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			} else if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			} else if (Input.GetMouseButtonUp (0)) {
				CheckTouch3 (Input.mousePosition);
			}
		}

//		if (loadLevel && transitionShadeController.GetAlpha () >= 1) {
//			//Debug.Log ("levelToPlay = " + levelToPlay);
//			Application.LoadLevel (levelToPlay);
//		}

	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject.name == "Purchase Coins Button(Clone)" && name == "Purchase Coins Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedPurchaseCoinsButton = (GameObject)Instantiate (pressedPurchaseCoinsButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedPurchaseCoinsButton.transform.parent = transform;
		}

		else if (hit != null && hit.gameObject.name == "Purchase Lives Button(Clone)" && name == "Purchase Lives Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedPurchaseLivesButton = (GameObject)Instantiate (pressedPurchaseLivesButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedPurchaseLivesButton.transform.parent = transform;
		}

		else if (hit != null && hit.gameObject.name == "Play Level Button(Clone)" && name == "Play Level Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedPlayLevelButton = (GameObject)Instantiate (pressedPlayLevelButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedPlayLevelButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if ((hit2 == null || hit2.gameObject.name != "Purchase Coins Button(Clone)") && instantiatedPressedPurchaseCoinsButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPurchaseCoinsButton);
		}

		else if ((hit2 == null || hit2.gameObject.name != "Purchase Lives Button(Clone)") && instantiatedPressedPurchaseLivesButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPurchaseLivesButton);
		}

		else if ((hit2 == null || hit2.gameObject.name != "Play Level Button(Clone)") && instantiatedPressedPlayLevelButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPlayLevelButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

	 	if (hit3 != null && hit3.gameObject.name == "Purchase Coins Button(Clone)" && instantiatedPressedPurchaseCoinsButton != null) {
			GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().GetRidOfScreen ();
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateScreen ();
		}

		else if (hit3 != null && hit3.gameObject.name == "Purchase Lives Button(Clone)" && instantiatedPressedPurchaseLivesButton != null) {
			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber () == 0) {
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().GetRidOfScreen ();
				if (GameObject.Find ("29 block") != null)
					GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().CameraOffsetPurchaseLivesScreen ();
				else
					GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().InstantiatedPurcahseLivesScreen ();
			}
		}

		else if (hit3 != null && hit3.gameObject.name == "Play Level Button(Clone)" && instantiatedPressedPlayLevelButton != null) {
			GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().GetRidOfScreen ();
			if (GameObject.Find ("The Mountain Level Progression") != null)
				GameObject.Find ("The Mountain Level Progression").GetComponent<MountainLevelSelectionTouchHandler> ().LoadLevelToPlay ();
			else if (GameObject.Find ("The City Level Progression") != null)
				GameObject.Find ("The City Level Progression").GetComponent<CityLevelSelectionTouchHandler> ().LoadLevelToPlay ();
			else if (GameObject.Find ("The Cabin Level Progression") != null)
				GameObject.Find ("The Cabin Level Progression").GetComponent<CabinLevelSelectionTouchHandler> ().LoadLevelToPlay ();
			else if (GameObject.Find ("Launchpad Level Progression") != null) 
				GameObject.Find ("Launchpad Level Progression").GetComponent<LaunchpadLevelSelectionTouchHandler> ().LoadLevelToPlay ();
//			levelToPlay = GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().GetLevelToPlay ();
//			transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
//			transitionShadeController.DarkenShade ();
			loadLevel = true;
		}

		if (instantiatedPressedPurchaseCoinsButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPurchaseCoinsButton);
		}
		if (instantiatedPressedPurchaseLivesButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPurchaseLivesButton);
		}
		if (instantiatedPressedPlayLevelButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPlayLevelButton);
		}
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
