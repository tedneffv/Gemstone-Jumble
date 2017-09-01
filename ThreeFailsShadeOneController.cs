using UnityEngine;
using System.Collections;

public class ThreeFailsShadeOneController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade, instantiateOwlie, bounceJewels, plusFiveMoved, jewelSwapMoved, rowDestructionMoved, powerBombMoved, multiStarMoved, bottomBannerMoved;
	RockLevelTouchHandler touchHandler;
	RockLevelCheckForMatches matchChecker;
	public GameObject owlie, owlieSwapper, nextTutorialShade, speechBubble, nextSpeechBubble, tenThousandCoins;
	public GameObject plusFiveExplaination, jewelSwapExplaination, rowDestructionExplaination, bombPowerExplaination, multiStarPowerExplaination, okayButton;
	GameObject currentOwlie, instantiatedOwlieSwapper, instantiatedSpeechBubble, firstExplaination, secondExplaination, thirdExplaination, fourthExplaination, fifthExplaination, instantiatedOkayButton, instantiatedCoins;
	float timeStamp, cooldown;
	RockLevelInstantiator instantiator;
	// Use this for initialization

	void Awake () {
		Debug.Log ("THREE FAILS SHADE ONE CONTROLLER START() METHOD");
		GameObject.Find ("Game Manager").GetComponent<OwliePowerUpTutorialController> ().SetPlayedTutorialTrue ();
		instantiateOwlie = true;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		matchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		oldColor = spriteRenderer.color;
		cooldown = 3;
	}
	void Start () {
	}
	
	void FixedUpdate () {

		if (!plusFiveMoved && GameObject.Find ("Plus Five Coin Holder") != null) {
			plusFiveMoved = true;
			GameObject.Find ("Plus Five Coin Holder").transform.Translate (new Vector3 (0, 0, -100));
			GameObject.Find ("Plus 5 Bomb Button(Clone)").GetComponent<PlusFiveButtonHandler> ().SetTouchOn (false);
		}

		if (!jewelSwapMoved && GameObject.Find ("Jewel Swap Coin Handler") != null) {
			jewelSwapMoved = true;
			GameObject.Find ("Jewel Swap Coin Handler").transform.Translate (new Vector3 (0, 0, -100));
			GameObject.Find ("Jewel Swap Button(Clone)").GetComponent<JewelSwapHandler> ().SetTouchOn (false);
		}

		if (!rowDestructionMoved && GameObject.Find ("Row Destruction Coin Holder") != null) {
			rowDestructionMoved = true;
			GameObject.Find ("Row Destruction Coin Holder").transform.Translate (new Vector3 (0, 0, -100));
			GameObject.Find ("Row Destruction Button(Clone)").GetComponent<RowDestructionButtonHandler> ().SetTouchOn (false);
		}

		if (!powerBombMoved && GameObject.Find ("Power Bomb Coin Handler") != null) {
			powerBombMoved = true;
			GameObject.Find ("Power Bomb Coin Handler").transform.Translate (new Vector3 (0, 0, -100));
			GameObject.Find ("Bomb Power Button(Clone)").GetComponent<PowerBombButtonHandler> ().SetTouchOn (false);
		}

		if (!multiStarMoved && GameObject.Find ("Multi Star Coin Holder") != null) {
			multiStarMoved = true;
			GameObject.Find ("Multi Star Coin Holder").transform.Translate (new Vector3 (0, 0, -100));
			GameObject.Find ("Multi Star Power Button(Clone)").GetComponent<MultiStarPowerHandler> ().SetTouchOn (false);
		}

		if (!bottomBannerMoved && GameObject.Find ("Bottom Banner(Clone)") != null) {
			bottomBannerMoved = true;
			GameObject.Find ("Bottom Banner(Clone)").transform.position = new Vector3 (transform.position.x, transform.position.y, -100);
		}

		if (darkenShade) {
			if (instantiateOwlie) {
				instantiateOwlie = false;
				instantiatedOwlieSwapper = (GameObject)Instantiate (owlieSwapper);
//				instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-10, 3.02f, -11.9f), Quaternion.identity);
//				instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.94f);
				currentOwlie = (GameObject)Instantiate (owlie, new Vector3 (-10, 1.23f, -55), Quaternion.identity);
				currentOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.75f);
				instantiatedOwlieSwapper.GetComponent<OwlieSwapper> ().SetCurrentOwlie (currentOwlie);
			}
			if (oldColor.a < .67f) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
				oldColor = spriteRenderer.color;
			} else {
//				MakeJewelsBounce ();
				timeStamp = Time.time;
				bounceJewels = true;
				darkenShade = false;
			}
		} else if (lightenShade) {
			if (oldColor.a > 0) {
				spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
				oldColor = spriteRenderer.color;
			} else {

				touchHandler.SetGameStarted (true);
				matchChecker.SetGameStarted (true);

				GameObject.Find ("Bottom Banner").transform.position = new Vector3 (GameObject.Find ("Bottom Banner").transform.position.x, GameObject.Find ("Bottom Banner").transform.position.y, -1);

				GameObject.Find ("Plus Five Coin Holder").transform.Translate (new Vector3 (0, 0, 100));
				GameObject.Find ("Plus 5 Bomb Button(Clone)").GetComponent<PlusFiveButtonHandler> ().SetTouchOn (true);
				
				GameObject.Find ("Jewel Swap Coin Handler").transform.Translate (new Vector3 (0, 0, 100));
				GameObject.Find ("Jewel Swap Button(Clone)").GetComponent<JewelSwapHandler> ().SetTouchOn (true);
				
				GameObject.Find ("Row Destruction Coin Holder").transform.Translate (new Vector3 (0, 0, 100));
				GameObject.Find ("Row Destruction Button(Clone)").GetComponent<RowDestructionButtonHandler> ().SetTouchOn (true);
				
				GameObject.Find ("Power Bomb Coin Handler").transform.Translate (new Vector3 (0, 0, 100));
				GameObject.Find ("Bomb Power Button(Clone)").GetComponent<PowerBombButtonHandler> ().SetTouchOn (true);
				
				GameObject.Find ("Multi Star Coin Holder").transform.Translate (new Vector3 (0, 0, 100));
				GameObject.Find ("Multi Star Power Button(Clone)").GetComponent<MultiStarPowerHandler> ().SetTouchOn (true);

				GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().SetTouchOn (true);
				GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().SetTouchOn (true);
			
				lightenShade = false;
				Destroy (gameObject);
			}
		}
		
