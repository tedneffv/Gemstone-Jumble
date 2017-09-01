using UnityEngine;
using System.Collections;

public class LevelTwoShadeThreeController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade, bounceJewels, instantiateSpeechBubble, translated;
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
		cooldown = 8;
		DarkenShade ();
		instantiateSpeechBubble = true;
	}
	
	void FixedUpdate () {
		if (darkenShade) {
			if (instantiateSpeechBubble) {
				instantiateSpeechBubble = false;
				instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-10, 2.77f, -11.9f), Quaternion.identity);
				instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.12f);
			}
			if (oldColor.a < .67f) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
				oldColor = spriteRenderer.color;
			} else {
				if (!translated) {
					translated = true;
					GameObject temp = instantiator.GetJewelGridGameObject (7, 6);
					temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

					temp = instantiator.GetJewelGridGameObject (6, 6);
					temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);

					temp = instantiator.GetJewelGridGameObject (5, 6);
					temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -11.9f);

					temp = instantiator.GetJewelGridGameObject (5, 7);
					temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
					
					temp = instantiator.GetJewelGridGameObject (4, 6);
					temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
					
					temp = instantiator.GetJewelGridGameObject (3, 6);
					temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				}
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
				//				GameObject temp = instantiator.GetJewelGridGameObject (4, 2);
				//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				//				
				//				temp = instantiator.GetJewelGridGameObject (4, 3);
				//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				//				
				//				temp = instantiator.GetJewelGridGameObject (3, 3);
				//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				//				
				//				temp = instantiator.GetJewelGridGameObject (4, 4);
				//				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
				
				lightenShade = false;
				GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().GetRidOfSloMo ();
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
		GameObject temp = instantiator.GetJewelGridGameObject (4, 7);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
		
		temp = instantiator.GetJewelGridGameObject (4, 6);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
		
		temp = instantiator.GetJewelGridGameObject (3, 6);
		temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -12);
		
		
		darkenShade = true;
	}
	
	public void LightenShade () {
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().BellyPoked ();   
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().GetCurrentOwlie ().GetComponent<LevelFailedScreenSlider> ().SetSlideSpeed (7);
//		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().TranslateOwlie (10, -.03f);
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().GetCurrentOwlie ().GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		Destroy (GameObject.Find ("OwlieSwapper(Clone)"));
		GameObject.Find ("Level Controller").GetComponent<RockLevelMatchAssistant> ().SetTutorialLevel (false);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
//		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		lightenShade = true;
		bounceJewels = false;
//		touchHandler.SetGameStarted (true);
//		matchChecker.SetGameStarted (true);
	}
	
	public void SetCurrentOwlie (GameObject newOwlie) {
		currentOwlie = newOwlie;
	}
	
	void MakeJewelsBounce () {
		GameObject a = instantiator.GetJewelGridGameObject (7, 6);
		RockLevelJewelMovement jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (6, 6);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (5, 7);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (4, 6);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (3, 6);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
	}
}
