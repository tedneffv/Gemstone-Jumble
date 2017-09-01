using UnityEngine;
using System.Collections;

public class CancelationButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	GameObject tempPressedCancelationButton;
	public GameObject pressedCancelationButton;
	bool touchOn;
	ButtonHandler buttonHandler;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
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
			if (Input.GetMouseButtonUp (0) && tempPressedCancelationButton != null) {
				CheckTouch3 (Input.mousePosition);
				if (tempPressedCancelationButton != null) 
					Destroy (tempPressedCancelationButton);
			}
		}
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && hit.gameObject.tag == "Cancel Button") {
			soundHandler.PlayButtonClickDown ();
			tempPressedCancelationButton = (GameObject)Instantiate(pressedCancelationButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.tag == "Cancel Button" && (hit2 == null || hit2.gameObject.tag != "Cancel Button")) {
			if (tempPressedCancelationButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedCancelationButton);
			}
		}
	}

	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (gameObject.name == "Quit Cancel Button(Clone)") {
			soundHandler.PlayButtonClickUp ();
			buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
			if (buttonHandler.GetQuitQ () != null)
				buttonHandler.GetQuitQ ().GetComponent<QuitWordHandler> ().GoHome ();
			if (buttonHandler.GetQuitU () != null)
				buttonHandler.GetQuitU ().GetComponent<QuitWordHandler> ().GoHome ();
			if (buttonHandler.GetQuitI () != null)
				buttonHandler.GetQuitI ().GetComponent<QuitWordHandler> ().GoHome ();
			if (buttonHandler.GetQuitT () != null)
				buttonHandler.GetQuitT ().GetComponent<QuitWordHandler> ().GoHome ();
			if (buttonHandler.GetQuitCancelButton () != null)
				buttonHandler.GetQuitCancelButton ().GetComponent<QuitWordHandler> ().GoHome ();
			if (buttonHandler.GetQuitConformationButton () != null)
				buttonHandler.GetQuitConformationButton ().GetComponent<QuitWordHandler> ().GoHome ();
		}
		if (gameObject.name == "Restart Cancel Button(Clone)") {
			soundHandler.PlayButtonClickUp ();
			buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
			if (buttonHandler.GetRestartFirstR () != null)
				buttonHandler.GetRestartFirstR ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartE () != null) 
				buttonHandler.GetRestartE ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartS () != null) 
				buttonHandler.GetRestartS ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartFirstT () != null) 
				buttonHandler.GetRestartFirstT ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartA () != null)
				buttonHandler.GetRestartA ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartSecondR () != null)
				buttonHandler.GetRestartSecondR ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartSecondT () != null) 
				buttonHandler.GetRestartSecondT ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartCancelButton () != null)
				buttonHandler.GetRestartCancelButton ().GetComponent<RestartWordHandler> ().GoHome ();
			if (buttonHandler.GetRestartConfirmationButton () != null)
				buttonHandler.GetRestartConfirmationButton ().GetComponent<RestartWordHandler> ().GoHome ();
		}
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
