using UnityEngine;
using System.Collections;

public class SoundButtonTouch : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedSoundButton, SoundOffButton, SoundOnButton;
	GameObject instantiatedPressedSoundButton;
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
		}
	}
	
	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		
		if (hit != null && (hit.gameObject.name == "Sound Button(Clone)" || hit.gameObject.name == "Sound Off Button(Clone)")) {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedSoundButton = (GameObject)Instantiate (pressedSoundButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedSoundButton.transform.parent = transform;
		}
	}
	
	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		
		if (hit2 == null || (hit2.gameObject.name != "Sound Button(Clone)" && hit2.gameObject.name != "Sound Off Button(Clone)") && instantiatedPressedSoundButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedSoundButton);
		}
	}
	
	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		
		if (hit3 != null && hit3.gameObject.name == "Sound Button(Clone)" && instantiatedPressedSoundButton != null) {
			soundHandler.PlayButtonClickUp ();
			GameObject tempObject = (GameObject)Instantiate (SoundOffButton, transform.position, Quaternion.identity);
			tempObject.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().SetInstantiatedSoundButton (tempObject);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetSoundOff ();
			Destroy (instantiatedPressedSoundButton);
			Destroy (gameObject);
		}
		
		else if (hit3 != null && hit3.gameObject.name == "Sound Off Button(Clone)" && instantiatedPressedSoundButton != null) {
			soundHandler.PlayButtonClickUp ();
			GameObject tempObject = (GameObject)Instantiate (SoundOnButton, transform.position, Quaternion.identity);
			tempObject.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().SetInstantiatedSoundButton (tempObject);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetSoundOn ();
			Destroy (instantiatedPressedSoundButton);
			Destroy (gameObject);
		}
		
		if (instantiatedPressedSoundButton != null) {
			Destroy (instantiatedPressedSoundButton);
		}
	}
}
