using UnityEngine;
using System.Collections;

public class PlayButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	bool waitingForQuitDestruction, touchOn, instantiateShade;
	public GameObject pressedPlayButton, pauseButton, pausedSlide, levelFailedShade;
	public GameObject letterP, letterA, letterU, letterS, letterE, letterD;
	public GameObject musicButton, musicOffButton, soundButton, soundOffButton, quitButton, buyCoinsButton;
	GameObject tempPressedPlayButton, instantiatedPausedShade;
	RockLevelTouchHandler touchHandler;
	ButtonHandler buttonHandler;
	MusicButtonHandler musicButtonHandler;
	SoundButtonHandler soundButtonHandler;
	RestartButtonHandler restartButtonHandler;
	QuitButtonHandler quitButtonHandler;
	BuyCoinsButtonHandler buyCoinsButtonHandler;
	PausedShadeHandler shadeHandler;
	PausedWordHandler pausedWordHandler;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
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
			if (Input.GetMouseButtonUp (0) && tempPressedPlayButton != null) {
				CheckTouch3 (Input.mousePosition);
			}
		}
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && hit.gameObject.tag == "Play Button") {
			soundHandler.PlayButtonClickDown ();
			tempPressedPlayButton = (GameObject)Instantiate(pressedPlayButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.tag == "Play Button" && (hit2 == null || hit2.gameObject.tag != "Play Button")) {
			if (tempPressedPlayButton != null) {	
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedPlayButton);
			}
		}
	}

	public bool GetWaitingForQuitDestruction () {
		return waitingForQuitDestruction;
	}

	public void GoHome () {

		if (GameObject.Find ("Quit Button(Clone)") != null)
			GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().DestroyTempPressedButton ();
		if (GameObject.Find ("Sound Off Button(Clone)") != null)
			GameObject.Find ("Sound Off Button(Clone)").GetComponent<SoundButtonHandler> ().DestroyTempPressedButton ();
		if (GameObject.Find ("Music Off Button(Clone)") != null)
			GameObject.Find ("Music Off Button(Clone)").GetComponent<MusicButtonHandler> ().DestroyTempPressedButton ();
		if (GameObject.Find ("Sound On Button(Clone)") != null)
			GameObject.Find ("Sound On Button(Clone)").GetComponent<SoundButtonHandler> ().DestroyTempPressedButton ();
		if (GameObject.Find ("Music On Button(Clone)") != null)
			GameObject.Find ("Music On Button(Clone)").GetComponent<MusicButtonHandler> ().DestroyTempPressedButton ();

		if (buttonHandler.GetRestartFirstR () != null) {
			buttonHandler.GetRestartFirstR ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartE () != null) {
			buttonHandler.GetRestartE ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartS () != null) {
			buttonHandler.GetRestartS ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartFirstT () != null) {
			buttonHandler.GetRestartFirstT ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartA () != null) {
			buttonHandler.GetRestartA ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartSecondR () != null) {
			buttonHandler.GetRestartSecondR ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartSecondT () != null) {
			buttonHandler.GetRestartSecondT ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartConfirmationButton () != null) {
			buttonHandler.GetRestartConfirmationButton ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetRestartCancelButton () != null) {
			buttonHandler.GetRestartCancelButton ().GetComponent<RestartWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetQuitQ () != null) {
			buttonHandler.GetQuitQ ().GetComponent<QuitWordHandler> ().GoHome ();
		}
		
		if (buttonHandler.GetQuitU () != null) {
			buttonHandler.GetQuitU ().GetComponent<QuitWordHandler> ().GoHome ();
		}
		
		if (buttonHandler.GetQuitI () != null) {
			buttonHandler.GetQuitI ().GetComponent<QuitWordHandler> ().GoHome ();
		}
		
		if (buttonHandler.GetQuitT () != null) {
			buttonHandler.GetQuitT ().GetComponent<QuitWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetQuitConformationButton () != null) {
			buttonHandler.GetQuitConformationButton ().GetComponent<QuitWordHandler> ().GoHome ();
		}

		if (buttonHandler.GetQuitCancelButton () != null) {
			buttonHandler.GetQuitCancelButton ().GetComponent<QuitWordHandler> ().GoHome ();
		}
		if (buttonHandler.GetQuitCancelButton () != null || buttonHandler.GetQuitConformationButton () != null || buttonHandler.GetQuitQ() != null || buttonHandler.GetQuitU() != null || buttonHandler.GetQuitI() != null || buttonHandler.GetQuitT() != null
		    || buttonHandler.GetRestartCancelButton () != null || buttonHandler.GetRestartConfirmationButton () != null ||
		    buttonHandler.GetRestartFirstR () != null || buttonHandler.GetRestartE () != null || buttonHandler.GetRestartS () != null ||
		    buttonHandler.GetRestartFirstT () != null || buttonHandler.GetRestartA () != null || buttonHandler.GetRestartSecondR () != null || buttonHandler.GetRestartSecondT () != null) {
			waitingForQuitDestruction = true;
			return;
		} 

		instantiatedPausedShade = GameObject.Find ("Paused Shade(Clone)");
		if (instantiatedPausedShade == null) {
			instantiatedPausedShade = (GameObject)Instantiate (pausedSlide);
		}
		shadeHandler = instantiatedPausedShade.GetComponent<PausedShadeHandler> ();
		shadeHandler.MakeInvisible ();
		if (buttonHandler.GetP () != null)
			buttonHandler.GetP ().GetComponent<PausedWordHandler> ().GoHome ();
		if (buttonHandler.GetA () != null) 
			buttonHandler.GetA ().GetComponent<PausedWordHandler> ().GoHome ();
		if (buttonHandler.GetU () != null)
			buttonHandler.GetU ().GetComponent<PausedWordHandler> ().GoHome ();
		if (buttonHandler.GetS () != null)
			buttonHandler.GetS ().GetComponent<PausedWordHandler> ().GoHome ();
		if (buttonHandler.GetE () != null)
			buttonHandler.GetE ().GetComponent<PausedWordHandler> ().GoHome ();
		if (buttonHandler.GetD () != null) 
			buttonHandler.GetD ().GetComponent<PausedWordHandler> ().GoHome ();

		if (buttonHandler.GetMusicButton () != null) {
			musicButtonHandler = buttonHandler.GetMusicButton ().GetComponent<MusicButtonHandler> ();
			musicButtonHandler.ReturnHome ();
		}

		if (buttonHandler.GetSoundButton () != null) {
			soundButtonHandler = buttonHandler.GetSoundButton ().GetComponent<SoundButtonHandler> ();
			soundButtonHandler.ReturnHome ();
		}
		
//			restartButtonHandler = buttonHandler.GetRestartButton ().GetComponent<RestartButtonHandler> ();
//			restartButtonHandler.ReturnHome ();
		if (buttonHandler.GetQuitButton () != null) {
			quitButtonHandler = buttonHandler.GetQuitButton ().GetComponent<QuitButtonHandler> ();
			quitButtonHandler.ReturnHome ();
		}

		if (buttonHandler.GetBuyCoinsButton () != null) {
			buyCoinsButtonHandler = buttonHandler.GetBuyCoinsButton ().GetComponent<BuyCoinsButtonHandler> ();
			buyCoinsButtonHandler.ReturnHome ();
		}
		
		touchHandler.UnpauseTouch ();
		Time.timeScale = 1;
		GameObject instantiatedPauseButton = (GameObject)Instantiate (pauseButton, gameObject.transform.position, Quaternion.identity);
		if (instantiateShade) {
			instantiatedPauseButton.GetComponent<PauseButtonHandler> ().SetTouchOn (false);
			instantiatedPauseButton.GetComponent<PauseButtonHandler> ().InstantiateShade ();
		}
		if (tempPressedPlayButton != null)
			Destroy (tempPressedPlayButton);
		Destroy (gameObject);
	}
	
	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject.tag == "Play Button" && hit3 != null && hit3.gameObject.tag == "Play Button" && tempPressedPlayButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (tempPressedPlayButton);
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
			GoHome ();
		}
	}

	public void InstantiateShade () {
		instantiateShade = true;
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
