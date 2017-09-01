using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelDeleteJewels : MonoBehaviour {

	RockLevelMoveJewelsDown moveJewelsDown;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement;
	//	LevelTwoTutorialShadeController shadeController;
	RockLevelBombHandler bombHandler;
	//	BannerMovement bannerMovement;
	//	BannerController bannerController;
	RockLevelController controller;
	//	LevelTwoTutorialTouchHandler tutorialTouchHandler;
	RockLevelTouchHandler touchHandler;
	//	LevelTwoBombNumberController bombNumberController;
	//	LevelOneCorners corners;
	int bombNumber, noMatchCount;
	bool deleteAgain, fourInARow, findMatchForPlayer, bombDestroyed, okayToMoveAgain, tutorialLevel1, tutorialLevel2, tutorialLevel3, tutorialLevel4, swapComplete, testBool;
	RockLevelCheckForMatches checkForMatches;
	LevelTwoBombInfo bombInfo;
	GameObject tempNumber, tempBoulder;
	List<PositionHolder> deletedPositions, boulderHolder;
	float testTimeStamp, timeStamp, assistanceTimeStamp, cooldown, assistanceCooldown, okayToMoveTimeStamp, okayToMoveCooldown, startingLeftJewelPosition, horizontalMultiplier, swapMadeTimestamp, swapMadeCoodown, powerTrackerTimestamp, powerTrackerCooldown, zPosition;
	HashSet<GameObject> deleteListCopy, boulderDeleteList, floatingJewelList, removeList;
	public bool explosionsOn, stopFindingMatches;
	public GameObject blueExplosion, greenExplosion, redExplosion, purpleExplosion, orangeExplosion, whiteExplosion, yellowBlockExplosion, boulderExplosion;
	public GameObject boulderChain1, boulderChain2, boulderChain3, boulder, slug, slugStage2;
	public GameObject blueBomb, greenBomb, orangeBomb, purpleBomb, redBomb, whiteBomb, bombSpark;
	public Sprite boulderSprite, boulderSprite2, boulderSprite3, boulderSprite4, blueBombSprite, greenBombSprite, orangeBombSprite, purpleBombSprite, redBombSprite, whiteBombSprite;
	public GameObject staticBlueJewel, staticGreenJewel, staticOrangeJewel, staticPurpleJewel, staticRedJewel, staticWhiteJewel;
	GameObject targetStaticBlueJewel, targetStaticGreenJewel, targetStaticOrangeJewel, targetStaticPurpleJewel, targetStaticRedJewel, targetStaticWhiteJewel;
	int shadeCount, deleteCount;
	RockLevelFourInARow fourInARowScript;
	RockLevelScoreKeeper scoreKeeper;
	RockLevelMovementChecker movementChecker;
	RockLevelMatchAssistant matchAssistant;
	RockLevelNoMatchChecker noMatchChecker;
	RockLevelStarShooter starShooter;
	AddTimeLevelOneJewels addJewels;
	DecreaseBombs decreaseBombs;
	PositionHolder tempPositionHolder;
	SoundHandler soundHandler;
	EndOfLevelCollectionJewelShooter endOfLevelJewelSprayer;

	// Use this for initialization
	void Start () {
		deleteAgain = false;
		fourInARow = false;
		explosionsOn = true;
		bombNumber = 8;
		deleteCount = 0;
		zPosition = -30;
		deleteListCopy = new HashSet<GameObject> ();
		boulderDeleteList = new HashSet<GameObject> ();
		floatingJewelList = new HashSet<GameObject> ();
		moveJewelsDown = gameObject.GetComponent<RockLevelMoveJewelsDown> ();
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		controller = gameObject.GetComponent<RockLevelController> ();
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		//		tutorialTouchHandler = gameObject.GetComponent<LevelTwoTutorialTouchHandler> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
		bombHandler = gameObject.GetComponent<RockLevelBombHandler> ();
		matchAssistant = gameObject.GetComponent<RockLevelMatchAssistant> ();
		fourInARowScript = gameObject.GetComponent<RockLevelFourInARow> ();
		//		corners = GameObject.Find ("Level One Controller").GetComponent<LevelOneCorners> ();
		scoreKeeper = gameObject.GetComponent<RockLevelScoreKeeper> ();
		movementChecker = gameObject.GetComponent<RockLevelMovementChecker> ();
		noMatchChecker = gameObject.GetComponent<RockLevelNoMatchChecker> ();
		starShooter = gameObject.GetComponent<RockLevelStarShooter> ();
		if (GameObject.Find ("Jewel Collector") != null) 
			endOfLevelJewelSprayer = GameObject.Find ("Jewel Collector").GetComponent<EndOfLevelCollectionJewelShooter> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		boulderHolder = new List<PositionHolder> ();
		targetStaticBlueJewel = GameObject.Find ("Static Jewel Four");
		targetStaticGreenJewel = GameObject.Find ("Static Jewel Three");
		targetStaticOrangeJewel = GameObject.Find ("Static Jewel Six");
		targetStaticPurpleJewel = GameObject.Find ("Static Jewel Five");
		targetStaticRedJewel = GameObject.Find ("Static Jewel Two");
		targetStaticWhiteJewel = GameObject.Find ("Static Jewel One");

//		if (GameObject.Find ("Time Bomb ID") != null)
//			decreaseBombs = GameObject.Find ("Time Bomb ID").GetComponent<DecreaseBombs> ();
		timeStamp = Time.time;
		assistanceTimeStamp = Time.time;
		testTimeStamp = Time.time;
		swapMadeTimestamp = Time.time;
		swapMadeCoodown = .1f;
//		okayToMoveTimeStamp = Time.time;
//		okayToMoveCooldown = .1f;
		assistanceCooldown = 7f;
		powerTrackerTimestamp = Time.time;
		powerTrackerCooldown = 5;
		cooldown = 1.5f;
		shadeCount = 0;
		noMatchCount = 0;
		deletedPositions = new List<PositionHolder> ();
		if (GameObject.Find ("Mountain Level One ID") != null) {
			tutorialLevel1 = true;
		} else if (GameObject.Find ("Mountain Level Two ID") != null) {
			tutorialLevel2 = true;
		} else if (GameObject.Find ("Mountain Level Three ID") != null) {
			tutorialLevel3 = true;
		} else if (GameObject.Find ("Cabin Level Two ID") != null) {
			tutorialLevel4 = true;
		}
		startingLeftJewelPosition = -2.415f;
		horizontalMultiplier = .6125f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!movementChecker.GetGridStatic () && Time.time > timeStamp + cooldown) {
			movementChecker.CheckGridForMovement ();
			timeStamp = Time.time;
		}

		if (Time.time > powerTrackerTimestamp + powerTrackerCooldown) {
			powerTrackerTimestamp = Time.time;
			PowerStarTracker.Clear ();
		}

//		if (!okayToMoveAgain && Time.time > okayToMoveTimeStamp + okayToMoveCooldown) {
//			//Debug.Log ("Okay To Move Again");
//			okayToMoveAgain = true;
//		}

		if (!stopFindingMatches && Time.time > assistanceTimeStamp + assistanceCooldown) {
			findMatchForPlayer = true;
			assistanceTimeStamp = Time.time;
		}

		if (findMatchForPlayer) {
			matchAssistant.FindMatchForPlayer ();
			findMatchForPlayer = false;
		}

	}

	void FixedUpdate () {
		if (swapComplete && Time.time > swapMadeCoodown + swapMadeTimestamp) {
			swapComplete = false;
			SlugListManager.MoveAllSlugs ();
		} 
	}

	public void PunchNewTimeStamp () {
		timeStamp = Time.time;
	}


	public void PunchAssistanceTimeStamp () {
		assistanceTimeStamp = Time.time;
	}

