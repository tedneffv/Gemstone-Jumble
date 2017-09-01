using UnityEngine;
using System.Collections;

public class RockLevelSwapJewel : MonoBehaviour {

	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelOneMovement, jewelTwoMovement;
	RockLevelCheckForMatches checkForMatches;
	RockLevelBombHandler bombHandler;
	RockLevelDeleteJewels deleteJewels;
	RockLevelTouchHandler touchHandler;
	MoveNumberHandler moveNumberHandler;
	public bool restrictMovements, jewelCollectorLevel;
	Vector3 jewelOneOriginalPos, jewelTwoOriginalPos;
	float translationMultiplier, distanceToTarget;
	int firstJewelLayer, secondJewelLayer;
	RockLevelMovementChecker stoppedMoving;
	bool startSwap, swapJewelsBack, pauseTouchForSwap, firstSwapPerformed, timeBombLevel, tutorialLevel, tutorialLevel2;
	GameObject jewel1, jewel2;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		jewel1 = null;
		jewel2 = null;
		startSwap = false;
		swapJewelsBack = false;
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
		bombHandler = gameObject.GetComponent<RockLevelBombHandler> ();
		stoppedMoving = gameObject.GetComponent<RockLevelMovementChecker> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		timeBombLevel = (GameObject.Find ("Time Bomb Identification") != null);
		translationMultiplier = .35f;
		distanceToTarget = .01f;
		firstSwapPerformed = false;
		if (GameObject.Find ("Mountain Level Two ID") != null) {
			tutorialLevel2 = true;
		}
		if (GameObject.Find ("Jewel Collection Level ID") != null) {
			jewelCollectorLevel = true;
			moveNumberHandler = GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (startSwap) {
			if (jewel1 == null || jewel2 == null)
				return;
			jewel1.transform.Translate (new Vector3 (-(jewel1.transform.position.x - jewelTwoOriginalPos.x) * translationMultiplier, -(jewel1.transform.position.y - jewelTwoOriginalPos.y) * translationMultiplier, 0));
			jewel2.transform.Translate (new Vector3 (-(jewel2.transform.position.x - jewelOneOriginalPos.x) * translationMultiplier, -(jewel2.transform.position.y - jewelOneOriginalPos.y) * translationMultiplier, 0));
			if (Mathf.Abs (jewel1.transform.position.x - jewelTwoOriginalPos.x) < distanceToTarget && Mathf.Abs (jewel1.transform.position.y - jewelTwoOriginalPos.y) < distanceToTarget &&
			    Mathf.Abs (jewel2.transform.position.x - jewelOneOriginalPos.x) < distanceToTarget && Mathf.Abs (jewel2.transform.position.y - jewelOneOriginalPos.y) < distanceToTarget) {
				jewel1.transform.position = jewelTwoOriginalPos;
				jewel2.transform.position = jewelOneOriginalPos;
				jewel1.layer = secondJewelLayer;
				jewel2.layer = firstJewelLayer;
				startSwap = false;
				//				firstJewelBlockMovement.moving = false;
				//				secondJewelBlockMovement.moving = false;
				pauseTouchForSwap = false;
			}
			if (!startSwap)  {
				jewel1.GetComponent<Rigidbody2D>().isKinematic = false;
				jewel2.GetComponent<Rigidbody2D>().isKinematic = false;
				instantiator.SetJewelGridGameObject (jewelOneMovement.GetRow (), jewelOneMovement.GetCol (), jewel1);
				instantiator.SetJewelGridGameObject (jewelTwoMovement.GetRow (), jewelTwoMovement.GetCol (), jewel2);
				jewelOneMovement = jewel1.GetComponent<RockLevelJewelMovement> ();
				jewelTwoMovement = jewel2.GetComponent<RockLevelJewelMovement> ();
				jewelOneMovement.SetMoving (false);
				jewelTwoMovement.SetMoving (false);
				if (jewelOneMovement.GetRow () < jewelTwoMovement.GetRow ()) {
					if (!checkForMatches.CheckForSwapBack (jewel1, jewelOneMovement.GetRow (), jewelOneMovement.GetCol ()) && !checkForMatches.CheckForSwapBack (jewel2, jewelTwoMovement.GetRow (), jewelTwoMovement.GetCol ())) {
						if (!swapJewelsBack && restrictMovements) {
							Swap (jewel1, jewel2, true);
						}
						else {  
							if (jewel1 != null) {
								PowerStarTracker.RemoveFromHashSet (jewel1);
								jewel1 = null;
								jewelOneMovement.SetSwappingJewel (false);
							}
							if (jewel2 != null) {
								PowerStarTracker.RemoveFromHashSet (jewel2);
								jewel2 = null;
								jewelTwoMovement.SetSwappingJewel (false);
							}
						}
						return; 
					}
					deleteJewels.SetSwapComplete (true);
					if (!timeBombLevel && !jewelCollectorLevel) {
						bombHandler.DecreaseAllBombsInList ();
					}
					else if (jewelCollectorLevel) {
						Debug.Log ("Calling moveNumberHandler.SubtractOneFromMoveNumber ()");
						moveNumberHandler.SubtractOneFromMoveNumber ();
					}
					if (!firstSwapPerformed) {
						GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (true);
						firstSwapPerformed = true;
						//Debug.Log ("First Swap Performed");
					}
					if (jewel1 != null) {
						PowerStarTracker.RemoveFromHashSet (jewel1);
						jewel1 = null;
						jewelOneMovement.SetSwappingJewel (false);
					}
					if (jewel2 != null) {
						PowerStarTracker.RemoveFromHashSet (jewel2);
						jewel2 = null;
						jewelTwoMovement.SetSwappingJewel (false);
					}
//					stoppedMoving.SetGridStaticToFalse ();
				} else {
					if (!checkForMatches.CheckForSwapBack (jewel2, jewelTwoMovement.GetRow (), jewelTwoMovement.GetCol ()) && !checkForMatches.CheckForSwapBack (jewel1, jewelOneMovement.GetRow (), jewelOneMovement.GetCol ())) {
						if (!swapJewelsBack && restrictMovements) {
							Swap (jewel1, jewel2, true);
						} 
						else {
							if (jewel1 != null) {
								PowerStarTracker.RemoveFromHashSet (jewel1);
								jewel1 = null;
								jewelOneMovement.SetSwappingJewel (false);
							}
							if (jewel2 != null) {
								PowerStarTracker.RemoveFromHashSet (jewel2);
								jewel2 = null;
								jewelTwoMovement.SetSwappingJewel (false);
							}
						}
						checkForMatches.deleteList.Clear ();
						return; 
					}
					deleteJewels.SetSwapComplete (true);
					if (!timeBombLevel && !jewelCollectorLevel) {
						bombHandler.DecreaseAllBombsInList ();
					} else if (jewelCollectorLevel) {
						moveNumberHandler.SubtractOneFromMoveNumber ();
					}
					if (!firstSwapPerformed) {
						GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (true);
						firstSwapPerformed = true;
						//Debug.Log ("First Swap Performed");
					}
					if (jewel1 != null) {
						PowerStarTracker.RemoveFromHashSet (jewel1);
						jewel1 = null;
						jewelOneMovement.SetSwappingJewel (false);
					}
					if (jewel2 != null) {
						PowerStarTracker.RemoveFromHashSet (jewel2);
						jewel2 = null;
						jewelTwoMovement.SetSwappingJewel (false);
					}
//					stoppedMoving.SetGridStaticToFalse ();
				}
			}
		}
	}
	
