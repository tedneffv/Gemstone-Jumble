using UnityEngine;
using System.Collections;

public class LevelFailedButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedRetryButton, pressedHomeButton;
	GameObject instantiatedPressedRetryButton, instantiatedPressedHomeButton;
	TransitionShadeController transitionShadeController;
	bool loadLevelInProgress;
	string levelToLoad;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}
		else if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}

		if (loadLevelInProgress && transitionShadeController.GetAlpha () >= 1)
			Application.LoadLevel (levelToLoad);
	}

	public void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.name == ("Level Failed Retry Button(Clone)") && name == "Level Failed Retry Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedRetryButton = (GameObject)Instantiate (pressedRetryButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedRetryButton.transform.parent = transform;
		}

		else if (hit != null && hit.gameObject.name == ("Level Failed Home Button(Clone)") && name == "Level Failed Home Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedHomeButton = (GameObject)Instantiate (pressedHomeButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedHomeButton.transform.parent = transform;
		}
	}

	public void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		if ((hit2 == null || hit2.gameObject.name != "Level Failed Retry Button(Clone)") && instantiatedPressedRetryButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedRetryButton);
		}

		if ((hit2 == null || hit2.gameObject.name != "Level Failed Home Button(Clone)") && instantiatedPressedHomeButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedHomeButton);
		}
	}

	public void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit3 != null && hit3.gameObject.name == "Level Failed Retry Button(Clone)" && instantiatedPressedRetryButton != null) {
			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetHeartNumber () == 0 && GameObject.Find ("Screen Handlers").GetComponent<LevelFailedScreenHandler> ().GetScreenInstantiated ()) {
				GameObject.Find ("Screen Handlers").GetComponent<LevelFailedScreenHandler> ().GetRidOfScreen ();
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().CameraOffsetPurchaseLivesScreen ();
				return; 
			}
			transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
			transitionShadeController.DarkenShade ();
			loadLevelInProgress = true;
			levelToLoad = Application.loadedLevelName;
		}

		else if (hit3 != null && hit3.gameObject.name == "Level Failed Home Button(Clone)" && instantiatedPressedHomeButton != null) {
			transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
			transitionShadeController.DarkenShade ();
			loadLevelInProgress = true;
			levelToLoad = "New Level Map";
		}

		if (instantiatedPressedRetryButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedRetryButton);
		}

		if (instantiatedPressedHomeButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedHomeButton);
		}

	}
}
