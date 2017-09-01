using UnityEngine;
using System.Collections;

public class ConfirmationButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	GameObject tempPressedConfirmationButton;
	int levelNumber;
	public GameObject pressedConfirmationButton, levelFailedShade;
	bool restartLevel, quitLevel, shadeInstantiated;
	TransitionShadeController transitionShadeController;
	RockLevelSwapJewel swapJewel;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();	
		swapJewel = GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			CheckTouch (Input.mousePosition);
		if (Input.GetMouseButton (0))
			CheckTouch2 (Input.mousePosition);
		if (Input.GetMouseButtonUp (0) && tempPressedConfirmationButton != null) {
			CheckTouch3 (Input.mousePosition);
			if (tempPressedConfirmationButton != null) 
				Destroy (tempPressedConfirmationButton);
		}
		if (restartLevel && transitionShadeController.GetAlpha () >= 1) {
			levelNumber = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ().GetLevelNumber ();
			switch (levelNumber) {
			case 1: Application.LoadLevel ("Level 1"); Time.timeScale = 1; break;
			case 2: Application.LoadLevel ("Level 2"); Time.timeScale = 1; break;
			case 3: Application.LoadLevel ("Level 3"); Time.timeScale = 1; break;
			case 4: Application.LoadLevel ("Level 4"); Time.timeScale = 1; break;
			case 5: Application.LoadLevel ("Level 5"); Time.timeScale = 1; break;
			case 6: Application.LoadLevel ("Level 6"); Time.timeScale = 1; break;
			case 7: Application.LoadLevel ("Level 7"); Time.timeScale = 1; break;
			case 8: Application.LoadLevel ("Level 8"); Time.timeScale = 1; break;
			case 9: Application.LoadLevel ("Level 9"); Time.timeScale = 1; break;
			case 10: Application.LoadLevel ("Level 10"); Time.timeScale = 1; break;
			}
			restartLevel = false;
		}
		else if (quitLevel && !swapJewel.GetFirstSwapPerformed () && transitionShadeController.GetAlpha () >= 1) {
			Time.timeScale = 1;
			Application.LoadLevel ("New Level Map");
		}
//		else if (quitLevel && !shadeInstantiated && swapJewel.GetFirstSwapPerformed ()) {
//			GameObject instantiatedShade = (GameObject)Instantiate (levelFailedShade);
//			DarkenOnInstantiaton darken = instantiatedShade.GetComponent<DarkenOnInstantiaton> ();
//			darken.SetLevelComplete (false);
//			shadeInstantiated = true;
//		}
	}
	
	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && hit.gameObject.tag == "Confirmation Button") {
			soundHandler.PlayButtonClickDown ();
			tempPressedConfirmationButton = (GameObject)Instantiate(pressedConfirmationButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.tag == "Confirmation Button" && (hit2 == null || hit2.gameObject.tag != "Confirmation Button")) {
			if (tempPressedConfirmationButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedConfirmationButton);
			}
		}
	}
	
	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (gameObject.name == "Restart Confirmation Button(Clone)") {
			soundHandler.PlayButtonClickUp ();
			restartLevel = true;
			transitionShadeController.DarkenShade ();
		}
		else if (gameObject.name == "Quit Confirmation Button(Clone)") {
			soundHandler.PlayButtonClickUp ();
			quitLevel = true;
			if (!swapJewel.GetFirstSwapPerformed ())
				transitionShadeController.DarkenShade ();
			else {
				GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().InstantiateShade ();
				GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().GoHome ();
			}
		}
	}
}
