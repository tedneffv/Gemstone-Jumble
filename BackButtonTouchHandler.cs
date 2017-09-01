using UnityEngine;
using System.Collections;

public class BackButtonTouchHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedBackButton;
	GameObject instantiatedPressedBackButton;
	bool loadLevelScreen;
	TransitionShadeController transitionShadeController;

	// Use this for initialization
	void Start () {
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0))
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButton (0)) 
			CheckTouch2 (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0)) 
			CheckTouch3 (Input.mousePosition);

		if (loadLevelScreen) {
			if (transitionShadeController.GetAlpha () >= 1) {
				if (GameObject.Find ("The Mountain Level Progression") != null) {
					GameObject.Find ("Level Selecton Position Saver").GetComponent<MountainLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
				}
				else if (GameObject.Find ("The City Level Progression") != null) {
					GameObject.Find ("Level Selection Position Saver").GetComponent<CityLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
				}
				else if (GameObject.Find ("The Cabin Level Progression") != null) {
					GameObject.Find ("Level Selection Position Saver").GetComponent<CabinLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
				}
				else if (GameObject.Find ("Launchpad Level Progression") != null) {
					GameObject.Find ("Level Selection Position Saver").GetComponent<LaunchpadLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
				}
				Application.LoadLevel ("New Level Map");
			}
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			instantiatedPressedBackButton = (GameObject)Instantiate (pressedBackButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .01f), Quaternion.identity);
			instantiatedPressedBackButton.transform.parent = transform.parent;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit2 == null || (hit2.gameObject != gameObject)) {
			if (instantiatedPressedBackButton != null)
				Destroy (instantiatedPressedBackButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit3.gameObject == gameObject) {
			if (instantiatedPressedBackButton != null) {
				loadLevelScreen = true;
				transitionShadeController.DarkenShade ();
			}
		}
	}
}
