using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelFiveInARow : MonoBehaviour {

	public GameObject greenMotherStar, blueMotherStar, redMotherStar, whiteMotherStar, orangeMotherStar, purpleMotherStar;
	bool timeSlowedForTutorial;
	GameObject instantiatedMotherStar;
	RockLevelTouchHandler touchHandler;
	List<GameObject> motherStarList, childStarList, tutorialChildStarList;
	RockLevelController levelThreeController;
	RockLevelMovementChecker stoppedMoving;
	LevelTwoScoreKeeper scoreKeeper;
	RockLevelDeleteJewels deleteJewels;
	SoundHandler soundHandler;
	
	// Use this for initialization
	void Start () {
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		levelThreeController = gameObject.GetComponent<RockLevelController> ();
		stoppedMoving = gameObject.GetComponent<RockLevelMovementChecker> ();
		scoreKeeper = gameObject.GetComponent<LevelTwoScoreKeeper> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		motherStarList = new List<GameObject> ();
		childStarList = new List<GameObject> ();
		tutorialChildStarList = new List<GameObject> ();
	}
	

	public void InstantiateMotherStar (GameObject jewel) {
		deleteJewels.StampTimeCard ();
//		scoreKeeper.IncreaseScoreByFiveInLineBonus ();
		switch (jewel.tag) {
		case "Red Bomb":
		case "Red Block": instantiatedMotherStar = (GameObject)Instantiate (redMotherStar, jewel.transform.position, Quaternion.identity); break;
		case "Green Bomb":
		case "Green Block": instantiatedMotherStar = (GameObject)Instantiate (greenMotherStar, jewel.transform.position, Quaternion.identity); break;
		case "Purple Bomb":
		case "Purple Block": instantiatedMotherStar = (GameObject)Instantiate (purpleMotherStar, jewel.transform.position, Quaternion.identity); break;
		case "Blue Bomb":
		case "Blue Block": instantiatedMotherStar = (GameObject)Instantiate (blueMotherStar, jewel.transform.position, Quaternion.identity); break;
		case "Orange Bomb":
		case "Yellow Block": instantiatedMotherStar = (GameObject)Instantiate (orangeMotherStar, jewel.transform.position, Quaternion.identity); break;
		case "White Bomb":
		case "White Block": instantiatedMotherStar = (GameObject)Instantiate (whiteMotherStar, jewel.transform.position, Quaternion.identity); break;
		}
		soundHandler.PlayPowerUpSound ();
		motherStarList.Add (instantiatedMotherStar);
	}
	
	
	public List<GameObject> GetMotherStarList () {
		return motherStarList;
	}
	
	public List<GameObject> GetChildStarList () {
		return childStarList;
	}
	
	public void SetTimeSlowedForTutorial () {
		timeSlowedForTutorial = true;
	}
	
	public void AddToTutorialList (GameObject star) {
		tutorialChildStarList.Add (star);
	}
	
	public bool TutorialListContainsStar (GameObject star) {
		return tutorialChildStarList.Contains (star);
	}

	public void SwapIfInChildStarList (GameObject oldBomb, GameObject newBomb) {
		foreach (GameObject a in childStarList) {
			RockLevelChildStarMovement childStarMovement = a.GetComponent<RockLevelChildStarMovement> ();
			if (childStarMovement.GetJewelToDestroy () == oldBomb) {
				childStarMovement.SetJewelToDestroy (newBomb);
				return;
			}
		}
	}
	
	public void RemoveStarFromTutorialList (GameObject star) {
		tutorialChildStarList.Remove (star);
		//Debug.Log ("tutorialChildStarList.Count = " + tutorialChildStarList.Count);
		if (tutorialChildStarList.Count == 0) {
			Time.timeScale = 1f;
		}
	}
}
