using UnityEngine;
using System.Collections;

public class PlaySignTouch : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedPlaySign;
	GameObject instantiatedPressedPlaySign;
	TransitionShadeController transitionShadeController;
	bool loadLevel, touchOn;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
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
			} else if (loadLevel && transitionShadeController.GetAlpha () >= 1)
				Application.LoadLevel ("New Level Map");

	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedPlaySign = (GameObject)Instantiate (pressedPlaySign, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), gameObject.transform.rotation);
			instantiatedPressedPlaySign.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if ((hit2 == null || hit2.gameObject.name != "Play Sign Child") && instantiatedPressedPlaySign != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPlaySign);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		if (hit3 != null && hit3.gameObject.name == "Play Sign Child" && instantiatedPressedPlaySign != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPlaySign);
			transitionShadeController.DarkenShade ();
			loadLevel = true;
		}

		if (instantiatedPressedPlaySign != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPlaySign);
		}
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
