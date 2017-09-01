using UnityEngine;
using System.Collections;
using Soomla.Store;

public class BuyCoinsTouch : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedBuyCoinsButton;
	GameObject instantiatedPressedBuyCoinsButton;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		} else if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		} else if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject.name == "Buy Coins Button(Clone)") {
			soundHandler.PlayButtonClickDown ();
			instantiatedPressedBuyCoinsButton = (GameObject)Instantiate (pressedBuyCoinsButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
			instantiatedPressedBuyCoinsButton.transform.parent = transform;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit2 == null || hit2.gameObject.name != "Buy Coins Button(Clone)" && instantiatedPressedBuyCoinsButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedBuyCoinsButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit3 != null && hit3.gameObject.name == "Buy Coins Button(Clone)" && instantiatedPressedBuyCoinsButton != null) {
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().SetSettingsInstantiated (false);
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().GetMusicButton ().GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().GetSoundButton ().GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			if (GameObject.Find ("Remove Ads Button(Clone)") != null) {
				GameObject.Find ("Remove Ads Button(Clone)").GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			}
			GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateScreen ();
		}

		if (instantiatedPressedBuyCoinsButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedBuyCoinsButton);
		}
	}
}
