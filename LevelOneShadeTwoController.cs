using UnityEngine;
using System.Collections;

public class LevelOneShadeTwoController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade, instantiateOwlie, bounceJewels;
	RockLevelTouchHandler touchHandler;
	RockLevelCheckForMatches matchChecker;
	public GameObject owlie, owlieSwapper, nextTutorialShade, speechBubble;
	GameObject currentOwlie, instantiatedOwlieSwapper, instantiatedSpeechBubble;
	float timeStamp, cooldown;
	RockLevelInstantiator instantiator;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		matchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		oldColor = spriteRenderer.color;
		DarkenShade ();
		cooldown = 8;
	}
	
	void FixedUpdate () {
		if (darkenShade) {
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
			}
		} else if (lightenShade) {
			if (oldColor.a > 0) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
				oldColor = spriteRenderer.color;
			} else {
				lightenShade = false;
				GameObject temp = instantiator.GetJewelGridGameObject (0, 3);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				
				temp = instantiator.GetJewelGridGameObject (1, 3);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				
				temp = instantiator.GetJewelGridGameObject (2, 2);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

				temp = instantiator.GetJewelGridGameObject (2, 3);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

				Instantiate (nextTutorialShade);
				Destroy (gameObject);
			}
		}

		if (bounceJewels && Time.time > timeStamp + cooldown) {
			timeStamp = Time.time;
			MakeJewelsBounce ();
		}
		
	}
	public void DarkenShade () {
		instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-10, 2.34f, -11.9f), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.96f);
		darkenShade = true;
	}
	
	public void LightenShade () {
		GameObject owlieSwapper = GameObject.Find ("OwlieSwapper(Clone)");
		owlieSwapper.GetComponent<OwlieSwapper> ().BellyPoked ();
		owlieSwapper.GetComponent<OwlieSwapper> ().GetCurrentOwlie ().GetComponent<LevelFailedScreenSlider> ().SetSlideSpeed (1);
		owlieSwapper.GetComponent<OwlieSwapper> ().TranslateOwlie (1.8f, -2.78f);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		bounceJewels = false;
		lightenShade = true;
		touchHandler.SetGameStarted (false);
		matchChecker.SetGameStarted (false);
	}
	
	public void SetCurrentOwlie (GameObject newOwlie) {
		currentOwlie = newOwlie;
	}

	void MakeJewelsBounce () {
		RockLevelInstantiator instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		
		GameObject a = instantiator.GetJewelGridGameObject (4, 2);
		RockLevelJewelMovement jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (3, 3);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (4, 4);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
	}
}
