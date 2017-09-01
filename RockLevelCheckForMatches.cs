using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelCheckForMatches : MonoBehaviour {

	RockLevelInstantiator instantiator;
	//	MoveJewelsDown moveJewelsDown;
	public HashSet<GameObject> deleteList, verticalDeleteList, horizontalDeleteList;
	RockLevelDeleteJewels deleteJewels;
	bool dontDelete, exitMatchCheck, powersActivated, horizontalMatch, verticalMatch, gameStarted;
	int horizontalMatchCheckCount, verticalMatchCheckCount, jewelDestroyNumber;
	List<GameObject> jewelsThatActivatedCorners;
	RockLevelJewelMovement jewelMovement, jewelMovementTwo;
	HashSet<GameObject>[] horizontalSetArray;
	HashSet<GameObject>[] verticalSetArray;
	RockLevelCorners corners;
	RockLevelFiveInARow fiveInARow;
	RockLevelSwapJewel swapJewel;
	LevelTwoScoreKeeper scoreKeeper;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		deleteList = new HashSet<GameObject> ();
		horizontalDeleteList = new HashSet<GameObject> ();
		verticalDeleteList = new HashSet<GameObject> ();
		horizontalSetArray = new HashSet<GameObject>[4];
		verticalSetArray = new HashSet<GameObject>[4];
		jewelsThatActivatedCorners = new List<GameObject> ();
		dontDelete = false;
		exitMatchCheck = false;
		powersActivated = false;
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		//		moveJewelsDown = GameObject.Find ("Level One Controller").GetComponent<MoveJewelsDown> ();
		corners = gameObject.GetComponent<RockLevelCorners> ();
		fiveInARow = gameObject.GetComponent<RockLevelFiveInARow> ();
		scoreKeeper = gameObject.GetComponent<LevelTwoScoreKeeper> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		swapJewel = gameObject.GetComponent<RockLevelSwapJewel> ();
		gameStarted = false;
	}
	
	void ClearLists () {
		verticalDeleteList.Clear ();
		horizontalDeleteList.Clear ();
		deleteList.Clear ();
		for (int i = 0; i < horizontalSetArray.Length; i++) {
			if (horizontalSetArray[i] != null)
				horizontalSetArray[i].Clear ();
		}
		for (int i = 0; i < verticalSetArray.Length; i++) {
			if (verticalSetArray[i] != null)
				verticalSetArray[i].Clear ();
		}
	}
	
	public bool JewelOkayToMove (GameObject jewel) {
		if (deleteList.Contains(jewel) || horizontalDeleteList.Contains(jewel) || verticalDeleteList.Contains(jewel))
			return true;
		for (int i = 0; i < horizontalSetArray.Length; i++) {
			if (horizontalSetArray[i] != null && horizontalSetArray[i].Contains (jewel))
				return true;
		}
		for (int i = 0; i < verticalSetArray.Length; i++) {
			if (verticalSetArray[i] != null && verticalSetArray[i].Contains (jewel))
				return true;
		}
		return false;
	}
	
	public bool CheckForSwapBack (GameObject jewel, int i , int j) {
		if (!gameStarted)
			return false;
		//Debug.Log ("Checking For SwapBack");
		horizontalMatchCheckCount = 0;
		verticalMatchCheckCount = 0;
		horizontalMatch = false;
		verticalMatch = false;
		exitMatchCheck = false;
		dontDelete = false;
		ClearLists ();
		int verticalJewelOffset = 1;
		int verticalJewelCount = 1;
		verticalDeleteList.Add (jewel);
		
		while (i - verticalJewelOffset >= 0 && instantiator.GetJewelGridGameObject (i - verticalJewelOffset, j) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i - verticalJewelOffset, j).tag, jewel.tag)) {
			verticalDeleteList.Add (instantiator.GetJewelGridGameObject (i - verticalJewelOffset, j));
			
			verticalJewelCount++;
			verticalJewelOffset++;
		}
		verticalJewelOffset = 1;
		
		while (i + verticalJewelOffset < 9 && instantiator.GetJewelGridGameObject (i + verticalJewelOffset, j) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i + verticalJewelOffset, j).tag, jewel.tag)) {
			verticalDeleteList.Add (instantiator.GetJewelGridGameObject (i + verticalJewelOffset, j));
			
			verticalJewelCount++;
			verticalJewelOffset++;
		}
		
		if (verticalJewelCount >= 3) {
			foreach (GameObject a in verticalDeleteList) {
				bool added = deleteList.Add (a);
				if (added && a != null) {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.SetToBeDestroyed ();
					if (jewelMovement.GetMoving ()) {
						ClearLists ();
						return true;
					}
					CheckForHorizontalMatches (a, jewelMovement.GetRow (), jewelMovement.GetCol ());
					if (exitMatchCheck) {
						ClearLists ();
						return true;
					}
					
				}
			}
			verticalMatch = true;
		} else {
			verticalDeleteList.Remove (jewel);
		}
		
		int horizontalJewelOffset = 1;
		int horizontalJewelCount = 1;
		horizontalDeleteList.Add (jewel);
		
		while (j - horizontalJewelOffset >= 0 && instantiator.GetJewelGridGameObject (i, j - horizontalJewelOffset) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j - horizontalJewelOffset).tag, jewel.tag)) {
			horizontalDeleteList.Add (instantiator.GetJewelGridGameObject (i, j - horizontalJewelOffset));
			horizontalJewelCount++;
			horizontalJewelOffset++;
		}
		
		horizontalJewelOffset = 1;
		while (j + horizontalJewelOffset < 9 && instantiator.GetJewelGridGameObject (i, j + horizontalJewelOffset) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j + horizontalJewelOffset).tag, jewel.tag)) {
			horizontalDeleteList.Add (instantiator.GetJewelGridGameObject (i, j + horizontalJewelOffset));
			horizontalJewelCount++;
			horizontalJewelOffset++;
		}
		if (horizontalJewelCount >= 3) {
			foreach (GameObject a in horizontalDeleteList) {
				bool added = deleteList.Add (a);
				if (added && a != null) {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.SetToBeDestroyed ();
					if (jewelMovement.GetMoving ()) {
						ClearLists ();
						return true;
					}
					CheckForVerticalMatches (a, jewelMovement.GetRow (), jewelMovement.GetCol ());
					if (exitMatchCheck) {
						ClearLists ();
						return true;
					}
					
				}
			}
			horizontalMatch = true;
		} else {
			horizontalDeleteList.Remove (jewel);
		}
		if (exitMatchCheck) {
			ClearLists ();
			return true;
		}
		if (deleteList.Count > 2) {
			if (deleteList.Count >= 5 && verticalMatch && horizontalMatch) {
				foreach (GameObject a in horizontalDeleteList) {
					if (verticalDeleteList.Contains (a)) {
						int count = 1;
						if (!jewelsThatActivatedCorners.Contains (a)) {
							jewelsThatActivatedCorners.Add (a);
							soundHandler.PlayPowerUpSound ();
							corners.ActivateCornersPowers (a);
						}
					}
				}
//				scoreKeeper.IncreaseScoreByCornerJewelBonus ();
				//								deleteList.Clear ();
			}
			else if (deleteList.Count >= 5 && !(verticalMatch && horizontalMatch)) {
				//				//Debug.Log ("5 in a row");
				fiveInARow.InstantiateMotherStar (jewel);
//				scoreKeeper.IncreaseScoreByFiveInLineBonus ();
			}
			if (deleteList.Count >= 7 && verticalMatch && horizontalMatch) {
				fiveInARow.InstantiateMotherStar (jewel);
			}
			//							if (deleteList.Count > 5 && verticalMatch && horizontalMatch) {
			//								fiveInARow.InstantiateMotherStar (jewel);
			//							}

			foreach (GameObject a in deleteList) {
				swapJewel.IfJewelSetToNull (a);
			}
			deleteJewels.DeleteAllJewelsInList (deleteList, false);
			//Debug.Log ("deleteList.Count = " + deleteList.Count);
		}
		else 
			dontDelete = false;
		if (verticalMatch || horizontalMatch) {
			ClearLists ();
			return true;
		} else {
			ClearLists ();
			return false;
		}
	}
	
	private void CheckForHorizontalMatches (GameObject jewel, int i, int j) {
		for (int a = 0; a < horizontalSetArray.Length; a++) {
			if (horizontalSetArray[a] != null && horizontalSetArray[a].Count == 0)
				horizontalMatchCheckCount = a;
		}
		horizontalSetArray[horizontalMatchCheckCount] = new HashSet<GameObject> ();
		int horizontalJewelOffset = 1;
		int horizontalJewelCount = 1;
		jewelDestroyNumber = jewel.GetComponent<RockLevelJewelMovement> ().GetDestroyNumber ();
		while (j - horizontalJewelOffset >= 0 && instantiator.GetJewelGridGameObject (i, j - horizontalJewelOffset) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j - horizontalJewelOffset).tag, jewel.tag)) {
			horizontalSetArray[horizontalMatchCheckCount].Add (instantiator.GetJewelGridGameObject (i, j - horizontalJewelOffset));
			horizontalJewelCount++;
			horizontalJewelOffset++;
		}
		horizontalJewelOffset = 1;
		while (j + horizontalJewelOffset < 9 && instantiator.GetJewelGridGameObject (i, j + horizontalJewelOffset) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i, j + horizontalJewelOffset).tag, jewel.tag)) {
			horizontalSetArray[horizontalMatchCheckCount].Add (instantiator.GetJewelGridGameObject (i, j + horizontalJewelOffset));
			horizontalJewelCount++;
			horizontalJewelOffset++;
		}
		if (horizontalJewelCount >= 3) {
			horizontalMatch = true;
			foreach (GameObject a in horizontalSetArray[horizontalMatchCheckCount]) {
				bool added = deleteList.Add (a);
				if (added && a != null) {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.SetToBeDestroyed ();
					if (jewelMovement.GetMoving ()) {
						exitMatchCheck = true;
						ClearLists ();
						return;
					}
					verticalMatchCheckCount++;
					CheckForVerticalMatches (a, jewelMovement.GetRow (), jewelMovement.GetCol ());
					if (exitMatchCheck) {
						ClearLists ();
						return;
					}
					//					else 
					//						dontDelete = true;
				}
			}
			if (!jewelsThatActivatedCorners.Contains (jewel)) {
				jewelsThatActivatedCorners.Add (jewel);
				soundHandler.PlayPowerUpSound ();
				corners.ActivateCornersPowers (jewel);
			}
		}
		horizontalSetArray[horizontalMatchCheckCount].Clear ();
	}
	
	private void CheckForVerticalMatches (GameObject jewel, int i, int j) {
		for (int a = 0; a < verticalSetArray.Length; a++) {
			if (verticalSetArray[a] != null && verticalSetArray[a].Count == 0)
				verticalMatchCheckCount = a;
		}
		verticalSetArray[verticalMatchCheckCount] = new HashSet<GameObject> ();
		int verticalJewelOffset = 1;
		int verticalJewelCount = 1;
		jewelDestroyNumber = jewel.GetComponent<RockLevelJewelMovement> ().GetDestroyNumber ();
		while (i - verticalJewelOffset >= 0 && instantiator.GetJewelGridGameObject (i - verticalJewelOffset, j) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i - verticalJewelOffset, j).tag, jewel.tag)) {
			verticalSetArray[verticalMatchCheckCount].Add (instantiator.GetJewelGridGameObject (i - verticalJewelOffset, j));
			verticalJewelCount++;
			verticalJewelOffset++;
		}
		verticalJewelOffset = 1;
		while (i + verticalJewelOffset < 9 && instantiator.GetJewelGridGameObject (i + verticalJewelOffset, j) != null && JewelsAreTheSame (instantiator.GetJewelGridGameObject (i + verticalJewelOffset, j).tag, jewel.tag)) {
			verticalSetArray[verticalMatchCheckCount].Add (instantiator.GetJewelGridGameObject (i + verticalJewelOffset, j));
			verticalJewelCount++;
			verticalJewelOffset++;
		}
		if (verticalJewelCount >= 3) {
			verticalMatch = true;
			foreach (GameObject a in verticalSetArray[verticalMatchCheckCount]) {
				bool added = deleteList.Add (a);
				if (added && a != null) {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.SetToBeDestroyed ();
					if (jewelMovement.GetMoving ()) {
						exitMatchCheck = true;
						ClearLists ();
						return;
					}
					horizontalMatchCheckCount++;
					CheckForHorizontalMatches (a, jewelMovement.GetRow (), jewelMovement.GetCol ());
					if (exitMatchCheck) {
						ClearLists ();
						return;
					}
					//					else 
					//						dontDelete = true;
				}
			}
			if (!jewelsThatActivatedCorners.Contains (jewel)) {
				jewelsThatActivatedCorners.Add (jewel);
				soundHandler.PlayPowerUpSound ();
				corners.ActivateCornersPowers (jewel);
			}
		}
		verticalSetArray[verticalMatchCheckCount].Clear ();
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
	
	public bool GetGameStarted () {
		return gameStarted;
	}
	
	public void SetGameStarted (bool gameStarted) {
		this.gameStarted = gameStarted;
	}
}
