using UnityEngine;
using System.Collections;

public class MusicButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	bool reachedHome, returnHome, destroyButton, touchOn, hide;
	float targetXPosition, horizontalCoefficient;
	ButtonHandler buttonHandler;
	GameObject tempPressedMusicButton, instantiatedMusicButton;
	public GameObject pressedMusicButton, musicOffButton, musicOnButton;
	RockLevelController controller;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		horizontalCoefficient = .8f;
		if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient);
		else if (GameObject.Find ("Play Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x - .2f - (horizontalCoefficient);
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
		controller = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!reachedHome)
			transform.Translate (new Vector3 ((targetXPosition - transform.position.x), 0, 0) * .0003f * Screen.width);
		if (Mathf.Abs (targetXPosition - transform.position.x) < .001f) {
			touchOn = true;
			transform.position = new Vector3 (targetXPosition, transform.position.y, transform.position.z);
			reachedHome = true;
			if (destroyButton) {
				buttonHandler.SetMusicButton (null);
				Destroy (gameObject);
			}
		}

		if (touchOn) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedMusicButton != null) {
				CheckTouch3 (Input.mousePosition);
				Destroy (tempPressedMusicButton);
			}
		}
	}

	public void ReturnHome () {
		if (GameObject.Find ("Play Button(Clone)") != null)
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x;
		else if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x;
		reachedHome = false;
		destroyButton = true;
	}

	public void LeaveHome () {
		reachedHome = false;
		targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient);
		destroyButton = false;
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && (hit.gameObject.tag == "Music Button" || hit.gameObject.tag == "Music Off Button")) {
			soundHandler.PlayButtonClickDown ();
			tempPressedMusicButton = (GameObject)Instantiate(pressedMusicButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
			tempPressedMusicButton.transform.parent = hit.gameObject.transform;
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && (hit.gameObject.tag == "Music Button" || hit.gameObject.tag == "Music Off Button") && (hit2 == null || (hit2.gameObject.tag != "Music Button" && hit2.gameObject.tag != "Music Off Button"))) {
			if (tempPressedMusicButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedMusicButton);
			}
		}
	}
	
	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit3 != null && hit3.gameObject.tag == "Music Button") {
			controller.musicOn = false;
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetMusicOff ();
			instantiatedMusicButton = (GameObject)Instantiate(musicOffButton, gameObject.transform.position, Quaternion.identity);
			buttonHandler.SetMusicButton (instantiatedMusicButton);
			soundHandler.PlayButtonClickUp ();
			Destroy (tempPressedMusicButton);
			Destroy (gameObject);

		}
		else if (hit3 != null && hit3.gameObject.tag == "Music Off Button") {
			controller.musicOn = true;
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetMusicOn ();
			instantiatedMusicButton = (GameObject)Instantiate(musicOnButton, gameObject.transform.position, Quaternion.identity);
			buttonHandler.SetMusicButton (instantiatedMusicButton);
			soundHandler.PlayButtonClickUp ();
			Destroy (tempPressedMusicButton);
			Destroy (gameObject);

		}
	}

	public void DestroyTempPressedButton () {
		if (tempPressedMusicButton != null) 
			Destroy (tempPressedMusicButton);
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
