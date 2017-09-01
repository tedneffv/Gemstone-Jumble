using UnityEngine;
using System.Collections;

public class LevelFailedBannerHandler : MonoBehaviour {

	Collider2D hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject.name == "Level Complete Home Button(Clone)") {
			//Debug.Log ("Pressed Home Button");
		}
	}
}
