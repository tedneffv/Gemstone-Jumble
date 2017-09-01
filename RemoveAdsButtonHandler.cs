﻿using UnityEngine;
using System.Collections;

public class RemoveAdsButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedRemoveAdsButton;
	GameObject instantiatedPressedRemoveAdsButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButton (0)) 
			CheckTouch2 (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0)) 
			CheckTouch3 (Input.mousePosition);
		else if (instantiatedPressedRemoveAdsButton != null) 
			Destroy (instantiatedPressedRemoveAdsButton);
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			instantiatedPressedRemoveAdsButton = Instantiate (pressedRemoveAdsButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity) as GameObject;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit2 == null || (hit != null && hit.gameObject != gameObject)) {
			if (instantiatedPressedRemoveAdsButton != null) 
				Destroy (instantiatedPressedRemoveAdsButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit.gameObject == gameObject) {
			if (instantiatedPressedRemoveAdsButton != null)
				Destroy (instantiatedPressedRemoveAdsButton);
			StoreAssets.BuyRemoveAds ();
		}
	}
}
