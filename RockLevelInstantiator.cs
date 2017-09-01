using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelInstantiator : MonoBehaviour {

	public GameObject redJewel, blueJewel, whiteJewel, greenJewel, orangeJewel, purpleJewel;
	public GameObject greenBomb20, greenBomb19, greenBomb18, greenBomb17, greenBomb16, greenBomb15, greenBomb14, greenBomb13, greenBomb12, greenBomb11, greenBomb10, greenBomb9, greenBomb8, greenBomb7, greenBomb6, greenBomb5, greenBomb4, greenBomb3, greenBomb2, greenBomb1, greenBomb0;
	public GameObject redBomb20, redBomb19, redBomb18, redBomb17, redBomb16, redBomb15, redBomb14, redBomb13, redBomb12, redBomb11, redBomb10, redBomb9, redBomb8, redBomb7, redBomb6, redBomb5, redBomb4, redBomb3, redBomb2, redBomb1, redBomb0;
	public GameObject blueBomb20, blueBomb19, blueBomb18, blueBomb17, blueBomb16, blueBomb15, blueBomb14, blueBomb13, blueBomb12, blueBomb11, blueBomb10, blueBomb9, blueBomb8, blueBomb7, blueBomb6, blueBomb5, blueBomb4, blueBomb3, blueBomb2, blueBomb1, blueBomb0;
	public GameObject orangeBomb20, orangeBomb19, orangeBomb18, orangeBomb17, orangeBomb16, orangeBomb15, orangeBomb14, orangeBomb13, orangeBomb12, orangeBomb11, orangeBomb10, orangeBomb9, orangeBomb8, orangeBomb7, orangeBomb6, orangeBomb5, orangeBomb4, orangeBomb3, orangeBomb2, orangeBomb1, orangeBomb0;
	public GameObject purpleBomb20, purpleBomb19, purpleBomb18, purpleBomb17, purpleBomb16, purpleBomb15, purpleBomb14, purpleBomb13, purpleBomb12, purpleBomb11, purpleBomb10, purpleBomb9, purpleBomb8, purpleBomb7, purpleBomb6, purpleBomb5, purpleBomb4, purpleBomb3, purpleBomb2, purpleBomb1, purpleBomb0;
	public GameObject whiteBomb20, whiteBomb19, whiteBomb18, whiteBomb17, whiteBomb16, whiteBomb15, whiteBomb14, whiteBomb13, whiteBomb12, whiteBomb11, whiteBomb10, whiteBomb9, whiteBomb8, whiteBomb7, whiteBomb6, whiteBomb5, whiteBomb4, whiteBomb3, whiteBomb2, whiteBomb1, whiteBomb0;
	public GameObject boulder, steelBlock;
	public GameObject pauseButton;
	RockLevelBombHandler bombHandler;
	RockLevelJewelMovement jewelMovement;
	GameObject tempJewel;
	GameObject[,] jewelGrid = new GameObject[9, 9];
	bool offsetChanged, bombNeedsInstantiating; 
	bool levelEight, threeJewelLevel, level9, level14, level16, level17, level18, level23, level28, level29;
	bool cityLevelThree, cityLevelSix, cityLevelEight, cityLevelEleven, cityLevelTwelve, cityLevelThirteen, cityLevelSixteen, cityLevelSeventeen, cityLevelEighteen, cityLevelNineteen, cityLevelTwenty, cityLevelTwentyThree, cityLevelTwentyFour;
	bool cityLevelTwentyFive, cityLevelTwentySix, cityLevelTwentySeven, cityLevelTwentyEight, cityLevelTwentyNine, cityLevelThirty, jewelCollectorLevel, collectionJewelsLaunching, swappingJewels;
	float startingLeftJewelPosition, horizontalMultiplier, jewelCollectorDifficultyPercentage;
	int jewelCount, layerOffset;
	RockLevelDeleteJewels deleteJewels;
	PowerPercentageController powerPercentageController;

	void Awake () {
		jewelGrid = new GameObject[9, 9];
		jewelCount = 0;
		startingLeftJewelPosition = -2.45f;
		horizontalMultiplier = .6125f;
		layerOffset = 17;
		bombHandler = gameObject.GetComponent<RockLevelBombHandler> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		powerPercentageController = gameObject.GetComponent<PowerPercentageController> ();
		jewelCollectorDifficultyPercentage = .1f;
		if (GameObject.Find ("Jewel Collector") != null)
			jewelCollectorLevel = true;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	public void InstantiateMountainJewels (int levelNumber) {
		GameObject tempObject = GameObject.Find ("Instantiator");
		switch (levelNumber) {
		case 0: tempObject.GetComponent<LevelOneCreator> ().InstantiateJewels (); break;
		case 1: tempObject.GetComponent<LevelTwoCreator> ().InstantiateJewels (); break;
		case 2: tempObject.GetComponent<LevelThreeCreator> ().InstantiateJewels (); break;
		case 3: tempObject.GetComponent<LevelFourCreator> ().InstantiateJewels (); break;
		case 4: tempObject.GetComponent<LevelFiveCreator> ().InstantiateJewels (); break;
		case 5: tempObject.GetComponent<LevelSixCreator> ().InstantiateJewels (); break;
		case 6: tempObject.GetComponent<LevelSevenCreator> ().InstantiateJewels (); break;
		case 7: tempObject.GetComponent<LevelEightCreator> ().InstantiateJewels (); break;
		case 8: level9 = true; tempObject.GetComponent<LevelNineCreator> ().InstantiateJewels (); break;
		case 9: tempObject.GetComponent<LevelTenCreator> ().InstantiateJewels (); break;
		case 10: tempObject.GetComponent<LevelElevenCreator> ().InstantiateJewels (); break;
		case 11: tempObject.GetComponent<LevelTwelveCreator> ().InstantiateJewels (); break;
		case 12: tempObject.GetComponent<LevelThirteenCreator> ().InstantiateJewels (); break;
		case 13: level14 = true; tempObject.GetComponent<LevelFourteenCreator> ().InstantiateJewels (); break;
		case 14: tempObject.GetComponent<LevelFifteenCreator> ().InstantiateJewels (); break;
		case 15: level16 = true; tempObject.GetComponent<LevelSixteenCreator> ().InstantiateJewels (); break;
		case 16: level17 = true;tempObject.GetComponent<LevelSeventeenCreator> ().InstantiateJewels (); break;
		case 17: level18 = true;tempObject.GetComponent<LevelEighteenCreator> ().InstantiateJewels (); break;
		case 18: tempObject.GetComponent<LevelNineteenCreator> ().InstantiateJewels (); break;
		case 19: tempObject.GetComponent<LevelTwentyCreator> ().InstantiateJewels (); break;
		case 20: tempObject.GetComponent<LevelTwentyOneCreator> ().InstantiateJewels (); break;
		case 21: tempObject.GetComponent<LevelTwentyTwoCreator> ().InstantiateJewels (); break;
		case 22: level23 = true; tempObject.GetComponent<LevelTwentyThreeCreator> ().InstantiateJewels (); break;
		case 23: tempObject.GetComponent<LevelTwentyFourCreator> ().InstantiateJewels (); break;
		case 24: tempObject.GetComponent<LevelTwentyFiveCreator> ().InstantiateJewels (); break;
		case 25: tempObject.GetComponent<LevelTwentySixCreator> ().InstantiateJewels (); break;
		case 26: tempObject.GetComponent<LevelTwentySevenCreator> ().InstantiateJewels (); break;
		case 27: level28 = true; tempObject.GetComponent<LevelTwentyEightCreator> ().InstantiateJewels (); break;
		case 28: level29 = true; tempObject.GetComponent<LevelTwentyNineCreator> ().InstantiateJewels (); break;
		case 29: tempObject.GetComponent<LevelThirtyCreator> ().InstantiateJewels (); break;
		}
	}

	public void InstantiateCityJewels (int levelNumber) {
		GameObject tempObject = GameObject.Find ("Instantiator");
		switch (levelNumber) {
		case 0: tempObject.GetComponent<CityLevelOneCreator> ().InstantiateJewels (); break;
		case 1: tempObject.GetComponent<CityLevelTwoCreator> ().InstantiateJewels (); break;
		case 2: cityLevelThree = true; tempObject.GetComponent<CityLevelThreeCreator> ().InstantiateJewels (); break;
		case 3: tempObject.GetComponent<CityLevelFourCreator> ().InstantiateJewels (); break;
		case 4: tempObject.GetComponent<CityLevelFiveCreator> ().InstantiateJewels (); break;
		case 5: cityLevelSix = true; tempObject.GetComponent<CityLevelSixCreator> ().InstantiateJewels (); break;
		case 6: tempObject.GetComponent<CityLevelSevenCreator> ().InstantiateJewels (); break;
		case 7: cityLevelEight = true; tempObject.GetComponent<CityLevelEightCreator> ().InstantiateJewels (); break;
		case 8: tempObject.GetComponent<CityLevelNineCreator> ().InstantiateJewels (); break;
		case 9: tempObject.GetComponent<CityLevelTenCreator> ().InstantiateJewels (); break;
		case 10: cityLevelEleven = true; tempObject.GetComponent<CityLevelElevenCreator> ().InstantiateJewels (); break;
		case 11: cityLevelTwelve = true; tempObject.GetComponent<CityLevelTwelveCreator> ().InstantiateJewels (); break;
		case 12: cityLevelThirteen = true; tempObject.GetComponent<CityLevelThirteenCreator> ().InstantiateJewels (); break;
		case 13: tempObject.GetComponent<CityLevelFourteenCreator> ().InstantiateJewels (); break;
		case 14: tempObject.GetComponent<CityLevelFifteenCreator> ().InstantiateJewels (); break;
		case 15: cityLevelSixteen = true; tempObject.GetComponent<CityLevelSixteenCreator> ().InstantiateJewels (); break;
		case 16: cityLevelSeventeen = true; tempObject.GetComponent<CityLevelSeventeenCreator> ().InstantiateJewels (); break;
		case 17: cityLevelEighteen = true; tempObject.GetComponent<CityLevelEighteenCreator> ().InstantiateJewels (); break;
		case 18: cityLevelNineteen = true; tempObject.GetComponent<CityLevelNineteenCreator> ().InstantiateJewels (); break;
		case 19: cityLevelTwenty = true; tempObject.GetComponent<CityLevelTwentyCreator> ().InstantiateJewels (); break;
		case 20: tempObject.GetComponent<CityLevelTwentyOneCreator> ().InstantiateJewels (); break;
		case 21: tempObject.GetComponent<CityLevelTwentyTwoCreator> ().InstantiateJewels (); break;
		case 22: cityLevelTwentyThree = true; tempObject.GetComponent<CityLevelTwentyThreeCreator> ().InstantiateJewels (); break;
		case 23: cityLevelTwentyFour = true; tempObject.GetComponent<CityLevelTwentyFourCreator> ().InstantiateJewels (); break;
		case 24: cityLevelTwentyFive = true; tempObject.GetComponent<CityLevelTwentyFiveCreator> ().InstantiateJewels (); break;
		case 25: cityLevelTwentySix = true; tempObject.GetComponent<CityLevelTwentySixCreator> ().InstantiateJewels (); break;
		case 26: cityLevelTwentySeven = true; tempObject.GetComponent<CityLevelTwentySevenCreator> ().InstantiateJewels (); break;
		case 27: cityLevelTwentyEight = true; tempObject.GetComponent<CityLevelTwentyEightCreator> ().InstantiateJewels (); break;
		case 28: cityLevelTwentyNine = true; tempObject.GetComponent<CityLevelTwentyNineCreator> ().InstantiateJewels (); break;
		case 29: cityLevelThirty = true; tempObject.GetComponent<CityLevelThirtyCreator> ().InstantiateJewels (); break;
		}
	}

	public void InstantiateCabinJewels (int levelNumber) {
		GameObject tempObject = GameObject.Find ("Instantiator");
		Debug.Log ("LEVEL NUMBER = " + levelNumber);
		switch (levelNumber) {
		case 0: tempObject.GetComponent<CabinLevelOneCreator> ().InstantiateJewels (); break;
		case 1: tempObject.GetComponent<CabinLevelTwoCreator> ().InstantiateJewels (); break;
		case 2: tempObject.GetComponent<CabinLevelThreeCreator> ().InstantiateJewels (); break;
		case 3: tempObject.GetComponent<CabinLevelFourCreator> ().InstantiateJewels (); break;
		case 4: tempObject.GetComponent<CabinLevelFiveCreator> ().InstantiateJewels (); break;
		case 5: tempObject.GetComponent<CabinLevelSixCreator> ().InstantiateJewels (); break;
		case 6: tempObject.GetComponent<CabinLevelSevenCreator> ().InstantiateJewels (); break;
		case 7: tempObject.GetComponent<CabinLevelEightCreator> ().InstantiateJewels (); break;
		case 8: tempObject.GetComponent<CabinLevelNineCreator> ().InstantiateJewels (); break;
		case 9: tempObject.GetComponent<CabinLevelTenCreator> ().InstantiateJewels (); break;
		case 10: tempObject.GetComponent<CabinLevelElevenCreator> ().InstantiateJewels (); break;
		case 11: tempObject.GetComponent<CabinLevelTwelveCreator> ().InstantiateJewels (); break;
		case 12: tempObject.GetComponent<CabinLevelThirteenCreator> ().InstantiateJewels (); break;
		case 13: tempObject.GetComponent<CabinLevelFourteenCreator> ().InstantiateJewels (); break;
		case 14: tempObject.GetComponent<CabinLevelFifteenCreator> ().InstantiateJewels (); break;
		case 15: tempObject.GetComponent<CabinLevelSixteenCreator> ().InstantiateJewels (); break;
		case 16: tempObject.GetComponent<CabinLevelSeventeenCreator> ().InstantiateJewels (); break;
		case 17: tempObject.GetComponent<CabinLevelEighteenCreator> ().InstantiateJewels (); break;
		case 18: tempObject.GetComponent<CabinLevelNineteenCreator> ().InstantiateJewels (); break;
		case 19: tempObject.GetComponent<CabinLevelTwentyCreator> ().InstantiateJewels (); break;
		case 20: tempObject.GetComponent<CabinLevelTwentyOneCreator> ().InstantiateJewels (); break;
		case 21: tempObject.GetComponent<CabinLevelTwentyTwoCreator> ().InstantiateJewels (); break;
		case 22: tempObject.GetComponent<CabinLevelTwentyThreeCreator> ().InstantiateJewels (); break;
		case 23: tempObject.GetComponent<CabinLevelTwentyFourCreator> ().InstantiateJewels (); break;
		case 24: tempObject.GetComponent<CabinLevelTwentyFiveCreator> ().InstantiateJewels (); break;
		case 25: tempObject.GetComponent<CabinLevelTwentySixCreator> ().InstantiateJewels (); break;
		case 26: tempObject.GetComponent<CabinLevelTwentySevenCreator> ().InstantiateJewels (); break;
		case 27: tempObject.GetComponent<CabinLevelTwentyEightCreator> ().InstantiateJewels (); break;
		case 28: tempObject.GetComponent<CabinLevelTwentyNineCreator> ().InstantiateJewels (); break;
		case 29: tempObject.GetComponent<CabinLevelThirtyCreator> ().InstantiateJewels (); break;
		}
	}

	public void InstantiateLaunchpadJewels (int levelNumber) {
		GameObject tempObject = GameObject.Find ("Instantiator");
		Debug.Log ("LEVEL NUMBER = " + levelNumber);
		switch (levelNumber) {
		case 0: tempObject.GetComponent<LaunchpadLevelOneCreator> ().InstantiateJewels (); break;
		case 1: tempObject.GetComponent<LaunchpadLevelTwoCreator> ().InstantiateJewels (); break;
		case 2: tempObject.GetComponent<LaunchpadLevelThreeCreator> ().InstantiateJewels (); break;
		case 3: tempObject.GetComponent<LaunchpadLevelFourCreator> ().InstantiateJewels (); break;
		case 4: tempObject.GetComponent<LaunchpadLevelFiveCreator> ().InstantiateJewels (); break;
		case 5: tempObject.GetComponent<LaunchpadLevelSixCreator> ().InstantiateJewels (); break;
		case 6: tempObject.GetComponent<LaunchpadLevelSevenCreator> ().InstantiateJewels (); break;
		case 7: tempObject.GetComponent<LaunchpadLevelEightCreator> ().InstantiateJewels (); break;
		case 8: tempObject.GetComponent<LaunchpadLevelNineCreator> ().InstantiateJewels (); break;
		case 9: tempObject.GetComponent<LaunchpadLevelTenCreator> ().InstantiateJewels (); break;
		case 10: tempObject.GetComponent<LaunchpadLevelElevenCreator> ().InstantiateJewels (); break;
		case 11: tempObject.GetComponent<LaunchpadLevelTwelveCreator> ().InstantiateJewels (); break;
		case 12: tempObject.GetComponent<LaunchpadLevelThirteenCreator> ().InstantiateJewels (); break;
		case 13: tempObject.GetComponent<LaunchpadLevelFourteenCreator> ().InstantiateJewels (); break;
		case 14: tempObject.GetComponent<LaunchpadLevelFifteenCreator> ().InstantiateJewels (); break;
		case 15: tempObject.GetComponent<LaunchpadLevelSixteenCreator> ().InstantiateJewels (); break;
		case 16: tempObject.GetComponent<LaunchpadLevelSeventeenCreator> ().InstantiateJewels (); break;
		case 17: tempObject.GetComponent<LaunchpadLevelEighteenCreator> ().InstantiateJewels (); break;
		case 18: tempObject.GetComponent<LaunchpadLevelNineteenCreator> ().InstantiateJewels (); break;
		case 19: tempObject.GetComponent<LaunchpadLevelTwentyCreator> ().InstantiateJewels (); break;
		case 20: tempObject.GetComponent<LaunchpadLevelTwentyOneCreator> ().InstantiateJewels (); break;
		case 21: tempObject.GetComponent<LaunchpadLevelTwentyTwoCreator> ().InstantiateJewels (); break;
		case 22: tempObject.GetComponent<LaunchpadLevelTwentyThreeCreator> ().InstantiateJewels (); break;
		case 23: tempObject.GetComponent<LaunchpadLevelTwentyFourCreator> ().InstantiateJewels (); break;
		case 24: tempObject.GetComponent<LaunchpadLevelTwentyFiveCreator> ().InstantiateJewels (); break;
		case 25: tempObject.GetComponent<LaunchpadLevelTwentySixCreator> ().InstantiateJewels (); break;
		case 26: tempObject.GetComponent<LaunchpadLevelTwentySevenCreator> ().InstantiateJewels (); break;
		case 27: tempObject.GetComponent<LaunchpadLevelTwentyEightCreator> ().InstantiateJewels (); break;
		case 28: tempObject.GetComponent<LaunchpadLevelTwentyNineCreator> ().InstantiateJewels (); break;
		case 29: tempObject.GetComponent<LaunchpadLevelThirtyCreator> ().InstantiateJewels (); break;
		}
	}

	public void InstantiateJewels () {
		levelEight = true;
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (Bomb (GetLevelEightJewels (jewelCount))) {
					//					horizontalMultiplier = .63f;
					startingLeftJewelPosition = -2.415f;
					offsetChanged = true;
				}
				else if (offsetChanged) {
					//					horizontalMultiplier = .6125f;
					startingLeftJewelPosition = -2.45f;
					offsetChanged = false;
				}
				if (GetLevelEightJewels (jewelCount).tag == "Boulder") {
					jewelGrid[i, j] = (GameObject)Instantiate(GetLevelEightJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 12), Quaternion.identity);
				} else if (GetLevelEightJewels (jewelCount).tag == "Steel Block") {
					jewelGrid[i, j] = (GameObject)Instantiate(GetLevelEightJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), -1.5f), Quaternion.identity);
				}
				else if (Bomb (GetLevelEightJewels (jewelCount))) {
					jewelGrid[i, j] = (GameObject)Instantiate(GetLevelEightJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 12), Quaternion.identity);
					bombHandler.AddBombToList (jewelGrid[i, j]);
				}
				else
					jewelGrid[i, j] = (GameObject)Instantiate(GetLevelEightJewels (jewelCount), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2), Quaternion.identity);
				
				if (!Bomb (jewelGrid [i, j])) {
					while ((j > 1 && jewelGrid[i, j].name != "Rock Blocked(Clone)" && JewelsAreTheSame (jewelGrid[i, j - 1].tag, jewelGrid[i, j].tag) && JewelsAreTheSame (jewelGrid[i, j - 2].tag, jewelGrid[i, j].tag)) || 
					       (i > 1 && jewelGrid[i, j].name != "Rock Blocked(Clone)" && JewelsAreTheSame (jewelGrid[i - 1, j].tag, jewelGrid[i, j].tag) && JewelsAreTheSame (jewelGrid[i - 2, j].tag, jewelGrid[i, j].tag))) {
						Destroy (jewelGrid[i, j]);
						jewelGrid[i, j] = (GameObject) Instantiate (GetBombNumberTwenty (), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2), Quaternion.identity);
					}
				} else if (Bomb (jewelGrid[i, j])) {
					startingLeftJewelPosition = -2.415f;
					offsetChanged = true;
					while ((j > 1 && JewelsAreTheSame (jewelGrid[i, j - 1].tag, jewelGrid[i, j].tag) && JewelsAreTheSame (jewelGrid[i, j - 2].tag, jewelGrid[i, j].tag)) || 
					       (i > 1 && JewelsAreTheSame (jewelGrid[i - 1, j].tag, jewelGrid[i, j].tag) && JewelsAreTheSame (jewelGrid[i - 2, j].tag, jewelGrid[i, j].tag))) {
						LevelTwoBombInfo bombInfo = jewelGrid[i, j].GetComponent<LevelTwoBombInfo> ();
						int bombNumber = bombInfo.GetBombNumber ();
						bombHandler.RemoveFromBombList (jewelGrid[i, j]);
						Destroy (jewelGrid[i, j]);
						//Debug.Log ("bombNumber = " + bombNumber);
						jewelGrid[i, j] = (GameObject) Instantiate (GetCorrectBomb (bombNumber), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) -2), Quaternion.identity);
						//Debug.Log ("jewelGrid[i, j] = " + jewelGrid[i, j].name);
						bombHandler.AddBombToList (jewelGrid[i, j]);
					}
				}
				
				//				if (jewelGrid[i, j].tag == "Green Bomb") { 
				//					bombHandler.AddBombToList (jewelGrid[i, j]);
				//				}
				jewelGrid[i, j].layer = i + layerOffset;
				jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
				jewelMovement.SetRow (i);
				jewelMovement.SetCol (j);
				AddToParent (jewelGrid[i, j], i); 
				//				if (jewelCount == 60 || jewelCount == 61)
				//					jewelGrid[i, j].tag = "Tutorial 1 Jewel";
				jewelCount++;
			}
		}
	}

	public void AddToJewelGrid (GameObject newAdd, int row, int col) {
		jewelGrid[row, col] = newAdd;
	}

	bool Bomb (GameObject possibleBomb) {
		if (possibleBomb.tag == "Blue Bomb" || possibleBomb.tag == "Red Bomb" || possibleBomb.tag == "Green Bomb" || possibleBomb.tag == "Purple Bomb" || possibleBomb.tag == "Orange Bomb" || possibleBomb.tag == "White Bomb") {
			return true;
		}
		return false;
	}

	public void SetJewelGrid (GameObject[,] newGrid) {
		jewelGrid = newGrid;
	}

	public void InstantiateLevelFourTutorialJewels (int destructionRow, int j) {
		if (PlayerPrefs.HasKey ("Level Four Tutorial Played")) {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		GameObject tempJewel = null;
		bool steelOrBoulder = false;
		int deleteNumber = GameObject.Find ("Instantiator").GetComponent<CabinLevelTwoCreator> ().GetDeleteNumber ();

		if (deleteNumber < 3 && destructionRow == 1 && j == 1) {
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (deleteNumber < 3 && destructionRow == 1 && j == 2) {
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (deleteNumber < 3 && destructionRow == 1 && j == 3) {
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		if (deleteNumber == 2) {
			GameObject.Find ("Tutorial 6 Shade 1").GetComponent<CabinLevelTwoShadeOneController> ().LightenShade ();
		}

		int i = 0;
		while (i < 9) {
			if (jewelGrid[i, j] == null)
				break;
			else
				i++;
		}
		jewelGrid[i, j] = tempJewel;
		jewelGrid[i, j].layer = i + layerOffset;
		jewelGrid[i, j].transform.Translate (new Vector3 (0, 0, -i));
		jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetRow (i);
		jewelMovement.SetCol (j);
		AddToParent (jewelGrid[i, j], i);
		jewelCount++;
	}

	public void InstantiateLevelThreeTutorialJewels (int destructionRow, int j) {

		if (PlayerPrefs.HasKey ("Level Three Tutorial Played")) {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		GameObject tempJewel = null;
		bool steelOrBoulder = false;
		int deleteNumber = GameObject.Find ("Instantiator").GetComponent<LevelThreeCreator> ().GetDeleteNumber ();

		if (deleteNumber < 3 && destructionRow == 3 && j == 6) {
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (deleteNumber < 3 && destructionRow == 4 && j == 6) {
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (deleteNumber < 3 && destructionRow == 5 && j == 6) {
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		if (deleteNumber == 2) {
			GameObject.Find ("Tutorial 3 Shade 1").GetComponent<LevelThreeShadeOneController> ().LightenShade ();
			PlayerPrefs.SetInt ("Level Three Tutorial Played", 1);
		}

		int i = 0;
		while (i < 9) {
			if (jewelGrid[i, j] == null) 
				break;
			else 
				i++;
		}
		jewelGrid[i, j] = tempJewel;
		jewelGrid[i, j].layer = i + layerOffset;
		jewelGrid[i, j].transform.Translate (new Vector3 (0, 0, -i));
		jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetRow (i);
		jewelMovement.SetCol (j);
		AddToParent (jewelGrid[i, j], i);
		jewelCount++;
	}

	public void InstantiateLevelTwoTutorialJewels (int destructionRow, int j) {

		if (PlayerPrefs.HasKey ("Level Two Tutorial Played")) {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		GameObject tempJewel = null;
		bool steelOrBoulder = false;
		int deleteNumber = GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().GetDeleteNumber ();

		if (deleteNumber == 0) 
			Time.timeScale = .25f;
		else if (deleteNumber == 8) {
			Time.timeScale = .25f;

		} else if (deleteNumber == 21) {
			Time.timeScale = .25f;
		} else if (deleteNumber == 37) {
			PlayerPrefs.SetInt ("Level Two Tutorial Played", 1);
			GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ().SetTutorialTwo (false);
			Time.timeScale = 1f;
		}

		if (deleteNumber < 4 && destructionRow == 2 && j == 1) 
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 4 && destructionRow == 2 && j == 2) 
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 4 && destructionRow == 2 && j == 3) 
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 4 && destructionRow == 2 && j == 4) 
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 8 && destructionRow == 1 && j == 5)
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 8 && destructionRow == 5 && j == 4)
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 8 && destructionRow == 6 && j == 2)
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 8 && destructionRow == 7 && j == 1) 
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 13 && destructionRow == 5 && j == 4) 
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 13 && destructionRow == 5 && j == 5)
			tempJewel = (GameObject)Instantiate (orangeJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 13 && destructionRow == 5 && j == 6)
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 13 && destructionRow == 6 && j == 4)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 13 && destructionRow == 7 && j == 4)
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);  
		else if (deleteNumber < 21 && destructionRow == 5 && j == 4)
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 6 && j == 4)
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 7 && j == 4)
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 8 && j == 4)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 5 && j == 5)
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 5 && j == 6)
			tempJewel = (GameObject)Instantiate (orangeJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 5 && j == 7)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 21 && destructionRow == 5 && j == 8)
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		else if (deleteNumber < 26 && destructionRow == 7 && j == 6)
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 26 && destructionRow == 6 && j == 6)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 26 && destructionRow == 5 && j == 6)
			tempJewel = (GameObject)Instantiate (orangeJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 26 && destructionRow == 4 && j == 6)
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 26 && destructionRow == 3 && j == 6)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 1 && j == 8)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 2 && j == 2)
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 3 && j == 2)
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 4 && j == 3)
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 5 && j == 1)
			tempJewel = (GameObject)Instantiate (orangeJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 6 && j == 7)
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 7 && j == 5)
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 8 && j == 3)
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 7 && j == 7)
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 6 && j == 2)
			tempJewel = (GameObject)Instantiate (orangeJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 5 && j == 4)
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (deleteNumber < 38 && destructionRow == 4 && j == 2) {
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier *j), Random.Range (7, 10), -2), Quaternion.identity);
		}
		else {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		if (deleteNumber == 3) {
			jewelGrid[2,2].transform.position = new Vector3 (jewelGrid[2,2].transform.position.x, jewelGrid[2,2].transform.position.y, -4); 
			GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().InstantiateSloMo ();
			GameObject.Find ("Tutorial 2 Shade 1").GetComponent<LevelTwoShadeOneController> ().LightenShade (); 
		}

		else if (deleteNumber == 7) {
			GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().GetRidOfSloMo ();
			Time.timeScale = 1; 
			jewelGrid [4,4].name = "Level Two Tutorial Jewel";
			jewelGrid [5,4].name = "Level Two Tutorial Jewel";
		}

		else if (deleteNumber == 12) {
			GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().InstantiateSloMo ();
			GameObject.Find ("Tutorial 2 Shade 2(Clone)").GetComponent<LevelTwoShadeTwoController> ().LightenShade ();
		}

		else if (deleteNumber == 20) {
			Time.timeScale = 1; 
			GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().GetRidOfSloMo ();
			jewelGrid [5,6].name = "Level Two Tutorial Jewel";
			jewelGrid [5,7].name = "Level Two Tutorial Jewel";
		}

		else if (deleteNumber == 25) {
			GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().InstantiateSloMo ();
			GameObject.Find ("Tutorial 2 Shade 3(Clone)").GetComponent<LevelTwoShadeThreeController> ().LightenShade ();
		}

//		else if (deleteNumber == 25) {
//			Time.timeScale = 1;
//			GameObject.Find ("Instantiator").GetComponent<LevelTwoCreator> ().GetRidOfSloMo ();
//		}

		int i = 0;
		while (i < 9) {
			if (jewelGrid[i, j] == null) 
				break;
			else 
				i++;
		}
		jewelGrid[i, j] = tempJewel;
		jewelGrid[i, j].layer = i + layerOffset;
		jewelGrid[i, j].transform.Translate (new Vector3 (0, 0, -i));
		jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetRow (i);
		jewelMovement.SetCol (j);
		AddToParent (jewelGrid[i, j], i);
		jewelCount++;
	}

	public void InstantiateLevelOneTutorialJewels (int destructionRow, int j) {

		if (PlayerPrefs.HasKey ("Level One Tutorial Played")) {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		GameObject tempJewel = null;
		bool steelOrBoulder = false;
		int deleteNumber = GameObject.Find ("Instantiator").GetComponent<LevelOneCreator> ().GetDeleteNumber ();

		if (deleteNumber < 3 && destructionRow == 2 && j == 3) {
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 3 && destructionRow == 3 && j == 3) {
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 3 && destructionRow == 4 && j == 3) {
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 6 && destructionRow == 4 && j == 2) {
			tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 6 && destructionRow == 4 && j == 3) {
			tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 6 && destructionRow == 4 && j == 4) {
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 9 && destructionRow == 0 && j == 3) {
			tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 9 && destructionRow == 1 && j == 3) {
			tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		} else if (deleteNumber < 9 && destructionRow == 2 && j == 3) {
			tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
		}
		else {
			InstantiateSingleJewels (destructionRow, j);
			return;
		}

		if (deleteNumber == 2) {
			GameObject.Find ("Tutorial Shade 1").GetComponent<LevelOneShadeOneController> ().LightenShade (); 
		}

		else if (deleteNumber == 5) {
			GameObject.Find ("Tutorial Shade 2(Clone)").GetComponent<LevelOneShadeTwoController> ().LightenShade (); 
			jewelGrid[4,3].transform.position = new Vector3 (jewelGrid[4,3].transform.position.x, jewelGrid[4,3].transform.position.y, -6); 
		}

		else if (deleteNumber == 8) {
			GameObject.Find ("Tutorial Shade 3(Clone)").GetComponent<LevelOneShadeThreeController> ().LightenShade (); 
			jewelGrid[2,2].transform.position = new Vector3 (jewelGrid[2,2].transform.position.x, jewelGrid[2,2].transform.position.y, -4);
			PlayerPrefs.SetInt ("Level One Tutorial Played", 1);
		}

//		switch (GameObject.Find ("Instantiator").GetComponent<LevelOneCreator> ().GetDeleteNumber ()) {
//		case 0: tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); break;
//		case 1: tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); break;
//		case 2: tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
//			GameObject.Find ("Tutorial Shade 1").GetComponent<LevelOneShadeOneController> ().LightenShade (); break;
//		case 3: tempJewel = (GameObject)Instantiate (greenJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); break;
//		case 4: tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); break;
//		case 5: tempJewel = (GameObject)Instantiate (blueJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
//			GameObject.Find ("Tutorial Shade 2(Clone)").GetComponent<LevelOneShadeTwoController> ().LightenShade (); 
//			jewelGrid[4,3].transform.position = new Vector3 (jewelGrid[4,3].transform.position.x, jewelGrid[4,3].transform.position.y, -6); break;
//		case 6: tempJewel = (GameObject)Instantiate (redJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); break;
//		case 7: tempJewel = (GameObject)Instantiate (whiteJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); break;
//		case 8: tempJewel = (GameObject)Instantiate (purpleJewel, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity); 
//			GameObject.Find ("Tutorial Shade 3(Clone)").GetComponent<LevelOneShadeThreeController> ().LightenShade (); 
//			jewelGrid[2,2].transform.position = new Vector3 (jewelGrid[2,2].transform.position.x, jewelGrid[2,2].transform.position.y, -4); break;
//		default: InstantiateSingleJewels (destructionRow, j); return;
//		}
		int i = 0;
		while (i < 9) {
			if (jewelGrid[i, j] == null)
				break;
			else 
				i++;
		}
		jewelGrid[i, j] = tempJewel;
		jewelGrid[i, j].layer = i + layerOffset;
		jewelGrid[i, j].transform.Translate (new Vector3 (0, 0, -i));
		jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetRow (i);
		jewelMovement.SetCol (j);
		AddToParent (jewelGrid[i, j], i);
		jewelCount++;
	}

	public int InstantiateSingleJewels (int destructionRow, int j) {
		GameObject tempJewel;
		bool steelOrBoulder = false;
		if (threeJewelLevel) {
			tempJewel = (GameObject)Instantiate(GetRandomWhiteBlueGreenAndOrangeJewel (), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (level9) {
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 4, redJewel, whiteJewel, purpleJewel, blueJewel, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (level14 || level16) {
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 5,  redJewel, whiteJewel, purpleJewel, blueJewel, greenJewel, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (level17 || level23 || level28) {
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 4, orangeJewel, whiteJewel, blueJewel, greenJewel, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		} else if (level23) 
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 3, whiteJewel, blueJewel, greenJewel, null, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (level29 || cityLevelThree || cityLevelSix)
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 5, whiteJewel, blueJewel, greenJewel, orangeJewel, purpleJewel, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (cityLevelEight) 
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 3, redJewel, whiteJewel, greenJewel, null, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (cityLevelEleven || cityLevelTwelve || cityLevelThirteen || cityLevelSixteen || cityLevelSeventeen || cityLevelEighteen || cityLevelTwentyFour || cityLevelTwentyEight)
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 5, redJewel, whiteJewel, greenJewel, orangeJewel, blueJewel, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else if (cityLevelNineteen || cityLevelTwenty || cityLevelTwentyThree || cityLevelTwentyFive || cityLevelTwentySix || cityLevelTwentySeven || cityLevelTwentyNine || cityLevelThirty) 
			tempJewel = (GameObject)Instantiate (GetRandomJewel (j, 4, redJewel, whiteJewel, greenJewel, blueJewel, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		else 
			tempJewel = (GameObject)Instantiate(GetRandomJewel (j), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), -2), Quaternion.identity);
		int i = 0;
//		while (i < destructionRow && (jewelGrid[i, j].tag == "Steel Block" || jewelGrid[i, j].tag == "Boulder" || IsBomb (jewelGrid[i, j].tag))) {
//			if (jewelGrid[i, j].tag == "Steel Block" || jewelGrid[i, j].tag == "Boulder" || IsBomb (jewelGrid[i, j].tag))
//				steelOrBoulder = true;
//			i++;
//		}
//		if (!steelOrBoulder)
//			i = 0;
		while (i < 9) {
			if (jewelGrid[i, j] == null)
				break;
			else 
				i++;
		}
		jewelGrid[i, j] = tempJewel;
		jewelGrid[i, j].layer = i + layerOffset;
		jewelGrid[i, j].transform.Translate (new Vector3 (0, 0, -i));
		jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetRow (i);
		jewelMovement.SetCol (j);
		AddToParent (jewelGrid[i, j], i);
		jewelCount++;
		return i;
	}

	public void InstantiateShuffledJewels (int i, int j) {
		if (threeJewelLevel)
			jewelGrid[i, j] = (GameObject)Instantiate(GetRandomWhiteBlueGreenAndOrangeJewel (), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) - 2), Quaternion.identity);
		else if (level9) {
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 4, redJewel, whiteJewel, purpleJewel, blueJewel, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) -2), Quaternion.identity);
		} else if (level14 || level16) {
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 5, redJewel, whiteJewel, purpleJewel, blueJewel, greenJewel, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) - 2), Quaternion.identity);
		} else if (level17 || level23 || level28) {
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 4, orangeJewel, whiteJewel, blueJewel, greenJewel, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) - 2), Quaternion.identity);
		} else if (level23) {
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 3, whiteJewel, blueJewel, greenJewel, null, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) -2), Quaternion.identity);
		} else if (level29 || cityLevelThree || cityLevelSix) {
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 5, whiteJewel, blueJewel, greenJewel, orangeJewel, purpleJewel, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) -2), Quaternion.identity);
		} else if (cityLevelEight)
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 3, redJewel, whiteJewel, greenJewel, null, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) -2), Quaternion.identity);
		else if (cityLevelEleven || cityLevelTwelve || cityLevelThirteen || cityLevelSixteen || cityLevelSeventeen || cityLevelEighteen || cityLevelTwentyFour || cityLevelTwentyEight)
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 5, redJewel, whiteJewel, greenJewel, orangeJewel, blueJewel, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) -2), Quaternion.identity);
		else if (cityLevelNineteen || cityLevelTwenty || cityLevelTwentyThree || cityLevelTwentyFive || cityLevelTwentySix || cityLevelTwentySeven || cityLevelTwentyNine || cityLevelThirty) 
			jewelGrid[i, j] = (GameObject)Instantiate (GetRandomJewel (j, 4, redJewel, whiteJewel, greenJewel, blueJewel, null, null), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) -2), Quaternion.identity);
		else
			jewelGrid[i, j] = (GameObject)Instantiate(GetRandomJewel (j), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (7, 10), (-1 * i) - 2), Quaternion.identity);

		jewelGrid[i, j].layer = i + layerOffset;
		jewelMovement = jewelGrid[i, j].GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetRow (i);
		jewelMovement.SetCol (j);
		AddToParent (jewelGrid[i, j], i);
	}

	private bool IsBomb (string bombTag) {
		return (bombTag == "Red Bomb" || bombTag == "Green Bomb" || bombTag == "Blue Bomb" || bombTag == "White Bomb" || bombTag == "Purple Bomb" || bombTag == "Orange Bomb");
	}
	
	public GameObject GetJewelGridGameObject (int row, int col) {
		if (row > 8 || row < 0 || col > 8 || col < 0)
			return null;
		return jewelGrid[row, col];
	}
	
	public void SetJewelGridGameObject (int row, int col, GameObject jewel) {
		jewelGrid [row, col] = jewel;
	}

	public void SetThreeJewelLevel () {
		this.threeJewelLevel = true;
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

	GameObject GetCorrectBomb (int bombNumber) {
		switch (bombNumber) {
		case 1: return GetBombNumberOne ();
		case 2: return GetBombNumberTwo ();
		case 3: return GetBombNumberThree ();
		case 4: return GetBombNumberFour ();
		case 5: return GetBombNumberFive ();
		case 6: return GetBombNumberSix ();
		case 7: return GetBombNumberSeven ();
		case 8: return GetBombNumberEight ();
		case 9: return GetBombNumberNine ();
		case 10: return GetBombNumberTen ();
		case 11: return GetBombNumberEleven ();
		case 12: return GetBombNumberTwelve ();
		case 13: return GetBombNumberThirteen ();
		case 14: return GetBombNumberFourteen ();
		case 15: return GetBombNumberFifteen ();
		case 16: return GetBombNumberSixteen ();
		case 17: return GetBombNumberSeventeen ();
		case 18: return GetBombNumberEighteen ();
		case 19: return GetBombNumberNinteen ();
		case 20: return GetBombNumberTwenty ();
		}
		return null;
	}

	string GetRandomJewelString () {
		switch (Random.Range (0, 6)) {
		case 0: return "Blue Block";
		case 1: return "Green Block";
		case 2: return "Yellow Block";
		case 3: return "Purple Block";
		case 4: return "Red Block";
		case 5: return "White Block";
		}
		return "";
	}

	GameObject GetRandomJewelForCollectionLevel (int col) {
		GameObject[] jewelArray = new GameObject[6];
		int i = 0;
		bool withinPercentage = false;
		JewelCollectorController jewelCollectorController = GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ();
		if (Random.Range (0f, 1f) > jewelCollectorDifficultyPercentage) {
			if (jewelCollectorController.GetCollectorOneFinished ()) {
				jewelArray[i] = whiteJewel;
				i++;
			}
			if (jewelCollectorController.GetCollectorTwoFinished ()) {
				jewelArray[i] = redJewel;
				i++;
			}
			if (jewelCollectorController.GetCollectorThreeFinished ()) {
				jewelArray[i] = greenJewel;
				i++;
			}
			if (jewelCollectorController.GetCollectorFourFinished ()) {
				jewelArray[i] = blueJewel;
				i++;
			}
			if (jewelCollectorController.GetCollectorFiveFinished ()) {
				jewelArray[i] = purpleJewel;
				i++;
			}
			if (jewelCollectorController.GetCollectorSixFinished ()) {
				jewelArray[i] = orangeJewel;
				i++;
			}
		} else {
			withinPercentage = true;
			if (!jewelCollectorController.GetCollectorOneFinished ()) {
				jewelArray[i] = whiteJewel;
				i++;
			}
			if (!jewelCollectorController.GetCollectorTwoFinished ()) {
				jewelArray[i] = redJewel;
				i++;
			}
			if (!jewelCollectorController.GetCollectorThreeFinished ()) {
				jewelArray[i] = greenJewel;
				i++;
			}
			if (!jewelCollectorController.GetCollectorFourFinished ()) {
				jewelArray[i] = blueJewel;
				i++;
			}
			if (!jewelCollectorController.GetCollectorFiveFinished ()) {
				jewelArray[i] = purpleJewel;
				i++;
			}
			if (!jewelCollectorController.GetCollectorSixFinished ()) {
				jewelArray[i] = orangeJewel;
				i++;
			}
		}

		if (!withinPercentage && i > 3) {
			return jewelArray[Random.Range (0, i)];
		} else if (withinPercentage) {
			return jewelArray[Random.Range (0, i)];
		} else {
			switch (Random.Range (0, 6)) {
			case 0: return greenJewel;
			case 1: return blueJewel;
			case 2: return orangeJewel;
			case 3: return purpleJewel;
			case 4: return redJewel;
			case 5: return whiteJewel;
			}
		}

//		int blueJewelCount = 0, greenJewelCount = 0, orangeJewelCount = 0, purpleJewelCount = 0, redJewelCount = 0, whiteJewelCount = 0;
//		string lowestNumberJewel = GetRandomJewelString ();
//		for (int i = 0; i < 3; i++) {
//
//			if (col - 1 >= 0 && jewelGrid[i, col - 1] != null) {
//				switch (jewelGrid[i, col - 1].tag) {
//				case "Blue Block": blueJewelCount++; break;
//				case "Green Block": greenJewelCount++; break;
//				case "Yellow Block": orangeJewelCount++; break;
//				case "Purple Block": purpleJewelCount++; break;
//				case "Red Block": redJewelCount++; break;
//				case "White Block": whiteJewelCount++; break;
//				} 
//			}
//
//			if (jewelGrid[i, col] != null) {
//				switch (jewelGrid[i, col].tag) {
//				case "Blue Block": blueJewelCount++; break;
//				case "Green Block": greenJewelCount++; break;
//				case "Yellow Block": orangeJewelCount++; break;
//				case "Purple Block": purpleJewelCount++; break;
//				case "Red Block": redJewelCount++; break;
//				case "White Block": whiteJewelCount++; break;
//				} 
//			}
//
//			if (col + 1 < 9 && jewelGrid[i, col + 1] != null) {
//				switch (jewelGrid[i, col + 1].tag) {
//				case "Blue Block": blueJewelCount++; break;
//				case "Green Block": greenJewelCount++; break;
//				case "Yellow Block": orangeJewelCount++; break;
//				case "Purple Block": purpleJewelCount++; break;
//				case "Red Block": redJewelCount++; break;
//				case "White Block": whiteJewelCount++; break;
//				} 
//			}
//		}
//
//		// Getting Least Common Jewel
//		if (Random.Range (0f, 1f) > jewelCollectorDifficultyPercentage) {
//			if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) > 
//			    GetJewelCountNumberFromString ("Blue Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount)) 
//				lowestNumberJewel = "Blue Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) >
//			         GetJewelCountNumberFromString ("Green Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount)) 
//				lowestNumberJewel = "Green Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) >
//			         GetJewelCountNumberFromString ("Yellow Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Yellow Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) >
//			         GetJewelCountNumberFromString ("Purple Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Purple Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) >
//			         GetJewelCountNumberFromString ("Red Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Red Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) >
//			         GetJewelCountNumberFromString ("White Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "White Block";
//		} 
//		// Getting Most Common Jewel
//		else {
//			if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) < 
//			    GetJewelCountNumberFromString ("Blue Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Blue Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) <
//			         GetJewelCountNumberFromString ("Green Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Green Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) <
//			         GetJewelCountNumberFromString ("Yellow Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Yellow Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) <
//			         GetJewelCountNumberFromString ("Purple Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Purple Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) <
//			         GetJewelCountNumberFromString ("Red Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "Red Block";
//			else if (GetJewelCountNumberFromString (lowestNumberJewel, blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount) <
//			         GetJewelCountNumberFromString ("White Block", blueJewelCount, greenJewelCount, orangeJewelCount, purpleJewelCount, redJewelCount, whiteJewelCount))
//				lowestNumberJewel = "White Block";
//		}
//
//		switch (lowestNumberJewel) {
//		case "Blue Block": return blueJewel;
//		case "Green Block": return greenJewel;
//		case "Yellow Block": return orangeJewel;
//		case "Purple Block": return purpleJewel;
//		case "Red Block": return redJewel;
//		case "White Block": return whiteJewel;
//		}
		return null;
	}

	int GetJewelCountNumberFromString (string tag, int blueJewelCount, int greenJewelCount, int orangeJewelCount, int purpleJewelCount, int redJewelCount, int whiteJewelCount) {
		switch (tag) {
		case "Blue Block": return blueJewelCount;
		case "Green Block": return greenJewelCount;
		case "Yellow Block": return orangeJewelCount;
		case "Purple Block": return purpleJewelCount;
		case "Red Block": return redJewelCount;
		case "White Block": return whiteJewelCount;
		}
		return 100;
	}

	GameObject GetRandomJewel (int col) {

		switch (Random.Range (0, 6)) {
		case 0: return blueJewel;
		case 1: return greenJewel;
		case 2: return orangeJewel;
		case 3: return purpleJewel;
		case 4: return redJewel;
		case 5: return whiteJewel;
		}

//		if (jewelCollectorLevel && !collectionJewelsLaunching && !swappingJewels) {
//			return GetRandomJewelForCollectionLevel (col);
////			switch (Random.Range (0, 6)) {
////			case 0: return blueJewel;
////			case 1: return greenJewel;
////			case 2: return orangeJewel;
////			case 3: return purpleJewel;
////			case 4: return redJewel;
////			case 5: return whiteJewel;
////			}
//		} 
//
//		GameObject bombColorJewel = null;
//
//		GameObject tempBomb = bombHandler.GetTheClosestBomb (col);
//
//		if (tempBomb != null) {
//			switch (tempBomb.tag) {
//			case "Blue Bomb": bombColorJewel = blueJewel; break;
//			case "Green Bomb": bombColorJewel = greenJewel; break;
//			case "Orange Bomb": bombColorJewel = orangeJewel; break;
//			case "Purple Bomb": bombColorJewel = purpleJewel; break;
//			case "Red Bomb": bombColorJewel = redJewel; break;
//			case "White Bomb": bombColorJewel = whiteJewel; break;
//			}
//		}
//
//		if (!powerPercentageController.IsJewelDropWithinPercentage ()) {
//			if (tempBomb != null) {
//				string bombTag = GetJewelTagFromBomb (tempBomb.tag);
//				if (bombTag != null && bombTag == blueJewel.tag) 
//					return GetSelectiveJewel (5, greenJewel, orangeJewel, purpleJewel, redJewel, whiteJewel);
//				if (bombTag != null && bombTag == greenJewel.tag) 
//					return GetSelectiveJewel (5, blueJewel, orangeJewel, purpleJewel, redJewel, whiteJewel);
//				if (bombTag != null && bombTag == orangeJewel.tag) 
//					return GetSelectiveJewel (5, blueJewel, greenJewel, purpleJewel, redJewel, whiteJewel);
//				if (bombTag != null && bombTag == purpleJewel.tag)
//					return GetSelectiveJewel (5, blueJewel, greenJewel, orangeJewel, redJewel, whiteJewel);
//				if (bombTag != null && bombTag == redJewel.tag)
//					return GetSelectiveJewel (5, blueJewel, greenJewel, orangeJewel, purpleJewel, whiteJewel);
//				if (bombTag != null && bombTag == whiteJewel.tag)
//					return GetSelectiveJewel (5, blueJewel, greenJewel, orangeJewel, purpleJewel, redJewel);
//			}
//		}
//		if (bombColorJewel != null && powerPercentageController.IsBombColorDropWithinPercentage ()) {
//			return bombColorJewel;
//		}
//
//		switch (Random.Range (0, 6)) {
//		case 0: return blueJewel;
//		case 1: return greenJewel;
//		case 2: return orangeJewel;
//		case 3: return purpleJewel;
//		case 4: return redJewel;
//		case 5: return whiteJewel;
//		}
		return null;
	}

	GameObject GetSelectiveJewel (int numberOfJewels, GameObject jewelOne, GameObject jewelTwo, GameObject jewelThree, GameObject jewelFour, GameObject jewelFive) {
		switch (Random.Range (0, numberOfJewels)) {
		case 0: return jewelOne;
		case 1: return jewelTwo;
		case 2: return jewelThree;
		case 3: return jewelFour;
		case 4: return jewelFive;
		}
		return null;
	}

	string GetJewelTagFromBomb (string bombTag) {
		switch (bombTag) {
		case "Blue Bomb": return "Blue Block";
		case "Green Bomb": return "Green Block";
		case "Orange Bomb": return "Yellow Block";
		case "Purple Bomb": return "Purple Block";
		case "Red Bomb": return "Red Block";
		case "White Bomb": return "White Block";
		}
		return null;
	}

	GameObject GetRandomJewel (int col, int numberOfJewels, GameObject jewelOne, GameObject jewelTwo, GameObject jewelThree, GameObject jewelFour, GameObject jewelFive, GameObject jewelSix) {

		switch (Random.Range (0, numberOfJewels)) {
		case 0: return jewelOne;
		case 1: return jewelTwo;
		case 2: return jewelThree;
		case 3: return jewelFour;
		case 4: return jewelFive;
		case 5: return jewelSix;
		}

//		GameObject bombColorJewel = null;
//		GameObject tempBomb = bombHandler.GetTheClosestBomb (col);
//		bool bombColorJewelOkay = true, isWithinDropRate = false;
//
//
//
//		if (tempBomb != null) {
//			switch (tempBomb.tag) {
//			case "Blue Bomb": bombColorJewel = blueJewel; break;
//			case "Green Bomb": bombColorJewel = greenJewel; break;
//			case "Orange Bomb": bombColorJewel = orangeJewel; break;
//			case "Purple Bomb": bombColorJewel = purpleJewel; break;
//			case "Red Bomb": bombColorJewel = redJewel; break;
//			case "White Bomb": bombColorJewel = whiteJewel; break;
//			}
//		}
//
//		if (!powerPercentageController.IsJewelDropWithinPercentage ()) {
//			if (tempBomb != null) {
//				numberOfJewels--;
//				string bombTag = GetJewelTagFromBomb (tempBomb.tag);
//				if (bombTag == null) {
//					numberOfJewels++;
//					bombColorJewel = null;
//				} else {
//					if (jewelOne != null && bombTag == jewelOne.tag) 
//						return GetSelectiveJewel (numberOfJewels, jewelTwo, jewelThree, jewelFour, jewelFive, jewelSix);
//					if (jewelTwo != null && bombTag == jewelTwo.tag) 
//						return GetSelectiveJewel (numberOfJewels, jewelOne, jewelThree, jewelFour, jewelFive, jewelSix);
//					if (jewelThree != null && bombTag == jewelThree.tag) 
//						return GetSelectiveJewel (numberOfJewels, jewelOne, jewelTwo, jewelFour, jewelFive, jewelSix);
//					if (jewelFour != null && bombTag == jewelFour.tag)
//						return GetSelectiveJewel (numberOfJewels, jewelOne, jewelTwo, jewelThree, jewelFive, jewelSix);
//					if (jewelFive != null && bombTag == jewelFive.tag)
//						return GetSelectiveJewel (numberOfJewels, jewelOne, jewelTwo, jewelThree, jewelFour, jewelSix);
//					if (jewelSix != null && bombTag == jewelSix.tag)
//						return GetSelectiveJewel (numberOfJewels, jewelOne, jewelTwo, jewelThree, jewelFive, jewelFive);
//					bombColorJewel = null;
//				}
//			}
//		}
//
//		isWithinDropRate = powerPercentageController.IsBombColorDropWithinPercentage ();
//		if (isWithinDropRate && bombColorJewel != null) {
//			bombColorJewelOkay = BombColorJewelOkay (bombColorJewel, jewelOne, jewelTwo, jewelThree, jewelFour, jewelFive, jewelSix);
//		}
//		if (bombColorJewelOkay && bombColorJewel != null && isWithinDropRate) {
//			return bombColorJewel;
//		}
//
//		switch (Random.Range (0, numberOfJewels)) {
//		case 0: return jewelOne;
//		case 1: return jewelTwo;
//		case 2: return jewelThree;
//		case 3: return jewelFour;
//		case 4: return jewelFive;
//		case 5: return jewelSix;
//		}
		return null;
	}

	bool BombColorJewelOkay (GameObject bombColorJewel, GameObject jewelOne, GameObject jewelTwo, GameObject jewelThree, GameObject jewelFour, GameObject jewelFive, GameObject jewelSix) {
		string bombTag = bombColorJewel.tag;
		if (jewelOne != null && bombTag == jewelOne.tag)
			return true;
		if (jewelTwo != null && bombTag == jewelTwo.tag)
			return true;
		if (jewelThree != null && bombTag == jewelThree.tag)
			return true;
		if (jewelFour != null && bombTag == jewelFour.tag)
			return true;
		if (jewelFive != null && bombTag == jewelFive.tag)
			return true;
		if (jewelSix != null && bombTag == jewelSix.tag)
			return true;
		return false;
	}



	GameObject GetRandomWhiteBlueGreenAndOrangeJewel () {
		switch (Random.Range (0, 4)) {
		case 0: return whiteJewel;
		case 1: return orangeJewel;
		case 2: return blueJewel;
		case 3: return greenJewel;
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

	GameObject GetRandomWhiteBlueAndOrangeBombTen () {
		switch (Random.Range (0, 3)) {
		case 0: return whiteBomb10;
		case 1: return orangeBomb10;
		case 2: return blueBomb10;
		}
		return null;
	}
	
	GameObject GetBombNumberOne () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb1;
		case 1: return redBomb1;
		case 2: return greenBomb1;
		case 3: return whiteBomb1;
		case 4: return orangeBomb1;
		case 5: return purpleBomb1;
		}
		return null;
	}

	GameObject GetBombNumberTwo () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb2;
		case 1: return redBomb2;
		case 2: return greenBomb2;
		case 3: return whiteBomb2;
		case 4: return orangeBomb2;
		case 5: return purpleBomb2;
		}
		return null;
	}

	GameObject GetBombNumberThree () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb3;
		case 1: return redBomb3;
		case 2: return greenBomb3;
		case 3: return whiteBomb3;
		case 4: return orangeBomb3;
		case 5: return purpleBomb3;
		}
		return null;
	}

	GameObject GetBombNumberFour () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb4;
		case 1: return redBomb4;
		case 2: return greenBomb4;
		case 3: return whiteBomb4;
		case 4: return orangeBomb4;
		case 5: return purpleBomb4;
		}
		return null;
	} 

	GameObject GetBombNumberFive () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb5;
		case 1: return redBomb5;
		case 2: return greenBomb5;
		case 3: return whiteBomb5;
		case 4: return orangeBomb5;
		case 5: return purpleBomb5;
		}
		return null;
	}

	GameObject GetBombNumberSix () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb6;
		case 1: return redBomb6;
		case 2: return greenBomb6;
		case 3: return whiteBomb6;
		case 4: return orangeBomb6;
		case 5: return purpleBomb6;
		}
		return null;
	}

	GameObject GetBombNumberSeven () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb7;
		case 1: return redBomb7;
		case 2: return greenBomb7;
		case 3: return whiteBomb7;
		case 4: return orangeBomb7;
		case 5: return purpleBomb7;
		}
		return null;
	}

	GameObject GetBombNumberEight () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb8;
		case 1: return redBomb8;
		case 2: return greenBomb8;
		case 3: return whiteBomb8;
		case 4: return orangeBomb8;
		case 5: return purpleBomb8;
		}
		return null;
	}

	GameObject GetBombNumberNine () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb9;
		case 1: return redBomb9;
		case 2: return greenBomb9;
		case 3: return whiteBomb9;
		case 4: return orangeBomb9;
		case 5: return purpleBomb9;
		}
		return null;
	}

	GameObject GetBombNumberTen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb10;
		case 1: return redBomb10;
		case 2: return greenBomb10;
		case 3: return whiteBomb10;
		case 4: return orangeBomb10;
		case 5: return purpleBomb10;
		}
		return null;
	}

	GameObject GetBombNumberEleven () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb11;
		case 1: return redBomb11;
		case 2: return greenBomb11;
		case 3: return whiteBomb11;
		case 4: return orangeBomb11;
		case 5: return purpleBomb11;
		}
		return null;
	}

	GameObject GetBombNumberTwelve () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb12;
		case 1: return redBomb12;
		case 2: return greenBomb12;
		case 3: return whiteBomb12;
		case 4: return orangeBomb12;
		case 5: return purpleBomb12;
		}
		return null;
	}

	GameObject GetBombNumberThirteen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb13;
		case 1: return redBomb13;
		case 2: return greenBomb13;
		case 3: return whiteBomb13;
		case 4: return orangeBomb13;
		case 5: return purpleBomb13;
		}
		return null;
	}

	GameObject GetBombNumberFourteen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb14;
		case 1: return redBomb14;
		case 2: return greenBomb14;
		case 3: return whiteBomb14;
		case 4: return orangeBomb14;
		case 5: return purpleBomb14;
		}
		return null;
	}

	GameObject GetBombNumberFifteen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb15;
		case 1: return redBomb15;
		case 2: return greenBomb15;
		case 3: return whiteBomb15;
		case 4: return orangeBomb15;
		case 5: return purpleBomb15;
		}
		return null;
	}

	GameObject GetBombNumberSixteen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb16;
		case 1: return redBomb16;
		case 2: return greenBomb16;
		case 3: return whiteBomb16;
		case 4: return orangeBomb16;
		case 5: return purpleBomb16;
		}
		return null;
	}

	GameObject GetBombNumberSeventeen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb17;
		case 1: return redBomb17;
		case 2: return greenBomb17;
		case 3: return whiteBomb17;
		case 4: return orangeBomb17;
		case 5: return purpleBomb17;
		}
		return null;
	}

	GameObject GetBombNumberEighteen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb18;
		case 1: return redBomb18;
		case 2: return greenBomb18;
		case 3: return whiteBomb18;
		case 4: return orangeBomb18;
		case 5: return purpleBomb18;
		}
		return null;
	}

	GameObject GetBombNumberNinteen () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb19;
		case 1: return redBomb19;
		case 2: return greenBomb19;
		case 3: return whiteBomb19;
		case 4: return orangeBomb19;
		case 5: return purpleBomb19;
		}
		return null;
	}

	GameObject GetBombNumberTwenty () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb20;
		case 1: return redBomb20;
		case 2: return greenBomb20;
		case 3: return whiteBomb20;
		case 4: return orangeBomb20;
		case 5: return purpleBomb20;
		}
		return null;
	}

	GameObject GetFourthLevelRandomBomb () {
		switch (Random.Range (0, 6)) {
		case 0: return blueBomb5;
		case 1: return redBomb5;
		case 2: return greenBomb5;
		case 3: return orangeBomb5;
		case 4: return purpleBomb5;
		case 5: return whiteBomb5;
		}
		return null;
	}
	
