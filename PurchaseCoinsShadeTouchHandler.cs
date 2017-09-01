using UnityEngine;
using System.Collections;

public class PurchaseCoinsShadeTouchHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	bool wandered;

	// Use this for initialization
	void Start () {
	
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
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit2 != null && hit2.gameObject != gameObject)
			wandered = true;
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject && hit2 != null && hit2.gameObject == gameObject && hit3 != null && hit.gameObject == gameObject) {
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
		}
		wandered = false;

	}
}
