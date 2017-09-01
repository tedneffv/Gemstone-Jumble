using UnityEngine;
using System.Collections;

public class LevelOneShadeFourController : MonoBehaviour {

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
	
	// Use this for initialization
	void Start () {
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		matchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ();
		oldColor = spriteRenderer.color;
		DarkenShade ();
		cooldown = 8;
	}

	void Update () {
		if (darkened) {
			if (Input.GetMouseButtonUp (0)) {
				CheckTouch (Input.mousePosition);
			}
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

	void FixedUpdate () {
		if (darkenShade) {
			if (oldColor.a < .67f) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
				oldColor = spriteRenderer.color;
			} else {
				MakeJewelsBounce ();
				darkened = true;
				timeStamp = Time.time;
				bounceJewels = true;
				touchHandler.SetGameStarted (true);
				matchChecker.SetGameStarted (true);
				darkenShade = false;
			}
		} else if (lightenShade) {
			if (oldColor.a > 0) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
				oldColor = spriteRenderer.color;
			} else {
				lightenShade = false;
				touchHandler.SetGameStarted (true);
				matchChecker.SetGameStarted (true);
//				Instantiate (nextTutorialShade);
				Destroy (gameObject);
			}
		}
		
		if (bounceJewels && Time.time > timeStamp + cooldown) {
			timeStamp = Time.time;
			MakeJewelsBounce ();
		}
		
	}
	public void DarkenShade () {
		instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-10, .18f, -11.9f), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.85f);
		darkenShade = true;
	}
	
	public void LightenShade () {
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().GetCurrentOwlie ().GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().SetOwlieSwap (false);
		GameObject.Find ("Level Controller").GetComponent<RockLevelMatchAssistant> ().SetTutorialLevel (false);
		bounceJewels = false;
		lightenShade = true;
	}
	
	public void SetCurrentOwlie (GameObject newOwlie) {
		currentOwlie = newOwlie;
	}

	void MakeJewelsBounce () {
		GameObject a = instantiator.GetJewelGridGameObject (1, 4);
		RockLevelJewelMovement jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (1, 6);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
	}
}
