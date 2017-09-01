using UnityEngine;
using System.Collections;

public class ShadeTouchSixHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	bool wanderedAwayFromShade, lightenShade;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	float timeStamp, cooldown;
	
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		oldColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
			CheckTouch (Input.mousePosition);
		else if (Input.GetMouseButton (0))
			CheckTouch2 (Input.mousePosition);
		else if (Input.GetMouseButtonUp (0)) 
			CheckTouch3 (Input.mousePosition);
		if (lightenShade) {
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
			oldColor = spriteRenderer.color;
			if (oldColor.a <= 0) {
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().SetShadeInstantiated (false);
				Destroy (gameObject);
			}
		}
	}
	
	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint(new Vector2 (wp.x, wp.y));
	}
	
	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint(new Vector2 (wp.x, wp.y));
		if (hit2 != null && hit2.gameObject.name != "Tutorial Shade 6(Clone)")
			wanderedAwayFromShade = true;
	}
	
	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint(new Vector2 (wp.x, wp.y));
		
		if (!wanderedAwayFromShade && hit != null && hit.gameObject.name == "Tutorial Shade 6(Clone)" && hit2 != null && hit.gameObject.name == "Tutorial Shade 6(Clone)" && hit3 != null && hit3.gameObject.name == "Tutorial Shade 6(Clone)") {
			if (GameObject.Find ("Settings Button") != null && GameObject.Find ("Buy Coins Button(Clone)") != null)
				GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().InstantiateSettingsScreen ();
			else if (GameObject.Find ("Purchase Lives Button(Clone)") != null && GameObject.Find ("Level Controller") == null) {
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().GetRidOfScreen ();
				lightenShade = true;
			}
			else if (GameObject.Find ("$.99 Button(Clone)") != null && GameObject.Find ("Settings Button") != null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
				GameObject.Find ("Settings Button").GetComponent<SettingsButtonTouch> ().InstantiateSettingsScreen ();
			}
			else if (GameObject.Find ("$.99 Button(Clone)") != null && GameObject.Find ("Settings Button") == null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().GetRidOfScreen ();
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
			}
			else if (GameObject.Find ("Coin Parent(Clone)") != null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetRidOfScreen ();
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
			}
			else if (GameObject.Find ("Heart Purchase Banner(Clone)") != null) {
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseLivesScreen> ().GetRidOfScreen ();
				GameObject.Find ("Heart Purchase Banner(Clone)").GetComponent<PurchaseHeartsBannerController> ().SetHeartsPurchased ();
			}
		}
		
		wanderedAwayFromShade = false;
	}
}
