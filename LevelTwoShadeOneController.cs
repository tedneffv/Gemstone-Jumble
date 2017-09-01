using UnityEngine;
using System.Collections;

public class LevelTwoShadeOneController : MonoBehaviour {

	
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
		instantiateOwlie = true;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		matchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		oldColor = spriteRenderer.color;
		cooldown = 8;
	}

	void Update () {

	}
	
	void FixedUpdate () {
		if (darkenShade) {
			if (instantiateOwlie) {
				instantiateOwlie = false;
				instantiatedOwlieSwapper = (GameObject)Instantiate (owlieSwapper);
				instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-10, 3.59f, -11.9f), Quaternion.identity);
				instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.94f);
				currentOwlie = (GameObject)Instantiate (owlie, new Vector3 (-10, 1.8f, -50), Quaternion.identity);
				currentOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.75f);
				instantiatedOwlieSwapper.GetComponent<OwlieSwapper> ().SetCurrentOwlie (currentOwlie);
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
			}
		} else if (lightenShade) {
			if (oldColor.a > 0) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
				oldColor = spriteRenderer.color;
			} else {
				GameObject temp = instantiator.GetJewelGridGameObject (4, 4);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -51);
				
				temp = instantiator.GetJewelGridGameObject (5, 4);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -50.99f);
				
				temp = instantiator.GetJewelGridGameObject (6, 4);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -51);
				
				temp = instantiator.GetJewelGridGameObject (7, 4);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -51);

				temp = instantiator.GetJewelGridGameObject (5, 5);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -51);

				temp = instantiator.GetJewelGridGameObject (5, 6);
				temp.transform.position = new Vector3 (temp.transform.position.x, temp.transform.position.y, -51);
				
				lightenShade = false;
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

		if (PlayerPrefs.HasKey ("Level Two Tutorial Played")) {
			GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ().SetTutorialTwo (false);
			touchHandler.SetGameStarted (true);
			matchChecker.SetGameStarted (true);
			Destroy (gameObject);
			return;
		}  

		GameObject tempJewel = instantiator.GetJewelGridGameObject (2, 1);
		tempJewel.transform.position = (new Vector3 (tempJewel.transform.position.x, tempJewel.transform.position.y, -12));

		tempJewel = instantiator.GetJewelGridGameObject (2, 2);
		tempJewel.transform.position = (new Vector3 (tempJewel.transform.position.x, tempJewel.transform.position.y, -12));

		tempJewel = instantiator.GetJewelGridGameObject (2, 3);
		tempJewel.transform.position = (new Vector3 (tempJewel.transform.position.x, tempJewel.transform.position.y, -12));

		tempJewel = instantiator.GetJewelGridGameObject (2, 4);
		tempJewel.transform.position = (new Vector3 (tempJewel.transform.position.x, tempJewel.transform.position.y, -12));

		tempJewel = instantiator.GetJewelGridGameObject (1, 2);
		tempJewel.transform.position = (new Vector3 (tempJewel.transform.position.x, tempJewel.transform.position.y, -12));


		darkenShade = true;
	}
	
	public void LightenShade () {
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().BellyPoked ();   
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().GetCurrentOwlie ().GetComponent<LevelFailedScreenSlider> ().SetSlideSpeed (5);
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().TranslateOwlie (1.75f, 1.1f);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		lightenShade = true;
		bounceJewels = false;
		touchHandler.SetGameStarted (false);
		matchChecker.SetGameStarted (false);
	}
	
	public void SetCurrentOwlie (GameObject newOwlie) {
		currentOwlie = newOwlie;
	}
	
	void MakeJewelsBounce () {
		GameObject a = instantiator.GetJewelGridGameObject (2, 1);
		RockLevelJewelMovement jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (2, 3);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
		a = instantiator.GetJewelGridGameObject (2, 4);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);

		a = instantiator.GetJewelGridGameObject (1, 2);
		jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
		a.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 300));
		jewelMovement.SetBounceAgain (true);
		jewelMovement.SetBounced (false);
		
	}
}