	public void Swap (GameObject firstJewel, GameObject secondJewel, bool swapBack) {
//		if (!swapBack && (PowerStarTracker.ContainsJewel (firstJewel) || PowerStarTracker.ContainsJewel (secondJewel))) {
//			return;
//		}

		jewel1 = firstJewel;
		jewel2 = secondJewel;
		if (firstJewel == null || secondJewel == null)
			return;
		jewelOneMovement = firstJewel.GetComponent<RockLevelJewelMovement> ();
		jewelOneMovement.SetSwappingJewel (true);
		jewelTwoMovement = secondJewel.GetComponent<RockLevelJewelMovement> ();
		jewelTwoMovement.SetSwappingJewel (true);

		PowerStarTracker.AddToHashSet (firstJewel);
		PowerStarTracker.AddToHashSet (secondJewel);
		soundHandler.ResetJewelBreakNumber ();
		soundHandler.PlayJewelSwap ();
		deleteJewels.PunchAssistanceTimeStamp ();
		swapJewelsBack = swapBack;
		pauseTouchForSwap = true;
		int tempRow, tempCol;
		deleteJewels.StampAssistantTimeCard ();
		//		stoppedMoving.SetGridStaticToFalse ();
		
		if (jewelOneMovement.GetMoving () || jewelTwoMovement.GetMoving () || jewelOneMovement.GetToBeDestroyed () || jewelTwoMovement.GetToBeDestroyed ())  {
			if (jewel1 != null) {
				PowerStarTracker.RemoveFromHashSet (jewel1);
				jewel1 = null;
				jewelOneMovement.SetSwappingJewel (false);
			}
			if (jewel2 != null) {
				PowerStarTracker.RemoveFromHashSet (jewel2);
				jewel2 = null;
				jewelTwoMovement.SetSwappingJewel (false);
			}
			return;
		}
		
		jewelOneMovement.SetMoving (true);
		jewelTwoMovement.SetMoving (true);
		
		tempRow = jewelOneMovement.GetRow ();
		tempCol = jewelOneMovement.GetCol ();;
		
		jewelOneOriginalPos = firstJewel.transform.position;
		jewelTwoOriginalPos = secondJewel.transform.position;
		
		if (IsBomb (firstJewel) && !IsBomb (secondJewel)) {
			jewelTwoOriginalPos.x = jewelTwoOriginalPos.x + .035f;
			jewelOneOriginalPos.x = jewelOneOriginalPos.x - .035f;
		}
		else if (IsBomb (secondJewel) && !IsBomb (firstJewel)) {
			jewelOneOriginalPos.x = jewelOneOriginalPos.x + .035f;
			jewelTwoOriginalPos.x = jewelTwoOriginalPos.x - .035f;
		}
		
		jewelOneMovement.SetRow (jewelTwoMovement.GetRow ());
		jewelOneMovement.SetCol (jewelTwoMovement.GetCol ());
		jewelTwoMovement.SetRow (tempRow);
		jewelTwoMovement.SetCol (tempCol);
		
		jewel1.GetComponent<Rigidbody2D>().isKinematic = true;
		jewel2.GetComponent<Rigidbody2D>().isKinematic = true;
		firstJewelLayer = jewel1.layer;
		secondJewelLayer = jewel2.layer;
		jewel1.layer = 0;
		jewel2.layer = 0;
		startSwap = true;
	}
	
	private bool IsBomb (GameObject jewel) {
		//Debug.Log ("Inside IsBomb");
		if (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "Purple Bomb" || jewel.tag == "White Bomb")
			return true;
		return false;
	}
	
	public bool GetPauseTouchForSwap () {
		return pauseTouchForSwap;
	}

	public bool IsASwapJewel (GameObject possibleSwapJewel) {
		if (jewel1 == possibleSwapJewel || jewel2 == possibleSwapJewel)
			return true;
		return false;
	}

	public void IfJewelSetToNull (GameObject jewel) {
		if (jewel1 == jewel || jewel2 == jewel) {
			PowerStarTracker.RemoveFromHashSet (jewel1);
			PowerStarTracker.RemoveFromHashSet (jewel2);
			jewel1 = null;
			jewel2 = null;
		}
	}

	public bool GetSwapStart () {
		if (!startSwap) {
			jewel1 = null;
			jewel2 = null;
		}
		return startSwap;
	}

	public bool GetFirstSwapPerformed () {
		return firstSwapPerformed;
	}

	public void SetFirstSwapPerformed (bool firstSwapPerformed) {
		this.firstSwapPerformed = firstSwapPerformed;
	}
}
