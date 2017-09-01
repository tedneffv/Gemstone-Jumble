using UnityEngine;
using System.Collections;

public class CheckForUpdatesButtonController : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedButton;
	GameObject instantiatedPressedButton;

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

		if (hit != null && hit.gameObject == gameObject) {
			instantiatedPressedButton = (GameObject)Instantiate (pressedButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit2 == null || hit2.gameObject != gameObject) {
			if (instantiatedPressedButton != null)
				Destroy (instantiatedPressedButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (instantiatedPressedButton != null) 
			Destroy (instantiatedPressedButton);
		if (hit3 != null && hit3.gameObject == gameObject) {
			//Debug.Log ("opening url");
#if UNITY_ANDROID
			Application.OpenURL ("market://details?id=com.LonelyManStudios.GemDropGold");	//Android implementation
#endif

#if UNITY_IOS
			Application.OpenURL ("https://itunes.apple.com/us/app/keynote/id1053383448?mt=8");	//iPhone implementation
#endif
		}
	}
}
