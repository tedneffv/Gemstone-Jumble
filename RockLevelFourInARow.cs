using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelFourInARow : MonoBehaviour {

	public GameObject greenHomingStar, redHomingStar, purpleHomingStar, orangeHomingStar, whiteHomingStar, blueHomingStar;
	GameObject instantiatedCrosshairs;
	RockLevelHomingStarMovement homingStarMovement, homingStarMovement2;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement, jewelMovement2;
	RockLevelTouchHandler touchHandler;
	RockLevelController controller;
	RockLevelDeleteJewels deleteJewels;
	RockLevelMovementChecker movementChecker;
	RockLevelBombHandler bombHandler;
	LevelTwoScoreKeeper scoreKeeper;
	int random1, random2, tutorialCount, tutorial2Count;
	HashSet<GameObject> targetJewels;
	RockLevelCheckForMatches checkForMatches;
	List<GameObject> homingStarList;
	bool instantiateShadeFour, tutorialLevel2, powerUsed;
	AudioSource[] audioSources;
	PowerPercentageController powerPercentageController;
	
	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		controller = gameObject.GetComponent<RockLevelController> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		scoreKeeper = gameObject.GetComponent<LevelTwoScoreKeeper> ();
		movementChecker = gameObject.GetComponent<RockLevelMovementChecker> ();
		bombHandler = gameObject.GetComponent<RockLevelBombHandler> ();
		powerPercentageController = gameObject.GetComponent<PowerPercentageController> ();
		homingStarList = new List<GameObject> ();
		targetJewels = new HashSet<GameObject> ();
		audioSources = GameObject.Find ("SoundHandler").GetComponents<AudioSource> ();
		tutorialCount = 0;
		tutorial2Count = 0;
		if (GameObject.Find ("Mountain Level Two ID") != null) {
			tutorialLevel2 = true;
		}
	}
	
	
	public bool AllStarsReadyToFire () {
		foreach (GameObject a in homingStarList) {
			homingStarMovement2 = a.GetComponent<RockLevelHomingStarMovement> ();
			if (!homingStarMovement2.ReadyToFire ())
				return false;
		}
		return true;
	}
	
	public void ActivateFourInARowPower (HashSet<GameObject> deleteList) {
		deleteJewels.StampTimeCard ();
//		scoreKeeper.IncreaseScoreByFourJewelBonus ();
		bool bombStarUsed = false;
		foreach (GameObject a in deleteList) {
			switch (a.tag) {
			case "Orange Bomb":
			case "Yellow Block": 
				if (bombHandler.GetBombListCount () > 0 && powerPercentageController.IsWithinPercentage () && !bombStarUsed) {
					InstantiateBombSeekingHomingStar (orangeHomingStar, a); 
					bombStarUsed = true;
				}
				else
					InstantiateHomingStar (orangeHomingStar, a); 
				break;
			case "Blue Bomb":
			case "Blue Block": 
				if (bombHandler.GetBombListCount () > 0 && powerPercentageController.IsWithinPercentage () && !bombStarUsed) {
					InstantiateBombSeekingHomingStar (blueHomingStar, a);
					bombStarUsed = true;
				}
				else
					InstantiateHomingStar (blueHomingStar, a); 
				break;

			case "Green Bomb":
			case "Green Block": 
				if (bombHandler.GetBombListCount () > 0 && powerPercentageController.IsWithinPercentage () && !bombStarUsed) {
					InstantiateBombSeekingHomingStar (greenHomingStar, a);
					bombStarUsed = true;
				}
				else 
					InstantiateHomingStar (greenHomingStar, a); 
				break;
			case "Red Bomb":
			case "Red Block": 
				if (bombHandler.GetBombListCount () > 0 && powerPercentageController.IsWithinPercentage () && !bombStarUsed) {
					InstantiateBombSeekingHomingStar (redHomingStar, a);
					bombStarUsed = true;
				}
				else 
					InstantiateHomingStar (redHomingStar, a); 
				break;
			case "Purple Bomb":
			case "Purple Block": 
				if (bombHandler.GetBombListCount () > 0 && powerPercentageController.IsWithinPercentage () && !bombStarUsed) {
					InstantiateBombSeekingHomingStar (purpleHomingStar, a);
					bombStarUsed = true;
				}
				else 
					InstantiateHomingStar (purpleHomingStar, a); 
				break;
			case "White Bomb":
			case "White Block": 
				if (bombHandler.GetBombListCount () > 0 && powerPercentageController.IsWithinPercentage () && !bombStarUsed) {
					InstantiateBombSeekingHomingStar (whiteHomingStar, a);
					bombStarUsed = true;
				}
				else 
					InstantiateHomingStar (whiteHomingStar, a); 
				break;
			}
		}
		targetJewels.Clear ();
	}

	private void InstantiateHomingStar (GameObject star, GameObject jewel) {
		if (tutorialLevel2) {
			InstantiateTutorialTwoHomingStar (star, jewel);
			return;
		}
		GameObject instantiatedStar = (GameObject)Instantiate (star, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -49), Quaternion.identity);
		homingStarMovement = instantiatedStar.GetComponent<RockLevelHomingStarMovement> ();
		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
		random1 = Random.Range (0, 9);
		random2 = Random.Range (0, 9);
		if (instantiator.GetJewelGridGameObject (random1, random2) != null)
			jewelMovement2 = instantiator.GetJewelGridGameObject (random1, random2).GetComponent<RockLevelJewelMovement> ();
		while (IsBomb (instantiator.GetJewelGridGameObject (random1, random2).tag) || instantiator.GetJewelGridGameObject (random1, random2) == null || (random1 == jewelMovement.GetRow () && random2 == jewelMovement.GetCol ()) || checkForMatches.deleteList.Contains(instantiator.GetJewelGridGameObject (random1, random2)) || jewelMovement2.GetMoving () || !targetJewels.Add (instantiator.GetJewelGridGameObject (random1, random2))) {
			random1 = Random.Range (0, 9);
			random2 = Random.Range (0, 9);
			if (instantiator.GetJewelGridGameObject (random1, random2) != null) 
				jewelMovement2 = instantiator.GetJewelGridGameObject (random1, random2).GetComponent<RockLevelJewelMovement> ();
		}
		homingStarMovement.SetRow (random1);
		homingStarMovement.SetCol (random2);
		homingStarMovement.SetFourInARowStar ();
		homingStarList.Add (instantiatedStar);
	}

	private void InstantiateBombSeekingHomingStar (GameObject star, GameObject jewel) {
		GameObject instantiatedStar = (GameObject)Instantiate (star, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -49), Quaternion.identity);
		homingStarMovement = instantiatedStar.GetComponent<RockLevelHomingStarMovement> ();
		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
		GameObject randomBomb = bombHandler.GetRandomBomb ();
		random1 = randomBomb.GetComponent<RockLevelJewelMovement> ().GetRow ();
		random2 = randomBomb.GetComponent<RockLevelJewelMovement> ().GetCol ();
		if (instantiator.GetJewelGridGameObject (random1, random2) != null)
			jewelMovement2 = instantiator.GetJewelGridGameObject (random1, random2).GetComponent<RockLevelJewelMovement> ();
		while (instantiator.GetJewelGridGameObject (random1, random2) == null || (random1 == jewelMovement.GetRow () && random2 == jewelMovement.GetCol ()) || checkForMatches.deleteList.Contains (instantiator.GetJewelGridGameObject (random1, random2)) || jewelMovement2.GetMoving () || !targetJewels.Add (instantiator.GetJewelGridGameObject (random1, random2))) {
			random1 = Random.Range (0, 9);
			random2 = Random.Range (0, 9);
			if (instantiator.GetJewelGridGameObject (random1, random2) != null) 
				jewelMovement2 = instantiator.GetJewelGridGameObject (random1, random2).GetComponent<RockLevelJewelMovement> ();
		}
		homingStarMovement.SetRow (random1);
		homingStarMovement.SetCol (random2);
		homingStarMovement.SetFourInARowStar ();
		homingStarList.Add (instantiatedStar);
	}

	void InstantiateTutorialTwoHomingStar (GameObject star, GameObject jewel) {
		GameObject instantiatedStar = (GameObject)Instantiate (star, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -49), Quaternion.identity);
		homingStarMovement = instantiatedStar.GetComponent<RockLevelHomingStarMovement> ();
		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
		switch (tutorial2Count) {
		case 0: random1 = 7; random2 = 1; break;
		case 1: random1 = 6; random2 = 2; break;
		case 2: random1 = 5; random2 = 4; break;
		case 3: random1 = 1; random2 = 5; break;
		default: tutorialLevel2 = false; InstantiateHomingStar (star, jewel); return;
		}
		tutorial2Count++;
		homingStarMovement.SetRow (random1);
		homingStarMovement.SetCol (random2);
		homingStarMovement.SetFourInARowStar ();
		homingStarList.Add (instantiatedStar);
	}

	public void RemoveHomingStarFromList (GameObject star) {
		//Debug.Log ("Removing star from list");
		homingStarList.Remove (star);
	}

	public void SwapIfContainedInList (GameObject oldBomb, GameObject newBomb) {
		foreach (GameObject a in homingStarList) {
			RockLevelHomingStarMovement homingStarMovement = a.GetComponent<RockLevelHomingStarMovement> ();
			if (homingStarMovement.GetJewelToDestroy () == oldBomb) {
				homingStarMovement.SetJewelToDestroy (newBomb);
				return;
			}
		}
	}

	public List<GameObject> GetHomingStarList () {
		return homingStarList;
	}

	bool IsBomb (string tag) {
		return (tag == "Blue Bomb" || tag == "Green Bomb" || tag == "Orange Bomb" || tag == "Purple Bomb" || tag == "Red Bomb" || tag == "White Bomb");
	}
}
