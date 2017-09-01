using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelMatchAssistant : MonoBehaviour {
	int randomRow, randomCol, originalRow, originalCol;
	bool resetRandoms, gameStarted, tutorialLevel;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement;
	HashSet<GameObject> matchList, secondaryMatchList;
	Vector2 bounceForce;

	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		matchList = new HashSet<GameObject> ();
		secondaryMatchList = new HashSet<GameObject> ();
		bounceForce = new Vector2 (0, 300);
		resetRandoms = true;
		gameStarted = true;
		if (GameObject.Find ("Mountain Level One ID") != null || GameObject.Find ("Mountain Level Two ID") != null || GameObject.Find ("Mountain Level Three ID") || GameObject.Find ("Mountain Level Four ID") ||
		    GameObject.Find ("Mountain Level Ten ID") != null || GameObject.Find ("Cabin Level Two ID") != null)
			tutorialLevel = true;
	}


	
	public void FindMatchForPlayer () {
		if (!gameStarted || tutorialLevel)
			return;
		if (resetRandoms) {
			randomRow = Random.Range (0, 9);
			randomCol = Random.Range (0, 9);
			resetRandoms = false;
		}
		originalRow = randomRow;
		originalCol = randomCol;

		for (int j = randomCol; j < 9; j++) {
			if (CheckForSwipeUpHorizontalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
			if (CheckForSwipeDownHorizontalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
			if (CheckForSwipeUpVerticalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
			if (CheckForSwipeDownVerticalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return; 
			if (CheckForSwipeLeftVerticalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
			if (CheckForSwipeRightVerticalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
			if (CheckForSwipeLeftHorizontalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
			if (CheckForSwipeRightHorizontalMatch (randomRow, j, instantiator.GetJewelGridGameObject (randomRow, j)))
				return;
		}

		for (int i = randomRow + 1; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (CheckForSwipeUpHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeDownHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeUpVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeDownVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return; 
				if (CheckForSwipeLeftVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeRightVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeLeftHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeRightHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
			}
		}

		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (CheckForSwipeUpHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeDownHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeUpVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeDownVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return; 
				if (CheckForSwipeLeftVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeRightVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeLeftHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (CheckForSwipeRightHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return;
				if (i == originalRow && j == originalCol) {
					//Debug.Log ("NO POSSIBLE MOVES SON!!!");
					return;
				}
			}
		}
	}

	public void SetGameStarted (bool gameStarted) {
		Debug.Log ("Setting game started to " + gameStarted);
		this.gameStarted = gameStarted;
	}

	private bool CheckForSwipeUpHorizontalMatch (int row, int col, GameObject jewel) {
		int oldRow = row;
		row = row - 1;
		if (row >= 0) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int colOffset = 1;
			matchList.Add (instantiator.GetJewelGridGameObject (oldRow, col));
			while (col - colOffset >= 0 && instantiator.GetJewelGridGameObject (row, col - colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col - colOffset).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row, col - colOffset));
				colOffset++;
			}
			colOffset = 1;
			while (col + colOffset < 9 && instantiator.GetJewelGridGameObject (row, col + colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col + colOffset).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row, col + colOffset));
				colOffset++;
			}
			if (matchList.Count > 2) {
				return CheckForSwipeUpVerticalMatch (oldRow, col, jewel);
			}
		}
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeLeftVerticalMatch (int row, int col, GameObject jewel) {
		int oldCol = col;
		col = col - 1;
		if (col >= 0) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int rowOffset = 1;
			matchList.Add (instantiator.GetJewelGridGameObject(row, oldCol));
			while (row - rowOffset >= 0 && instantiator.GetJewelGridGameObject (row - rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row - rowOffset, col).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row - rowOffset, col));
				rowOffset++;
			}
			rowOffset = 1;
			while (row + rowOffset < 9 && instantiator.GetJewelGridGameObject (row + rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row + rowOffset, col).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row + rowOffset, col));
				rowOffset++;
			}
			if (matchList.Count > 2) {
				return CheckForSwipeLeftHorizontalMatch (row, oldCol, jewel);
			}
		}
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeRightVerticalMatch (int row, int col, GameObject jewel) {
		int oldCol = col;
		col = col + 1;
		if (col < 9) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int rowOffset = 1;
			matchList.Add (instantiator.GetJewelGridGameObject (row, oldCol));
			while (row - rowOffset >= 0 && instantiator.GetJewelGridGameObject (row - rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row - rowOffset, col).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row - rowOffset, col));
				rowOffset++;
			}
			rowOffset = 1;
			while (row + rowOffset < 9 && instantiator.GetJewelGridGameObject (row + rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row + rowOffset, col).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row + rowOffset, col));
				rowOffset++;
			}
			if (matchList.Count > 2) {
				return CheckForSwipeRightHorizontalMatch (row, oldCol, jewel);
			}
		}
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeDownHorizontalMatch (int row, int col, GameObject jewel) {
		int oldRow = row;
		row = row + 1;
		if (row < 9) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int colOffset = 1;
			matchList.Add (instantiator.GetJewelGridGameObject (oldRow, col));
			while (col - colOffset >= 0 && instantiator.GetJewelGridGameObject (row, col - colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col - colOffset).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row, col - colOffset));
				colOffset++;
			}
			colOffset = 1;
			while (col + colOffset < 9 && instantiator.GetJewelGridGameObject (row, col + colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col + colOffset).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row, col + colOffset));
				colOffset++;
			}
			if (matchList.Count > 2) {
				return CheckForSwipeDownVerticalMatch (oldRow, col, jewel);
			}
		}
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeUpVerticalMatch (int row, int col, GameObject jewel) {
		int oldRow = row;
		row = row - 1;
		if (row >= 0) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int rowOffset = 1;
			secondaryMatchList.Add (instantiator.GetJewelGridGameObject (oldRow, col));
			while (row - rowOffset >= 0 && instantiator.GetJewelGridGameObject (row - rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row - rowOffset, col).tag)) {
				secondaryMatchList.Add (instantiator.GetJewelGridGameObject (row - rowOffset, col));
				rowOffset++;
			}
		}
		if (secondaryMatchList.Count > 2) {
			foreach (GameObject a in secondaryMatchList) {
				matchList.Add (a);
			}
		}
		if (matchList.Count > 2) {
			foreach (GameObject a in matchList) {
				jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
				a.GetComponent<Rigidbody2D>().AddForce (bounceForce);
				jewelMovement.SetBounceAgain (true);
				jewelMovement.SetBounced (false);
			}
			secondaryMatchList.Clear ();
			matchList.Clear ();
			return true;
		}
		secondaryMatchList.Clear ();
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeLeftHorizontalMatch (int row, int col, GameObject jewel) {
		int oldCol = col;
		col = col - 1;
		if (col >= 0) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int colOffset = 1;
			secondaryMatchList.Add (instantiator.GetJewelGridGameObject (row, oldCol));
			while (col - colOffset >= 0 && instantiator.GetJewelGridGameObject (row, col - colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col - colOffset).tag)) {
				secondaryMatchList.Add (instantiator.GetJewelGridGameObject (row, col - colOffset));
				colOffset++;
			}
			if (secondaryMatchList.Count > 2) {
				foreach (GameObject a in secondaryMatchList) {
					matchList.Add (a);
				}
			}
			if (matchList.Count > 2) {
				foreach (GameObject a in matchList) {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					a.GetComponent<Rigidbody2D>().AddForce (bounceForce);
					jewelMovement.SetBounceAgain (true);
					jewelMovement.SetBounced (false);
				}
				secondaryMatchList.Clear ();
				matchList.Clear ();
				return true;
			}
		}
		secondaryMatchList.Clear ();
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeRightHorizontalMatch (int row, int col, GameObject jewel) {
		int oldCol = col;
		col = col + 1;
		if (col < 9) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int colOffset = 1;
			secondaryMatchList.Add (instantiator.GetJewelGridGameObject (row, oldCol));
			while (col + colOffset < 9 && instantiator.GetJewelGridGameObject (row, col + colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col + colOffset).tag)) {
				secondaryMatchList.Add (instantiator.GetJewelGridGameObject (row, col + colOffset));
				colOffset++;
			}
			if (secondaryMatchList.Count > 2) {
				foreach (GameObject a in secondaryMatchList) {
					matchList.Add (a);
				}
			}
			if (matchList.Count > 2) {
				foreach (GameObject a in matchList) {
					jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
					a.GetComponent<Rigidbody2D>().AddForce (bounceForce);
					jewelMovement.SetBounceAgain (true);
					jewelMovement.SetBounced (false);
				}
				secondaryMatchList.Clear ();
				matchList.Clear ();
				return true;
			}
		}
		secondaryMatchList.Clear ();
		matchList.Clear ();
		return false;
	}

	private bool CheckForSwipeDownVerticalMatch (int row, int col, GameObject jewel) {
		int oldRow = row;
		row = row + 1;
		if (row < 9) {
			if (instantiator.GetJewelGridGameObject (row, col).tag == "Boulder" || instantiator.GetJewelGridGameObject (row, col).tag == "Steel Block")
				return false;
			int rowOffset = 1;
			secondaryMatchList.Add (instantiator.GetJewelGridGameObject (oldRow, col));
			while (row + rowOffset >= 0 && instantiator.GetJewelGridGameObject (row + rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row + rowOffset, col).tag)) {
				secondaryMatchList.Add (instantiator.GetJewelGridGameObject (row + rowOffset, col));
				rowOffset++;
			}
		}
		if (secondaryMatchList.Count > 2) {
			foreach (GameObject a in secondaryMatchList) {
				matchList.Add (a);
			}
		}
		if (matchList.Count > 2) {
			foreach (GameObject a in matchList) {
				jewelMovement = a.GetComponent<RockLevelJewelMovement> ();
				jewelMovement.SetMoving (true);
				a.GetComponent<Rigidbody2D>().AddForce (bounceForce);
				jewelMovement.SetBounceAgain (true);
//				jewelMovement.SetBounced (false);
			}
			secondaryMatchList.Clear ();
			matchList.Clear ();
			return true;
		}
		secondaryMatchList.Clear ();
		matchList.Clear ();
		return false;
	}

	public void SetResetRandoms (bool resetRandoms) {
		this.resetRandoms = resetRandoms;
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

	public void SetTutorialLevel (bool tutorialLevel) {
		this.tutorialLevel = tutorialLevel;
	}

}
