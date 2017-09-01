using UnityEngine;
using System.Collections;

public class PausedShadeHandler : MonoBehaviour {

	float alpha;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool makeVisible, levelComplete, slideInstantiated, makeInvisible, wandered;
	Collider2D hit, hit2, hit3;
	
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		oldColor = spriteRenderer.color;
		makeVisible = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (makeVisible) {
			IncreaseAlpha ();
		} 
		else if (makeInvisible)
			DecreaseAlpha ();

		if (Input.GetMouseButtonDown (0)) 
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButton (0)) 
			CheckTouch2 (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0)) 
			CheckTouch3 (Input.mousePosition);
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
		if (!wandered && hit != null && hit.gameObject == gameObject && hit2 != null && hit2.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {
			if (GameObject.Find ("$.99 Button(Clone)") != null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
			} else {
				if (GameObject.Find ("Play Button(Clone)") != null)
					GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().GoHome ();
			}
		}
		wandered = false;
	}
	
	void IncreaseAlpha () {
		alpha += .05f;
		spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, alpha);
		oldColor = spriteRenderer.color;
		if (alpha >= 1f) {
			makeVisible = false;
		}
	}

	void DecreaseAlpha () {
		alpha -= .05f;
		spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, alpha);
		oldColor = spriteRenderer.color;
		if (alpha <= 0) {
			makeInvisible = false;
			Destroy (gameObject);
		}
	}
	
	public void MakeVisible () {
		makeVisible = true;
		makeInvisible = false;
	}

	public void MakeInvisible () {
		makeInvisible = true;
		makeVisible = false;
	}
}
