using UnityEngine;
using System.Collections;

public class RockLevelShuffleGameBoard : MonoBehaviour {

	GameObject[,] tempJewelGrid;
	RockLevelJewelMovement jewelMovement;
	RockLevelInstantiator instantiator;
	RockLevelCheckForMatches checkForMatches;
	RockLevelMovementChecker movementChecker;
	RockLevelTouchHandler touchHandler;
	RockLevelNoMatchChecker noMatchChecker;
	RockLevelSwapJewel swapJewel;
	bool shuffleFinished, turnTouchBackOn, matchPossibleCheck, stopShuffle;
	float timeStamp, cooldown;
	public GameObject levelCompleteShade;

	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
		movementChecker = gameObject.GetComponent<RockLevelMovementChecker> ();
		touchHandler = gameObject.GetComponent<RockLevelTouchHandler> ();
		noMatchChecker = gameObject.GetComponent<RockLevelNoMatchChecker> ();
		swapJewel = gameObject.GetComponent<RockLevelSwapJewel> ();
		cooldown = 2f;
		shuffleFinished = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (turnTouchBackOn && Time.time < timeStamp + cooldown) {
			checkForMatches.SetGameStarted (true);
			touchHandler.UnpauseTouch ();
			turnTouchBackOn = false;
		}
	}

	public void SetShuffleFinished (bool shuffleFinished) {
		this.shuffleFinished = shuffleFinished;
	}

	public bool GetShuffleFinished () {
		return shuffleFinished;
	}

	public void ResetTheGameboard () {
		touchHandler.PauseTouch ();
		checkForMatches.SetGameStarted (false);
		instantiator.SetSwappingJewels (true);
		turnTouchBackOn = true;
		shuffleFinished = false;
		tempJewelGrid = new GameObject[9, 9];
		KeepBombsAndRocksTheSame ();
		ShuffleRemainingJewels ();
		instantiator.SetSwappingJewels (false);
	}

	private void KeepBombsAndRocksTheSame () {
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (IsBomb (instantiator.GetJewelGridGameObject (i, j)) || instantiator.GetJewelGridGameObject (i, j).tag == "Boulder" || instantiator.GetJewelGridGameObject (i, j).tag == "Steel Block")
					tempJewelGrid[i, j] = instantiator.GetJewelGridGameObject (i, j);
			}
		}
	}

	private void ShuffleRemainingJewels () {

		if (stopShuffle)
			return;

		if (GameObject.Find ("Time Bomb Identification") != null)
			GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted(false);

		if (!MatchPossibleCheck() && (gameObject.GetComponent<RockLevelBombHandler> ().GetBombListCount () != 0 || GameObject.Find ("Jewel Collector") != null)) {
			Debug.Log ("Ending Game From ShuffleJewels");
			if (GameObject.Find ("Time Bomb Identification") != null) {
				GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted (false);
			}
			stopShuffle = true;
			touchHandler.SetGameStarted (false);
			checkForMatches.SetGameStarted (false);
			GameObject instantiatedShade = (GameObject)Instantiate (levelCompleteShade);
			instantiatedShade.GetComponent<DarkenOnInstantiaton> ().SetLevelComplete (false);
			return;
		}

		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (IsJewel (instantiator.GetJewelGridGameObject (i, j)) && !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (i, j))) {
//					instantiator.GetJewelGridGameObject (i, j).rigidbody2D.isKinematic = true;
					DropJewel (instantiator.GetJewelGridGameObject (i, j));
					movementChecker.SetGridStaticToFalse ();
//					AddJewelToRandomSpot (instantiator.GetJewelGridGameObject (i, j));
				}
			}
		}

		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (IsJewel (instantiator.GetJewelGridGameObject (i, j)) && !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (i, j))) {
					instantiator.InstantiateShuffledJewels (i, j);
				}
