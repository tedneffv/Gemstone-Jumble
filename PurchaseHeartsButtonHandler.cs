﻿using UnityEngine;
using System.Collections;

public class PurchaseHeartsButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedBuyButton;
	GameObject instantiatedPresedBuyButton;

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
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			instantiatedPresedBuyButton = (GameObject)Instantiate (pressedBuyButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPresedBuyButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if ((hit2 == null || hit2.gameObject != gameObject) && instantiatedPresedBuyButton != null)
			Destroy (instantiatedPresedBuyButton);
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit3.gameObject == gameObject && instantiatedPresedBuyButton != null) {
			StoreAssets.BuyFiveLivesPack ();
		}

		if (instantiatedPresedBuyButton != null) {
			Destroy (instantiatedPresedBuyButton);
		}
	}
}
