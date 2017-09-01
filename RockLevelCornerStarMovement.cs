using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelCornerStarMovement : MonoBehaviour {

	float targetX, targetY, timeStamp, cooldown, oppositeX, oppositeY;
	bool horizontalPositive, horizontalNegative, verticalPositive, verticalNegative, movementStart, stampTimeStamp, timeForDelete, addedCornerJewel, fireStar, arraysFilled, changeJewelName, soundPlayed;
	HashSet<GameObject> deleteList;
	RockLevelCorners corners;
	GameObject cornerJewel;
	RockLevelDeleteJewels deleteJewels;
	int nextJewelToExplodeRow, nextJewelToExplodeCol, jewelDeletedCounter, arrayCount;
	RockLevelInstantiator instantiator;
	GameObject[] positiveHorizontalDeletes, negativeHorizontalDeletes, positiveVerticalDeletes, negativeVerticalDeletes;
	RockLevelMoveJewelsDown moveJewelsDown;
	RockLevelTouchHandler touchHandler;
	RockLevelController levelOneController;
	RockLevelJewelMovement jewelMovement;
	SoundHandler soundHandler;
	int soundNumber;
	
	// Use this for initialization
	void Start () {
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		moveJewelsDown = GameObject.Find ("Level Controller").GetComponent<RockLevelMoveJewelsDown> ();
		corners = GameObject.Find ("Level Controller").GetComponent<RockLevelCorners> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		levelOneController = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		if (horizontalPositive) {
			oppositeX = transform.position.x - 1;
			oppositeY = transform.position.y;
		}
		else if (horizontalNegative) {
			oppositeX = transform.position.x + 1;
			oppositeY = transform.position.y;
		}
		else if (verticalPositive) {
			oppositeY = transform.position.y - 1;
			oppositeX = transform.position.x;
		}
		else if (verticalNegative) {
			oppositeY = transform.position.y + 1;
			oppositeX = transform.position.x;
		}
		
		jewelDeletedCounter = 0;
		arrayCount = 0;
		soundNumber = 0;
		GetComponent<Rigidbody2D>().AddTorque (540);
		deleteList = new HashSet<GameObject> ();
		timeForDelete = true;
		cooldown = .5f;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Mathf.Abs (transform.position.x) > 10 || Mathf.Abs (transform.position.y) > 10) {
//			corners.GetCornerStarList ().Remove (gameObject);
//		}

		if (movementStart && !fireStar) {
			transform.Translate (new Vector3 ((oppositeX - transform.position.x) * Time.deltaTime * 5f, (oppositeY - transform.position.y) * Time.deltaTime * 5f), Space.World);
			if (Mathf.Abs(transform.position.x - oppositeX) < .05f && Mathf.Abs(transform.position.y - oppositeY) < .05f)
				fireStar = true;
		}
		
		if (movementStart && fireStar) {
			if (!soundPlayed) {
				soundHandler.PlayPowerShot ();
				soundPlayed = true;
			}
			transform.Translate (new Vector3((targetX - transform.position.x) * Time.deltaTime * .25f, (targetY - transform.position.y) * Time.deltaTime * .25f, 0), Space.World);
		}
//		if (Mathf.Abs (transform.position.x) > 99) {
//			Destroy (gameObject);
//		}
//		if (Mathf.Abs (transform.position.y) > 99) {
//			Destroy (gameObject);
//		}
		if (arraysFilled && (Mathf.Abs (transform.position.x) > 10) && (horizontalPositive || horizontalNegative)) {
			if (positiveHorizontalDeletes != null && positiveHorizontalDeletes.Length >= arrayCount) {
				corners.GetCornerStarList ().Remove (gameObject);
				Destroy (gameObject);
			}
			else if (negativeHorizontalDeletes != null && negativeHorizontalDeletes.Length >= arrayCount) {
				corners.GetCornerStarList ().Remove (gameObject);
				Destroy (gameObject);
			}
		} 
		else if (arraysFilled && (Mathf.Abs (transform.position.y) > 10) && (verticalPositive || verticalNegative)) {
			if (positiveVerticalDeletes != null && positiveVerticalDeletes.Length >= arrayCount) {
				corners.GetCornerStarList ().Remove (gameObject);
				Destroy (gameObject);
			}
			else if (negativeVerticalDeletes != null && negativeVerticalDeletes.Length >= arrayCount) {
				corners.GetCornerStarList ().Remove (gameObject);
				Destroy (gameObject);
			}
		}
		
		if (movementStart && fireStar) {
			if (!arraysFilled) {
				FillArrays ();
				arraysFilled = true;
			}
			
			if (timeForDelete && horizontalPositive && arrayCount < 9 && positiveHorizontalDeletes[arrayCount] != null && transform.position.x > positiveHorizontalDeletes[arrayCount].transform.position.x) {
				timeForDelete = false;
				if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
					jewelMovement = positiveHorizontalDeletes[arrayCount].GetComponent<RockLevelJewelMovement> ();
					jewelMovement.StartDestroyCountdown ();
				}
				if (positiveHorizontalDeletes[arrayCount] != null) {
					deleteList.Add (positiveHorizontalDeletes[arrayCount]);
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
				}
				deleteList.Clear ();
				arrayCount++;
			} else if (horizontalPositive && arrayCount < 9 && positiveHorizontalDeletes[arrayCount] == null) {
				arrayCount++;
			} else 
				timeForDelete = true;
			
			if (horizontalNegative && arrayCount < 9 && negativeHorizontalDeletes[arrayCount] != null && transform.position.x < negativeHorizontalDeletes[arrayCount].transform.position.x) {
				if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
					jewelMovement = negativeHorizontalDeletes[arrayCount].GetComponent<RockLevelJewelMovement> ();
					jewelMovement.StartDestroyCountdown ();
				}
				if (negativeHorizontalDeletes[arrayCount] != null) {
					deleteList.Add (negativeHorizontalDeletes[arrayCount]);
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
				}
				deleteList.Clear ();
				arrayCount++;
			} else if (horizontalNegative && arrayCount < 9 && negativeHorizontalDeletes[arrayCount] == null) {
				arrayCount++;
			}
			
			if (timeForDelete && verticalNegative && arrayCount < 9 && negativeVerticalDeletes[arrayCount] != null && transform.position.y < negativeVerticalDeletes[arrayCount].transform.position.y) {
				timeForDelete = false;
				if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
					jewelMovement = positiveVerticalDeletes[arrayCount].GetComponent<RockLevelJewelMovement> ();
					jewelMovement.StartDestroyCountdown ();
				}
				if (negativeVerticalDeletes != null) {
					deleteList.Add (negativeVerticalDeletes[arrayCount]);
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
				}
				deleteList.Clear ();
				arrayCount++;
			} else if (verticalNegative && arrayCount < 9 && negativeVerticalDeletes[arrayCount] == null) {
				arrayCount++;
			} else 
				timeForDelete = true;
			
			if (verticalPositive && arrayCount < 9 && positiveVerticalDeletes[arrayCount] != null && transform.position.y > positiveVerticalDeletes[arrayCount].transform.position.y) {
				if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
					jewelMovement = negativeVerticalDeletes[arrayCount].GetComponent<RockLevelJewelMovement> ();
					jewelMovement.StartDestroyCountdown ();
				}
				if (positiveVerticalDeletes[arrayCount] != null) {
					deleteList.Add (positiveVerticalDeletes[arrayCount]);
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
				}
				deleteList.Clear ();
				arrayCount++;
			} else if (verticalPositive && arrayCount < 9 && positiveVerticalDeletes[arrayCount] == null) {
				arrayCount++;
			}
		}
	}
	
	private void FillArrays () {
		RockLevelJewelMovement jewelMovement;
		if (horizontalPositive) {
			positiveHorizontalDeletes = new GameObject[9];
			int count;
			if (!RockLevelCorners.cornerJewelAdded) {
				count = nextJewelToExplodeCol - 1;
				RockLevelCorners.cornerJewelAdded = true;
			} else 
				count = nextJewelToExplodeCol;
			int index = 0;
			while (count < 9) {
				addedCornerJewel = true;
				if (instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count) != null) {
					jewelMovement = instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count).GetComponent<RockLevelJewelMovement> ();
					if (!jewelMovement.JewelAddedToArray () && PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count))) {
						positiveHorizontalDeletes[index] = instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count);
						jewelMovement.SetAddedToArray (true);
					}
				}
				index++;
				count++;
			}
		} else if (horizontalNegative) {
			negativeHorizontalDeletes = new GameObject[9];
			int count;
			if (!RockLevelCorners.cornerJewelAdded) {
				count = nextJewelToExplodeCol + 1;
				RockLevelCorners.cornerJewelAdded = true;
			} else 
				count = nextJewelToExplodeCol;
			int index = 0; 
			while (count >= 0) {
				if (instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count) != null) {
					jewelMovement = instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count).GetComponent<RockLevelJewelMovement> ();
					if (!jewelMovement.JewelAddedToArray () && PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count))) {
						negativeHorizontalDeletes[index] = instantiator.GetJewelGridGameObject (nextJewelToExplodeRow, count);
						jewelMovement.SetAddedToArray (true);
					}
				}
				index++;
				count--;
			}
		} else if (verticalPositive) {
			positiveVerticalDeletes = new GameObject[9];
			int count;
			if (!RockLevelCorners.cornerJewelAdded) {
				count = nextJewelToExplodeRow + 1;
				RockLevelCorners.cornerJewelAdded = true;
			} else
				count = nextJewelToExplodeRow;
			int index = 0;
			while (count >= 0) {
				if (instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol) != null) {
					jewelMovement = instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol).GetComponent<RockLevelJewelMovement> ();
					if (!jewelMovement.JewelAddedToArray () && PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol))) {
						positiveVerticalDeletes[index] = instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol);
						jewelMovement.SetAddedToArray (true);
					}
				}
				index++;
				count--;
			}
		} else if (verticalNegative) {
			negativeVerticalDeletes = new GameObject[9];
			int count;
			if (!RockLevelCorners.cornerJewelAdded)  {
				count = nextJewelToExplodeRow - 1;
				RockLevelCorners.cornerJewelAdded = true;
			} else
				count = nextJewelToExplodeRow;
			int index = 0;
			while (count < 9) {
				if (instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol) != null);
				jewelMovement = instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol).GetComponent<RockLevelJewelMovement> ();
				if (!jewelMovement.JewelAddedToArray () && PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol))) {
					negativeVerticalDeletes[index] = instantiator.GetJewelGridGameObject (count, nextJewelToExplodeCol);
					jewelMovement.SetAddedToArray (true);
				}
				index++;
				count++;
			}
		}
		changeJewelName = true;
	}
	
	public void SetCornerJewel (GameObject jewel) {
		cornerJewel = jewel;
		RockLevelCorners.cornerJewelDestroyed = false;
	}
	
	public void SetTargetX (float xNumber) {
		targetX = xNumber;
	}
	
	public void SetTargetY (float yNumber) {
		targetY = yNumber;
	}
	
	public void ToggleMovementStart (bool trueOrFalse) {
		movementStart = trueOrFalse;
		if (!stampTimeStamp) {
			timeStamp = Time.time;
			stampTimeStamp = true;
		}
	}
	
	public void SetJewelToExplodeRowAndCol (int row, int col) {
		nextJewelToExplodeRow = row;
		nextJewelToExplodeCol = col;
	}
	
	public void SetHorizontalPositive (bool trueOrFalse) {
		horizontalPositive = trueOrFalse;
	}
	
	public void SetHorizontalNegative (bool trueOrFalse) {
		horizontalNegative = trueOrFalse;
	}
	
	public void SetVerticalPositive (bool trueOrFalse) {
		verticalPositive = trueOrFalse;
	}
	
	public void SetVerticalNegative (bool trueOrFalse) {
		verticalNegative = trueOrFalse;
	}

	public void SwapIfInDeleteArrays (GameObject oldBomb, GameObject newBomb) {
		for (int i = 0; i < 9; i++) {
			if (positiveHorizontalDeletes != null && positiveHorizontalDeletes[i] != null && positiveHorizontalDeletes[i] == oldBomb) {
				positiveHorizontalDeletes[i] = newBomb;
				return;
			}
			if (negativeHorizontalDeletes != null && negativeHorizontalDeletes[i] != null && negativeHorizontalDeletes[i] == oldBomb) {
				negativeHorizontalDeletes[i] = newBomb;
				return;
			}
			if (positiveVerticalDeletes != null && positiveVerticalDeletes[i] != null && positiveVerticalDeletes[i] == oldBomb) {
				positiveVerticalDeletes[i] = newBomb;
				return;
			}
			if (negativeVerticalDeletes != null && negativeVerticalDeletes[i] != null && negativeVerticalDeletes[i] == oldBomb) {
				negativeVerticalDeletes[i] = newBomb;
				return;
			}
		}
	}

	private bool IsJewel (string jewelTag) {
		return (jewelTag == "Red Block" || jewelTag == "Green Block" || jewelTag == "Yellow Block" || jewelTag == "Purple Block" || jewelTag == "White Block" || jewelTag == "Blue Block");
	}
	
	bool IsBomb (GameObject jewel) {
		if (jewel != null && (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "White Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "Purple Bomb"))
			return true;
		return false;
	}

}
