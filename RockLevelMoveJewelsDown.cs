using UnityEngine;
using System.Collections;

public class RockLevelMoveJewelsDown : MonoBehaviour {

	RockLevelJewelMovement jewelMovement;
	RockLevelInstantiator instantiator;
	RockLevelDeleteJewels deleteJewels;
	RockLevelController controller;
	RockLevelSwapJewel swapJewel;
	RockLevelCheckForMatches checkForMatches;
	int immovableObjectCount;
	bool immovableObjectBelow, setImmovableObjectBelow;

	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		controller = gameObject.GetComponent<RockLevelController> ();
		swapJewel = gameObject.GetComponent<RockLevelSwapJewel> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
	}

	bool IsBomb (GameObject jewel) {
		if (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "White Bomb" || jewel.tag == "Purple Bomb")
			return true;
		return false;
	}

	bool NeverMoveDownObject (GameObject possibleBoulder) {
		if (possibleBoulder.name == "Rock 3 Chain(Clone)" || possibleBoulder.name == "Rock 2 Chain(Clone)" || possibleBoulder.name == "Rock 1 Chain(Clone)" || possibleBoulder.name == "Rock Blocked(Clone)" || possibleBoulder.tag == "Steel Block")
			return true;
		return false;
	}

	bool SometimesMoveDownObject (GameObject possibleBoulder) {
		if ((possibleBoulder.tag == "Boulder" || possibleBoulder.tag == "Steel Block") && !possibleBoulder.GetComponent<RockLevelJewelMovement> ().IsContainedInRocksToBeDestoryed (possibleBoulder))
			return true;
		return false;
	}

	public void MoveJewelsAboveDown (int col, int row) {
//		int movableObjectPlaceHolder = 0, immovableObjectCount = 0;
//		bool jewelFound = false, bombFound = false;
//		GameObject tempObject = null;
//
//		for (int i = 0; i < 9; i++) {
//			if ((instantiator.GetJewelGridGameObject(i, col) != null && IsJewel (instantiator.GetJewelGridGameObject (i, col).tag)) && tempObject == null) {
//				tempObject = instantiator.GetJewelGridGameObject (i, col);
//				continue;
//			}
//			else if ((instantiator.GetJewelGridGameObject (i, col) == null || (instantiator.GetJewelGridGameObject (i, col) != null && IsJewel (instantiator.GetJewelGridGameObject (i, col).tag))) && tempObject != null) {
//				jewelMovement = tempObject.GetComponent<RockLevelJewelMovement> ();
//				tempObject.layer = 17 + i;
//				tempObject.transform.Translate (new Vector3 (0, 0, -(i - jewelMovement.GetRow ())));
//				instantiator.SetJewelGridGameObject (i, col, tempObject);
//				instantiator.SetJewelGridGameObject (jewelMovement.GetRow (), col, null);
//				jewelMovement.SetRow (i);
//				tempObject = null;
//			}
//		}
		int nullObjectCount = 0;
		int movedPassedImmovableObjects = 0;
		int immovableObjectCount = 0;
		bool lastBlockWasBoulder = false;
		int i = row, returnRow = 0;
		for (; i >= 0; i--) {
			if (instantiator.GetJewelGridGameObject (i, col) != null && (instantiator.GetJewelGridGameObject (i, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (i, col).tag == "Steel Block" /*||
			                                                             (PowerStarTracker.ContainsJewel (instantiator.GetJewelGridGameObject (i, col)) && swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (i, col))) /*|| swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (i, col))
			                                                             || instantiator.GetJewelGridGameObject (i, col).GetComponent<RockLevelJewelMovement> ().GetToBeDestroyed () || 
			                                                             checkForMatches.deleteList.Contains (instantiator.GetJewelGridGameObject (i, col)*/)) {
				lastBlockWasBoulder = true;
				immovableObjectCount++;
			}
			if (lastBlockWasBoulder && instantiator.GetJewelGridGameObject (i, col) != null && IsBomb (instantiator.GetJewelGridGameObject (i, col))) {
				immovableObjectCount++;
			}
			if (instantiator.GetJewelGridGameObject (i, col) == null) {
				nullObjectCount++;
			}
			if (nullObjectCount > 0 && instantiator.GetJewelGridGameObject (i, col) != null && instantiator.GetJewelGridGameObject (i, col).tag != "Boulder" && instantiator.GetJewelGridGameObject (i, col).tag != "Steel Block") {
				if ((PowerStarTracker.ContainsJewel (instantiator.GetJewelGridGameObject (i, col)) && swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (i, col))))
					break;
				if (IsBomb (instantiator.GetJewelGridGameObject (i, col)) && lastBlockWasBoulder) {
					lastBlockWasBoulder = true;
					continue;
				}
//				else if (immovableObjectCount > 0 && movedPassedImmovableObjects < nullObjectCount) {
				instantiator.GetJewelGridGameObject (i, col).layer = instantiator.GetJewelGridGameObject (i, col).layer + nullObjectCount + immovableObjectCount;
				jewelMovement = instantiator.GetJewelGridGameObject (i, col).GetComponent<RockLevelJewelMovement> ();
				jewelMovement.SetMoving (true);
				instantiator.GetJewelGridGameObject (i, col).transform.Translate (new Vector3 (0, 0, - (nullObjectCount + immovableObjectCount)));
				instantiator.SetJewelGridGameObject (i + nullObjectCount + immovableObjectCount, col, instantiator.GetJewelGridGameObject (i, col));
				instantiator.SetJewelGridGameObject (i, col, null);
				jewelMovement.SetRow (i + nullObjectCount + immovableObjectCount);
				movedPassedImmovableObjects++;
	//				if (immovableObjectCount > 0 && movedPassedImmovableObjects >= nullObjectCount) {
				immovableObjectCount = 0;
				movedPassedImmovableObjects = 0;
				nullObjectCount = 0;
				i = i + 1;
	//				}
				lastBlockWasBoulder = false;
//					if (movedPassedImmovableObjects >= nullObjectCount) {
//						immovableObjectCount = 0;
//						movedPassedImmovableObjects = 0;
//					}
//				} else {
//						immovableObjectCount = 0;
////						movedPassedImmovableObjects = 0;
//					instantiator.GetJewelGridGameObject (i, col).layer = instantiator.GetJewelGridGameObject (i, col).layer + nullObjectCount;
//					jewelMovement = instantiator.GetJewelGridGameObject (i, col).GetComponent<RockLevelJewelMovement> ();
//					instantiator.GetJewelGridGameObject (i, col).transform.Translate (new Vector3 (0, 0, - (nullObjectCount)));
//					instantiator.SetJewelGridGameObject (i + nullObjectCount, col, instantiator.GetJewelGridGameObject (i, col));
//					instantiator.SetJewelGridGameObject (i, col, null);
//					jewelMovement.SetRow (i + nullObjectCount);
//					lastBlockWasBoulder = false;
//				}
				//				lastBlockWasBoulder = false;
			}
		}
//		for (int j = 0; j < nullObjectCount; j++) {
//			instantiator.InstantiateSingleJewels (j, col);
//		}
//		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
//		if (jewel.name == ("Rock Blocked(Clone)")) {
//			jewelMovement.AddToRocksToBeDestroyed (jewel);
//		}
//		int i = jewelMovement.GetRow () - 1;
//		int j = jewelMovement.GetCol ();
//		immovableObjectCount = 0;
//		while (i >= 0) {
//			if (!NeverMoveDownObject (instantiator.GetJewelGridGameObject (i, j))) {
//				if (IsBomb (instantiator.GetJewelGridGameObject (i, j)) && (instantiator.GetJewelGridGameObject (i + 1, j) != null && NeverMoveDownObject (instantiator.GetJewelGridGameObject (i + 1, j)) && SometimesMoveDownObject (instantiator.GetJewelGridGameObject (i + 1, j))))
//				{}
////				if (SometimesMoveDownObject (instantiator.GetJewelGridGameObject (i, j)) 
////				    || ((IsBomb (instantiator.GetJewelGridGameObject (i, j)) &&
////				     (NeverMoveDownObject (instantiator.GetJewelGridGameObject (i + 1, j)) || 
////				 SometimesMoveDownObject (instantiator.GetJewelGridGameObject (i + 1, j))))))
////				{}
//				else {
//					int k = i + 1;
//					instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ().MoveDown (true);
//
////					immovableObjectCount = 0;
////					while (k < 9 && !deleteJewels.IsContainedInDeleteList (instantiator.GetJewelGridGameObject (k, j)) && (instantiator.GetJewelGridGameObject (k, j).tag == "Boulder" || instantiator.GetJewelGridGameObject (k, j).tag == "Steel Block")) {
////						k++;
////						immovableObjectCount++;
////					}
////					instantiator.GetJewelGridGameObject (i, j).layer = (instantiator.GetJewelGridGameObject (i, j).layer + 1 + immovableObjectCount);
////					jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
////					instantiator.GetJewelGridGameObject (i, j).transform.Translate (new Vector3 (0, 0, -(1 + immovableObjectCount)));
////					instantiator.SetJewelGridGameObject (jewelMovement.GetRow () + 1 + immovableObjectCount, jewelMovement.GetCol (), tempJewel);
////					jewelMovement.SetRow (jewelMovement.GetRow () + 1 + immovableObjectCount);
//				}
//			} 
//			i--;
//		}
	}