//	GameObject GetLevelFourJewels (int jewelNumber) {
//		switch (jewelNumber) {
//		case 0: return GetRandomJewel ();
//		case 1: return GetRandomJewel ();
//		case 2: return GetRandomJewel ();
//		case 3: return GetRandomJewel ();
//		case 4: return GetRandomJewel (); 
//		case 5: return GetRandomJewel ();
//			
//		case 6: return GetRandomJewel ();
//		case 7: return GetRandomJewel ();
//		case 8: return GetRandomJewel ();
//		case 9: return GetRandomJewel ();
//		case 10: return GetRandomJewel (); 
//		case 11: return GetRandomJewel ();
//			
//		case 12: return GetRandomJewel ();
//		case 13: return GetRandomJewel ();
//		case 14: return GetRandomJewel ();
//		case 15: return GetRandomJewel ();
//		case 16: return GetRandomJewel ();
//		case 17: return GetRandomJewel ();
//			
//		case 18: return GetRandomJewel ();
//		case 19: return GetRandomJewel ();
//		case 20: return GetRandomJewel ();
//		case 21: return GetRandomJewel ();
//		case 22: return GetRandomJewel ();
//		case 23: return GetRandomJewel ();
//			
//		case 24: return GetRandomJewel ();
//		case 25: return GetRandomJewel ();
//		case 26: return GetRandomJewel ();
//		case 27: return GetRandomJewel ();
//		case 28: return boulder;
//		case 29: return boulder;
//			
//		case 30: return boulder;
//		case 31: return boulder;
//		case 32: return boulder;
//		case 33: return boulder;
//		case 34: return boulder;
//		case 35: return GetRandomJewel ();
//			
//		case 36: return GetRandomJewel ();
//		case 37: return boulder;
//		case 38: return GetBombNumberTen ();
//		case 39: return GetBombNumberTen ();
//		case 40: return GetBombNumberTen ();
//		case 41: return GetBombNumberTen ();
//			
//		case 42: return GetBombNumberTen ();
//		case 43: return boulder;
//		case 44: return GetRandomJewel ();
//		case 45: return GetRandomJewel ();
//		case 46: return boulder;
//		case 47: return boulder;
//			
//		case 48: return boulder;
//		case 49: return boulder;
//		case 50: return boulder;
//		case 51: return boulder;
//		case 52: return boulder;
//		case 53: return GetRandomJewel ();
//			
//		case 54: return GetRandomJewel ();
//		case 55: return GetRandomJewel ();
//		case 56: return GetRandomJewel ();
//		case 57: return GetRandomJewel ();
//		case 58: return GetRandomJewel ();
//		case 59: return GetRandomJewel ();
//			
//		case 60: return GetRandomJewel ();
//		case 61: return GetRandomJewel ();
//		case 62: return GetRandomJewel ();
//		case 63: return GetRandomJewel ();
//		case 64: return GetRandomJewel (); 
//		case 65: return GetRandomJewel ();
//			
//		case 66: return GetRandomJewel ();
//		case 67: return GetRandomJewel ();
//		case 68: return GetRandomJewel ();
//		case 69: return GetRandomJewel ();
//		case 70: return GetRandomJewel (); 
//		case 71: return GetRandomJewel ();
//			
//		case 72: return GetRandomJewel ();
//		case 73: return GetRandomJewel ();
//		case 74: return GetRandomJewel ();
//		case 75: return GetRandomJewel ();
//		case 76: return GetRandomJewel ();
//		case 77: return GetRandomJewel ();
//			
//		case 78: return GetRandomJewel ();
//		case 79: return GetRandomJewel ();
//		case 80: return GetRandomJewel ();
//		case 81: return GetRandomJewel ();
//			
//		}
//		return null;
//	}

