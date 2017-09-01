using UnityEngine;
using System.Collections;

public class NoMoreLivesTouchHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedBuyLivesButton;
	GameObject instantiatedPressedBuyLivesButton;

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
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit = Physics2D.OverlapPoint (touchPos);

		//Debug.Log ("hit = " + hit);
		
		if (hit != null && hit.gameObject.name == "Level Failed Retry Button") {
			instantiatedPressedBuyLivesButton = (GameObject) Instantiate (pressedBuyLivesButton, new Vector3 (hit.gameObject.transform.position.x, hit.gameObject.transform.position.y, hit.gameObject.transform.position.z - 1), Quaternion.identity);
		} 
	}
	
	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && hit.gameObject.name == "Level Failed Retry Button") {
			if (hit2 == null || (hit2 != null && hit2.gameObject.name != "Level Failed Retry Button")) {
				if (instantiatedPressedBuyLivesButton != null)
					Destroy (instantiatedPressedBuyLivesButton);
			}
			
			if (hit2 != null && hit2.gameObject.name == "Level Failed Retry Button" && instantiatedPressedBuyLivesButton == null) {
				instantiatedPressedBuyLivesButton = (GameObject) Instantiate (pressedBuyLivesButton, new Vector3 (hit.gameObject.transform.position.x, hit.gameObject.transform.position.y, hit.gameObject.transform.position.z - 1), Quaternion.identity);
			}
		}
	}
	
	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);

		if (hit != null && hit.gameObject.name == "Level Failed Retry Button" && hit3 != null && hit3.gameObject.name == "Level Failed Retry Button") {
			if (instantiatedPressedBuyLivesButton != null) 
				Destroy (instantiatedPressedBuyLivesButton);
			GameObject.Find ("Coin Instantiator").GetComponent<NoLivesCoinInstantiator> ().SetTargetY (-1);

			GameObject yesButton, noButton;
			yesButton = GameObject.Find ("Buy Lives Confirmation Button(Clone)");
			noButton = GameObject.Find ("Buy Lives Cancelation Button(Clone)");

			if (!GameObject.Find ("Coin Instantiator").GetComponent<NoLivesCoinInstantiator> ().GetMoveButtons () && yesButton != null &&
			    noButton != null) {

				if (yesButton.GetComponent<Rigidbody2D>().isKinematic && noButton.GetComponent<Rigidbody2D>().isKinematic) {
					yesButton.GetComponent<Rigidbody2D>().isKinematic = false;
					yesButton.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
					noButton.GetComponent<Rigidbody2D>().isKinematic = false;
					noButton.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
				}
			}
		}
	}
}
