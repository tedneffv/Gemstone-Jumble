using UnityEngine;
using System.Collections;

public class RestartButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	bool retrunHome, reachedHome, destroyButton, leftHome, waitingForDestruction, touchOn;
	float targetXPosition, horizontalCoefficient;
	ButtonHandler buttonHandler;
	public GameObject pressedRestartButton, restartFirstR, restartE, restartS, restartFirstT, restartA, restartSecondR, restartSecondT, restartConfirmationButton, restartCancelButton;
	GameObject tempPressedRestartButton;
	RestartWordHandler restartWordHandler;
	Vector3 homePosition;
	Texture2D heightMap;

	// Use this for initialization
	void Start () {
		horizontalCoefficient = .8f;
		if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 3);
		else 
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 3);
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
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
				buttonHandler.SetRestartButton (null);
				Destroy (gameObject);
			}
		}

		if (touchOn) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedRestartButton != null) {
				CheckTouch3 (Input.mousePosition);
				Destroy (tempPressedRestartButton);
			}
		}

	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && hit.gameObject.tag == "Restart Button") {
			tempPressedRestartButton = (GameObject)Instantiate(pressedRestartButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.tag == "Restart Button" && hit2 == null) {
			if (tempPressedRestartButton != null)
				Destroy (tempPressedRestartButton);
		}
	}

	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

//		if (tempPressedRestartButton != null && hit3 != null && hit3.gameObject.tag == "Restart Button") {

		if (buttonHandler.GetQuitQ () != null) {
			buttonHandler.GetQuitQ ().GetComponent<QuitWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}

		if (buttonHandler.GetQuitU () != null) {
			buttonHandler.GetQuitU ().GetComponent<QuitWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}

		if (buttonHandler.GetQuitI () != null) {
			buttonHandler.GetQuitI ().GetComponent<QuitWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}

		if (buttonHandler.GetQuitT () != null) {
			buttonHandler.GetQuitT ().GetComponent<QuitWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}

		if (buttonHandler.GetQuitConformationButton () != null) {
			buttonHandler.GetQuitConformationButton ().GetComponent<QuitWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}

		if (buttonHandler.GetQuitCancelButton () != null) {
			buttonHandler.GetQuitCancelButton ().GetComponent<QuitWordHandler> ().GoHome ();
			waitingForDestruction = true;
		}

		if (waitingForDestruction)
			return;

		if (buttonHandler.GetRestartFirstR () == null) {
			buttonHandler.SetRestartFirstR ((GameObject)Instantiate (restartFirstR, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartFirstR ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartE () == null) {
			buttonHandler.SetRestartE ((GameObject)Instantiate (restartE, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartE ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartS () == null) {
			buttonHandler.SetRestartS ((GameObject)Instantiate (restartS, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartS ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartFirstT () == null) {
			buttonHandler.SetRestartFirstT ((GameObject)Instantiate (restartFirstT, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartFirstT ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();

		if (buttonHandler.GetRestartA () == null) {
			buttonHandler.SetRestartA ((GameObject)Instantiate (restartA, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartA ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else 
			restartWordHandler.GoHome ();

		if (buttonHandler.GetRestartSecondR () == null) {
			buttonHandler.SetRestartSecondR ((GameObject)Instantiate (restartSecondR, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .06f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartSecondR ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();

		if (buttonHandler.GetRestartSecondT () == null)
			buttonHandler.SetRestartSecondT ((GameObject)Instantiate (restartSecondT, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .07f), Quaternion.identity));
		restartWordHandler = buttonHandler.GetRestartSecondT ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();

		if (buttonHandler.GetRestartConfirmationButton () == null) {
			buttonHandler.SetRestartConfirmationButton ((GameObject)Instantiate (restartConfirmationButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .08f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartConfirmationButton ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();


		if (buttonHandler.GetRestartCancelButton () == null)
			buttonHandler.SetRestartCancelButton ((GameObject)Instantiate (restartCancelButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .09f), Quaternion.identity));
		restartWordHandler = buttonHandler.GetRestartCancelButton ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();

		Destroy (tempPressedRestartButton);
		leftHome = !leftHome;
//		}
	}

	public void TimeToLeave () {
		if (buttonHandler.GetRestartFirstR () == null) {
			buttonHandler.SetRestartFirstR ((GameObject)Instantiate (restartFirstR, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .01f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartFirstR ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartE () == null) {
			buttonHandler.SetRestartE ((GameObject)Instantiate (restartE, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .02f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartE ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartS () == null) {
			buttonHandler.SetRestartS ((GameObject)Instantiate (restartS, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .03f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartS ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartFirstT () == null) {
			buttonHandler.SetRestartFirstT ((GameObject)Instantiate (restartFirstT, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .04f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartFirstT ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartA () == null) {
			buttonHandler.SetRestartA ((GameObject)Instantiate (restartA, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .05f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartA ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else 
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartSecondR () == null) {
			buttonHandler.SetRestartSecondR ((GameObject)Instantiate (restartSecondR, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .06f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartSecondR ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartSecondT () == null)
			buttonHandler.SetRestartSecondT ((GameObject)Instantiate (restartSecondT, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .07f), Quaternion.identity));
		restartWordHandler = buttonHandler.GetRestartSecondT ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		if (buttonHandler.GetRestartConfirmationButton () == null) {
			buttonHandler.SetRestartConfirmationButton ((GameObject)Instantiate (restartConfirmationButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .08f), Quaternion.identity));
			leftHome = false;
		}
		restartWordHandler = buttonHandler.GetRestartConfirmationButton ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();
		
		
		if (buttonHandler.GetRestartCancelButton () == null)
			buttonHandler.SetRestartCancelButton ((GameObject)Instantiate (restartCancelButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .09f), Quaternion.identity));
		restartWordHandler = buttonHandler.GetRestartCancelButton ().GetComponent<RestartWordHandler> ();
		if (!leftHome)
			restartWordHandler.LeaveHome ();
		else
			restartWordHandler.GoHome ();

		waitingForDestruction = false;
		leftHome = true;
	}
	
	public void ReturnHome () {
		touchOn = false;
		targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x;
		reachedHome = false;
		destroyButton = true;
	}
	
	public void LeaveHome () {
		reachedHome = false;
		targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 3);
		destroyButton = false;
	}

	public void DestroyTempPressedButton () {
		if (tempPressedRestartButton != null) 
			Destroy (tempPressedRestartButton);
	}

	public bool GetWaitingForDestruction () {
		return waitingForDestruction;
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
