﻿using UnityEngine;
using System.Collections;

public class LevelTwentyTwoCreator : MonoBehaviour {

	public GameObject blueJewel, greenJewel, orangeJewel, purpleJewel, redJewel, whiteJewel;
	public GameObject blueBomb, greenBomb, orangeBomb, purpleBomb, redBomb, whiteBomb, bombNumber30;
	public GameObject boulder, steelBlock, pauseButton, topBanner, bottomBanner, powerButton, glassBorder;
	RockLevelBombHandler bombHandler;
	RockLevelNoMatchChecker noMatchChecker;
	int[] bombArray;
	RockLevelJewelMovement jewelMovement;
	RockLevelInstantiator instantiator;
	GameObject tempJewel;
	bool offsetChanged, bombNeedsInstantiating, levelEight, jewelsInstantiated;
	float startingLeftJewelPosition, horizontalMultiplier;
	int jewelCount, layerOffset;
	
	void Awake () {
		jewelCount = 0;
		startingLeftJewelPosition = -2.45f;
		horizontalMultiplier = .6125f;
		layerOffset = 17;
		bombHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelBombHandler> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		noMatchChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelNoMatchChecker> ();
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
	}
	
	// Update is called once per frame
	void Update () {
		if (jewelsInstantiated) {
			jewelsInstantiated = false;
			if (noMatchChecker.CheckForMatchesOnInstantiation ()) {
				jewelCount = 0;
				for (int i = 0; i < 9; i++) {
					for (int j = 0; j < 9; j++) {
						if (IsBomb (jewelCount))
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
				} else if (IsBomb (jewelCount)) {
					tempJewel = GetJewels (jewelCount, new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), (-1 * i) - 2));
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
		case 30: return GetBomb (position, bombNumber30);
		}
		return null;
	}
	
	GameObject GetRandomJewel () {
		switch (Random.Range (0, 6)) {
		case 0: return blueJewel;
		case 1: return greenJewel;
		case 2: return orangeJewel;
		case 3: return purpleJewel;
		case 4: return redJewel;
		case 5: return whiteJewel;
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
		case 2: tempBomb = (GameObject)Instantiate (orangeBomb, position, Quaternion.identity); break;
		case 3: tempBomb = (GameObject)Instantiate (purpleBomb, position, Quaternion.identity); break;
		case 4: tempBomb = (GameObject)Instantiate (redBomb, position, Quaternion.identity); break;
		case 5: tempBomb = (GameObject)Instantiate (whiteBomb, position, Quaternion.identity); break;
		}
		tempNumber = (GameObject)Instantiate (bombNumber, new Vector3 (tempBomb.transform.position.x, tempBomb.transform.position.y, tempBomb.transform.position.z - .1f), Quaternion.identity);
		tempNumber.transform.parent = tempBomb.transform;
		return tempBomb;
	}
	
	bool IsBomb (int jewelNumber) {
		switch (jewelNumber) {
		case 0:
		case 8:
		case 72:
		case 80:
			return true;
		}
		return false;
	}
	
	bool IsBoulder (int jewelNumber) {
		switch (jewelNumber) {
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
			
		case 9:
		case 10:
		case 11:
		case 12: 
		case 13: 
		case 14: 
		case 15: 
		case 16: 
		case 17: 
			
		case 18: 
		case 19: 
		case 20: 
		case 21: 
		case 22: 
		case 23: 
		case 24: 
		case 25: 
		case 26: 
			
		case 27: 
		case 28: 
		case 29: 
		case 33: 
		case 34: 
		case 35: 
			
		case 36: 
		case 37: 
		case 38: 
		case 42: 
		case 43: 
		case 44: 
			
		case 45: 
		case 46: 
		case 47: 
		case 51: 
		case 52: 
		case 53: 
			
		case 54: 
		case 55: 
		case 56: 
		case 57: 
		case 58: 
		case 59: 
		case 60: 
		case 61: 
		case 62: 
			
		case 63: 
		case 64: 
		case 65: 
		case 66: 
		case 67: 
		case 68: 
		case 69: 
		case 70: 
		case 71: 
			
		case 73: 
		case 74: 
		case 75: 
		case 76: 
		case 77: 
		case 78: 
		case 79: 
			return true;
		}
		return false;
	}
	
	GameObject GetJewels (int jewelNumber, Vector3 position) {
		switch (jewelNumber) {
		case 0: return GetBomb (position, bombNumber30);
		case 1: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 2: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 3: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 4: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 5: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 6: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 7: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 8: return GetBomb (position, bombNumber30);
			
		case 9: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 10: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 11: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 12: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 13: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 14: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 15: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 16: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 17: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 18: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 19: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 20: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 21: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 22: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 23: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 24: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 25: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 26: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 27: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 28: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 29: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 30: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 31: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 32: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 33: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 34: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 35: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 36: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 37: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 38: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 39: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 40: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 41: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 42: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 43: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 44: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 45: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 46: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 47: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 48: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 49: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 50: return (GameObject)Instantiate (GetRandomJewel (), position, Quaternion.identity);
		case 51: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 52: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 53: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 54: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 55: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 56: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 57: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 58: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 59: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 60: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 61: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 62: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 63: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 64: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 65: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 66: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 67: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 68: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 69: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 70: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 71: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
			
		case 72: return GetBomb (position, bombNumber30);
		case 73: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 74: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 75: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 76: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 77: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 78: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 79: return (GameObject)Instantiate (boulder, position, Quaternion.identity);
		case 80: return GetBomb (position, bombNumber30);
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
}