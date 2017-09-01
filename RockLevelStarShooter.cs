using UnityEngine;
using System.Collections;

public class RockLevelStarShooter : MonoBehaviour {

	public GameObject redStar, greenStar, orangeStar, blueStar, whiteStar, purpleStar, levelCompleteShade;
	public GameObject blueBomb, greenBomb, orangeBomb, purpleBomb, redBomb, whiteBomb;
	public GameObject blueRowDestruction, greenRowDestruction, orangeRowDestruction, purpleRowDestruction, redRowDestruction, whiteRowDestruction;
	float timeStamp, cooldown, endOfLevelCooldown;
	int row, col, starShotCount;
	LevelTwoBombRemainderController bombRemainderController;
	RockLevelHomingStarMovement homingStarMovement;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement;
	RockLevelMovementChecker stoppedMoving;
	RockLevelDeleteJewels deleteJewels;
	RockLevelScoreKeeper scoreKeeper;
	RockLevelCheckForMatches checkForMatches;
	PowerStarTracker powerStarTracker;
	SoundHandler soundHandler;
	DarkenOnInstantiaton darkenOnInstantiaton;
	Vector3 starStartingPosition;
	GameObject tempStar, instantiatedShade;
	int bombCount, starLaunchCount;
	bool startLaunchingStars;
	
	// Use this for initialization
	void Start () {
		starShotCount = 0;
		timeStamp = Time.time;
		cooldown = .125f;
		endOfLevelCooldown = .75f;
		bombRemainderController = gameObject.GetComponent<LevelTwoBombRemainderController> ();
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		starStartingPosition = new Vector3 (-2.25f, -2.9f, -19);
		stoppedMoving = gameObject.GetComponent<RockLevelMovementChecker> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		scoreKeeper = gameObject.GetComponent<RockLevelScoreKeeper> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
		starLaunchCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (checkForMatches.GetGameStarted () && startLaunchingStars && bombCount > 0 && Time.time > timeStamp + cooldown) {
			starLaunchCount++;
			soundHandler.PlayStarShot ();
			if (starLaunchCount == 10 && bombCount >= 10) {
				starLaunchCount = 0;
				InstantiateRowDestructionStar ();
				scoreKeeper.IncreaseScoreByRowDestructionStar (bombCount);
				bombRemainderController.DecreaseBombRemainderByTen ();
				bombCount -= 10;
			}
			else if (starLaunchCount == 5 && bombCount >= 5) {
				InstantiateFallingBomb ();
				scoreKeeper.IncreaseScoreByFallingBomb (bombCount);
				bombRemainderController.DecreaseBombRemainderByFive ();
				bombCount -= 5;
			} else {
				InstantiateHomingStar ();
				scoreKeeper.IncreaseScoreByBombNumberStar (bombCount);
				bombRemainderController.DecreaseBombRemainderByOne ();
				bombCount--;
			}
			timeStamp = Time.time;
		}
		else if (startLaunchingStars && bombCount == 0 && Time.time > timeStamp + endOfLevelCooldown && stoppedMoving.GetGridStatic ()) {
			startLaunchingStars = false;
			deleteJewels.SetStopFindingMatches (true);
			instantiatedShade = (GameObject)Instantiate (levelCompleteShade);
			darkenOnInstantiaton = instantiatedShade.GetComponent<DarkenOnInstantiaton> ();
			darkenOnInstantiaton.SetLevelComplete (true);
		}
	}

	void InstantiateFallingBomb () {
		PaidPowerTracker.AddPowerToList ((GameObject) Instantiate (GetRandomFallingBomb (), starStartingPosition, Quaternion.identity));
	}

	void InstantiateRowDestructionStar () {
		PaidPowerTracker.AddPowerToList ((GameObject) Instantiate (GetRandomRowDestructionStar (), starStartingPosition, Quaternion.identity));
	}
	
	void InstantiateHomingStar () {
//		Debug.Log ("Instantiating Shooting Star");
		tempStar = (GameObject)Instantiate (GetRandomStar (), starStartingPosition, Quaternion.identity);
		homingStarMovement = tempStar.GetComponent<RockLevelHomingStarMovement> ();
		row = Random.Range (0, 9);
		col = Random.Range (0, 9);
		jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
		while (!PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (row, col))) {
			row = Random.Range (0, 9);
			col = Random.Range (0, 9);
		}
//		jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
//		while (jewelMovement.GetMoving () || !PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (row, col))) {
//			row = Random.Range (0, 9);
//			col = Random.Range (0, 9);
//			while (instantiator.GetJewelGridGameObject (row, col) == null) {
//				row = Random.Range (0, 9);
//				col = Random.Range (0, 9);
//			}
//			jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
//		}
		homingStarMovement.SetRow (row);
		homingStarMovement.SetCol (col);
	}

	private GameObject GetRandomRowDestructionStar () {
		switch (Random.Range (0, 6)) {
		case 0: return blueRowDestruction;
		case 1: return greenRowDestruction;
		case 2: return orangeRowDestruction;
		case 3: return purpleRowDestruction;
		case 4: return redRowDestruction;
		case 5: return whiteRowDestruction;
		}
		return null;
	}

	private GameObject GetRandomFallingBomb () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb;
		case 1: return greenBomb;
		case 2: return orangeBomb;
		case 3: return purpleBomb;
		case 4: return redBomb;
		case 5: return whiteBomb;
		}
		return null;
	}
	
	private GameObject GetRandomStar () {
		switch (Random.Range (0, 6)) {
		case 0: return redStar;
		case 1: return greenStar;
		case 2: return orangeStar;
		case 3: return blueStar;
		case 4: return whiteStar;
		case 5: return purpleStar;
		}
		return null;
	}
	
	public void SetStartLaunchingStars (bool startLaunchingStars) {
		if (startLaunchingStars) {
			if (GameObject.Find ("Time Bomb Identification") != null) {
				GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted (false);
			}
			//Debug.Log ("Setting startLaunchingStars = true");
			PowerStarTracker.SetStarsShooting (true);
		}
		this.startLaunchingStars = startLaunchingStars;
		bombCount = bombRemainderController.GetBombRemainderTotal ();
	}
	
	public bool GetStartLaunchingStars () {
		return startLaunchingStars;
	}

	public int GetBombCount () {
		//Debug.Log ("BombCount = " + bombCount);
		return bombCount;
	}
}
