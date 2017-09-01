using UnityEngine;
using System.Collections;

public class BuyLivesButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedConfirmationButton, pressedCancelationButton;
	GameObject instantiatedPressedConfButton, instantiatedPressedCancButton;
	bool touchOn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (touchOn) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			}
			if (Input.GetMouseButtonUp (0)) {
				if (instantiatedPressedCancButton != null && gameObject.tag == ("Cancel Button"))
					Destroy (instantiatedPressedCancButton);
				if (instantiatedPressedConfButton != null && gameObject.tag == "Confirmation Button")
					Destroy (instantiatedPressedConfButton);
			}
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject == gameObject && hit.gameObject.tag == "Confirmation Button") {
			instantiatedPressedConfButton = (GameObject)Instantiate (pressedConfirmationButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity);
		} else if (hit != null && hit.gameObject == gameObject && hit.gameObject.tag == "Cancel Button") {
			instantiatedPressedCancButton = (GameObject)Instantiate (pressedCancelationButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity);
		}
	}

	public void TouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
