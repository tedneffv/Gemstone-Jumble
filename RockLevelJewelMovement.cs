using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelJewelMovement : MonoBehaviour {

	public int row, col;
	bool bounced, moving, checkMatches, jewelAddedToArray, bounceAgain, shuffleJewel, shufflePositionSet, onPlatform, destroyJewelCountdown, inDeleteList, immovableObject, swappingJewel, jewelFalling;
	Vector3 firstTargetPosition;
	Vector2 bounceForce;
	public bool toBeDestroyed, firstBounce;
	public int destroyNumber, bounceNumber;
	static HashSet<GameObject> rocksToBeDestroyed;
	RockLevelCheckForMatches checkForMatch;
	RockLevelTouchHandler touchHandler;
	RockLevelController controller;
	RockLevelInstantiator instantiator;
	RockLevelDeleteJewels deleteJewels;
	SoundController soundController;
	RockLevelShuffleGameBoard shuffleGameboard;
	float cooldown, timeStamp, floatingJewelCooldown, floatingJewelTimestamp;
	RockLevelStarShooter starShooter;
	GameManagerScript gameManager;
	RockLevelSwapJewel swapJewel;
	int nullCount = 0;

	void Awake () {
		bounceForce  = new Vector2 (0, 300);
		cooldown = .01f;
		floatingJewelCooldown = 5;
		floatingJewelTimestamp = Time.time;
		moving = true;
		SetDestroyNumber ();
		rocksToBeDestroyed = new HashSet<GameObject> ();
		controller = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		//		movementChecker = GameObject.Find ("Level Three Controller").GetComponent<LevelThreeJewelsStoppedMoving> ();
		checkForMatch = controller.GetComponent<RockLevelCheckForMatches> ();
		touchHandler = controller.GetComponent<RockLevelTouchHandler> ();
		soundController = controller.GetComponent<SoundController> ();
		instantiator = controller.GetComponent<RockLevelInstantiator> ();
		deleteJewels = controller.GetComponent<RockLevelDeleteJewels> ();
		starShooter = controller.GetComponent<RockLevelStarShooter> ();
		bounceNumber = 0;
		firstBounce = true;
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ();
		shuffleGameboard = controller.GetComponent<RockLevelShuffleGameBoard> ();
		swapJewel = controller.GetComponent<RockLevelSwapJewel> ();
	}
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Time.time > floatingJewelCooldown + floatingJewelTimestamp) {
			floatingJewelTimestamp = Time.time;
			if (instantiator.GetJewelGridGameObject (row, col) != gameObject && !swapJewel.IsASwapJewel (gameObject) && shuffleGameboard.GetShuffleFinished () && tag != "Slug Meal" && !jewelFalling) {
				Debug.Log ("Destroying Floating Jewel");
				Destroy (gameObject);
			}
		}

		if (shuffleJewel) {
			if (transform.position.y < -10)
				Destroy (gameObject);
//			if (!shufflePositionSet) {
//				firstTargetPosition = new Vector3 ((-2.45f + (.6125f * col)), Random.Range (3.5f, 4.5f), 0);
//				shufflePositionSet = true;
//			}
//			transform.Translate (new Vector3 (firstTargetPosition.x - transform.position.x, firstTargetPosition.y - transform.position.y, 0) * Time.deltaTime * 6f);
//			if (Mathf.Abs (firstTargetPosition.x - transform.position.x) < .005f && Mathf.Abs (firstTargetPosition.y - transform.position.y) < .005f) {
//				transform.position = (new Vector3 (firstTargetPosition.x, firstTargetPosition.y, ((-1 * row) - 2)));
//				rigidbody2D.isKinematic = false;
//				shuffleJewel = false;
//				shufflePositionSet = false;
//			}
		}

//		if (destroyJewelCountdown && Time.time >= timeStamp + cooldown) {
//			//Debug.Log ("DESTROYING FLOATING JEWEL");
//			Destroy (gameObject);
//		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (!bounced) {
			if (gameManager.GetSoundState () && JewelClackCounter.GetJewelClackNumber () % 40 == 0) {
				GetComponent<AudioSource> ().Play ();
			}
			GetComponent<Rigidbody2D>().AddForce(bounceForce);
			bounced = true;
		} else {
			if (gameManager.GetSoundState () && firstBounce) {
				firstBounce = false; 
				if (JewelClackCounter.GetJewelClackNumber () % 20 == 0) {
					GetComponent<AudioSource> ().Play ();
				}
			}
			else if (gameManager.GetSoundState () && !firstBounce && !starShooter.GetStartLaunchingStars () && JewelClackCounter.GetJewelClackNumber () % 3 == 0) {
				GetComponent<AudioSource> ().Play ();
			}
			else if (gameManager.GetSoundState () && JewelClackCounter.GetJewelClackNumber () % 6 == 0) {
				GetComponent<AudioSource> ().Play ();
			} 

		} 

		if (bounceAgain) {
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 120));
			bounceNumber++;
			if (bounceNumber > 1)
				bounceAgain = false;
		}
		moving = false;
		checkForMatch.CheckForSwapBack (gameObject, row, col);
