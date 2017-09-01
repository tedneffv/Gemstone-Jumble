using UnityEngine;
using System.Collections;

public class ShowAdsOKButtonHandler : MonoBehaviour {
	
	Collider2D hit, hit2, hit3;
	SpriteRenderer pressedOkaySpriteRenderer;
	Color oldColor;

	// Use this for initialization
	void Start () {
		pressedOkaySpriteRenderer = GetComponent<SpriteRenderer> ();
		oldColor = pressedOkaySpriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButton (0)) 
			CheckTouch2 (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0))
			CheckTouch3 (Input.mousePosition);
		else if (pressedOkaySpriteRenderer.color.a == 1) 
			pressedOkaySpriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0);
			

	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			pressedOkaySpriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, 1);
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (pressedOkaySpriteRenderer.color.a == 1 && (hit2 == null || (hit2 != null && hit2.gameObject != gameObject))) {
			pressedOkaySpriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (pressedOkaySpriteRenderer.color.a == 1 && hit3 != null && hit3.gameObject == gameObject) {
			pressedOkaySpriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0);
			GameObject.Find ("Earn Coins Button 1").GetComponent<EarnedCoinsButtonHandler> ().StartAd ();
		}
	}
}
