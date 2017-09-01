using UnityEngine;
using System.Collections;

public class QuitButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	bool reachedHome, returnHome, destroyButton, leftHome, waitingForDestruction, touchOn, wordLeaveHome, wordComeHome, finalStarsFiring, starsLaunching, pressedQuitInstantiated;
	float targetXPosition, horizontalCoefficient;
	ButtonHandler buttonHandler;
	public GameObject pressedQuitButton, quitQ, quitU, quitI, quitT, quitConformationButton, quitCancelButton;
	GameObject tempPressedQuitButton;
	QuitWordHandler quitWordHandler;
	Texture2D heightMap;
	SoundHandler soundHandler;
	
	// Use this for initialization
	void Start () {
		horizontalCoefficient = .8f;
		if (GameObject.Find ("Pause Button(Clone)") != null)
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 3);
		else 
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 3);
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();

		
		if (!pressedQuitInstantiated && GameObject.Find ("Quit Button(Clone)") != null && (GameObject.Find ("Level Controller").GetComponent<RockLevelBombHandler> ().GetBombCount () == 0 || 
		                                                                                   (GameObject.Find ("Jewel Collector") != null && GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().GetJewelCollectorsFinshedCollecting ())) && gameObject.name == "Quit Button(Clone)") {
			GameObject qb = GameObject.Find ("Quit Button(Clone)");
			starsLaunching = true;
			tempPressedQuitButton = (GameObject)Instantiate(pressedQuitButton, new Vector3 (qb.transform.position.x, qb.transform.position.y, qb.transform.position.z - .001f), Quaternion.identity);
			tempPressedQuitButton.transform.parent = transform;
			pressedQuitInstantiated = true;
		}

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
				buttonHandler.SetQuitButton (null);
				Destroy (gameObject);
			}
		}

		if (touchOn && !starsLaunching) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedQuitButton != null) {
				CheckTouch3 (Input.mousePosition);
			}
		}
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && hit.gameObject.tag == "Quit Button") {
			soundHandler.PlayButtonClickDown ();
			if (tempPressedQuitButton == null) {
				tempPressedQuitButton = (GameObject)Instantiate(pressedQuitButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .001f), Quaternion.identity);
				tempPressedQuitButton.transform.parent = hit.gameObject.transform;
			}
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.tag == "Quit Button" && hit2 != null) {
			if (tempPressedQuitButton != null && hit2.gameObject.tag != "Quit Button") {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedQuitButton);
			}
		}
	}
	
	public void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject.tag == "Quit Button" && hit3 != null && hit.gameObject.tag == "Quit Button" && tempPressedQuitButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (tempPressedQuitButton);
			LeaveHomeGoHome ();
		}
	}

	public bool GetWaitingForDestruction () {
		return waitingForDestruction;
	}

	public void TimeToLeave () {
		if (buttonHandler.GetQuitQ () == null) {
			buttonHandler.SetQuitQ ((GameObject)Instantiate (quitQ, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitQ ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitU () == null) {
			buttonHandler.SetQuitU ((GameObject)Instantiate (quitU, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitU ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitI () == null) {
			buttonHandler.SetQuitI ((GameObject)Instantiate (quitI, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitI ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitT () == null) {
			buttonHandler.SetQuitT ((GameObject)Instantiate (quitT, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitT ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitConformationButton () == null) 
			buttonHandler.SetQuitConformationButton ((GameObject)Instantiate (quitConformationButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .06f), Quaternion.identity));
		quitWordHandler = buttonHandler.GetQuitConformationButton ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitCancelButton () == null) {
			buttonHandler.SetQuitCancelButton ((GameObject)Instantiate (quitCancelButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitCancelButton ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();

		waitingForDestruction = false;
		leftHome = true;
	}
	
	public void ReturnHome () {
		touchOn = false;
		if (GameObject.Find ("Play Button(Clone)") != null)
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x;
		else if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x;
		reachedHome = false;
		destroyButton = true;
	}
	
	public void LeaveHome () {
		reachedHome = false;
		targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 3);
		destroyButton = false;
	}

	public void LeaveHomeGoHome () {
		GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
		if (buttonHandler.GetRestartFirstR () != null) {
			buttonHandler.GetRestartFirstR ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartE () != null) {
			buttonHandler.GetRestartE ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartS () != null) {
			buttonHandler.GetRestartS ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartFirstT () != null) {
			buttonHandler.GetRestartFirstT ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartA () != null) {
			buttonHandler.GetRestartA ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartSecondR () != null) {
			buttonHandler.GetRestartSecondR ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartSecondT () != null) {
			buttonHandler.GetRestartSecondT ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartConfirmationButton () != null) {
			buttonHandler.GetRestartConfirmationButton().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (buttonHandler.GetRestartCancelButton () != null) {
			buttonHandler.GetRestartCancelButton ().GetComponent<RestartWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}
		
		if (waitingForDestruction)
			return;
		
		//		if (tempPressedQuitButton != null && hit3 != null && hit3.gameObject.tag == "Quit Button") {
		if (buttonHandler.GetQuitQ () == null) {
			buttonHandler.SetQuitQ ((GameObject)Instantiate (quitQ, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitQ ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitU () == null) {
			buttonHandler.SetQuitU ((GameObject)Instantiate (quitU, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitU ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitI () == null) {
			buttonHandler.SetQuitI ((GameObject)Instantiate (quitI, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitI ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitT () == null) {
			buttonHandler.SetQuitT ((GameObject)Instantiate (quitT, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitT ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitConformationButton () == null) 
			buttonHandler.SetQuitConformationButton ((GameObject)Instantiate (quitConformationButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .06f), Quaternion.identity));
		quitWordHandler = buttonHandler.GetQuitConformationButton ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		
		if (buttonHandler.GetQuitCancelButton () == null) {
			buttonHandler.SetQuitCancelButton ((GameObject)Instantiate (quitCancelButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
			leftHome = false;
		}
		quitWordHandler = buttonHandler.GetQuitCancelButton ().GetComponent<QuitWordHandler> ();
		if (!leftHome)
			quitWordHandler.LeaveHome ();
		else
			quitWordHandler.GoHome ();
		Destroy (tempPressedQuitButton);
		leftHome = !leftHome;
	}

	public void DestroyTempPressedButton () {
//		if (tempPressedQuitButton != null) 
//			Destroy (tempPressedQuitButton);
	}

	public void SetWaitingForDestruction (bool waitingForDestruction) {
		this.waitingForDestruction = waitingForDestruction;
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}

	public bool GetLeftHome () {
		return leftHome;
	}
}
