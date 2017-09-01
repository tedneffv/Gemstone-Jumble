using UnityEngine;
using System.Collections;

public class LaunchpadLevelFourteenCreator : MonoBehaviour {

	public GameObject blueJewel, greenJewel, orangeJewel, purpleJewel, redJewel, whiteJewel;
	public GameObject blueBomb, greenBomb, orangeBomb, purpleBomb, redBomb, whiteBomb, bombNumber90;
	public GameObject boulder, boulderStage2, boulderStage3, boulderStage4, steelBlock, pauseButton, topBanner, bottomBanner, powerButton, glassBorder, slugStage1, slugStage2, slugStage3, slugStage4;
	public GameObject jewelCollector;
	public GameObject staticJewelOne, staticJewelTwo, staticJewelThree, staticJewelFour, staticJewelFive, staticJewelSix;
	public GameObject letterM, letterO, letterV, letterE, letterS;
	GameObject instantiatedStaticJewelOne, instantiatedStaticJewelTwo, instantiatedStaticJewelThree, instantiatedStaticJewelFour, instantiatedStaticJewelFive, instantiatedStaticJewelSix; 
	GameObject jewelCollectorOne, jewelCollectorTwo, jewelCollectorThree, jewelCollectorFour, jewelCollectorFive, jewelCollectorSix, instantiatedMoveNumber;
	Vector3 jewelCollectorOnePosition, jewelCollectorTwoPosition, jewelCollectorThreePosition, jewelCollectorFourPosition, jewelCollectorFivePosition, jewelCollectorSixPosition;
	Vector3 staticJewelOnePosition, staticJewelTwoPosition, staticJewelThreePosition, staticJewelFourPosition, staticJewelFivePosition, staticJewelSixPosition;
	Vector3 twoMoveNumbersFirstDigitPosiiton, twoMoveNumbersSecondDigitPosition, oneMoveNumberDigitPosition, letterMPosition, letterOPosition, letterVPosition, letterEPosition, letterSPosition;
	RockLevelBombHandler bombHandler;
	RockLevelNoMatchChecker noMatchChecker;
	JewelCollectorController jewelCollectorController;
	int[] bombArray;
	RockLevelJewelMovement jewelMovement;
	RockLevelInstantiator instantiator;
	GameObject tempJewel;
	bool offsetChanged, bombNeedsInstantiating, levelEight, jewelsInstantiated;
	float startingLeftJewelPosition, horizontalMultiplier;
	int jewelCount, layerOffset, deleteNumber;
	
	void Awake () {
		jewelCount = 0;
		deleteNumber = 0;
		startingLeftJewelPosition = -2.45f;
		horizontalMultiplier = .6125f;
		layerOffset = 17;
		bombHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelBombHandler> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		noMatchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelNoMatchChecker> ();
		jewelCollectorController = GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ();
		
		jewelCollectorOnePosition = new Vector3 (-2.325f, 1 + 3.86f, -.01f);
		jewelCollectorTwoPosition = new Vector3 (-1.395f, 1 + 3.72f, -.01f);
		jewelCollectorThreePosition = new Vector3 (-.465f, 1 + 3.66f, -.01f);
		jewelCollectorFourPosition = new Vector3 (.465f, 1 + 3.66f, -.01f);
		jewelCollectorFivePosition = new Vector3 (1.395f, 1 + 3.72f, -.01f);
		jewelCollectorSixPosition = new Vector3 (2.325f, 1 + 3.86f, -.01f);
		
		staticJewelOnePosition = new Vector3 (-2.325f, 2 + 3.4f, -.03f);
		staticJewelTwoPosition = new Vector3 (-1.395f, 2 + 3.27f, -.03f);
		staticJewelThreePosition = new Vector3 (-.465f, 2 + 3.18f, -.03f);
		staticJewelFourPosition = new Vector3 (.465f, 2 + 3.18f, -.03f);
		staticJewelFivePosition = new Vector3 (1.395f, 2 + 3.31f, -.03f);
		staticJewelSixPosition = new Vector3 (2.325f, 2 + 3.4f, -.03f);
		
		letterMPosition = new Vector3 (2.03f, -3.23f, -1);
		letterOPosition = new Vector3 (2.25f, -3.23f, -1);
		letterVPosition = new Vector3 (2.41f, -3.23f, -1);
		letterEPosition = new Vector3 (2.57f, -3.23f, -1);
		letterSPosition = new Vector3 (2.71f, -3.23f, -1);
		
		bombArray = new int[4];
	}
	