//	public void MoveJewelsAboveDown (GameObject jewel) {
//		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
//		int i = jewelMovement.GetRow () - 1;
//		int j = jewelMovement.GetCol ();
//		immovableObjectCount = 0;
//		while (i >= 0) {
//			if (!NeverMoveDownObject (instantiator.GetJewelGridGameObject (i, j))) {
//				if (SometimesMoveDownObject (instantiator.GetJewelGridGameObject (i, j)) 
//				    || ((IsBomb (instantiator.GetJewelGridGameObject (i, j)) &&
//				    (NeverMoveDownObject (instantiator.GetJewelGridGameObject (i + 1, j)) || 
//				    SometimesMoveDownObject (instantiator.GetJewelGridGameObject (i + 1, j))))))
//				    {}
//				else {
//					GameObject tempJewel = instantiator.GetJewelGridGameObject (i, j);
//					int k = i + 1;
//					immovableObjectCount = 0;
//					while (k < 9 && !deleteJewels.IsContainedInDeleteList (instantiator.GetJewelGridGameObject (k, j)) && (instantiator.GetJewelGridGameObject (k, j).tag == "Boulder" || instantiator.GetJewelGridGameObject (k, j).tag == "Steel Block")) {
//						k++;
//						immovableObjectCount++;
//					}
//					instantiator.GetJewelGridGameObject (i, j).layer = (instantiator.GetJewelGridGameObject (i, j).layer + 1 + immovableObjectCount);
//					jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
//					instantiator.GetJewelGridGameObject (i, j).transform.Translate (new Vector3 (0, 0, -(1 + immovableObjectCount)));
//					instantiator.SetJewelGridGameObject (jewelMovement.GetRow () + 1 + immovableObjectCount, jewelMovement.GetCol (), tempJewel);
//					jewelMovement.SetRow (jewelMovement.GetRow () + 1 + immovableObjectCount);
////					if (setImmovableObjectBelow) {
////						immovableObjectBelow = true;
////						setImmovableObjectBelow = false;
////					} else {
////						immovableObjectBelow = false;
////						immovableObjectCount = 0;
////					}
//				}
//			} /*else {*/
////				immovableObjectCount++;
////				setImmovableObjectBelow = true;
////			}
//			i--;
//		}
//	}
//	
//	public void MoveJewelsAboveDown (GameObject jewel) {
//		GameObject tempJewel;
//		bool steelBlocksStarted = false;
//		int row, steelBlockRow = 0;
//		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
//		int i = jewelMovement.GetRow () - 1;
//		int j = jewelMovement.GetCol ();
//		if (!deleteJewels.IsContainedInDeleteList (instantiator.GetJewelGridGameObject (steelBlockRow, j)) && (instantiator.GetJewelGridGameObject (steelBlockRow, j).tag == "Steel Block" || instantiator.GetJewelGridGameObject (steelBlockRow , j).tag == "Boulder" || IsBomb (instantiator.GetJewelGridGameObject (steelBlockRow, j)))) {
//			steelBlockRow++;
//			while (!deleteJewels.IsContainedInDeleteList (instantiator.GetJewelGridGameObject (steelBlockRow, j)) && (steelBlockRow < 9 && (instantiator.GetJewelGridGameObject (steelBlockRow, j).tag == "Steel Block" || /*instantiator.GetJewelGridGameObject (steelBlockRow, j).tag == "Boulder"*/ instantiator.name == "Rock 1 Chain(Clone)" || instantiator.name == "Rock 2" || IsBomb (instantiator.GetJewelGridGameObject (steelBlockRow, j))))) {
//				steelBlockRow++;
//			}
//			steelBlockRow--;
//		}
//		while (i >= 0) {
//			if ((instantiator.GetJewelGridGameObject (i, j).tag == "Steel Block" || /*instantiator.GetJewelGridGameObject (i, j).tag == "Boulder"*/ instantiator.GetJewelGridGameObject (i, j).name == "Rock 1 Chain(Clone)" || 
//			    instantiator.GetJewelGridGameObject (i, j).name == "Rock 2 Chain(Clone)" || instantiator.GetJewelGridGameObject (i, j).name == "Rock 3 Chain(Clone)")&& !deleteJewels.IsContainedInDeleteList (instantiator.GetJewelGridGameObject (i, j))) {
//				steelBlocksStarted = true;
//				if (i > steelBlockRow)
//					immovableObjectCount++;
//			} else if (IsBomb (instantiator.GetJewelGridGameObject (i, j)) && steelBlocksStarted) {
//				if (i > steelBlockRow)
//					immovableObjectCount++;
//			}
//			else if (immovableObjectCount > 0){
//				steelBlocksStarted = false;
//				jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
//				jewelMovement.SetMoving (true);
//				jewelMovement.SetRow (jewelMovement.GetRow () + 1 + immovableObjectCount);
//				instantiator.GetJewelGridGameObject (i, j).layer = instantiator.GetJewelGridGameObject (i, j).layer + 1 + immovableObjectCount;
//				instantiator.GetJewelGridGameObject (i, j).transform.Translate (new Vector3 (0, 0, -1 - immovableObjectCount));
//				instantiator.SetJewelGridGameObject (jewelMovement.GetRow (), j, instantiator.GetJewelGridGameObject (i, j));
//				immovableObjectCount = 0;
//			}
//			else {
//				steelBlocksStarted = false;
//				jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
//				jewelMovement.SetMoving (true);
//				jewelMovement.SetRow (jewelMovement.GetRow () + 1);
//				instantiator.GetJewelGridGameObject (i, j).layer++;
//				instantiator.GetJewelGridGameObject (i, j).transform.Translate (new Vector3 (0, 0, -1));
//				instantiator.SetJewelGridGameObject (jewelMovement.GetRow (), j, instantiator.GetJewelGridGameObject (i, j));
//			}
//			i--;
//		}
//	}	

	private bool IsJewel (string jewelTag) {
		return (jewelTag == "Red Block" || jewelTag == "Green Block" || jewelTag == "Yellow Block" || jewelTag == "Purple Block" || jewelTag == "White Block" || jewelTag == "Blue Block");
	}
}
