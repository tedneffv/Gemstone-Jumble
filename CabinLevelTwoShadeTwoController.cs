using UnityEngine;
using System.Collections;

public class CabinLevelTwoShadeTwoController : MonoBehaviour {

	Collider2D hit;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade, instantiateOwlie, bounceJewels, darkened;
	RockLevelTouchHandler touchHandler;
	RockLevelCheckForMatches matchChecker;
	public GameObject owlie, owlieSwapper, nextTutorialShade, speechBubble;
	GameObject currentOwlie, instantiatedOwlieSwapper, instantiatedSpeechBubble;
	float timeStamp, cooldown;
	RockLevelInstantiator instantiator;
	public Sprite regularButton;
	// Use this for initialization
	void Start () {
		instantiateOwlie = true;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		matchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		oldColor = spriteRenderer.color;
		cooldown = 3;
		Debug.Log ("Darkening Shade");
		DarkenShade ();
	}
	
	void Update () {
		if (darkened) {
			if (Input.GetMouseButtonUp (0)) {
				CheckTouch (Input.mousePosition);
			}
		}
	}
	
	void FixedUpdate () {
		if (darkenShade) {
			if (instantiateOwlie) {
				instantiateOwlie = false;
				instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-10, 3.74f, -11.9f), Quaternion.identity);
				instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.94f);
			}
			if (oldColor.a < .67f) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
				oldColor = spriteRenderer.color;
			} else {
				MakeJewelsBounce ();
				timeStamp = Time.time;
				bounceJewels = true;
				touchHandler.SetGameStarted (true);
				matchChecker.SetGameStarted (true);
				darkenShade = false;
				darkened = true;
			}
		} else if (lightenShade) {
			if (oldColor.a > 0) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
				oldColor = spriteRenderer.color;
			} else {
				
//				GameObject temp = instantiator.GetJewelGridGameObject (3, 4);
//				temp.transform.position =  new Vector3 (temp.transform.position.x, temp.transform.position.y, -.5f);
//				
//				temp = instantiator.GetJewelGridGameObject (4, 3);
//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -.5f);
//				
//				temp = instantiator.GetJewelGridGameObject (4, 5);
//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -.5f);
//				
//				temp = instantiator.GetJewelGridGameObject (5, 4);
//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -.5f);

				GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().SetTouchOn (true);
				GameObject.Find ("Power Up Button(Clone)").GetComponent<SpriteRenderer> ().sprite = regularButton;

				lightenShade = false;
				PlayerPrefs.SetInt ("Level Four Tutorial Played", 1);
				//				Instantiate (nextTutorialShade);
				Destroy (gameObject);
			}
		}
		
		if (bounceJewels && Time.time > timeStamp + cooldown) {
			timeStamp = Time.time;
			MakeJewelsBounce ();
		}
		
	}
	
	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		
		if (hit != null && hit.gameObject == gameObject) {
			darkened = false;
			LightenShade ();
		}
	}
	
	public void DarkenShade () {
		
		if (PlayerPrefs.HasKey ("Level Four Tutorial Played")) {
			touchHandler.SetGameStarted (true);
			matchChecker.SetGameStarted (true);
			Destroy (gameObject);
		}  
		
		darkenShade = true;
		GameObject temp = instantiator.GetJewelGridGameObject (2, 4);
		temp.transform.position =  new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
		
		temp = instantiator.GetJewelGridGameObject (3, 4);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12f);
		
		temp = instantiator.GetJewelGridGameObject (5, 4);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
		
		temp = instantiator.GetJewelGridGameObject (6, 4);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

		temp = instantiator.GetJewelGridGameObject (4, 2);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

		temp = instantiator.GetJewelGridGameObject (4, 3);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

		temp = instantiator.GetJewelGridGameObject (4, 5);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

		temp = instantiator.GetJewelGridGameObject (4, 6);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
	}
	
	public void LightenShade () {
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().BellyPoked ();   
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().TranslateOwlie (10f, 2.28f);
		Destroy (GameObject.Find ("OwlieSwapper(Clone)"));
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		lightenShade = true;
		bounceJewels = false;
		GameObject.Find ("Level Controller").GetComponent<RockLevelMatchAssistant> ().SetTutorialLevel (false);
	}
	
	public void SetCurrentOwlie (GameObject newOwlie) {
		currentOwlie = newOwlie;
	}
	
	void MakeJewelsBounce () {
		GameObject a = instantiator.GetJewelGridGameObject (2, 4);
		RockLevelJewelMovement jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (3, 4);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (5, 4);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (6, 4);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);

		a = instantiator.GetJewelGridGameObject (4, 2);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);

		a = instantiator.GetJewelGridGameObject (4, 3);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);

		a = instantiator.GetJewelGridGameObject (4, 5);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);

		a = instantiator.GetJewelGridGameObject (4, 6);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
	}
}
