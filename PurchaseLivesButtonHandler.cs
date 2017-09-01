using UnityEngine;
using System.Collections;

public class PurchaseLivesButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedCheckButton, pressedXButton;
	GameObject instantiatedPressedCheckButton, instantiatedPressedXButton;
	SoundHandler soundHandler;
	bool touchOn;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		touchOn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (touchOn) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			} else if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			} else if (Input.GetMouseButtonUp (0)) {
				CheckTouch3 (Input.mousePosition);
			} else if (Input.GetKeyUp (KeyCode.Escape) && GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetScreenInstantiated ()) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetRidOfScreen ();
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
			}
		}

	}

	public void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject.name == "Check Button(Clone)" && gameObject.name == "Check Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedCheckButton = (GameObject)Instantiate (pressedCheckButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedCheckButton.transform.parent = transform;
		}
		else if (hit != null && hit.gameObject.name == "X Button(Clone)" && gameObject.name == "X Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedXButton = (GameObject)Instantiate  (pressedXButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedXButton.transform.parent = transform;
		}
	}

	public void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		if ((hit2 == null || hit2.gameObject.name != "Check Button(Clone)") && instantiatedPressedCheckButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedCheckButton);
		}
		else if ((hit2 == null || hit2.gameObject.name != "X Button(Clone)") && instantiatedPressedXButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedXButton);
		}
	}

	public void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit3.gameObject.name == "Check Button(Clone)" && instantiatedPressedCheckButton != null) {
			StoreAssets.BuyFiveLivesPack ();
//			if (GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetCoinTotal () >= 5000) {
//				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().AddToCoinTotal (-5000);
//				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetHeartNumber (5);
//				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().UpdateCoinNumber ();
//				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetRidOfScreen ();
//				if (GameObject.Find ("29 block") != null)
//					GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().CameraOffsetNoLivesScreen ();
//				else 
//					GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
//			} else {
//				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetRidOfScreen ();
//				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateScreen ();
//			}
		} 


		else if (hit3 != null && hit3.gameObject.name == "X Button(Clone)" && instantiatedPressedXButton != null) {
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetRidOfScreen ();
			if (GameObject.Find ("29 block") != null) 
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().CameraOffsetNoLivesScreen ();
			else 
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
		}

		if (instantiatedPressedCheckButton != null && gameObject.name == "Check Button(Clone)") {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedCheckButton);
		}
		if (instantiatedPressedXButton != null && gameObject.name == "X Button(Clone)") {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedXButton);
		}
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
