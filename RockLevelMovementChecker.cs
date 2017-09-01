using UnityEngine;
using System.Collections;

public class RockLevelMovementChecker : MonoBehaviour {

	bool gridStatic, fifthSlideGone, gameStarted, startLaunchingStars, launchBombs, artilleryLaunched, tutorialLevel, collectingDone;
	RockLevelController rockLevelController;
	RockLevelCheckForMatches checkForMatches;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement;
	RockLevelBombHandler bombHandler;
	RockLevelStarShooter starShooter;
	//	LevelThreeBombRemainderController remainderController;
	//	LevelThreeTutorialShadeController shadeController;
	RockLevelTouchHandler touchHandler;
	RockLevelFourInARow fourInARow;
	RockLevelFiveInARow fiveInARow;
	RockLevelCorners corners;
	RockLevelNoMatchChecker noMatchChecker;
	RockLevelShuffleGameBoard shuffle;
	RockLevelDeleteJewels deleteJewels;
	SoundController soundController;
	public GameObject levelCompleteShade;
	GameObject instantiatedShade;

	// Use this for initialization
	void Start () {
		rockLevelController = gameObject.GetComponent<RockLevelController> ();
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		checkForMatches = rockLevelController.GetComponent<RockLevelCheckForMatches> ();
		instantiator = rockLevelController.GetComponent<RockLevelInstantiator> ();
		bombHandler = rockLevelController.GetComponent<RockLevelBombHandler> ();
		starShooter = gameObject.GetComponent<RockLevelStarShooter> ();
		//		remainderController = levelThreeController.GetComponent<LevelThreeBombRemainderController> ();
		touchHandler = rockLevelController.GetComponent<RockLevelTouchHandler> ();
		fourInARow = gameObject.GetComponent<RockLevelFourInARow> ();
		fiveInARow = gameObject.GetComponent<RockLevelFiveInARow> ();
		corners = gameObject.GetComponent<RockLevelCorners> ();
		noMatchChecker = gameObject.GetComponent<RockLevelNoMatchChecker> ();
		shuffle = gameObject.GetComponent<RockLevelShuffleGameBoard> ();
		soundController = gameObject.GetComponent<SoundController> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		if (GameObject.Find ("Mountain Level One ID") != null) 
			tutorialLevel = true;
	}
	

	public void CheckGridForMovement () {
		gridStatic = false;
		if (GameObject.Find ("Time Bomb Identification") != null) {
			if (GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().GetBombsExploding ()) {
				return;
			}
		}
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (instantiator.GetJewelGridGameObject (i, j) == null)
					return;
				jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
				if (PaidPowerTracker.GetPowerListSize () != 0 /*|| starShooter.GetBombCount () > 0 || fourInARow.GetHomingStarList ().Count > 0 || fiveInARow.GetMotherStarList ().Count > 0 || fiveInARow.GetMotherStarList ().Count > 0 || corners.GetCornerStarList ().Count > 0*/) {
					gridStatic = false;
					deleteJewels.PunchNewTimeStamp ();
					return;
				}
			}
		}

		if (!shuffle.GetShuffleFinished ()) {
			shuffle.SetShuffleFinished (true);
		}
		if (noMatchChecker.CheckForNoMatches ()) {
			gridStatic = false;
			deleteJewels.PunchNewTimeStamp ();
			return;
		}
		if (!soundController.GetGameStarted ()) {
			soundController.SetGameStarted (true);
		}
		CheckEveryJewelForMatch ();
		gridStatic = true;
		PowerStarTracker.Clear ();
	}
	
	void CheckEveryJewelForMatch () {
		//Debug.Log ("gridStatic = true");
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (gameStarted) {
					GameObject tempObject = instantiator.GetJewelGridGameObject (i, j);
					if (checkForMatches.CheckForSwapBack (tempObject, i , j)) {
						gridStatic = false;
						deleteJewels.PunchNewTimeStamp ();
//						CheckGridForMovement ();
						return;
					}
//					tempObject.GetComponent<Rigidbody2D> ().isKinematic = false;
					tempObject.GetComponent<RockLevelJewelMovement> ().SetMoving (false);
					tempObject.GetComponent<RockLevelJewelMovement> ().SetNotToBeDestroyed ();
				}
			}
		}

		bombHandler.ZeroBombsStillExist ();

		if (!bombHandler.BombIsExploded () && !touchHandler.GetGameStarted ()) {
			touchHandler.SetGameStarted (true);
		}

//		if (tutorialLevel && GameObject.Find ("No Extras Shade").GetComponent<LevelOneShadeController> ().GetOwlieNumber () == 1 && bombHandler.GetBombCount () > 0) {
//			GameObject.Find ("No Extras Shade").GetComponent<LevelOneShadeController> ().DarkenShade ();
//		}
		if (GameObject.Find ("Jewel Collection Level ID") == null && !startLaunchingStars && bombHandler.GetBombListCount () == 0) {
			startLaunchingStars = true;
			touchHandler.SetGameStarted (false);
			starShooter.SetStartLaunchingStars (true);
		} else if (GameObject.Find ("Jewel Collection Level ID") != null && collectingDone && !GameObject.Find ("Jewel Collector").GetComponent<EndOfLevelCollectionJewelShooter> ().GetStartSpray ()) {
			GameObject.Find ("Jewel Collector").GetComponent<EndOfLevelCollectionJewelShooter> ().StartJewelSpray ();
		} /*else if (GameObject.Find ("Jewel Collection Level ID") != null && collectingDone && GameObject.Find ("Jewel Collector").GetComponent<EndOfLevelCollectionJewelShooter> ().GetStartSpray () &&
		           GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().GetTotalJewelCollectionGoalNumber () <= 0) {
//			GameObject instantiatedShade = (GameObject)Instantiate (levelCompleteShade);
//			instantiatedShade.GetComponent<DarkenOnInstantiaton> ().SetLevelComplete(true);
			GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ().StartLaunchingRowDestructionStars ();
		} 
*/
		else if (GameObject.Find ("Jewel Collection Level ID") != null && !collectingDone && GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ().GetMoveNumber () == 0) {
			GameObject instantiatedShade = (GameObject)Instantiate (levelCompleteShade);
			instantiatedShade.GetComponent<DarkenOnInstantiaton> ().SetLevelComplete (false);
		}

		if (GameObject.Find ("Jewel Collector") != null && GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ().GetFinishedLaunching ()) {
			GameObject instantiatedShade = (GameObject)Instantiate (levelCompleteShade);
			instantiatedShade.GetComponent<DarkenOnInstantiaton> ().SetLevelComplete (true);
		}
			

		if (!launchBombs && bombHandler.TimeBombIsExploded ()) {
			launchBombs = true;
			touchHandler.SetGameStarted (false);
			bombHandler.LaunchArtillery ();
		}

		if (gameStarted == false) {
			gameStarted = true;
		}
		
	}
	
	public bool GetGridStatic () {
		return gridStatic;
	}
	
	public void SetGridStaticToFalse () {
		gridStatic = false;
	}
	
	public void SetFifthSlideGoneTrue () {
		fifthSlideGone = true;
	}
	
	public bool GetGameStarted () {
		return gameStarted;
	}

	public void SetCollectingDone () {
		collectingDone = true;
	}
}
