using UnityEngine;
using System.Collections;

public class BuyCoinsButtonHandler : MonoBehaviour {
	Collider2D hit, hit2, hit3;
	bool reachedHome, returnHome, destroyButton, touchOn;
	float targetXPosition, horizontalCoefficient;
	ButtonHandler buttonHandler;
	GameObject tempPressedBuyCoinsButton, instantiatedBuyCoinsButton;
	public GameObject pressedMusicButton;
	RockLevelController controller;
	SoundHandler soundHandler;
	
	// Use this for initialization
	void Start () {
		horizontalCoefficient = .8f;
		if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 4);
		else if (GameObject.Find ("Play Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 4);
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
		controller = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!reachedHome)
			transform.Translate (new Vector3 ((targetXPosition - transform.position.x), 0, 0) * .0003f * Screen.width);
		if (Mathf.Abs (targetXPosition - transform.position.x) < .001f) {
			touchOn = true;
			transform.position = new Vector3 (targetXPosition, transform.position.y, transform.position.z);
			reachedHome = true;
			if (destroyButton) {
				buttonHandler.SetMusicButton (null);
				Destroy (gameObject);
			}
		}
		
		if (touchOn) {
			if (Input.GetMouseButtonDown (0))
				CheckTouch (Input.mousePosition);
			if (Input.GetMouseButton (0))
				CheckTouch2 (Input.mousePosition);
			if (Input.GetMouseButtonUp (0) && tempPressedBuyCoinsButton != null) {
				CheckTouch3 (Input.mousePosition);
			}
		}
	}
	
	public void ReturnHome () {
		if (GameObject.Find ("Play Button(Clone)") != null)
			targetXPosition = GameObject.Find ("Play Button(Clone)").transform.position.x;
		else if (GameObject.Find ("Pause Button(Clone)") != null) 
			targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x;
		reachedHome = false;
		destroyButton = true;
	}
	
	public void LeaveHome () {
		reachedHome = false;
		targetXPosition = GameObject.Find ("Pause Button(Clone)").transform.position.x - .2f - (horizontalCoefficient * 4);
		destroyButton = false;
	}
	
	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	
		
		if (hit != null && (hit.gameObject.tag == "Buy Coins Button" || hit.gameObject.tag == "Buy Coins Button")) {
			soundHandler.PlayButtonClickDown ();
			tempPressedBuyCoinsButton = (GameObject)Instantiate(pressedMusicButton, new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f), Quaternion.identity);
			tempPressedBuyCoinsButton.transform.parent = hit.gameObject.transform;
		}
	}
	
	private void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint (touchPos);
		
		if (hit != null && (hit.gameObject.tag == "Buy Coins Button" || hit.gameObject.tag == "Buy Coins Button") && (hit2 == null || hit2.gameObject.tag != "Buy Coins Button")) {
			if (tempPressedBuyCoinsButton != null) {
				soundHandler.PlayButtonClickUp ();
				Destroy (tempPressedBuyCoinsButton);
			}
		}
	}
	
	private void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint (touchPos);
		
		if (hit3 != null && hit3.gameObject.tag == "Buy Coins Button" && tempPressedBuyCoinsButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (tempPressedBuyCoinsButton);
			GameObject bottomBanner = GameObject.Find ("Bottom Banner");
//			bottomBanner.transform.position = new Vector3 (0, -6, -102);
			GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateScreen ();
		}
	}
	
	public void DestroyTempPressedButton () {
		if (tempPressedBuyCoinsButton != null) 
			Destroy (tempPressedBuyCoinsButton);
	}
	
	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}
}
