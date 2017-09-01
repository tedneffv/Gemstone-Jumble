using UnityEngine;
using System.Collections;

public class ShowAdsCheckMarkHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	SpriteRenderer pressedCheckMarkSpriteRenderer;
	Color oldColor;
	bool checkBoxHit;

	// Use this for initialization
	void Start () {
		pressedCheckMarkSpriteRenderer = GetComponent<SpriteRenderer> ();
		oldColor = pressedCheckMarkSpriteRenderer.color;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0))
			CheckTouch3 (Input.mousePosition);
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			checkBoxHit = true;
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (checkBoxHit && hit3 != null && hit3.gameObject == gameObject) {
			if (pressedCheckMarkSpriteRenderer.color.a == 1) {
				PlayerPrefs.SetString ("showAdBox", "false");
				pressedCheckMarkSpriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0);
			} else {
				PlayerPrefs.SetString ("showAdBox", "true");
				pressedCheckMarkSpriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, 1);
			}
		}
	}
}