//	bool CheckForBouldersForBoulders (int row, int col) {
//		tempBoulder = instantiator.GetJewelGridGameObject (row, col);
//		if (tempBoulder != null && tempBoulder.name == "Rock 3 Chain(Clone)") {
//			PowerStarTracker.AddToHashSet (tempBoulder);
//			tempBoulder.name = "Rock 2 Chain(Clone)";
//			tempBoulder.GetComponent<SpriteRenderer> ().sprite = boulderSprite3;
//		}
//		else if (tempBoulder != null && tempBoulder.name == "Rock 2 Chain(Clone)") {
//			PowerStarTracker.AddToHashSet (tempBoulder);
//			tempBoulder.name = "Rock 1 Chain(Clone)";
//			tempBoulder.GetComponent<SpriteRenderer> ().sprite = boulderSprite2;
//		}
//		else if (tempBoulder != null && tempBoulder.name == "Slug Stage 3(Clone)") {
//			tempBoulder.GetComponent<SlugMovementController> ().StageThreeToStageTwo ();	
//			return true;
//		}
//		else if (tempBoulder != null && tempBoulder.name == "Rock 1 Chain(Clone)") {
//			PowerStarTracker.AddToHashSet (tempBoulder);
//			tempBoulder.name = "Rock Blocked(Clone)";
//			tempBoulder.GetComponent<SpriteRenderer> ().sprite = boulderSprite;
//		}
//		else if (tempBoulder != null && tempBoulder.name == "Slug Stage 2(Clone)") {
//			tempBoulder.GetComponent<SlugMovementController> ().StageTwoToStageOne ();
//			return true;
//		}
//		else if (tempBoulder != null && tempBoulder.name == "Rock Blocked(Clone)") {
//			tempBoulder.GetComponent<RockLevelJewelMovement> ().AddToRocksToBeDestroyed (tempBoulder);
//			boulderDeleteList.Add (tempBoulder);
//		}
//		else if (tempBoulder != null && tempBoulder.name == "Slug(Clone)") {
//			if (tempBoulder.transform.childCount > 1) {
//				Sprite tempBombSprite = null;
//				switch (Random.Range (0, 6)) {
//				case 0: tempBombSprite = blueBombSprite; tempBoulder.name = "Blue Bomb"; tempBoulder.tag = "Blue Bomb"; break;
//				case 1: tempBombSprite = greenBombSprite; tempBoulder.name = "Green Bomb"; tempBoulder.tag = "Green Bomb"; break;
//				case 2: tempBombSprite = orangeBombSprite; tempBoulder.name = "Orange Bomb"; tempBoulder.tag = "Orange Bomb"; break;
//				case 3: tempBombSprite = purpleBombSprite; tempBoulder.name = "Purple Bomb"; tempBoulder.tag = "Purple Bomb"; break;
//				case 4: tempBombSprite = redBombSprite; tempBoulder.name = "Red Bomb"; tempBoulder.tag = "Red Bomb"; break;
//				case 5: tempBombSprite = whiteBombSprite; tempBoulder.name = "White Bomb"; tempBoulder.tag = "White Bomb"; break;
//				}
//				if (tempBoulder.transform.GetChild (0).tag == "Slug Eyes") {
//					Destroy (tempBoulder.GetComponent<SlugEyeController> ());
//					Destroy (tempBoulder.GetComponent<SlugMovementController> ());
//					SlugListManager.RemoveFromSlugList (tempBoulder);
//					Instantiate (whiteExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
//					Destroy (tempBoulder.transform.GetChild (0).gameObject);
//				} 
//				
//				else if (tempBoulder.transform.GetChild (1).tag == "Slug Eyes") {
//					Destroy (tempBoulder.GetComponent<SlugEyeController> ());
//					Destroy (tempBoulder.GetComponent<SlugMovementController> ());
//					SlugListManager.RemoveFromSlugList (tempBoulder);
//					Instantiate (whiteExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
//					Destroy (tempBoulder.transform.GetChild (1).gameObject);
//				}
//				
//				tempBoulder.transform.localScale = new Vector3 (.1f, .1f, 1f);
//				tempBoulder.GetComponent<BoxCollider2D> ().size = new Vector2 (5.47f, 6);
//				tempBoulder.GetComponent<SpriteRenderer> ().sprite = tempBombSprite;
//				GameObject instantiatedBombSpark = (GameObject) Instantiate(bombSpark, new Vector3 (tempBoulder.transform.position.x + .212f, tempBoulder.transform.position.y + .247f, tempBoulder.transform.position.z - .1f), Quaternion.identity);
//				instantiatedBombSpark.transform.parent = tempBoulder.transform;
//				int newRow = tempBoulder.GetComponent<RockLevelJewelMovement> ().GetRow ();
//				int newCol = tempBoulder.GetComponent<RockLevelJewelMovement> ().GetCol ();
//				tempBoulder.transform.position = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * newCol), 2.591258f - (.61f * newRow), (-1 * newRow) - 2);
//				
//				
//				if (tempBoulder.transform.GetChild (0).tag == "Bomb Number") {
//					tempBoulder.transform.GetChild (0).gameObject.transform.position = new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - .1f);
//				}
//				else if (tempBoulder.transform.GetChild (1).tag == "Bomb Number") {
//					tempBoulder.transform.GetChild (1).gameObject.transform.position = new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - .1f);
//				}
//				tempBoulder.GetComponent<Rigidbody2D> ().isKinematic = false;
//				return true;
//			}
//			else {
//				tempBoulder.GetComponent<RockLevelJewelMovement> ().AddToRocksToBeDestroyed (tempBoulder);
//				boulderDeleteList.Add (tempBoulder);
//			}
//		}
//		return false;
//	}

