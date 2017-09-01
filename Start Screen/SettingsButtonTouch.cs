using UnityEngine;
using System.Collections;

public class SettingsButtonTouch : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedSettingsButton, shade, buyCoinsButton, soundButton, soundOffButton, musicButton, musicOffButton, highScoreButton, removeAdsButton;
	GameObject instantiatedPressedSettingsButton, instantiatedShade, instantiatedBuyCoinsButton, instantiatedSoundButton, instantiatedMusicButton, instantiatedHighScoreButton, instantiatedRemoveAdsButton;
	bool settingsInstantiated, touchOn;
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
			} else if (Input.GetKeyUp (KeyCode.Escape) && settingsInstantiated) {
				InstantiateSettingsScreen ();
			}
		
	}
	
	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));


		if (hit != null && hit.gameObject == gameObject) {
			soundHandler.PlayButtonClickDown ();
			if (GameObject.Find ("Level Map ID") != null) 
				instantiatedPressedSettingsButton = (GameObject)Instantiate (pressedSettingsButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler (0, 180, 0));
			else 
				instantiatedPressedSettingsButton = (GameObject)Instantiate (pressedSettingsButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedSettingsButton.transform.parent = transform;
		}
	}
	
	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if ((hit2 == null || hit2.gameObject.name != "Settings Button") && instantiatedPressedSettingsButton != null) {
			Destroy (instantiatedPressedSettingsButton);
			soundHandler.PlayButtonClickUp ();
		} 
	}
	
	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit3.gameObject.name == "Settings Button" && instantiatedPressedSettingsButton != null) {
			InstantiateSettingsScreen ();
		}

		if (instantiatedPressedSettingsButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedSettingsButton);
		}
	}

	public void InstantiateSettingsScreen () {
		if (!settingsInstantiated) {
			settingsInstantiated = true;
			soundHandler.PlayWoosh ();
			if (GameObject.Find ("Shade(Clone)") == null) {
				instantiatedShade = (GameObject)Instantiate (shade, new Vector3 (0, 0, -6), Quaternion.identity);
				instantiatedShade.GetComponent<ShadeController> ().DarkenShade ();
			}

			float buttonY;
			if (!PlayerPrefs.HasKey ("removeAds")) {
				buttonY = 0.6f;
				instantiatedRemoveAdsButton = Instantiate (removeAdsButton, new Vector3 (-40, -.93f, -10), Quaternion.identity) as GameObject;
				instantiatedRemoveAdsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			} else 
				buttonY = 0;
			
			instantiatedBuyCoinsButton = (GameObject)Instantiate (buyCoinsButton, new Vector3 (-10, buttonY, -10), Quaternion.identity);
			instantiatedBuyCoinsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.75f);
			
			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetSoundState ()) {
				instantiatedSoundButton = (GameObject)Instantiate (soundButton, new Vector3 (-30, buttonY, -8), Quaternion.identity);
				instantiatedSoundButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			} else {
				instantiatedSoundButton = (GameObject)Instantiate (soundOffButton, new Vector3 (-30, buttonY, -8), Quaternion.identity);
				instantiatedSoundButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			}
			
			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetMusicState ()) {
				instantiatedMusicButton = (GameObject)Instantiate (musicButton, new Vector3 (-40, buttonY, -7), Quaternion.identity);
				instantiatedMusicButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.75f);
			} else {
				instantiatedMusicButton = (GameObject)Instantiate (musicOffButton, new Vector3 (-40, buttonY, -7), Quaternion.identity);
				instantiatedMusicButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.75f);
			}
			

		}
		else if (settingsInstantiated) {
			settingsInstantiated = false;
			instantiatedShade.GetComponent<ShadeController> ().LightenShade ();
			instantiatedBuyCoinsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedSoundButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedMusicButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			if (instantiatedRemoveAdsButton != null)
				instantiatedRemoveAdsButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		}
	}

	public void SetInstantiatedMusicButton (GameObject button) {
		instantiatedMusicButton = button;
	}

	public void SetInstantiatedSoundButton (GameObject button) {
		instantiatedSoundButton = button;
	}

	public void SetSettingsInstantiated (bool settingsInstantiated) {
		this.settingsInstantiated = settingsInstantiated;
	}

	public GameObject GetHighScoreButton () {
		return instantiatedHighScoreButton;
	}

	public GameObject GetSoundButton () {
		if (instantiatedSoundButton != null) {
			return instantiatedSoundButton;
		}
		return null;
	}

	public GameObject GetMusicButton () {
		if (instantiatedMusicButton != null) {
			return instantiatedMusicButton;
		}
		return null;
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}

}