//	GameObject GetLevelFiveJewels (int jewelNumber) {
//		switch (jewelNumber) {
//		case 0: return GetRandomJewel ();
//		case 1: return GetRandomJewel ();
//		case 2: return GetRandomJewel ();
//		case 3: return GetRandomJewel ();
//		case 4: return GetRandomJewel (); 
//		case 5: return GetRandomJewel ();
//			
//		case 6: return GetRandomJewel ();
//		case 7: return GetRandomJewel ();
//		case 8: return GetRandomJewel ();
//		case 9: return GetRandomJewel ();
//		case 10: return GetRandomJewel (); 
//		case 11: return GetRandomJewel ();
//			
//		case 12: return GetRandomJewel ();
//		case 13: return GetRandomJewel ();
//		case 14: return GetRandomJewel ();
//		case 15: return GetRandomJewel ();
//		case 16: return GetRandomJewel ();
//		case 17: return GetRandomJewel ();
//			
//		case 18: return GetRandomJewel ();
//		case 19: return GetRandomJewel ();
//		case 20: return boulder;
//		case 21: return boulder;
//		case 22: return boulder;
//		case 23: return boulder;
//			
//		case 24: return boulder;
//		case 25: return GetRandomJewel ();
//		case 26: return GetRandomJewel ();
//		case 27: return GetRandomJewel ();
//		case 28: return GetRandomJewel ();
//		case 29: return boulder;
//			
//		case 30: return boulder;
//		case 31: return boulder;
//		case 32: return boulder;
//		case 33: return boulder;
//		case 34: return GetRandomJewel ();
//		case 35: return GetRandomJewel ();
//			
//		case 36: return GetRandomJewel ();
//		case 37: return GetRandomJewel ();
//		case 38: return boulder;
//		case 39: return boulder;
//		case 40: return GetBombNumberTen ();
//		case 41: return boulder;
//			
//		case 42: return boulder;
//		case 43: return GetRandomJewel ();
//		case 44: return GetRandomJewel ();
//		case 45: return GetRandomJewel ();
//		case 46: return GetRandomJewel ();
//		case 47: return boulder;
//			
//		case 48: return boulder;
//		case 49: return boulder;
//		case 50: return boulder;
//		case 51: return boulder;
//		case 52: return GetRandomJewel ();
//		case 53: return GetRandomJewel ();
//			
//		case 54: return GetRandomJewel ();
//		case 55: return GetRandomJewel ();
//		case 56: return boulder;
//		case 57: return boulder;
//		case 58: return boulder;
//		case 59: return boulder;
//			
//		case 60: return boulder;
//		case 61: return GetRandomJewel ();
//		case 62: return GetRandomJewel ();
//		case 63: return GetRandomJewel ();
//		case 64: return GetRandomJewel (); 
//		case 65: return GetRandomJewel ();
//			
//		case 66: return GetRandomJewel ();
//		case 67: return GetRandomJewel ();
//		case 68: return GetRandomJewel ();
//		case 69: return GetRandomJewel ();
//		case 70: return GetRandomJewel (); 
//		case 71: return GetRandomJewel ();
//			
//		case 72: return GetRandomJewel ();
//		case 73: return GetRandomJewel ();
//		case 74: return GetRandomJewel ();
//		case 75: return GetRandomJewel ();
//		case 76: return GetRandomJewel ();
//		case 77: return GetRandomJewel ();
//			
//		case 78: return GetRandomJewel ();
//		case 79: return GetRandomJewel ();
//		case 80: return GetRandomJewel ();
//		case 81: return GetRandomJewel ();
//			
//		}
//		return null;
//	}
//
//	GameObject GetLevelSixJewels (int jewelNumber) {
//		switch (jewelNumber) {
//		case 0: return GetRandomJewel ();
//		case 1: return GetRandomJewel ();
//		case 2: return GetRandomJewel ();
//		case 3: return GetRandomJewel ();
//		case 4: return GetRandomJewel (); 
//		case 5: return GetRandomJewel ();
//			
//		case 6: return GetRandomJewel ();
//		case 7: return GetRandomJewel ();
//		case 8: return GetRandomJewel ();
//		case 9: return GetRandomJewel ();
//		case 10: return GetRandomJewel (); 
//		case 11: return GetRandomJewel ();
//			
//		case 12: return GetRandomJewel ();
//		case 13: return GetRandomJewel ();
//		case 14: return GetRandomJewel ();
//		case 15: return GetRandomJewel ();
//		case 16: return GetRandomJewel ();
//		case 17: return GetRandomJewel ();
//			
//		case 18: return GetRandomJewel ();
//		case 19: return GetRandomJewel ();
//		case 20: return GetRandomJewel ();
//		case 21: return GetRandomJewel ();
//		case 22: return GetRandomJewel ();
//		case 23: return GetRandomJewel ();
//			
//		case 24: return GetRandomJewel ();
//		case 25: return GetRandomJewel ();
//		case 26: return GetRandomJewel ();
//		case 27: return GetRandomJewel ();
//		case 28: return GetRandomJewel ();
//		case 29: return GetRandomJewel ();
//			
//		case 30: return GetRandomJewel ();
//		case 31: return GetRandomJewel ();
//		case 32: return GetRandomJewel ();
//		case 33: return GetRandomJewel ();
//		case 34: return GetRandomJewel ();
//		case 35: return GetRandomJewel ();
//			
//		case 36: return boulder;
//		case 37: return boulder;
//		case 38: return boulder;
//		case 39: return boulder;
//		case 40: return boulder;
//		case 41: return boulder;
//			
//		case 42: return boulder;
//		case 43: return boulder;
//		case 44: return boulder;
//		case 45: return boulder;
//		case 46: return boulder;
//		case 47: return GetBombNumberTwelve ();
//			
//		case 48: return GetBombNumberTwelve ();
//		case 49: return GetBombNumberTwelve ();
//		case 50: return GetBombNumberTwelve ();
//		case 51: return GetBombNumberTwelve ();
//		case 52: return boulder;
//		case 53: return boulder;
//			
//		case 54: return boulder;
//		case 55: return boulder;
//		case 56: return boulder;
//		case 57: return GetBombNumberFourteen ();
//		case 58: return GetBombNumberFourteen ();
//		case 59: return GetBombNumberFourteen ();
//			
//		case 60: return boulder;
//		case 61: return boulder;
//		case 62: return boulder;
//		case 63: return boulder;
//		case 64: return boulder; 
//		case 65: return boulder;
//			
//		case 66: return boulder;
//		case 67: return GetBombNumberSixteen ();
//		case 68: return boulder;
//		case 69: return boulder;
//		case 70: return boulder; 
//		case 71: return boulder;
//			
//		case 72: return boulder;
//		case 73: return boulder;
//		case 74: return boulder;
//		case 75: return boulder;
//		case 76: return boulder;
//		case 77: return boulder;
//			
//		case 78: return boulder;
//		case 79: return boulder;
//		case 80: return boulder;
//		case 81: return boulder;
//			
//		}
//		return null;
//	}