//	bool CheckForBouldersForBoulders (int row, int col) {
//		tempBoulder = instantiator.GetJewelGridGameObject (row, col);
//		if (tempBoulder != null && tempBoulder.name == "Rock 3 Chain(Clone)") {
	//			PowerStarTracker.AddToHashSet (tempBoulder);new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2)
//			Vector3 boulderPosition = tempBoulder.transform.position;
//			int boulderLayer = tempBoulder.layer;
//			Destroy (tempBoulder);
//			instantiator.SetJewelGridGameObject (row, col, (GameObject) Instantiate (boulderChain2, boulderPosition, Quaternion.identity));
//			tempBoulder = instantiator.GetJewelGridGameObject (row, col);
//			tempBoulder.layer = boulderLayer;
//			RockLevelJewelMovement boulderMovement = tempBoulder.GetComponent<RockLevelJewelMovement> ();
//			boulderMovement.SetBounced (true);
//			boulderMovement.SetRow (row);
//			boulderMovement.SetCol (col);
//		}
//		else if (tempBoulder != null && (tempBoulder.name == "Rock 2 Chain(Clone)" || tempBoulder.name == "Slug Stage 3(Clone)")) {
//			if (tempBoulder.name == "Slug Stage 3(Clone)") {
//				tempBoulder.GetComponent<SlugMovementController> ().StageThreeToStageTwo ();
//				return true;
//			} else {
//				PowerStarTracker.AddToHashSet (tempBoulder);
//				Vector3 boulderPosition = tempBoulder.transform.position;
//				int boulderLayer = tempBoulder.layer;
//				Destroy (tempBoulder);
//				instantiator.SetJewelGridGameObject (row, col, (GameObject) Instantiate (boulderChain1, boulderPosition, Quaternion.identity));
//				tempBoulder = instantiator.GetJewelGridGameObject (row, col);
//				tempBoulder.layer = boulderLayer;
//				RockLevelJewelMovement boulderMovement = tempBoulder.GetComponent<RockLevelJewelMovement> ();
//				boulderMovement.SetBounced (true);
//				boulderMovement.SetRow (row);
//				boulderMovement.SetCol (col);
//			}
//		}
//		else if (tempBoulder != null && (tempBoulder.name == "Rock 1 Chain(Clone)" || tempBoulder.name == "Slug Stage 2(Clone)")) {
//			if (tempBoulder.name == "Slug Stage 2(Clone)") {
////				SlugListManager.RemoveFromSlugList (tempBoulder);
////				Destroy (tempBoulder);
////				instantiator.SetJewelGridGameObject (row, col, (GameObject) Instantiate (slug, boulderPosition, Quaternion.identity));
////				SlugListManager.AddToSlugList (tempBoulder);
//				tempBoulder.GetComponent<SlugMovementController> ().StageTwoToStageOne ();
//				return true;
//			} else {
//				PowerStarTracker.AddToHashSet (tempBoulder);
//				Vector3 boulderPosition = tempBoulder.transform.position;
//				int boulderLayer = tempBoulder.layer;
//				bombHandler.RemoveFromBombList (tempBoulder);
//				Instantiate (whiteExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
//				Destroy (tempBoulder);
//				instantiator.SetJewelGridGameObject (row, col, (GameObject) Instantiate (boulder, boulderPosition, Quaternion.identity));
//				tempBoulder = instantiator.GetJewelGridGameObject (row, col);
//				tempBoulder.layer = boulderLayer;
//				RockLevelJewelMovement boulderMovement = tempBoulder.GetComponent<RockLevelJewelMovement> ();
//				boulderMovement.SetBounced (true);
//				boulderMovement.SetRow (row);
//				boulderMovement.SetCol (col);
//				bombHandler.AddBombToList (tempBoulder);
//			}
//		}
//
//		else if (row >= 0 && (tempBoulder.name == "Rock Blocked(Clone)" || tempBoulder.name == "Slug(Clone)")) {
//			if (tempBoulder.transform.childCount > 1) {
//				PowerStarTracker.AddToHashSet (tempBoulder);
//				Vector3 boulderPosition = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * col), 2.591258f - (.61f * row), (-1 * row) - 2);
////				Vector3 boulderPosition = tempBoulder.transform.position;
//				int boulderLayer = tempBoulder.layer;
//				GameObject bombNumber = null;
//				if (tempBoulder.transform.GetChild (0) != null && tempBoulder.transform.GetChild (0).tag == "Bomb Number")
//					bombNumber = tempBoulder.transform.GetChild (0).gameObject;
//				else if (tempBoulder.transform.GetChild (1) != null && tempBoulder.transform.GetChild (1).tag == "Bomb Number")
//					bombNumber = tempBoulder.transform.GetChild (1).gameObject;
//				bombNumber.transform.parent = null;
//				tempBoulder.GetComponent<SlugMovementController> ().BeingDeleted (true);
//				bombHandler.RemoveFromBombList (tempBoulder);
//				Instantiate (whiteExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
//				Destroy (tempBoulder);
//				instantiator.SetJewelGridGameObject (row, col, GetRandomBomb (boulderPosition, bombNumber));
//				Debug.Log ("Child 0 = " + instantiator.GetJewelGridGameObject (row, col).transform.GetChild (0) + " Child 1 = " + instantiator.GetJewelGridGameObject (row, col).transform.GetChild (1));
//				tempBoulder = instantiator.GetJewelGridGameObject (row, col);
//				bombHandler.AddBombToList (tempBoulder);
//				bombNumber.transform.position = new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - .1f);
//				tempBoulder.layer = boulderLayer;
//				RockLevelJewelMovement boulderMovement = tempBoulder.GetComponent<RockLevelJewelMovement> ();
//				boulderMovement.SetBounced (true);
//				boulderMovement.SetRow (row);
//				boulderMovement.SetCol (col);
//				return true;
//			} else {
//				tempBoulder.GetComponent<RockLevelJewelMovement> ().AddToRocksToBeDestroyed (tempBoulder);
//				boulderDeleteList.Add (tempBoulder);
//			}
//		}
//		return false;