//		if (touchHandler.GetGameStarted ())
	}

	public void CheckForMatch () {
		checkForMatch.CheckForSwapBack (gameObject, row, col);
	}

	private bool ImmovableObject (GameObject block) {
		if (!rocksToBeDestroyed.Contains (block) && !deleteJewels.IsElementOfBoulderDeleteList (block) && (block.tag == "Boulder" || block.tag == "Steel Block" || IsBomb (block))) 
			return true;
		return false;
	}

	private bool IsJewel (string jewelTag) {
		return (jewelTag == "Red Block" || jewelTag == "Green Block" || jewelTag == "Yellow Block" || jewelTag == "Purple Block" || jewelTag == "White Block" || jewelTag == "Blue Block");
	}

	public void MoveDown () {
		//Debug.Log ("Jewel row = " + row + " col = " + col + " is attempting to move down");
		for (int i = row; i < 9; i++) {
			if (IsJewel (gameObject.tag) && (instantiator.GetJewelGridGameObject (i, col) == null || IsJewel (instantiator.GetJewelGridGameObject (i, col).tag))) {
				instantiator.SetJewelGridGameObject (row, col, null);
				gameObject.layer = gameObject.layer + (i - row);
				transform.Translate (new Vector3 (0, 0, -(i - row)));
				instantiator.SetJewelGridGameObject (i, col, gameObject);
				row = i;
				return;
			}

		}
//		//Debug.Log ("Called MoveDown for row = " + row + " col = " + col);
//		bool jewelMovedDown = false, nextJewelFound = false;;
//		int tempRow = row - 1;
//		int tempCol = col;
//		int i = 8;
//		while (i >= 0) {
//			if (gameObject.tag == "Boulder" || (IsBomb (gameObject) && previousWasBoulder)) {
//				jewelMovedDown = true;
//				previousWasBoulder = true;
//			}
//			if (!jewelMovedDown && instantiator.GetJewelGridGameObject (i, col) == null) {
//				//Debug.Log ("Moving Jewel Down");
//				instantiator.SetJewelGridGameObject (row, col, null);
//				gameObject.layer = gameObject.layer + (i - row);
//				transform.Translate (new Vector3 (0, 0, -(i - row)));
//				instantiator.SetJewelGridGameObject (i, col, gameObject);
//				row = i;
//				jewelMovedDown = true; 
//				previousWasBoulder = false;
//			} 
//			if (jewelMovedDown && !nextJewelFound && tempRow >= 0) {
//				if (instantiator.GetJewelGridGameObject (tempRow, tempCol) == null)
//					tempRow--;
//				else {
//					instantiator.GetJewelGridGameObject (tempRow, tempCol).GetComponent<RockLevelJewelMovement> ().MoveDown (previousWasBoulder);
//					nextJewelFound = true;
//				}
//			}
//			i--;
//		}

//		if (k < 9 && (!deleteJewels.IsContainedInDeleteList (instantiator.GetJewelGridGameObject (k, col)) && !deleteJewels.IsElementOfBoulderDeleteList (instantiator.GetJewelGridGameObject (k, col)) && 
//	                 !instantiator.GetJewelGridGameObject (k, col).GetComponent<RockLevelJewelMovement> ().GetInDeleteList ())&& (instantiator.GetJewelGridGameObject (k, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (k, col).tag == "Steel Block")) {
//			MoveDown (k + 1);
//			return;
//		}
//		if (instantiator.GetJewelGridGameObject(k, col) == null)  {
//			k++;
//		}
//		gameObject.layer = gameObject.layer + (k - row);
//		transform.Translate (new Vector3 (0, 0, -(k - row)));
//		instantiator.SetJewelGridGameObject (k, col, gameObject);
//		row = k;
//		int immovableObjectCount = 0;
//		if (IsBomb (gameObject)) {
//			int currentBombCount = 0;
//			bool lastBlockWasBomb = false;
////			while (k < 9 && ImmovableObject (instantiator.GetJewelGridGameObject (k + 1, col))) {
////				//Debug.Log ("Moving Bomb Down");
////				if (IsBomb (instantiator.GetJewelGridGameObject (k + 1, col))) {
////					currentBombCount++;
////				} else {
////					currentBombCount = 0;
////				} 
////				k++;
////				immovableObjectCount++;
////			}
////			immovableObjectCount -= currentBombCount;
//			gameObject.layer = gameObject.layer + 1 + immovableObjectCount;
//			transform.Translate (new Vector3 (0, 0, -(1 + immovableObjectCount)));
//			instantiator.SetJewelGridGameObject (row + 1 + immovableObjectCount, col, gameObject);
//			row = (row + 1 + immovableObjectCount);
//		}
//		else {
//			while (k < 9 && !rocksToBeDestroyed.Contains (instantiator.GetJewelGridGameObject (k, col)) && (instantiator.GetJewelGridGameObject (k, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (k, col).tag == "Steel Block" ||
//			                                                                                                (IsBomb (instantiator.GetJewelGridGameObject (k, col)) && (k + 1 < 9 && instantiator.GetJewelGridGameObject (k + 1, col).tag == "Boulder" || 
//			                                                           instantiator.GetJewelGridGameObject (k + 1, col).tag == "Steel Block")))) { /*&&*/ 
//				//		                 !instantiator.GetJewelGridGameObject (k, col).GetComponent<RockLevelJewelMovement> ().GetInDeleteList ()*/) && (instantiator.GetJewelGridGameObject (k, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (k, col).tag == "Steel Block")) {
//				k++;
//				immovableObjectCount++;
//			}
//			gameObject.layer = gameObject.layer + 1 + immovableObjectCount;
//			transform.Translate (new Vector3 (0, 0, -(1 + immovableObjectCount)));
//			instantiator.SetJewelGridGameObject (row + 1 + immovableObjectCount, col, gameObject);
//			row = (row + 1 + immovableObjectCount);
//		}
	}
	
	void OnCollisionStay2D (Collision2D collision) {
		if (bounced)
			onPlatform = true;
	}

	bool IsBomb (GameObject jewel) {
		if (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "White Bomb" || jewel.tag == "Purple Bomb")
			return true;
		return false;
	}

	public void AddToRocksToBeDestroyed (GameObject rock) {
		rocksToBeDestroyed.Add (rock);
	}

	public bool IsContainedInRocksToBeDestoryed (GameObject rock) {
		return rocksToBeDestroyed.Contains (rock);
	}

	void OnCollisionExit2D (Collision2D collision) {
		onPlatform = false;
		moving = true;
	}

	public void StartDestroyCountdown () {
//		destroyJewelCountdown = true;
//		timeStamp = Time.time;
//		Destroy (gameObject);
	}

	public void SetToBeDestroyed () {
		toBeDestroyed = true;
	}

	public void SetNotToBeDestroyed () {
		toBeDestroyed = false;
	}

	public bool GetToBeDestroyed () {
		return toBeDestroyed;
	}

	public bool GetOnPlatform () {
		return onPlatform;
	}

	public void SetRow (int row) {
		this.row = row;
	}
	
	public void SetCol (int col) {
		this.col = col;
	}
	
	public int GetRow () {
		return row;
	}
	
	public int GetCol () {
		return col;
	}
	
	public void SetMoving (bool moving) {
		this.moving = moving;
	}
	
	public bool GetMoving () {
		return moving;
	}

	public void SetBounced (bool bounced) {
		this.bounced = bounced;
	}
	
	public bool GetBounced () {
		return bounced;
	}
	
	public int GetDestroyNumber () {
		return destroyNumber;
	}
	
	public bool JewelAddedToArray () {
		return jewelAddedToArray;
	}

	public void SetShuffleJewel (bool shuffleJewel) {
		this.shuffleJewel = shuffleJewel;
	}
	
	public void SetAddedToArray (bool jewelAddedToArray) {
		this.jewelAddedToArray = jewelAddedToArray;
	}

	public void SetBounceAgain (bool bounceAgain) {
		this.bounceAgain = bounceAgain;
	}

	public void SetInDeleteList (bool inDeleteList) {
		inDeleteList = inDeleteList;
	}

	public bool GetInDeleteList () {
		return inDeleteList;
	}

	public void SetSwappingJewel (bool swappingJewel) {
		this.swappingJewel = swappingJewel;
	}

	public bool GetSwappingJewel () {
		return swappingJewel;
	}

	public void SetJewelFalling (bool jewelFalling) {
		this.jewelFalling = jewelFalling;
	}

	public void SetDestroyNumber () {
		switch (tag) {
		case "Blue Block":
		case "Blue Bomb": destroyNumber = 0; break;
		case "Green Block":
		case "Green Bomb": destroyNumber = 1; break;
		case "Yellow Block":
		case "Orange Bomb": destroyNumber = 2; break;
		case "Purple Block":
		case "Purple Bomb": destroyNumber = 3; break;
		case "Red Block":
		case "Red Bomb": destroyNumber = 4; break;
		case "White Block":
		case "White Bomb": destroyNumber = 5;; break;
		}
	}
}
