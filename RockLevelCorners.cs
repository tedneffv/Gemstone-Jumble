using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelCorners : MonoBehaviour {

	public GameObject greenCornerStar, redCornerStar, blueCornerStar, whiteCornerStar, orangeCornerStar, purpleCornerStar;
	GameObject starToInstantiate;
	RockLevelJewelMovement jewelMovement;
	RockLevelInstantiator instantiator;
	RockLevelCornerStarMovement cornerStarMovement;
	RockLevelTouchHandler touchHandler;
	RockLevelDeleteJewels deleteJewels;
	LevelTwoScoreKeeper scoreKeeper;
	int starNumber;
	List<GameObject> cornerStarList;
	bool positiveVerticalStar, negativeVerticalStar, positiveHorizontalStar, negativeHorizontalStar;
	public static bool cornerJewelDestroyed, cornerJewelAdded;
	
	// Use this for initialization
	void Start () {
		scoreKeeper = gameObject.GetComponent<LevelTwoScoreKeeper> ();
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		cornerJewelDestroyed = false;
		cornerStarList = new List<GameObject> ();
	}
	
	private void ResetIntsAndBools () {
		starNumber = 0;
		positiveVerticalStar = false;
		positiveHorizontalStar = false;
		negativeVerticalStar = false;
		negativeHorizontalStar = false;
	}
	
	public void ActivateCornersPowers (GameObject jewel) {
		deleteJewels.StampTimeCard ();
		ResetIntsAndBools ();
		switch (jewel.tag) {
		case "Green Bomb":
		case "Green Block": starToInstantiate = greenCornerStar; break;
		case "Red Bomb":
		case "Red Block": starToInstantiate = redCornerStar; break;
		case "Blue Bomb":
		case "Blue Block": starToInstantiate = blueCornerStar; break;
		case "White Bomb": 
		case "White Block": starToInstantiate = whiteCornerStar; break;
		case "Orange Bomb":
		case "Yellow Block": starToInstantiate = orangeCornerStar; break;
		case "Purple Bomb":
		case "Purple Block": starToInstantiate = purpleCornerStar; break;
		}
		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
		if (jewelMovement.GetRow () + 1 < 9 && JewelsAreTheSame (instantiator.GetJewelGridGameObject (jewelMovement.GetRow () + 1, jewelMovement.GetCol ()).tag, jewel.tag)) {
			starNumber++;
			negativeVerticalStar = true;
		}
		if (jewelMovement.GetRow () - 1 >= 0 && JewelsAreTheSame (instantiator.GetJewelGridGameObject (jewelMovement.GetRow () -1, jewelMovement.GetCol ()).tag, jewel.tag)) {
			starNumber++;
			positiveVerticalStar = true;
		}
		if (jewelMovement.GetCol () + 1 < 9 && JewelsAreTheSame (instantiator.GetJewelGridGameObject (jewelMovement.GetRow (), jewelMovement.GetCol () + 1).tag, jewel.tag)) {
			starNumber++;
			positiveHorizontalStar = true;
		}
		if (jewelMovement.GetCol () - 1 >= 0 && JewelsAreTheSame (instantiator.GetJewelGridGameObject (jewelMovement.GetRow (), jewelMovement.GetCol () - 1).tag, jewel.tag)) {
			starNumber++;
			negativeHorizontalStar = true;
		}
		InstantiateCornerStars (jewel);
	}
	
	private void InstantiateCornerStars (GameObject jewel) {
//		scoreKeeper.IncreaseScoreByCornerJewelBonus ();
		GameObject instantiatedStar;
		int starCount = 0;
		//Debug.Log ("starToInstantiate = " + starToInstantiate);
		instantiatedStar = (GameObject)Instantiate (starToInstantiate, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -90), Quaternion.identity);
		cornerStarList.Add (instantiatedStar);
		cornerStarMovement = instantiatedStar.GetComponent<RockLevelCornerStarMovement> ();
		if (positiveVerticalStar) {
			cornerStarMovement.SetTargetX (jewel.transform.position.x);
			cornerStarMovement.SetTargetY (100);
			cornerStarMovement.ToggleMovementStart (true);
			cornerStarMovement.SetJewelToExplodeRowAndCol (jewelMovement.GetRow () - 1, jewelMovement.GetCol ());
			cornerStarMovement.SetVerticalPositive (true);
			cornerStarMovement.SetCornerJewel (jewel);
			starCount++;
			
			instantiatedStar = (GameObject)Instantiate (starToInstantiate, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -91), Quaternion.identity);
			cornerStarMovement = instantiatedStar.GetComponent<RockLevelCornerStarMovement> ();
		}
		if (negativeVerticalStar) {
			cornerStarMovement.SetTargetX (jewel.transform.position.x);
			cornerStarMovement.SetTargetY (-100);
			cornerStarMovement.ToggleMovementStart (true);
			cornerStarMovement.SetJewelToExplodeRowAndCol (jewelMovement.GetRow () + 1, jewelMovement.GetCol ());
			cornerStarMovement.SetVerticalNegative (true);
			cornerStarMovement.SetCornerJewel (jewel);
			starCount++;
			
			if (starNumber > starCount) {
				instantiatedStar = (GameObject)Instantiate (starToInstantiate, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -92), Quaternion.identity);
				cornerStarMovement = instantiatedStar.GetComponent<RockLevelCornerStarMovement> ();
			}
		}
		if (positiveHorizontalStar) {
			cornerStarMovement.SetTargetX (100);
			cornerStarMovement.SetTargetY (jewel.transform.position.y);
			cornerStarMovement.ToggleMovementStart (true);
			cornerStarMovement.SetJewelToExplodeRowAndCol (jewelMovement.GetRow (), jewelMovement.GetCol () + 1);
			cornerStarMovement.SetHorizontalPositive (true);
			cornerStarMovement.SetCornerJewel (jewel);
			starCount++;
			
			if (starNumber > starCount) {
				instantiatedStar = (GameObject)Instantiate (starToInstantiate, new Vector3 (jewel.transform.position.x, jewel.transform.position.y, -93), Quaternion.identity);
				cornerStarMovement = instantiatedStar.GetComponent<RockLevelCornerStarMovement> ();
			}
		}
		if (negativeHorizontalStar) {
			cornerStarMovement.SetTargetX (-100);
			cornerStarMovement.SetTargetY (jewel.transform.position.y);
			cornerStarMovement.ToggleMovementStart (true);
			cornerStarMovement.SetJewelToExplodeRowAndCol (jewelMovement.GetRow (), jewelMovement.GetCol () - 1);
			cornerStarMovement.SetHorizontalNegative (true);
			cornerStarMovement.SetCornerJewel (jewel);
			starCount++;
		}
		cornerJewelAdded = false;
	}
	
	public List<GameObject> GetCornerStarList () {
		return cornerStarList;
	}

	public bool GetCornerStarGreaterThan10 () {
		foreach (GameObject a in cornerStarList) {
			if (Mathf.Abs (a.transform.position.x) < 6 && Mathf.Abs (a.transform.position.y) < 6)
				return false;
		}
		return true;
	}

	public void SwapIfInDeleteArrays (GameObject oldBomb, GameObject newBomb) {
		foreach (GameObject a in cornerStarList) {
			RockLevelCornerStarMovement cornerStarMovement = a.GetComponent<RockLevelCornerStarMovement> ();
			cornerStarMovement.SwapIfInDeleteArrays (oldBomb, newBomb);
		}
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