//	}
	
	bool CheckForBoulders (int row, int col) {
		tempBoulder = instantiator.GetJewelGridGameObject (row, col);
		if (tempBoulder != null && tempBoulder.name == "Rock 3 Chain(Clone)") {
			tempBoulder.name = "Rock 2 Chain(Clone)";
			tempBoulder.GetComponent<SpriteRenderer> ().sprite = boulderSprite3;
			tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
			PowerStarTracker.RemoveFromHashSet (tempBoulder);
			return true;
		}
		else if (tempBoulder != null && tempBoulder.name == "Rock 2 Chain(Clone)") {
			tempBoulder.name = "Rock 1 Chain(Clone)";
			tempBoulder.GetComponent<SpriteRenderer> ().sprite = boulderSprite2;
			tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
			PowerStarTracker.RemoveFromHashSet (tempBoulder);
			Instantiate (redExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
			return true;
		}
		else if (tempBoulder != null && tempBoulder.name == "Slug Stage 4(Clone)") {
			tempBoulder.GetComponent<SlugMovementController> ().StageFourToStageThree ();
			tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
			PowerStarTracker.RemoveFromHashSet (tempBoulder);
			return true;
		}
		else if (tempBoulder != null && tempBoulder.name == "Slug Stage 3(Clone)") {
			tempBoulder.GetComponent<SlugMovementController> ().StageThreeToStageTwo ();
			tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
			PowerStarTracker.RemoveFromHashSet (tempBoulder);
			Instantiate (redExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
			return true;
		}
		else if (tempBoulder != null && tempBoulder.name == "Rock 1 Chain(Clone)") {
			tempBoulder.name = "Rock Blocked(Clone)";
			tempBoulder.GetComponent<SpriteRenderer> ().sprite = boulderSprite;
			tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
			PowerStarTracker.RemoveFromHashSet (tempBoulder);
			Instantiate (yellowBlockExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
			return true;
		}
		else if (tempBoulder != null && tempBoulder.name == "Slug Stage 2(Clone)") {
			tempBoulder.GetComponent<SlugMovementController> ().StageTwoToStageOne ();
			tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
			PowerStarTracker.RemoveFromHashSet (tempBoulder);
			Instantiate (yellowBlockExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
			return true;
		}
		else if (tempBoulder != null && tempBoulder.name == "Rock Blocked(Clone)") {
			if (tempBoulder.transform.childCount > 0 && bombHandler.BombListContains (tempBoulder)) {
				Sprite tempBombSprite = null;
				switch (Random.Range (0, 6)) {
				case 0: tempBombSprite = blueBombSprite; tempBoulder.name = "Blue Bomb"; tempBoulder.tag = "Blue Bomb"; break;
				case 1: tempBombSprite = greenBombSprite; tempBoulder.name = "Green Bomb"; tempBoulder.tag = "Green Bomb"; break;
				case 2: tempBombSprite = orangeBombSprite; tempBoulder.name = "Orange Bomb"; tempBoulder.tag = "Orange Bomb"; break;
				case 3: tempBombSprite = purpleBombSprite; tempBoulder.name = "Purple Bomb"; tempBoulder.tag = "Purple Bomb"; break;
				case 4: tempBombSprite = redBombSprite; tempBoulder.name = "Red Bomb"; tempBoulder.tag = "Red Bomb"; break;
				case 5: tempBombSprite = whiteBombSprite; tempBoulder.name = "White Bomb"; tempBoulder.tag = "White Bomb"; break;
				}

				for (int i = 0; i < tempBoulder.transform.childCount; i++) {
					if (tempBoulder.transform.GetChild (i).tag == "Slug Eyes") {
						Destroy (tempBoulder.GetComponent<SlugEyeController> ());
						Destroy (tempBoulder.GetComponent<SlugMovementController> ());
						SlugListManager.RemoveFromSlugList (tempBoulder);
						Instantiate (whiteExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
						Destroy (tempBoulder.transform.GetChild (i).gameObject);
						break;
					}
				}

				tempBoulder.transform.localScale = new Vector3 (.1f, .1f, 1f);
				tempBoulder.GetComponent<BoxCollider2D> ().size = new Vector2 (5.47f, 6);
				tempBoulder.GetComponent<SpriteRenderer> ().sprite = tempBombSprite;
				GameObject instantiatedBombSpark = (GameObject) Instantiate(bombSpark, new Vector3 (tempBoulder.transform.position.x + .212f, tempBoulder.transform.position.y + .247f, tempBoulder.transform.position.z - .1f), Quaternion.identity);
				instantiatedBombSpark.transform.parent = tempBoulder.transform;
				int newRow = tempBoulder.GetComponent<RockLevelJewelMovement> ().GetRow ();
				int newCol = tempBoulder.GetComponent<RockLevelJewelMovement> ().GetCol ();
				tempBoulder.transform.position = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * newCol), 2.591258f - (.61f * newRow), (-1 * newRow) - 2);
				tempBoulder.layer = row + 17;

				for (int i = 0; i < tempBoulder.transform.childCount; i++) {
					if (tempBoulder.transform.GetChild (i).tag == "Bomb Number") {
						tempBoulder.transform.GetChild (i).gameObject.transform.position = new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - .1f);
						break;
					}
				}
				
				tempBoulder.GetComponent<Rigidbody2D> ().isKinematic = false;
				tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
				PowerStarTracker.RemoveFromHashSet (tempBoulder);
				tempBoulder.GetComponent<Rigidbody2D> ().AddForce (new Vector3 (0, 300));
				return true;
			} else {
				tempBoulder.GetComponent<RockLevelJewelMovement> ().AddToRocksToBeDestroyed (tempBoulder);
				boulderDeleteList.Add (tempBoulder);
				return false;
			}
		}
		else if (tempBoulder != null && tempBoulder.name == "Slug(Clone)") {
			if (tempBoulder.transform.childCount > 1 && bombHandler.BombListContains (tempBoulder)) {

				Sprite tempBombSprite = null;
				switch (Random.Range (0, 6)) {
				case 0: tempBombSprite = blueBombSprite; tempBoulder.name = "Blue Bomb"; tempBoulder.tag = "Blue Bomb"; break;
				case 1: tempBombSprite = greenBombSprite; tempBoulder.name = "Green Bomb"; tempBoulder.tag = "Green Bomb"; break;
				case 2: tempBombSprite = orangeBombSprite; tempBoulder.name = "Orange Bomb"; tempBoulder.tag = "Orange Bomb"; break;
				case 3: tempBombSprite = purpleBombSprite; tempBoulder.name = "Purple Bomb"; tempBoulder.tag = "Purple Bomb"; break;
				case 4: tempBombSprite = redBombSprite; tempBoulder.name = "Red Bomb"; tempBoulder.tag = "Red Bomb"; break;
				case 5: tempBombSprite = whiteBombSprite; tempBoulder.name = "White Bomb"; tempBoulder.tag = "White Bomb"; break;
				}

				tempBoulder.transform.localScale = new Vector3 (.1f, .1f, 1f);
				tempBoulder.GetComponent<BoxCollider2D> ().size = new Vector2 (5.47f, 6);
				tempBoulder.GetComponent<SpriteRenderer> ().sprite = tempBombSprite;
				GameObject instantiatedBombSpark = (GameObject) Instantiate(bombSpark, new Vector3 (tempBoulder.transform.position.x + .212f, tempBoulder.transform.position.y + .247f, tempBoulder.transform.position.z - .1f), Quaternion.identity);
				instantiatedBombSpark.transform.parent = tempBoulder.transform;
				int newRow = tempBoulder.GetComponent<RockLevelJewelMovement> ().GetRow ();
				int newCol = tempBoulder.GetComponent<RockLevelJewelMovement> ().GetCol ();
				tempBoulder.transform.position = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * newCol), 2.591258f - (.61f * newRow), (-1 * newRow) - 2);
				tempBoulder.layer = row + 17;

				for (int i = 0; i < tempBoulder.transform.childCount; i++) {
					if (tempBoulder.transform.GetChild (i).tag == "Slug Eyes") {
						Destroy (tempBoulder.GetComponent<SlugEyeController> ());
						Destroy (tempBoulder.GetComponent<SlugMovementController> ());
						SlugListManager.RemoveFromSlugList (tempBoulder);
						Instantiate (whiteExplosion, new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - 10), Quaternion.Euler (-90, 0, 0));
						DestroySlugEyes (tempBoulder);
						break;
					}
				}

				for (int i = 0; i < tempBoulder.transform.childCount; i++) {
					if (tempBoulder.transform.GetChild (i).tag == "Bomb Number") {
						tempBoulder.transform.GetChild (i).gameObject.transform.position = new Vector3 (tempBoulder.transform.position.x, tempBoulder.transform.position.y, tempBoulder.transform.position.z - .1f);
						break;
					}
				}
				tempBoulder.GetComponent<Rigidbody2D> ().isKinematic = false;
				tempBoulder.GetComponent<RockLevelJewelMovement> ().SetAddedToArray (false);
				PowerStarTracker.RemoveFromHashSet (tempBoulder);
				tempBoulder.GetComponent<Rigidbody2D> ().AddForce (new Vector3 (0, 300));
				return true;
			}
			else {
				tempBoulder.GetComponent<RockLevelJewelMovement> ().AddToRocksToBeDestroyed (tempBoulder);
				boulderDeleteList.Add (tempBoulder);
				return false;
			}
		}
		return false;
	}

	void DestroySlugEyes (GameObject slug) {
		for (int i = 0; i < slug.transform.childCount; i++) {
			if (slug.transform.GetChild (i).tag == "Slug Eyes")
				Destroy (slug.transform.GetChild (i).gameObject);
		}
	}
	
	public void DeleteAllJewelsInList (HashSet<GameObject> deleteList, bool powerUpDelete) {
		removeList = new HashSet<GameObject> ();
		if (deleteList.Count >= 3) {
			soundHandler.PlayJewelBreak ();
		}
		assistanceTimeStamp = Time.time;
		timeStamp = Time.time;
		okayToMoveTimeStamp = Time.time;
		okayToMoveAgain = false;
		matchAssistant.SetResetRandoms (true);
		movementChecker.SetGridStaticToFalse ();
		GameObject tempJewel;
		if (deleteList.Count == 4) {
			soundHandler.PlayPowerUpSound ();
			fourInARowScript.ActivateFourInARowPower (deleteList);
//			scoreKeeper.IncreaseScoreByFourJewelBonus ();
		}
		foreach (GameObject a in deleteList) {
			if (a != null) {
				if (!powerUpDelete && a.tag != "Boulder") {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					if (jewelMovement.GetRow () + 1 < 9)
						CheckForBoulders (jewelMovement.GetRow () + 1, jewelMovement.GetCol ());
					if (jewelMovement.GetRow () - 1 >= 0)
						CheckForBoulders (jewelMovement.GetRow () - 1, jewelMovement.GetCol ());
					if (jewelMovement.GetCol () + 1 < 9)
						CheckForBoulders (jewelMovement.GetRow (), jewelMovement.GetCol () + 1);
					if (jewelMovement.GetCol () - 1 >= 0)
						CheckForBoulders (jewelMovement.GetRow (), jewelMovement.GetCol () - 1);
				}
				if (a.tag == "Boulder") {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					if (CheckForBoulders (jewelMovement.GetRow (), jewelMovement.GetCol ())) {
						removeList.Add (a);
					}
				}
			}
		}


		foreach (GameObject a in removeList) {
			deleteList.Remove (a);
		}
		
		foreach (GameObject a in boulderDeleteList) {
			deleteList.Add (a);
		}
		
		foreach (GameObject a in deleteList) {
			if (a != null) {
				deleteListCopy = deleteList;
				if (a != null && a.tag == "Steel Block") 
					PowerStarTracker.RemoveFromHashSet (a);
				if (a != null && a.tag != "Steel Block" && a.name != "Rock 3 Chain(Clone)" && a.name != "Rock 2 Chain(Clone)" && a.name != "Rock 1 Chain(Clone)") {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					//				moveJewelsDown.MoveJewelsAboveDown(a);
//					if (GameObject.Find ("Time Bomb ID") != null) {
//						if (addJewels == null)
//							addJewels = GameObject.Find ("Time Bomb ID").GetComponent<AddTimeLevelOneJewels> ();
//						if (Bomb(a) && !bombHandler.TimeBombIsExploded ()) {
//							bombHandler.RemoveFromBombList (a);
//							addJewels.InstantiateSingleJewels (jewelMovement.GetCol (), true);
//						} else if (!bombHandler.TimeBombIsExploded ())
//							addJewels.InstantiateSingleJewels (jewelMovement.GetCol (), false);
//						else if (bombHandler.TimeBombIsExploded ()) {
//							addJewels.InstantiateOnlyJewels (jewelMovement.GetCol ());
//						}
//					}
					//				else
					//					instantiator.InstantiateSingleJewels (jewelMovement.GetRow (), jewelMovement.GetCol ());
					
					
					if (Bomb (a) && GameObject.Find ("Time Bomb ID") == null) {
						GameObject tempNumber = null;
						for (int i = 0; i < a.transform.childCount; i++) {
							if (a.transform.GetChild (i).tag == "Bomb Number") {
								tempNumber = a.transform.GetChild (i).gameObject;
								break;
							}
						}
						if (tempNumber != null) {
							tempNumber.transform.parent = null;
							tempNumber.GetComponent<BombNumberHandler> ().SetBombDestroyed (true);
						}
						if (bombHandler.GetBombListCount () == 1) {
							//Debug.Log ("Setting touch to fault");
							touchHandler.SetGameStarted (false);
						}
//						bombInfo = a.GetComponent<LevelTwoBombInfo> ();
//						Instantiate (bombInfo.GetBombNumberSprite (), a.transform.position, Quaternion.identity);
					}
					else if (Bomb (a) && GameObject.Find ("Time Bomb ID") != null) {
						GameObject tempBomb;
//						bombInfo = a.GetComponent<LevelTwoBombInfo> ();
						a.GetComponent<Rigidbody2D>().isKinematic = true;
//						bombInfo.SetBombDestroyed (true);
					}
				}
			}
		}
		
		boulderDeleteList.Clear ();
		
		foreach (GameObject a in deleteList) {
			if (a != null) {
				if (explosionsOn)
					InstantiateExplosions (a);
				if (a.tag != "Steel Block") {
					scoreKeeper.IncreaseScoreByOneJewel ();
					if (bombHandler.BombListContains (a)) {
						jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
						bombHandler.RemoveFromBombList (a);
						bombHandler.DecreaseBombCounterByOne ();
//						scoreKeeper.IncreaseScoreByOneBomb ();
					}
					if (/*(GameObject.Find ("Time Bomb ID") == null || (GameObject.Find ("Time Bomb ID") != null && !Bomb (a))) &&*/ a.name != "Rock 3 Chain(Clone)" && a.name != "Rock 2 Chain(Clone)" && a.name != "Rock 1 Chain(Clone)") {
						if (GameObject.Find ("Time Bomb ID") != null && Bomb (a))
							bombDestroyed = true;
						tempPositionHolder = new PositionHolder (a.GetComponent<RockLevelJewelMovement> ().GetRow (), a.GetComponent<RockLevelJewelMovement> ().GetCol ());
						deletedPositions.Add (tempPositionHolder);
						int row = a.GetComponent<RockLevelJewelMovement> ().GetRow ();
						int col = a.GetComponent<RockLevelJewelMovement> ().GetCol ();
						a.GetComponent<RockLevelJewelMovement> ().StartDestroyCountdown ();
						PowerStarTracker.RemoveFromHashSet (a);
						if (a.name == "Slug(Clone)")
							SlugListManager.RemoveFromSlugList (a);
						if (GameObject.Find ("Jewel Collection Level ID") != null && !endOfLevelJewelSprayer.GetSprayingInProgress ()) {
							InstantiateStaticJewel (a);
						}
						Destroy (a);
						instantiator.SetJewelGridGameObject (row, col, null);
//						moveJewelsDown.MoveJewelsAboveDown (tempPositionHolder.GetCol (), tempPositionHolder.GetRow ());
//						instantiator.InstantiateSingleJewels (tempPositionHolder.GetRow (), tempPositionHolder.GetCol ());
					}
				}
			}
		}

//		deleteCount++;
//
//		if (deleteCount == 3) {
//			deleteCount = 0;
//			GameObject.Find ("Pump Up Word Holder").GetComponent<PumpUpWordHolder> ().InstantiateWow ();
//		}
		powerTrackerTimestamp = Time.time;
 		swapMadeTimestamp = Time.time;
//		if (swapComplete) {
//			swapComplete = false;
//			SlugListManager.MoveAllSlugs ();
//		}
		deleteList.Clear ();

		foreach (PositionHolder a in deletedPositions) {
//			//Debug.Log ("deletePositions.Size = " + deletedPositions.Count);
			int row = a.GetRow ();
			int col = a.GetCol ();
			moveJewelsDown.MoveJewelsAboveDown (col, row);
//			instantiator.InstantiateSingleJewels (row, col);
////			while (row >= 0 && instantiator.GetJewelGridGameObject (row, col) == null) 
////				row--;
////			if (instantiator.GetJewelGridGameObject (row, col) != null)
////				instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ().MoveDown (false);
		}

		List<PositionHolder> nullPositions = null;
		if (!starShooter.GetStartLaunchingStars ()) {
			nullPositions = new List<PositionHolder> ();
			int positionTotal = deletedPositions.Count, nullCount = 0;
			bool breakSecondLoop = false;

			for (int i = 0; i < 9; i++) {
				for (int j = 0; j < 9; j++) {
					if (instantiator.GetJewelGridGameObject (i, j) == null) {
						nullCount++;
						nullPositions.Add (new PositionHolder(i, j));
						if (nullCount >= positionTotal) {
							breakSecondLoop = true;
						}
					}
				}
				if (breakSecondLoop)
					break;
			}
		}

		foreach (PositionHolder a in deletedPositions) {
			if (tutorialLevel1) 
				instantiator.InstantiateLevelOneTutorialJewels (a.GetRow (), a.GetCol ());
			else if (tutorialLevel2) 
				instantiator.InstantiateLevelTwoTutorialJewels (a.GetRow (), a.GetCol ());
			else if (tutorialLevel3) 
				instantiator.InstantiateLevelThreeTutorialJewels (a.GetRow (), a.GetCol ());
			else if (tutorialLevel4)
				instantiator.InstantiateLevelFourTutorialJewels (a.GetRow (), a.GetCol ());
			else {
				instantiator.InstantiateSingleJewels (a.GetRow (), a.GetCol ());
			}
		}

		if (!starShooter.GetStartLaunchingStars ()) {
			int whileCount = 0;
			if (!tutorialLevel1 && !tutorialLevel2 && !tutorialLevel3 && noMatchChecker.CheckForNoMatchesWithoutShuffle () && noMatchCount < 3) {
				Debug.Log ("MAKING A NEW MATCH");
				noMatchCount++;
				do {
					foreach (PositionHolder a in nullPositions) {
						Destroy (instantiator.GetJewelGridGameObject (a.GetRow (), a.GetCol ()));
						instantiator.SetJewelGridGameObject (a.GetRow (), a.GetCol (), null);
						instantiator.InstantiateSingleJewels (a.GetRow (), a.GetCol ());
					}
					if (whileCount > 100) {
						Debug.Log ("WHILECOUNT BREAK");
						break;
					}
					whileCount++;
					
				} while (noMatchChecker.CheckForNoMatchesWithoutShuffle ());
			} else {
				noMatchCount = 0;
			}
		}

		deletedPositions.Clear ();

//		foreach (PositionHolder a in boulderHolder) {
//			if (instantiator.GetJewelGridGameObject (a.GetRow (), a.GetCol ()) != null) {
//				checkForMatches.CheckForSwapBack (instantiator.GetJewelGridGameObject (a.GetRow (), a.GetCol ()), a.GetRow (), a.GetCol ());
//			}
//		}
//		boulderHolder.Clear ();
		//		if (shadeCount == 0 || shadeCount == 1) {
		//			shadeController = controller.GetCurrentTutorialShade ().GetComponent<LevelTwoTutorialShadeController> ();
		//			shadeController.MakeInvisible ();
		//			shadeCount++;
		//		} 
//		if (swapComplete) {
//			swapComplete = false;
//			SlugListManager.MoveAllSlugs ();
//		}
	}


	public bool IsContainedInDeleteList (GameObject jewel) {
		return deleteListCopy.Contains (jewel);
	}

	public bool CheckForMatches (List<PositionHolder> positions) {
		foreach (PositionHolder a in positions) {
			if (!noMatchChecker.CheckForNoMatchSingleJewel (a.GetRow (), a.GetCol ()))
				return false;
		}
		return true;
	}
	
	bool Bomb (GameObject possibleBomb) {
		if (possibleBomb.tag == "Blue Bomb" || possibleBomb.tag == "Red Bomb" || possibleBomb.tag == "Green Bomb" || possibleBomb.tag == "Purple Bomb" || possibleBomb.tag == "Orange Bomb" || possibleBomb.tag == "White Bomb") {
			return true;
		}
		return false;
	}
	
	public void InstantiateExplosions (GameObject jewel) {
		if (jewel == null)
			return;
		switch (jewel.tag) {
		case "Blue Bomb":
		case "Blue Block": Instantiate (blueExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Green Bomb": 
		case "Green Block": Instantiate (greenExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Purple Bomb":
		case "Purple Block": Instantiate (purpleExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Orange Bomb":
		case "Yellow Block": Instantiate (orangeExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "White Bomb":
		case "White Block": Instantiate (whiteExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Red Bomb":
		case "Red Block": Instantiate (redExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		case "Steel Block":
		case "Boulder": Instantiate (boulderExplosion, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - 10), Quaternion.Euler (-90, 0, 0)); break;
		}
	}

	GameObject GetRandomBomb (Vector3 position, GameObject bombNumber) {
		GameObject tempBomb = null, tempNumber;
		Debug.Log ("bombNumber = " + bombNumber);
		switch (Random.Range (0, 6)) {
		case 0: tempBomb = (GameObject)Instantiate (blueBomb, position, Quaternion.identity); break;
		case 1: tempBomb = (GameObject)Instantiate (greenBomb, position, Quaternion.identity); break;
		case 2: tempBomb = (GameObject)Instantiate (whiteBomb, position, Quaternion.identity); break;
		case 3: tempBomb = (GameObject)Instantiate (orangeBomb, position, Quaternion.identity); break;
		case 4: tempBomb = (GameObject)Instantiate (redBomb, position, Quaternion.identity); break;
		case 5: tempBomb = (GameObject)Instantiate (purpleBomb, position, Quaternion.identity); break;
		}
		tempNumber = bombNumber;
		Debug.Log ("tempNumber = " + tempNumber);
		tempNumber.transform.parent = tempBomb.transform;
		return tempBomb;
	}

	void InstantiateStaticJewel (GameObject jewel) {
		GameObject temp;
		switch (jewel.name) {
		case "Blue Gem(Clone)": temp = (GameObject)Instantiate (staticBlueJewel, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - .1f), Quaternion.identity); 
			temp.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (targetStaticBlueJewel.transform.position.x, targetStaticBlueJewel.transform.position.y, zPosition), Random.Range (3.0f, 6.0f)); 
			temp.name = "Blue Seeking Jewel";
			temp.GetComponent<JewelCollectionSlider> ().Move (); break;
		case "Green Gem(Clone)": temp = (GameObject)Instantiate (staticGreenJewel, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - .1f), Quaternion.identity); 
			temp.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (targetStaticGreenJewel.transform.position.x, targetStaticGreenJewel.transform.position.y, zPosition), Random.Range (3.0f, 6.0f)); 
			temp.name = "Green Seeking Jewel";
			temp.GetComponent<JewelCollectionSlider> ().Move (); break;
		case "Orange Gem(Clone)": temp = (GameObject)Instantiate (staticOrangeJewel, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - .1f), Quaternion.identity); 
			temp.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (targetStaticOrangeJewel.transform.position.x, targetStaticOrangeJewel.transform.position.y, zPosition), Random.Range (3.0f, 6.0f)); 
			temp.name = "Orange Seeking Jewel";
			temp.GetComponent<JewelCollectionSlider> ().Move (); break;
		case "Purple Gem(Clone)": temp = (GameObject)Instantiate (staticPurpleJewel, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - .1f), Quaternion.identity); 
			temp.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (targetStaticPurpleJewel.transform.position.x, targetStaticPurpleJewel.transform.position.y, zPosition), Random.Range (3.0f, 6.0f));  
			temp.name = "Purple Seeking Jewel";
			temp.GetComponent<JewelCollectionSlider> ().Move (); break;
		case "Red Gem(Clone)": temp = (GameObject)Instantiate (staticRedJewel, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - .1f), Quaternion.identity); 
			temp.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (targetStaticRedJewel.transform.position.x, targetStaticRedJewel.transform.position.y, zPosition), Random.Range (3.0f, 6.0f));  
			temp.name = "Red Seeking Jewel";
			temp.GetComponent<JewelCollectionSlider> ().Move (); break;
		case "White Gem(Clone)": temp = (GameObject)Instantiate (staticWhiteJewel, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, jewel.transform.position.z - .1f), Quaternion.identity); 
			temp.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (targetStaticWhiteJewel.transform.position.x, targetStaticWhiteJewel.transform.position.y, zPosition), Random.Range (3.0f, 6.0f));  
			temp.name = "White Seeking Jewel";
			temp.GetComponent<JewelCollectionSlider> ().Move (); break;
		}
		zPosition = Random.Range (-40f, -30f);
//		if (zPosition < -40) 
//			zPosition = -30;
	}

	public void StampTimeCard () {
		timeStamp = Time.time;
	}

	public void StampAssistantTimeCard () {
		assistanceTimeStamp = Time.time;
	}

	public void SetStopFindingMatches (bool stopFindingMatches) {
		this.stopFindingMatches = stopFindingMatches;
	}

	public bool IsElementOfBoulderDeleteList (GameObject possibleBoulder) {
		return boulderDeleteList.Contains (possibleBoulder);
	}

	public bool OkayToMoveAgain () {
		return okayToMoveAgain;
	}

	public void SetSwapComplete (bool swapComplete) {
		swapMadeTimestamp = Time.time;
		this.swapComplete = swapComplete;
	}

	public void ResetDeleteCount () {
		deleteCount = 0;
	}

}
