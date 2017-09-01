using UnityEngine;
using System.Collections;

public class MusicButtonTouch : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedMusicButton, musicOffButton, musicOnButton;
	GameObject instantiatedPressedMusicButton;
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
		
		if (hit != null && (hit.gameObject.name == "Music Button(Clone)" || hit.gameObject.name == "Music Off Button(Clone)")) {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedMusicButton = (GameObject)Instantiate (pressedMusicButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedMusicButton.transform.parent = transform;
		}
	}
	
	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		
		if (hit2 == null || (hit2.gameObject.name != "Music Button(Clone)" && hit2.gameObject.name != "Music Off Button(Clone)") && instantiatedPressedMusicButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedMusicButton);
		}
	}
	
	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		
		if (hit3 != null && hit3.gameObject.name == "Music Button(Clone)" && instantiatedPressedMusicButton != null) {
			GameObject tempObject = (GameObject)Instantiate (musicOffButton, transform.position, Quaternion.identity);
			tempObject.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.75f);
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().SetInstantiatedMusicButton (tempObject);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetMusicOff ();
			Destroy (instantiatedPressedMusicButton);
			Destroy (gameObject);
		}

		else if (hit3 != null && hit3.gameObject.name == "Music Off Button(Clone)" && instantiatedPressedMusicButton != null) {
			GameObject tempObject = (GameObject)Instantiate (musicOnButton, transform.position, Quaternion.identity);
			tempObject.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.75f);
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().SetInstantiatedMusicButton (tempObject);
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetMusicOn ();
			Destroy (instantiatedPressedMusicButton);
			Destroy (gameObject);
		}
		
		if (instantiatedPressedMusicButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedMusicButton);
		}
	}
}