//	GameObject GetLevelSevenJewels (int jewelNumber) {
//		switch (jewelNumber) {
//		case 0: return GetRandomJewel ();
//		case 1: return GetRandomJewel ();
//		case 2: return GetRandomJewel ();
//		case 3: return GetRandomJewel ();
//		case 4: return GetRandomJewel (); 
//		case 5: return GetRandomJewel ();
//			
//		case 6: return GetRandomJewel ();
//		case 7: return GetRandomJewel ();
//		case 8: return GetRandomJewel ();
//		case 9: return GetRandomJewel ();
//		case 10: return GetRandomJewel (); 
//		case 11: return GetRandomJewel ();
//			
//		case 12: return GetRandomJewel ();
//		case 13: return GetRandomJewel ();
//		case 14: return GetRandomJewel ();
//		case 15: return GetRandomJewel ();
//		case 16: return GetRandomJewel ();
//		case 17: return GetRandomJewel ();
//			
//		case 18: return GetRandomJewel ();
//		case 19: return GetRandomJewel ();
//		case 20: return GetRandomJewel ();
//		case 21: return GetRandomJewel ();
//		case 22: return GetRandomJewel ();
//		case 23: return GetRandomJewel ();
//			
//		case 24: return GetRandomJewel ();
//		case 25: return GetRandomJewel ();
//		case 26: return GetRandomJewel ();
//		case 27: return GetRandomJewel ();
//		case 28: return GetRandomJewel ();
//		case 29: return GetRandomJewel ();
//			
//		case 30: return GetRandomJewel ();
//		case 31: return GetRandomJewel ();
//		case 32: return GetRandomJewel ();
//		case 33: return GetRandomJewel ();
//		case 34: return GetRandomJewel ();
//		case 35: return GetRandomJewel ();
//			
//		case 36: return GetRandomJewel ();
//		case 37: return GetRandomJewel ();
//		case 38: return GetRandomJewel ();
//		case 39: return GetRandomJewel ();
//		case 40: return GetRandomJewel ();
//		case 41: return GetRandomJewel ();
//			
//		case 42: return GetRandomJewel ();
//		case 43: return GetRandomJewel ();
//		case 44: return GetRandomJewel ();
//		case 45: return GetRandomJewel ();
//		case 46: return GetRandomJewel ();
//		case 47: return GetRandomJewel ();
//			
//		case 48: return GetRandomJewel ();
//		case 49: return GetRandomJewel ();
//		case 50: return GetRandomJewel ();
//		case 51: return GetRandomJewel ();
//		case 52: return GetRandomJewel ();
//		case 53: return GetRandomJewel ();
//			
//		case 54: return GetRandomJewel ();
//		case 55: return GetRandomJewel ();
//		case 56: return GetRandomJewel ();
//		case 57: return GetRandomJewel ();
//		case 58: return GetRandomJewel ();
//		case 59: return GetRandomJewel ();
//			
//		case 60: return GetRandomJewel ();
//		case 61: return GetRandomJewel ();
//		case 62: return GetRandomJewel ();
//		case 63: return GetRandomJewel ();
//		case 64: return GetRandomJewel ();
//		case 65: return GetRandomJewel ();
//			
//		case 66: return GetRandomJewel ();
//		case 67: return GetRandomJewel ();
//		case 68: return GetRandomJewel ();
//		case 69: return GetRandomJewel ();
//		case 70: return GetRandomJewel ();
//		case 71: return GetRandomJewel ();
//			
//		case 72: return GetRandomJewel ();
//		case 73: return GetRandomJewel ();
//		case 74: return GetRandomJewel ();
//		case 75: return GetRandomJewel ();
//		case 76: return GetRandomJewel ();
//		case 77: return GetRandomJewel ();
//			
//		case 78: return GetRandomJewel ();
//		case 79: return GetRandomJewel ();
//		case 80: return GetRandomJewel ();
//		case 81: return GetRandomJewel ();
//			
//		}
//		return null;
//	}

	GameObject GetLevelEightJewels (int jewelNumber) {
		switch (jewelNumber) {
		case 0: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 1: return steelBlock;
		case 2: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 3: return steelBlock;
		case 4: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 5: return steelBlock;
			
		case 6: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 7: return steelBlock;
		case 8: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 9: return steelBlock;
		case 10: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 11: return steelBlock;
			
		case 12: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 13: return steelBlock;
		case 14: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 15: return steelBlock;
		case 16: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 17: return steelBlock;
			
		case 18: return steelBlock;
		case 19: return steelBlock;
		case 20: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 21: return steelBlock;
		case 22: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 23: return steelBlock;
			
		case 24: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 25: return steelBlock;
		case 26: return steelBlock;
		case 27: return steelBlock;
		case 28: return steelBlock;
		case 29: return steelBlock;
			
		case 30: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 31: return steelBlock;
		case 32: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 33: return steelBlock;
		case 34: return steelBlock;
		case 35: return steelBlock;
			
		case 36: return steelBlock;
		case 37: return steelBlock;
		case 38: return steelBlock;
		case 39: return steelBlock;
		case 40: return GetRandomWhiteBlueAndOrangeBombTen ();
		case 41: return steelBlock;
			
		case 42: return steelBlock;
		case 43: return steelBlock;
		case 44: return steelBlock;
		case 45: return steelBlock;
		case 46: return steelBlock;
		case 47: return steelBlock;
			
		case 48: return steelBlock;
		case 49: return steelBlock;
		case 50: return steelBlock;
		case 51: return steelBlock;
		case 52: return steelBlock;
		case 53: return steelBlock;
			
		case 54: return GetRandomWhiteBlueAndOrangeJewel ();
		case 55: return GetRandomWhiteBlueAndOrangeJewel ();
		case 56: return GetRandomWhiteBlueAndOrangeJewel ();
		case 57: return GetRandomWhiteBlueAndOrangeJewel ();
		case 58: return GetRandomWhiteBlueAndOrangeJewel ();
		case 59: return GetRandomWhiteBlueAndOrangeJewel ();
			
		case 60: return GetRandomWhiteBlueAndOrangeJewel ();
		case 61: return GetRandomWhiteBlueAndOrangeJewel ();
		case 62: return GetRandomWhiteBlueAndOrangeJewel ();
		case 63: return GetRandomWhiteBlueAndOrangeJewel ();
		case 64: return GetRandomWhiteBlueAndOrangeJewel ();
		case 65: return GetRandomWhiteBlueAndOrangeJewel ();
			
		case 66: return GetRandomWhiteBlueAndOrangeJewel ();
		case 67: return GetRandomWhiteBlueAndOrangeJewel ();
		case 68: return GetRandomWhiteBlueAndOrangeJewel ();
		case 69: return GetRandomWhiteBlueAndOrangeJewel ();
		case 70: return GetRandomWhiteBlueAndOrangeJewel ();
		case 71: return GetRandomWhiteBlueAndOrangeJewel ();
			
		case 72: return GetRandomWhiteBlueAndOrangeJewel ();
		case 73: return GetRandomWhiteBlueAndOrangeJewel ();
		case 74: return GetRandomWhiteBlueAndOrangeJewel ();
		case 75: return GetRandomWhiteBlueAndOrangeJewel ();
		case 76: return GetRandomWhiteBlueAndOrangeJewel ();
		case 77: return GetRandomWhiteBlueAndOrangeJewel ();
			
		case 78: return GetRandomWhiteBlueAndOrangeJewel ();
		case 79: return GetRandomWhiteBlueAndOrangeJewel ();
		case 80: return GetRandomWhiteBlueAndOrangeJewel ();
		case 81: return GetRandomWhiteBlueAndOrangeJewel ();
			
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

	public void SetCollectionJewelsLaunching (bool collectionJewelsLaunching) {
		this.collectionJewelsLaunching = collectionJewelsLaunching;
	}

	public void SetSwappingJewels (bool swappingJewels) {
		this.swappingJewels = swappingJewels;
	}

	public void IncreaseJewelCollectorDifficultyPercentage (float increment) {
		jewelCollectorDifficultyPercentage += increment;
	}
}