//		if (bounceJewels && Time.time > timeStamp + cooldown) {
//			timeStamp = Time.time;
////			MakeJewelsBounce ();
//		}
		
	}
	
	public void InstantiateCoins () {
		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
		instantiatedCoins = (GameObject)Instantiate (tenThousandCoins, new Vector3 (1.75f, 1.23f, -200), Quaternion.identity);
		instantiatedCoins.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 400));
		instantiatedCoins.GetComponent<FallingObjectPositionController> ().SetTarget (GameObject.Find ("Bottom Banner").transform.position.x, GameObject.Find ("Bottom Banner").transform.position.y);
	}

	public void NextSpeechBubble () {
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedSpeechBubble = (GameObject)Instantiate (nextSpeechBubble, new Vector3 (-20, 2.87f, -54), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.48f);
	}

	public void DarkenShade () {
		GameObject.Find ("Power Up Button(Clone)").GetComponent<PowerButtonHandler> ().SetTouchOn (false);
		GameObject.Find ("Pause Button(Clone)").GetComponent<PauseButtonHandler> ().SetTouchOn (false);
		GameObject.Find ("Bottom Banner").GetComponent<BottomBannerCoinHandler> ().Move (true, -100);
		instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3 (-20, 2.87f, -54), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.48f);
		firstExplaination = (GameObject)Instantiate (plusFiveExplaination, new Vector3 (-10, -2.72f, -55), Quaternion.identity);
		firstExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-2);
		secondExplaination = (GameObject)Instantiate (jewelSwapExplaination, new Vector3 (-10, -2.29f, -55), Quaternion.identity);
		secondExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1);
		thirdExplaination = (GameObject)Instantiate (rowDestructionExplaination, new Vector3 (-10, -2.13f, -55), Quaternion.identity);
		thirdExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
		fourthExplaination = (GameObject)Instantiate (bombPowerExplaination, new Vector3 (-10, -2.29f, -55), Quaternion.identity);
		fourthExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1);
		fifthExplaination = (GameObject)Instantiate (multiStarPowerExplaination, new Vector3 (-10, -2.72f, -55), Quaternion.identity);
		fifthExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (2);
		instantiatedOkayButton = (GameObject)Instantiate (okayButton, new Vector3 (-20, -.65f, -54), Quaternion.identity);
		instantiatedOkayButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.73f);
		darkenShade = true;
	}
	
	public void LightenShade () {
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().BellyPoked ();   
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().GetCurrentOwlie ().GetComponent<LevelFailedScreenSlider> ().SetSlideSpeed (5);
		GameObject.Find ("OwlieSwapper(Clone)").GetComponent<OwlieSwapper> ().TranslateOwlie (10, 1.75f);
		Destroy (GameObject.Find ("OwlieSwapper(Clone)"));
		GameObject.Find ("Bottom Banner").transform.position = (new Vector3 (GameObject.Find ("Bottom Banner").transform.position.x, GameObject.Find ("Bottom Banner").transform.position.y, -1));
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);

		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		firstExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		secondExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		thirdExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		fourthExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		fifthExplaination.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
		instantiatedOkayButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);

		lightenShade = true;
		bounceJewels = false;
	}
	
	public void SetCurrentOwlie (GameObject newOwlie) {
		currentOwlie = newOwlie;
	}
	
	void MakeJewelsBounce () {
		GameObject a = instantiator.GetJewelGridGameObject (2, 3);
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
