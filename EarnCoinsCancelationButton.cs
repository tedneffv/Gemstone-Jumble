using UnityEngine;
using System.Collections;

public class EarnCoinsCancelationButton : MonoBehaviour {

	Collider2D hit, hit2;
	bool colliderHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) 
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0)) 
			CheckTouch2 (Input.mousePosition);
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			colliderHit = true;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (colliderHit && hit2 != null && hit2.gameObject == gameObject) {
			GameObject.Find ("Earn Coins Explaination Banner(Clone)").GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		}
	}
}
