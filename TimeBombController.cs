using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeBombController : MonoBehaviour {

	RockLevelBombHandler bombHandler;
	RockLevelTouchHandler touchHandler;
	RockLevelMovementChecker movementChecker;
	RockLevelInstantiator instantiator;
	SoundHandler soundHandler;
	List<GameObject> timeBombScreenList;
	SpriteRenderer[] timeScreenRenderers, goRenderers;
	float alphaIncrease;
	bool increaseAlpha, start321Countdown, increaseNumberAlpha, fadeNumbers, startGO, decreaseAllAlpha, startGame, gameStarted, bombsExploding, playSound, timeStampReset;
	GameObject timeCountdown1, timeCountdown2, timeCountdown3, instantiatedGO;
	SpriteRenderer spriteRenderer1, spriteRenderer2, spriteRenderer3, currentRenderer;
	public GameObject go;
	float timeStamp, cooldown, fadeTimeStamp, fadeCooldown;

	// Use this for initialization
	void Start () {

		bombHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelBombHandler> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		movementChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelMovementChecker> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();

//		timeCountdown1 = GameObject.Find ("Time Countdown 1");
//		spriteRenderer1 = timeCountdown1.GetComponent<SpriteRenderer> ();
//
//		timeCountdown2 = GameObject.Find ("Time Countdown 2");
//		spriteRenderer2 = timeCountdown2.GetComponent<SpriteRenderer> ();
//
//		timeCountdown3 = GameObject.Find ("Time Countdown 3");
//		spriteRenderer3 = timeCountdown3.GetComponent<SpriteRenderer> ();
//
//		timeBombScreenList = new List<GameObject> ();
//		timeScreenRenderers = GameObject.Find ("Time Bomb Level Screen").GetComponentsInChildren<SpriteRenderer> ();
//		alphaIncrease = .03f;
//		increaseAlpha = true;
//		cooldown = 1;
//		timeStamp = Time.time;
//		playSound = true;
		timeStamp = Time.time;
		cooldown = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (gameStarted && !timeStampReset && Time.time > timeStamp + cooldown) {
			bombHandler.DecreaseAllBombsInList ();
			bombHandler.ZeroBombsStillExist ();
			timeStamp = Time.time;
		}

		if (timeStampReset && Time.time > timeStamp + cooldown) {
			RockLevelJewelMovement jewelMovement;
			timeStamp = Time.time;
			for (int i = 0; i < 9; i++) {
				for (int j = 0; j < 9; j++) {
					jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
					if (!jewelMovement.GetBounced ())
						return;
				}
			}
			timeStampReset = false;
			gameStarted = true;
		}
	}

//	void FixedUpdate () {
//		if (increaseAlpha) {
//			foreach (SpriteRenderer a in timeScreenRenderers) {
//				if ((a.name == "Time Countdown 1" || a.name == "Time Countdown 2" || a.name == "Time Countdown 3")) {
//					if (a.color.a <= .24f)
//						a.color = new Color (a.color.r, a.color.g, a.color.b, (a.color.a + alphaIncrease));
//				}
//				if (a.name != "Time Countdown 1" && a.name != "Time Countdown 2" && a.name != "Time Countdown 3") 
//					a.color = new Color (a.color.r, a.color.g, a.color.b, (a.color.a + alphaIncrease));
//			}
//			if (timeScreenRenderers[0].color.a >= 1)  {
//				increaseAlpha = false;
	//			}GetChild
//		}
//		
//		else if (start321Countdown) {
//			if (increaseNumberAlpha)
//				IncreaseAlpha (currentRenderer);
//			else if (!increaseNumberAlpha)
//				DecreaseAlpha (currentRenderer);
//		}
//		
//		else if (fadeNumbers) {
//			spriteRenderer1.color = new Color (spriteRenderer1.color.r, spriteRenderer1.color.g, spriteRenderer1.color.b, (spriteRenderer1.color.a - alphaIncrease));
//			spriteRenderer2.color = new Color (spriteRenderer2.color.r, spriteRenderer2.color.g, spriteRenderer2.color.b, (spriteRenderer2.color.a - alphaIncrease));
//			spriteRenderer3.color = new Color (spriteRenderer3.color.r, spriteRenderer3.color.g, spriteRenderer3.color.b, (spriteRenderer3.color.a - alphaIncrease));
//			if (spriteRenderer3.color.a <= 0) {
//				fadeNumbers = false;
//				startGO = true;
//			}
//		}
//		
//		else if (startGO) {
//			if (playSound) {
//				soundHandler.PlayGO ();
//				playSound = false;
//			}
//			if (instantiatedGO == null) {
//				instantiatedGO = (GameObject)Instantiate (go);
//				goRenderers = instantiatedGO.GetComponentsInChildren<SpriteRenderer> ();
//				GameObject.Find ("Level Controller").GetComponent<RockLevelCheckForMatches> ().SetGameStarted (true);
//			}
//			foreach (SpriteRenderer a in goRenderers) {
//				a.color = new Color (a.color.r, a.color.g, a.color.b, (a.color.a + alphaIncrease));
//			}
//			if (goRenderers[0].color.a >= 1) {
//				startGO = false;
//				decreaseAllAlpha = true;
//			}
//		}
//		
//		else if (decreaseAllAlpha) {
//			foreach (SpriteRenderer a in timeScreenRenderers) {
//				a.color = new Color (a.color.r, a.color.g, a.color.b, (a.color.a - alphaIncrease));
//			}
//			foreach (SpriteRenderer a in goRenderers) {
//				a.color = new Color (a.color.r, a.color.g, a.color.b, (a.color.a - alphaIncrease));
//			}
//			if (goRenderers[0].color.a <= 0) {
//				decreaseAllAlpha = false;
//				Destroy (GameObject.Find ("Time Bomb Level Screen"));
//				Destroy (instantiatedGO);
//				gameStarted = true;
//			}
//		}
//
//	}
//
//	public void IncreaseAlpha (SpriteRenderer tempSprite) {
//		if (playSound) {
//			soundHandler.Play321 ();
//			playSound = false;
//		}
//		tempSprite.color = new Color (tempSprite.color.r, tempSprite.color.g, tempSprite.color.b, (tempSprite.color.a + alphaIncrease));
//		if (tempSprite.color.a >= 1)
//			increaseNumberAlpha = false;
//	}
//
//	public void DecreaseAlpha (SpriteRenderer tempSprite) {
//		tempSprite.color = new Color (tempSprite.color.r, tempSprite.color.g, tempSprite.color.b, (tempSprite.color.a - alphaIncrease));
//		if (tempSprite.color.a <= .24) {
//			if (currentRenderer.name == "Time Countdown 3")
//				currentRenderer = spriteRenderer2;
//			else if (currentRenderer.name == "Time Countdown 2")
//				currentRenderer = spriteRenderer1;
//			else if (currentRenderer.name == "Time Countdown 1") {
//				start321Countdown = false;
//				fadeNumbers = true;
//			}
//			playSound = true;
//			increaseNumberAlpha = true;
//		}
//	}
//
//	public void Start321Countdown (bool start321Countdown) {
//		this.start321Countdown = start321Countdown;
//		currentRenderer = spriteRenderer3;
//		increaseNumberAlpha = true;
//	}

	public void SetGameStarted (bool gameStarted) {
		this.gameStarted = gameStarted;
	}

	public bool GetBombsExploding () {
		return bombsExploding;
	}

	public void SetBombsExploding (bool bombsExploding) {
		this.bombsExploding = bombsExploding;
	}

	public void ResetTimestamp () {
		gameStarted = false;
		timeStampReset = true;
	}

}
