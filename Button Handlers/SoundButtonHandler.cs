using UnityEngine;
using System.Collections;

public class SoundButtonHandler : MonoBehaviour {

	Collider2D hit,hit2,hit3;
	bool reachedHome, returnHome, destroyButton, touchOn;
	float targetXPosition, horizontalCoefficient;
	ButtonHandler buttonHandler;
	GameObject tempPressedSoundButton, instantiatedSoundButton;
	public GameObject pressedSoundButton, soundOffButton, soundOnButton;
	RockLevelController controller;
	Texture2D heightMap;
	SoundHandler soundHandler;
	
	// Use this for initialization
	void Start () {
		horizontalCoefficient = .8f;
		if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 2);
		else
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 2);
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
				buttonHandler.SetSoundButton (null);
				Destroy (gameObject);
			}
		}
		if (touchOn) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedSoundButton != null) {
				CheckTouch3 (Input.mousePosition);
			}
		}
	}
	
	public void ReturnHome () {
		touchOn = false;
		if (GameObject.Find ("Play Button(Clone)") != null)
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x;
		reachedHome = false;
		destroyButton = true;
	}
	
	public void LeaveHome () {
		reachedHome = false;
		targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 2);
		destroyButton = false;
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && (hit.gameObject.tag == "Sound On Button" || hit.gameObject.tag == "Sound Off Button")) {
			soundHandler.PlayButtonClickDown ();
			tempPressedSoundButton = (GameObject)Instantiate(pressedSoundButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && (hit.gameObject.tag == "Sound On Button" || hit.gameObject.tag == "Sound Off Button") && (hit2 == null || (hit2.gameObject.tag != "Sound Off Button" && hit2.gameObject.tag != "Sound On Button"))) {
			if (tempPressedSoundButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedSoundButton);
			}
		}
	}
	
	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);
		
		if (hit3 != null && hit3.gameObject.tag == "Sound On Button" && tempPressedSoundButton != null) {
			soundHandler.PlayButtonClickUp ();
			controller.soundOn = false;
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetSoundOff ();
			instantiatedSoundButton = (GameObject)Instantiate(soundOffButton, gameObject.transform.position, Quaternion.identity);
			buttonHandler.SetSoundButton (instantiatedSoundButton);
			Destroy (tempPressedSoundButton);
			Destroy (gameObject);
			
		}
		else if (hit3 != null && hit3.gameObject.tag == "Sound Off Button" && tempPressedSoundButton != null) {
			soundHandler.PlayButtonClickUp ();
			controller.soundOn = true;
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetSoundOn ();
			instantiatedSoundButton = (GameObject)Instantiate(soundOnButton, gameObject.transform.position, Quaternion.identity);
			buttonHandler.SetSoundButton (instantiatedSoundButton);
			Destroy (tempPressedSoundButton);
			Destroy (gameObject);
			
		}
	}

	public void DestroyTempPressedButton () {
		if (tempPressedSoundButton != null) 
			Destroy (tempPressedSoundButton);
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
