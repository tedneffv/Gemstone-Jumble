using UnityEngine;
using System.Collections;

public class OkayButtonHandler : MonoBehaviour {

	public GameObject pressedOkayButton;
	GameObject instantiatedPressedOkayButton;
	Collider2D hit;
	LevelFailedScreenSlider screenSlider;
	bool buttonPressed, tutorialOne, tutorialTwo;
	int buttonPressNum;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("Mountain Level One ID") != null)
			tutorialOne = true;
		else if (GameObject.Find ("Mountain Level Three ID") != null)
			tutorialTwo = true;
		screenSlider = GetComponent<LevelFailedScreenSlider> ();
		buttonPressNum = 0;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!screenSlider.Sliding () && !buttonPressed) {
			if (Input.GetMouseButtonDown (0)) 
				CheckTouch (Input.mousePosition);
			else if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			else if (Input.GetMouseButtonUp (0))
				CheckTouch3 (Input.mousePosition);
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject.name == "Okay Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedOkayButton = (GameObject)Instantiate (pressedOkayButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .01f), Quaternion.identity);
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject.name != "Okay Button(Clone)" && instantiatedPressedOkayButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedOkayButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (tutorialOne) {
			if (!GameObject.Find ("No Extras Shade").GetComponent<LevelOneShadeController> ().SpeechBubbleSliding ()) {
				if (buttonPressNum == 1 && hit != null && hit.gameObject.name == "Okay Button(Clone)" && instantiatedPressedOkayButton != null) {
					buttonPressed = true;
					GetComponent<LevelFailedScreenSlider> ().SetSlideSpeed (.1f);
					GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
					GameObject.Find ("No Extras Shade").GetComponent<LevelOneShadeController> ().LightenShade ();
					GameObject.Find ("No Extras Shade").GetComponent<LevelOneShadeController> ().NextSpeechBubble ();
					GameObject.Find ("No Extras Shade").GetComponent<OwlieHandler> ().GetRidOfOwlie ();
				}
				
				else if (buttonPressNum == 0 && hit != null && hit.gameObject.name == "Okay Button(Clone)" && instantiatedPressedOkayButton != null) {
					GameObject.Find ("No Extras Shade").GetComponent<LevelOneShadeController> ().NextSpeechBubble ();
					buttonPressNum++;
				}
			}
		} else if (tutorialTwo) {
			if (!GameObject.Find ("No Extras Shade").GetComponent<LevelThreeShadeController> ().SpeechBubbleSliding ()) {
				if (buttonPressNum == 0 && hit != null && hit.gameObject.name == "Okay Button(Clone)" && instantiatedPressedOkayButton != null) {
					buttonPressed = true;
					GetComponent<LevelFailedScreenSlider> ().SetSlideSpeed (.1f);
					GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
					GameObject.Find ("No Extras Shade").GetComponent<LevelThreeShadeController> ().LightenShade ();
					GameObject.Find ("No Extras Shade").GetComponent<LevelThreeShadeController> ().NextSpeechBubble ();
					GameObject.Find ("No Extras Shade").GetComponent<OwlieHandler> ().GetRidOfOwlie ();
				}
			}
		}
		if (instantiatedPressedOkayButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedOkayButton);
		}
	}
}
