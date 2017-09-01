using UnityEngine;
using System.Collections;

public class PauseButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedPauseButton, playButton, quitButton, restartButton, musicButton, musicOffButton, soundButton, soundOffButton, buyCoinsButton, pausedShade, levelFailedShade;
	public GameObject letterD, letterE, letterS, letterU, letterA, letterP;
	bool touchOn, instantiateShade, buttonClickUp;
	GameObject tempPressedPauseButton, instantiatedPausedShade, instantiatedP, instantiatedA, instantiatedU, instantiatedS, instantiatedE, instantiatedD;
	PausedShadeHandler shadeHandler;
	PausedWordHandler pausedWordHandler;
	RockLevelTouchHandler touchHandler;
	RockLevelController controller;
	ButtonHandler buttonHandler;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
		controller = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		touchOn = true;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (touchOn) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedPauseButton != null) {
				CheckTouch3 (Input.mousePosition);
				Destroy (tempPressedPauseButton);
			}
		}
		if (instantiateShade && GameObject.Find ("Paused Shade(Clone)") == null) {
			if (GameObject.Find ("Time Bomb Identification") != null) {
				GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted (false);
			}
			GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().SetGameStarted (false);
			GameObject instantiatedShade = (GameObject)Instantiate (levelFailedShade);
			DarkenOnInstantiaton darken = instantiatedShade.GetComponent<DarkenOnInstantiaton> ();
			darken.SetLevelComplete (false);
			instantiateShade = false;
		}
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		if (hit != null && hit.gameObject.tag == "Pause Button" && tempPressedPauseButton == null) {
			soundHandler.PlayButtonClickDown ();
			tempPressedPauseButton = (GameObject)Instantiate(pressedPauseButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}

	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject.tag == "Pause Button" && hit2 == null) {
			if (tempPressedPauseButton != null) {
				buttonClickUp = true;
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedPauseButton);
			}
		}
	}

	public GameObject GetP () {
		return letterP;
	}

	public GameObject GetA () {
		return letterA;
	}

	public GameObject GetU () {
		return letterU;
	}

	public GameObject GetS () {
		return letterS;
	}

	public GameObject GetE () {
		return letterE;
	}

	public GameObject GetD () {
		return letterD;
	}

	public void InstantiateShade () {
		instantiateShade = true;
	}

	public void GoHome () {
		if (tempPressedPauseButton != null) {
			Destroy (tempPressedPauseButton);
		}
		if (buttonHandler.GetD () != null) {
			pausedWordHandler = buttonHandler.GetD ().GetComponent<PausedWordHandler> ();
			buttonHandler.GetD ().transform.parent = gameObject.transform;
			pausedWordHandler.GoHome ();
		}
		
		if (buttonHandler.GetE () != null) {
			pausedWordHandler = buttonHandler.GetE ().GetComponent<PausedWordHandler> ();
			buttonHandler.GetE ().transform.parent = gameObject.transform;
			pausedWordHandler.GoHome ();
		}
		
		if (buttonHandler.GetS () != null) {
			pausedWordHandler = buttonHandler.GetS ().GetComponent<PausedWordHandler> ();
			buttonHandler.GetS ().transform.parent = gameObject.transform;
			pausedWordHandler.GoHome ();
		}
		
		if (buttonHandler.GetU () != null) {
			pausedWordHandler = buttonHandler.GetU ().GetComponent<PausedWordHandler> ();
			buttonHandler.GetU ().transform.parent = gameObject.transform;
			pausedWordHandler.GoHome ();
		}
		
		if (buttonHandler.GetA () != null) {
			pausedWordHandler = buttonHandler.GetA ().GetComponent<PausedWordHandler> ();
			buttonHandler.GetA ().transform.parent = gameObject.transform;
			pausedWordHandler.GoHome ();
		}
		
		if (buttonHandler.GetP () != null) {
			pausedWordHandler = buttonHandler.GetP ().GetComponent<PausedWordHandler> ();
			buttonHandler.GetP ().transform.parent = gameObject.transform;
			pausedWordHandler.GoHome ();
		}

		if (buttonHandler.GetSoundButton () != null) {
			buttonHandler.GetSoundButton ().transform.Translate (0, 0, 88);
			buttonHandler.GetSoundButton ().GetComponent<SoundButtonHandler> ().ReturnHome ();
		}

		if (buttonHandler.GetMusicButton () != null) {
			buttonHandler.GetMusicButton ().transform.Translate (0, 0, 88);
			buttonHandler.GetMusicButton ().GetComponent<MusicButtonHandler> ().ReturnHome ();
		}

//		if (buttonHandler.GetRestartButton () != null) {
//			buttonHandler.GetRestartButton ().transform.Translate (0, 0, 88);
//			buttonHandler.GetRestartButton ().GetComponent<RestartButtonHandler> ().ReturnHome ();
//		}

		if (buttonHandler.GetQuitButton () != null) {
			buttonHandler.GetQuitButton ().transform.Translate (0, 0, 88);
			buttonHandler.GetQuitButton ().GetComponent<QuitButtonHandler> ().ReturnHome ();
		}

		if (buttonHandler.GetBuyCoinsButton () != null) {
			buttonHandler.GetBuyCoinsButton ().transform.Translate (0, 0, 88);
			buttonHandler.GetBuyCoinsButton ().GetComponent<BuyCoinsButtonHandler> ().ReturnHome ();
		}
	
	}

	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit3 != null && hit3.gameObject.tag == "Pause Button") {
			if (!buttonClickUp) {
				soundHandler.PlayButtonClickUp ();
			} else 
				buttonClickUp = false;
			touchHandler.PauseTouch ();
			Time.timeScale = 0;
			if (buttonHandler.GetD () == null) 
				buttonHandler.SetD ((GameObject)Instantiate (letterD, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			pausedWordHandler = buttonHandler.GetD ().GetComponent<PausedWordHandler> ();
			pausedWordHandler.LeaveHome ();

			if (buttonHandler.GetE () == null) 
				buttonHandler.SetE ((GameObject)Instantiate (letterE, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			pausedWordHandler = buttonHandler.GetE ().GetComponent<PausedWordHandler> ();
			pausedWordHandler.LeaveHome ();


			if (buttonHandler.GetS () == null) 
				buttonHandler.SetS ((GameObject)Instantiate (letterS, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
			pausedWordHandler = buttonHandler.GetS ().GetComponent<PausedWordHandler> ();
			pausedWordHandler.LeaveHome ();

			if (buttonHandler.GetU () == null)
				buttonHandler.SetU ((GameObject)Instantiate (letterU, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			pausedWordHandler = buttonHandler.GetU ().GetComponent<PausedWordHandler> ();
			pausedWordHandler.LeaveHome ();

			if (buttonHandler.GetA () == null)
				buttonHandler.SetA ((GameObject)Instantiate (letterA, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
			pausedWordHandler = buttonHandler.GetA ().GetComponent<PausedWordHandler> ();
			pausedWordHandler.LeaveHome ();

			if (buttonHandler.GetP () == null)
				buttonHandler.SetP ((GameObject)Instantiate (letterP, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
			pausedWordHandler = buttonHandler.GetP ().GetComponent<PausedWordHandler> ();
			pausedWordHandler.LeaveHome ();

			if (GameObject.Find ("Paused Shade(Clone)") == null)
				instantiatedPausedShade = (GameObject)Instantiate (pausedShade);
			else 
				instantiatedPausedShade = GameObject.Find ("Paused Shade(Clone)");
			shadeHandler = instantiatedPausedShade.GetComponent<PausedShadeHandler> ();
			shadeHandler.MakeVisible ();

			Instantiate (playButton, gameObject.transform.position, Quaternion.identity);
			if (buttonHandler.GetMusicButton () == null) {
				if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetMusicState ())
					buttonHandler.SetMusicButton ((GameObject)Instantiate (musicButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
				else 
					buttonHandler.SetMusicButton ((GameObject)Instantiate (musicOffButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			}
			else if (buttonHandler.GetMusicButton () != null) {
				MusicButtonHandler musicButtonHandler = buttonHandler.GetMusicButton ().GetComponent<MusicButtonHandler> ();
				musicButtonHandler.LeaveHome ();
			} 

			if (buttonHandler.GetSoundButton () == null) {
				if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetSoundState ())
					buttonHandler.SetSoundButton ((GameObject)Instantiate (soundButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
				else
					buttonHandler.SetSoundButton ((GameObject)Instantiate (soundOffButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			}
			else if (buttonHandler.GetSoundButton () != null) {
				SoundButtonHandler soundButtonHandler = buttonHandler.GetSoundButton ().GetComponent<SoundButtonHandler> ();
				soundButtonHandler.LeaveHome ();
			}

//			if (buttonHandler.GetRestartButton () == null) 
//				buttonHandler.SetRestartButton ((GameObject)Instantiate (restartButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
//			else if (buttonHandler.GetRestartButton () != null) {
//				RestartButtonHandler restartButtonHandler = buttonHandler.GetRestartButton ().GetComponent<RestartButtonHandler> ();
//				restartButtonHandler.LeaveHome ();
//			}

			if (buttonHandler.GetQuitButton () == null)
				buttonHandler.SetQuitButton ((GameObject)Instantiate (quitButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			else if (buttonHandler.GetQuitButton () != null) {
				QuitButtonHandler quitButtonHandler = buttonHandler.GetQuitButton ().GetComponent<QuitButtonHandler> ();
				quitButtonHandler.LeaveHome ();
			}

			if (buttonHandler.GetBuyCoinsButton () == null) 
				buttonHandler.SetBuyCoinsButton ((GameObject)Instantiate (buyCoinsButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			else if (buttonHandler.GetBuyCoinsButton () != null) {
				//Debug.Log ("Ordering buyCoinsButton to leave home");
				BuyCoinsButtonHandler buyCoinsButtonHandler = buttonHandler.GetBuyCoinsButton ().GetComponent<BuyCoinsButtonHandler> ();
				buyCoinsButtonHandler.LeaveHome ();
			}
			Destroy (gameObject);
		}
	}

	public void PauseGameRemotely () {
		touchHandler.PauseTouch ();
		Time.timeScale = 0;
		if (buttonHandler.GetD () == null) 
			buttonHandler.SetD ((GameObject)Instantiate (letterD, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
		pausedWordHandler = buttonHandler.GetD ().GetComponent<PausedWordHandler> ();
		pausedWordHandler.LeaveHome ();
		
		if (buttonHandler.GetE () == null) 
			buttonHandler.SetE ((GameObject)Instantiate (letterE, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
		pausedWordHandler = buttonHandler.GetE ().GetComponent<PausedWordHandler> ();
		pausedWordHandler.LeaveHome ();
		
		
		if (buttonHandler.GetS () == null) 
			buttonHandler.SetS ((GameObject)Instantiate (letterS, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
		pausedWordHandler = buttonHandler.GetS ().GetComponent<PausedWordHandler> ();
		pausedWordHandler.LeaveHome ();
		
		if (buttonHandler.GetU () == null)
			buttonHandler.SetU ((GameObject)Instantiate (letterU, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
		pausedWordHandler = buttonHandler.GetU ().GetComponent<PausedWordHandler> ();
		pausedWordHandler.LeaveHome ();
		
		if (buttonHandler.GetA () == null)
			buttonHandler.SetA ((GameObject)Instantiate (letterA, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
		pausedWordHandler = buttonHandler.GetA ().GetComponent<PausedWordHandler> ();
		pausedWordHandler.LeaveHome ();
		
		if (buttonHandler.GetP () == null)
			buttonHandler.SetP ((GameObject)Instantiate (letterP, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
		pausedWordHandler = buttonHandler.GetP ().GetComponent<PausedWordHandler> ();
		pausedWordHandler.LeaveHome ();
		
		if (GameObject.Find ("Paused Shade(Clone)") == null)
			instantiatedPausedShade = (GameObject)Instantiate (pausedShade);
		else 
			instantiatedPausedShade = GameObject.Find ("Paused Shade(Clone)");
		shadeHandler = instantiatedPausedShade.GetComponent<PausedShadeHandler> ();
		shadeHandler.MakeVisible ();
		
		Instantiate (playButton, gameObject.transform.position, Quaternion.identity);
		if (buttonHandler.GetMusicButton () == null) {
			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetMusicState ())
				buttonHandler.SetMusicButton ((GameObject)Instantiate (musicButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			else 
				buttonHandler.SetMusicButton ((GameObject)Instantiate (musicOffButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
		}
		else if (buttonHandler.GetMusicButton () != null) {
			MusicButtonHandler musicButtonHandler = buttonHandler.GetMusicButton ().GetComponent<MusicButtonHandler> ();
			musicButtonHandler.LeaveHome ();
		} 
		
		if (buttonHandler.GetSoundButton () == null) {
			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetSoundState ())
				buttonHandler.SetSoundButton ((GameObject)Instantiate (soundButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			else
				buttonHandler.SetSoundButton ((GameObject)Instantiate (soundOffButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
		}
		else if (buttonHandler.GetSoundButton () != null) {
			SoundButtonHandler soundButtonHandler = buttonHandler.GetSoundButton ().GetComponent<SoundButtonHandler> ();
			soundButtonHandler.LeaveHome ();
		}
		
		//			if (buttonHandler.GetRestartButton () == null) 
		//				buttonHandler.SetRestartButton ((GameObject)Instantiate (restartButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
		//			else if (buttonHandler.GetRestartButton () != null) {
		//				RestartButtonHandler restartButtonHandler = buttonHandler.GetRestartButton ().GetComponent<RestartButtonHandler> ();
		//				restartButtonHandler.LeaveHome ();
		//			}
		
		if (buttonHandler.GetQuitButton () == null)
			buttonHandler.SetQuitButton ((GameObject)Instantiate (quitButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
		else if (buttonHandler.GetQuitButton () != null) {
			QuitButtonHandler quitButtonHandler = buttonHandler.GetQuitButton ().GetComponent<QuitButtonHandler> ();
			quitButtonHandler.LeaveHome ();
		}
		
		if (buttonHandler.GetBuyCoinsButton () == null) 
			buttonHandler.SetBuyCoinsButton ((GameObject)Instantiate (buyCoinsButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
		else if (buttonHandler.GetBuyCoinsButton () != null) {
			//Debug.Log ("Ordering buyCoinsButton to leave home");
			BuyCoinsButtonHandler buyCoinsButtonHandler = buttonHandler.GetBuyCoinsButton ().GetComponent<BuyCoinsButtonHandler> ();
			buyCoinsButtonHandler.LeaveHome ();
		}
		Destroy (gameObject);
	}
}
