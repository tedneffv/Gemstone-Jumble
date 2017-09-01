using UnityEngine;
using System.Collections;

public class NoLivesScreenTouchHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	TransitionShadeController transitionShadeController;
	bool goHomePressed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}
		if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		}
		if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}
		if (goHomePressed && transitionShadeController.GetAlpha () >= 1) 
			Application.LoadLevel ("New Level Map");

	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit = Physics2D.OverlapPoint (touchPos); 

		if (hit != null && IsButton (hit.gameObject)) {
			hit.gameObject.GetComponent<NoLivesScreenButtonHandler> ().ButtonPressed ();
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);

		//Debug.Log ("hit2 = " + hit2);

		if (hit != null && IsButton (hit.gameObject) && hit2 != null && IsButton (hit2.gameObject)) {
			if (hit.gameObject != hit2.gameObject) {
				hit.gameObject.GetComponent<NoLivesScreenButtonHandler> ().ButtonUnpressed ();
			} else if (hit.gameObject == hit2.gameObject && !hit2.gameObject.GetComponent<NoLivesScreenButtonHandler> ().IsPressed ()) {
				hit2.gameObject.GetComponent<NoLivesScreenButtonHandler> ().ButtonPressed ();
			} 
		}

		if (hit2 == null || (hit2 != null && !IsButton (hit2.gameObject))) {
			DestroyAllPressedButtons ();
		}

		if (hit != null && IsButton(hit.gameObject) && hit2 != null && IsButton(hit2.gameObject) && hit.gameObject == hit2.gameObject) 
			hit2.gameObject.GetComponent<NoLivesScreenButtonHandler> ().ButtonPressed ();
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && IsButton(hit.gameObject)) {
			hit.gameObject.GetComponent<NoLivesScreenButtonHandler> ().ButtonUnpressed ();
			if (hit3 != null && IsButton (hit3.gameObject) && hit.gameObject.name == hit3.gameObject.name && hit3.gameObject.name == "Return Home Button(Clone)") {
				transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
				transitionShadeController.DarkenShade ();
				goHomePressed = true;
			}
		}


	}

	bool IsButton (GameObject possibleButton) {
		switch (possibleButton.name) {
		case "Purchase Lives Button(Clone)": return true;
		case "Purchase Coins Button(Clone)": return true;
		case "Return Home Button(Clone)": return true;
		}
		return false;
	}

	void DestroyAllPressedButtons () {
		GameObject.Find ("Purchase Lives Button(Clone)").GetComponent<NoLivesScreenButtonHandler> ().ButtonUnpressed ();
		GameObject.Find ("Purchase Coins Button(Clone)").GetComponent<NoLivesScreenButtonHandler> ().ButtonUnpressed ();
		GameObject.Find ("Return Home Button(Clone)").GetComponent<NoLivesScreenButtonHandler> ().ButtonUnpressed ();
	}
}