	// Use this for initialization
	void Start () {
		Instantiate (pauseButton, Camera.main.ViewportToWorldPoint (new Vector3 (.93f, .04f, 200f)), Quaternion.identity);
		Instantiate (powerButton, Camera.main.ViewportToWorldPoint (new Vector3 (.07f,.04f, 210f)), Quaternion.identity);
		Instantiate (topBanner);
		//bottomBanner = (GameObject)Instantiate (bottomBanner, new Vector3 (0, -6, -2), Quaternion.identity);
		//		Instantiate (powerButton, new Vector3 (-2.3f, -4f, -90f), Quaternion.identity);
		Instantiate (glassBorder, new Vector3 (0, .16f, -.25f), Quaternion.identity);
		jewelCollectorOne = (GameObject)Instantiate (jewelCollector, jewelCollectorOnePosition, Quaternion.identity);
		jewelCollectorOne.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (-2.325f, 3.86f, -.02f), 11);
		jewelCollectorOne.name = "Jewel Collector One"; //white jewel
		
		jewelCollectorTwo = (GameObject)Instantiate (jewelCollector, jewelCollectorTwoPosition, Quaternion.identity);
		jewelCollectorTwo.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (-1.395f, 3.72f, -.02f), 10f);
		jewelCollectorTwo.name = "Jewel Collector Two"; //red jewel
		
		jewelCollectorThree = (GameObject)Instantiate (jewelCollector, jewelCollectorThreePosition, Quaternion.identity);
		jewelCollectorThree.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (-.465f, 3.66f, -.02f), 9);
		jewelCollectorThree.name = "Jewel Collector Three"; //green jewel
		
		jewelCollectorFour = (GameObject)Instantiate (jewelCollector, jewelCollectorFourPosition, Quaternion.identity);
		jewelCollectorFour.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (.465f, 3.66f, -.02f), 8f);
		jewelCollectorFour.name = "Jewel Collector Four"; //blue jewel
		
		jewelCollectorFive = (GameObject)Instantiate (jewelCollector, jewelCollectorFivePosition, Quaternion.identity);
		jewelCollectorFive.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (1.395f, 3.72f, -.02f), 7);
		jewelCollectorFive.name = "Jewel Collector Five"; //purple jewel
		
		jewelCollectorSix = (GameObject)Instantiate (jewelCollector, jewelCollectorSixPosition, Quaternion.identity);
		jewelCollectorSix.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (2.325f, 3.86f, -.02f), 6f);
		jewelCollectorSix.name = "Jewel Collector Six"; //orange jewel
		
		jewelCollectorController.SetJewelCollectorOneNumbers (jewelCollectorOne, jewelCollectorOnePosition, 0, 0, 2, 5, 25);
		jewelCollectorController.AddToTotalJewelGoal (25);
		jewelCollectorController.SetJewelCollectorCap (1, 25);
		jewelCollectorController.SetJewelCollectorTwoNumbers (jewelCollectorTwo,jewelCollectorTwoPosition, 0, 0, 2, 5, 25);
		jewelCollectorController.AddToTotalJewelGoal (25);
		jewelCollectorController.SetJewelCollectorCap (2, 25);
		jewelCollectorController.SetJewelCollectorThreeNumbers (jewelCollectorThree, jewelCollectorThreePosition, 0, 0, 2, 5, 25);
		jewelCollectorController.AddToTotalJewelGoal (25);
		jewelCollectorController.SetJewelCollectorCap (3, 25);
		jewelCollectorController.SetJewelCollectorFourNumbers (jewelCollectorFour, jewelCollectorFourPosition, 0, 0, 2, 5, 25);
		jewelCollectorController.AddToTotalJewelGoal (25);
		jewelCollectorController.SetJewelCollectorCap (4, 25);
		jewelCollectorController.SetJewelCollectorFiveNumbers (jewelCollectorFive, jewelCollectorFivePosition, 0, 0, 2, 5, 25);
		jewelCollectorController.AddToTotalJewelGoal (25);
		jewelCollectorController.SetJewelCollectorCap (5, 25);
		jewelCollectorController.SetJewelCollectorSixNumbers (jewelCollectorSix, jewelCollectorSixPosition, 0, 0, 2, 5, 25);
		jewelCollectorController.AddToTotalJewelGoal (25);
		jewelCollectorController.SetJewelCollectorCap (6, 25);
		
		instantiatedStaticJewelOne = (GameObject)Instantiate (staticJewelOne, staticJewelOnePosition, Quaternion.identity);
		instantiatedStaticJewelOne.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (-2.325f, 3.4f, -.03f), 11);
		instantiatedStaticJewelOne.name = "Static Jewel One"; //white jewel
		
		instantiatedStaticJewelTwo = (GameObject)Instantiate (staticJewelTwo, staticJewelTwoPosition, Quaternion.identity);
		instantiatedStaticJewelTwo.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (-1.395f, 3.27f, -.03f), 10);
		instantiatedStaticJewelTwo.name = "Static Jewel Two"; //red jewel
		
		instantiatedStaticJewelThree = (GameObject)Instantiate (staticJewelThree, staticJewelThreePosition, Quaternion.identity);
		instantiatedStaticJewelThree.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (-.465f, 3.18f, -.03f), 9);
		instantiatedStaticJewelThree.name = "Static Jewel Three"; //green jewel
		
		instantiatedStaticJewelFour = (GameObject)Instantiate (staticJewelFour, staticJewelFourPosition, Quaternion.identity);
		instantiatedStaticJewelFour.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (.465f, 3.18f, -.03f), 8);
		instantiatedStaticJewelFour.name = "Static Jewel Four"; //blue jewel
		
		instantiatedStaticJewelFive = (GameObject)Instantiate (staticJewelFive, staticJewelFivePosition, Quaternion.identity);
		instantiatedStaticJewelFive.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (1.395f, 3.31f, -.03f), 7);
		instantiatedStaticJewelFive.name = "Static Jewel Five"; //purple jewel
		
		instantiatedStaticJewelSix = (GameObject)Instantiate (staticJewelSix, staticJewelSixPosition, Quaternion.identity);
		instantiatedStaticJewelSix.GetComponent<JewelCollectionSlider> ().SetTargetPosition (new Vector3 (2.325f, 3.4f, -.03f), 6);
		instantiatedStaticJewelSix.name = "Static Jewel Six"; //orange jewel
		
		GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ().SetMoveNumbers (30);
		
		Instantiate (letterM, letterMPosition, Quaternion.identity);
		Instantiate (letterO, letterOPosition, Quaternion.identity);
		Instantiate (letterV, letterVPosition, Quaternion.identity);
		Instantiate (letterE, letterEPosition, Quaternion.identity);
		Instantiate (letterS, letterSPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (jewelsInstantiated) {
			jewelsInstantiated = false;
			if (noMatchChecker.CheckForMatchesOnInstantiation ()) {
				jewelCount = 0;
				for (int i = 0; i < 9; i++) {
					for (int j = 0; j < 9; j++) {
						if (IsBomb (jewelCount) || IsSlugBomb (jewelCount))
							bombHandler.RemoveFromBombList (instantiator.GetJewelGridGameObject (i, j));
						Destroy (instantiator.GetJewelGridGameObject (i, j));
						jewelCount++;
					}
				}
				//Debug.Log ("Getting New Jewels");
				jewelCount = 0;
				InstantiateJewels ();
			}
		}
	}
	
	public void InstantiateJewels () {
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (IsBomb (jewelCount)) {
					//					horizontalMultiplier = .63f;
					startingLeftJewelPosition = -2.415f;
					offsetChanged = true;
				}
				else if (offsetChanged) {
					//					horizontalMultiplier = .6125f;
					startingLeftJewelPosition = -2.45f;
					offsetChanged = false;
				}
				if (IsBoulder (jewelCount)) {
					tempJewel = GetJewels (jewelCount, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), -.5f));
					//					tempJewel = (GameObject)Instantiate(GetJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), -.5f), Quaternion.identity);
					instantiator.SetJewelGridGameObject (i, j, tempJewel);
					//					jewelGrid[i, j] = (GameObject)Instantiate(GetJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 12), Quaternion.identity);
				} else if (IsBomb (jewelCount) || IsSlugBomb (jewelCount)) {
					tempJewel = GetJewels (jewelCount, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2));
					Debug.Log ("Bomb = " + tempJewel);
					instantiator.SetJewelGridGameObject (i, j, tempJewel);
					//					jewelGrid[i, j] = (GameObject)Instantiate(GetJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 12), Quaternion.identity);
					bombHandler.AddBombToList (instantiator.GetJewelGridGameObject(i, j));
				}
				else {
					tempJewel = GetJewels (jewelCount, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2));
					instantiator.SetJewelGridGameObject (i, j, tempJewel);
					//					jewelGrid[i, j] = (GameObject)Instantiate(GetJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2), Quaternion.identity);
				}
				if (!Bomb (instantiator.GetJewelGridGameObject (i, j))) {
					while ((j > 1 && instantiator.GetJewelGridGameObject (i, j).name != "Rock Blocked(Clone)" && instantiator.GetJewelGridGameObject (i, j).tag != "Steel Block" && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j - 1).tag, instantiator.GetJewelGridGameObject (i, j).tag) && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j - 2).tag, instantiator.GetJewelGridGameObject (i, j).tag)) || 
					       (i > 1 && instantiator.GetJewelGridGameObject (i, j).name != "Rock Blocked(Clone)" && instantiator.GetJewelGridGameObject (i, j).tag != "Steel Block" && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i - 1, j).tag, instantiator.GetJewelGridGameObject (i, j).tag) && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i - 2, j).tag, instantiator.GetJewelGridGameObject (i, j).tag))) {
						Destroy (instantiator.GetJewelGridGameObject (i, j));
						tempJewel = (GameObject) Instantiate (GetRandomJewel (), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2), Quaternion.identity);
						instantiator.SetJewelGridGameObject (i, j, tempJewel);
						//						jewelGrid[i, j] = (GameObject) Instantiate (GetRandomJewel (), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2), Quaternion.identity);
					}
				} else if (Bomb (instantiator.GetJewelGridGameObject (i, j))) {
					startingLeftJewelPosition = -2.415f;
					offsetChanged = true;
					while ((j > 1 && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j - 1).tag, instantiator.GetJewelGridGameObject (i, j).tag) && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j - 2).tag, instantiator.GetJewelGridGameObject (i, j).tag)) || 
					       (i > 1 && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i - 1, j).tag, instantiator.GetJewelGridGameObject (i, j).tag) && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i - 2, j).tag, instantiator.GetJewelGridGameObject (i, j).tag))) {
						BombNumberHandler bombInfo = instantiator.GetJewelGridGameObject (i, j).GetComponentInChildren<BombNumberHandler> ();
						int bombNumber = bombInfo.GetBombNumber ();
						bombHandler.RemoveFromBombList (instantiator.GetJewelGridGameObject (i, j));
						Destroy (instantiator.GetJewelGridGameObject (i, j));
						tempJewel = GetCorrectBomb (bombNumber, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) -2));
						instantiator.SetJewelGridGameObject (i, j, tempJewel);
						bombHandler.AddBombToList (instantiator.GetJewelGridGameObject (i, j));
					}
				}
				
				//				if (jewelGrid[i, j].tag == "Green Bomb") { 
				//					bombHandler.AddBombToList (jewelGrid[i, j]);
				//				}
				instantiator.GetJewelGridGameObject (i, j).layer = i + layerOffset;
				jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
				jewelMovement.SetRow (i);
				jewelMovement.SetCol (j);
				AddToParent (instantiator.GetJewelGridGameObject (i, j), i);
				jewelCount++;
				
				//				jewelGrid[i, j].layer = i + layerOffset;
				//				jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
				//				jewelMovement.SetRow (i);
				//				jewelMovement.SetCol (j);
				//				AddToParent (jewelGrid[i, j], i); 
				//				if (jewelCount == 60 || jewelCount == 61)
				//					jewelGrid[i, j].tag = "Tutorial 1 Jewel";
				//				jewelCount++;
			}
		}
		jewelsInstantiated = true;
	}
	
	bool Bomb (GameObject possibleBomb) {
		if (possibleBomb.tag == "Blue Bomb" || possibleBomb.tag == "Red Bomb" || possibleBomb.tag == "Green Bomb" || possibleBomb.tag == "Purple Bomb" || possibleBomb.tag == "Orange Bomb" || possibleBomb.tag == "White Bomb") {
			return true;
		}
		return false;
	}
	
	private bool IsBomb (string bombTag) {
		return (bombTag == "Red Bomb" || bombTag == "Green Bomb" || bombTag == "Blue Bomb" || bombTag == "White Bomb" || bombTag == "Purple Bomb" || bombTag == "Orange Bomb");
	}
	
	private void AddToParent (GameObject jewel, int row) {
		switch (row) {
		case 0: jewel.transform.parent = GameObject.Find ("Jewel Layer One").transform; break;
		case 1: jewel.transform.parent = GameObject.Find ("Jewel Layer Two").transform; break;
		case 2: jewel.transform.parent = GameObject.Find ("Jewel Layer Three").transform; break;
		case 3: jewel.transform.parent = GameObject.Find ("Jewel Layer Four").transform; break;
		case 4: jewel.transform.parent = GameObject.Find ("Jewel Layer Five").transform; break;
		case 5: jewel.transform.parent = GameObject.Find ("Jewel Layer Six").transform; break;
		case 6: jewel.transform.parent = GameObject.Find ("Jewel Layer Seven").transform; break;
		case 7: jewel.transform.parent = GameObject.Find ("Jewel Layer Eight").transform; break;
		case 8: jewel.transform.parent = GameObject.Find ("Jewel Layer Nine").transform; break;
		default: Debug.Log ("Didn't find a parent for block in method AddToParent in class LevelOneInstantiator"); break;
		}
	}
	
	GameObject GetCorrectBomb (int bombNumber, Vector3 position) {
		switch (bombNumber) {
		case 100: return GetBomb (position, bombNumber90);
		}
		return null;
	}
	
	GameObject GetRandomJewel () {
		switch (Random.Range (0, 6)) {
		case 0: return blueJewel;
		case 1: return greenJewel;
		case 2: return whiteJewel;
		case 3: return orangeJewel;
		case 4: return redJewel;
		case 5: return purpleJewel;
		}
		return null;
	}
	
	GameObject GetRandomWhiteBlueAndOrangeJewel () {
		switch (Random.Range (0, 3)) {
		case 0: return whiteJewel;
		case 1: return orangeJewel;
		case 2: return blueJewel;
		}
		return null;
	}
	
	GameObject GetBomb (Vector3 position, GameObject bombNumber) {
		GameObject tempBomb = null, tempNumber;
		switch (Random.Range (0, 6)) {
		case 0: tempBomb = (GameObject)Instantiate (blueBomb, position, Quaternion.identity); break;
		case 1: tempBomb = (GameObject)Instantiate (greenBomb, position, Quaternion.identity); break;
		case 2: tempBomb = (GameObject)Instantiate (whiteBomb, position, Quaternion.identity); break;
		case 3: tempBomb = (GameObject)Instantiate (orangeBomb, position, Quaternion.identity); break;
		case 4: tempBomb = (GameObject)Instantiate (redBomb, position, Quaternion.identity); break;
		case 5: tempBomb = (GameObject)Instantiate (purpleBomb, position, Quaternion.identity); break;
		}
		tempNumber = (GameObject)Instantiate (bombNumber, new Vector3 (tempBomb.transform.position.x, tempBomb.transform.position.y, tempBomb.transform.position.z - .1f), Quaternion.identity);
		tempNumber.transform.parent = tempBomb.transform;
		return tempBomb;
	}
	
	GameObject GetSlugBomb (Vector3 position, GameObject bombNumber, GameObject slug) {
		GameObject tempSlug, tempNumber;
		tempSlug = (GameObject)Instantiate (slug, new Vector3 (position.x, position.y, -.5f), Quaternion.identity); 
		tempNumber = (GameObject)Instantiate (bombNumber, new Vector3 (tempSlug.transform.position.x + .03f, tempSlug.transform.position.y -.1f, tempSlug.transform.position.z - .1f), Quaternion.identity);
		tempNumber.transform.parent = tempSlug.transform;
		return tempSlug;
	}
	
	GameObject GetBlockBomb (Vector3 position, GameObject bombNumber, GameObject block) {
		GameObject tempBlock, tempNumber;
		tempBlock = (GameObject)Instantiate (block, new Vector3 (position.x, position.y, -.5f), Quaternion.identity);
		tempNumber = (GameObject)Instantiate (bombNumber, new Vector3 (tempBlock.transform.position.x + .03f, tempBlock.transform.position.y + .03f, tempBlock.transform.position.z - .1f), Quaternion.identity);
		tempNumber.transform.parent = tempBlock.transform;
		return tempBlock;
	}
	
	
	bool IsSlugBomb (int jewelNumber) {
		//		switch (jewelNumber) {
		//			return true;
		//		}
		return false;
	}
	
	bool IsBomb (int jewelNumber) {
		//		switch (jewelNumber) {
		//			return true;
		//		}
		return false;
	}
	
	bool IsBoulder (int jewelNumber) {
		switch (jewelNumber) {
		case 72: 
		case 73: 
		case 74: 
		case 75: 
		case 76: 
		case 77: 
		case 78: 
		case 79: 
		case 80: 
			return true;
		}
		return false;
	}
	
	GameObject GetJewels (int jewelNumber, Vector3 position) {
		GameObject temp;
		switch (jewelNumber) {
		case 0: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 1: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 2: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 3: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 4: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 5: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 6: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 7: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 8: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 9: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 10: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 11: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity); 
		case 12: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 13: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 14: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 15: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 16: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 17: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 18: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 19: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 20: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 21: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 22: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 23: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 24: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 25: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 26: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 27: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 28: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 29: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 30: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity); 
		case 31: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity); 
		case 32: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity); 
		case 33: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 34: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 35: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 36: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 37: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 38: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 39: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 40: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 41: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 42: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 43: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 44: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 45: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 46: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 47: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 48: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 49: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 50: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 51: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 52: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 53: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 54: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 55: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 56: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 57: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 58: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 59: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 60: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 61: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 62: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
			
		case 63: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 64: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 65: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 66: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 67: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 68: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 69: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 70: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 71: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity); 
			
		case 72: temp = (GameObject)Instantiate (slugStage4, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 73: temp = (GameObject)Instantiate (slugStage4, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 74: temp = (GameObject)Instantiate (slugStage3, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 75: temp = (GameObject)Instantiate (slugStage2, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 76: temp = (GameObject)Instantiate (slugStage1, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 77: temp = (GameObject)Instantiate (slugStage2, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 78: temp = (GameObject)Instantiate (slugStage3, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 79: temp = (GameObject)Instantiate (slugStage4, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		case 80: temp = (GameObject)Instantiate (slugStage4, position, Quaternion.identity);
			SlugListManager.AddToSlugList (temp);
			return temp;
		}
		return null;
	}
	
	bool JewelsAreTheSame (string jewelOneTag, string jewelTwoTag) {
		if ((jewelOneTag == "Blue Block" || jewelOneTag == "Blue Bomb") && (jewelTwoTag == "Blue Block" || jewelTwoTag == "Blue Bomb"))
			return true;
		if ((jewelOneTag == "Red Block" || jewelOneTag == "Red Bomb") && (jewelTwoTag == "Red Block" || jewelTwoTag == "Red Bomb"))
			return true;
		if ((jewelOneTag == "Green Block" || jewelOneTag == "Green Bomb") && (jewelTwoTag == "Green Block" || jewelTwoTag == "Green Bomb"))
			return true;
		if ((jewelOneTag == "Yellow Block" || jewelOneTag == "Orange Bomb") && (jewelTwoTag == "Yellow Block" || jewelTwoTag == "Orange Bomb"))
			return true;
		if ((jewelOneTag == "Purple Block" || jewelOneTag == "Purple Bomb") && (jewelTwoTag == "Purple Block" || jewelTwoTag == "Purple Bomb"))
			return true;
		if ((jewelOneTag == "White Block" || jewelOneTag == "White Bomb") && (jewelTwoTag == "White Block" || jewelTwoTag == "White Bomb"))
			return true;
		return false;
	}
	
	public int GetDeleteNumber () {
		int temp = deleteNumber;
		deleteNumber++;
		return temp;
	}
}