//				jewelMovement = instantiator.GetJewelGridGameObject (i, j).GetComponent<RockLevelJewelMovement> ();
			}
		}
		EnsureMatch ();
		if (GameObject.Find ("Time Bomb Identification") != null) {
			GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().ResetTimestamp ();
			GameObject.Find ("Time Bomb Identification").GetComponent<TimeBombController> ().SetGameStarted (true);
		}
	}

	void EnsureMatch () {

		if (noMatchChecker.CheckForMatchesOnInstantiation ()) {
			//Debug.Log ("Getting New Jewel For Match");
			for (int i = 0; i < 9; i++) {
				for (int j = 0; j < 9; j++) {
					if (IsJewel (instantiator.GetJewelGridGameObject (i, j))) {
						Destroy (instantiator.GetJewelGridGameObject (i, j));
						instantiator.InstantiateShuffledJewels (i, j);
					}
				}
			}
		}

		if (noMatchChecker.CheckForMatchesOnInstantiation ()) {
			EnsureMatch ();
			return;
		} else {
			timeStamp = Time.time;
			shuffleFinished = false;
			matchPossibleCheck = false;
		}
	}

	bool MatchPossibleCheck () {

		if (!matchPossibleCheck) {
			bool offsetJewelAvailable = false;
			int jewelsInARow = 0;
			for (int i = 0; i < 9; i++) {
				for (int j = 0; j < 9; j++) {
					if (IsJewel (instantiator.GetJewelGridGameObject (i, j))) {
						jewelsInARow++;
						if (!offsetJewelAvailable && i - 1 >= 0 && IsJewel (instantiator.GetJewelGridGameObject (i - 1, j))) {
							offsetJewelAvailable = true;
						}
						if (!offsetJewelAvailable && i + 1 < 9 && IsJewel (instantiator.GetJewelGridGameObject (i + 1, j))) {
							offsetJewelAvailable = true;
						}
						
						if (jewelsInARow >= 3 && offsetJewelAvailable) {
							matchPossibleCheck = true;
							return true;
						}
						if (jewelsInARow > 3) {
							matchPossibleCheck = true;
							return true;
						}
					} else {
						offsetJewelAvailable = false;
						jewelsInARow = 0;
					}
				}
			}
			
			if (!matchPossibleCheck) {
				offsetJewelAvailable = false;
				jewelsInARow = 0;
				for (int j = 0; j < 9; j++) {
					for (int i = 0; i < 9; i++) {
						if (IsJewel (instantiator.GetJewelGridGameObject (i, j))) {
							jewelsInARow++;
							if (!offsetJewelAvailable && j - 1 >= 0 && IsJewel (instantiator.GetJewelGridGameObject (i, j - 1))) 
								offsetJewelAvailable = true;
							if (!offsetJewelAvailable && j + 1 < 9 && IsJewel (instantiator.GetJewelGridGameObject (i, j + 1)))
								offsetJewelAvailable = true;
							if (jewelsInARow >= 3 && offsetJewelAvailable) {
								matchPossibleCheck = true;
								return true;
							}
							if (jewelsInARow > 3) {
								matchPossibleCheck = true;
								return true;
							}
						} else {
							offsetJewelAvailable = false;
							jewelsInARow = 0;
						}
					}
				}
			}
		}
		return false;
	}

	void AddNewJewels () {
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (instantiator.GetJewelGridGameObject (i, j) == null)
					instantiator.InstantiateShuffledJewels (i, j);
			}
		}
	}

	private void DropJewel (GameObject jewel) {
		jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
		jewelMovement.SetJewelFalling (true);
		jewelMovement.SetShuffleJewel (true);
		jewel.GetComponent<Rigidbody2D>().gravityScale = Random.Range (3, 6);
		jewel.layer = 0;
	}
	private void AddJewelToRandomSpot (GameObject jewel) {
		int randomRow = Random.Range (0, 9);
		int randomCol = Random.Range (0, 9);
		for (int j = randomCol; j < 9; j++) {
			if (tempJewelGrid[randomRow, j] == null) {
				jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
				jewelMovement.SetRow (randomRow);
				jewelMovement.SetCol (j);
				jewel.layer = randomRow + 17;
				tempJewelGrid[randomRow, j] = jewel;
				jewelMovement.SetShuffleJewel (true);
				return;
			}
		}

		for (int i = randomRow + 1; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (tempJewelGrid[i, j] == null) {
					jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.SetRow (i);
					jewelMovement.SetCol (j);
					jewel.layer = i + 17;
					tempJewelGrid[i, j] = jewel;
					jewelMovement.SetShuffleJewel (true);
					return;
				}
			}
		}

		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (tempJewelGrid[i, j] == null) {
					jewelMovement = jewel.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.SetRow (i);
					jewelMovement.SetCol (j);
					jewel.layer = i + 17;
					tempJewelGrid[i, j] = jewel;
					jewelMovement.SetShuffleJewel (true);
					return;
				}
			}
		}
	}

	private bool IsBomb (GameObject possibleBomb) {
		if (possibleBomb.tag == "Green Bomb" || possibleBomb.tag == "Blue Bomb" || possibleBomb.tag == "Red Bomb" || possibleBomb.tag == "Orange Bomb" || possibleBomb.tag == "White Bomb" || possibleBomb.tag == "Purple Bomb")
			return true;
		return false;
	}

	private bool IsJewel (GameObject possibleJewel) {
		if (possibleJewel.tag == "Green Block" || possibleJewel.tag == "Blue Block" || possibleJewel.tag == "Red Block" || possibleJewel.tag == "Yellow Block" || possibleJewel.tag == "White Block" || possibleJewel.tag == "Purple Block")
			return true;
		return false;
	}
}
