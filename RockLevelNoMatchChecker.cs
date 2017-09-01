using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelNoMatchChecker : MonoBehaviour {

	RockLevelInstantiator instantiator;
	HashSet<GameObject> matchList;
	RockLevelShuffleGameBoard shuffle;
	NoMatchesSlider noMatchesSlider;
	RockLevelBombHandler bombHandler;
	public GameObject noMatchesBanner;
	bool jewelCollectorLevel;

	// Use this for initialization
	void Start () {
		instantiator = gameObject.GetComponent<RockLevelInstantiator> ();
		shuffle = gameObject.GetComponent<RockLevelShuffleGameBoard> ();
		bombHandler = gameObject.GetComponent<RockLevelBombHandler> ();
		noMatchesSlider = GameObject.Find ("No Matches Banner").GetComponent<NoMatchesSlider> ();
		matchList = new HashSet<GameObject> ();
		if (GameObject.Find ("Jewel Collector") != null)
			jewelCollectorLevel = true;
	}

	public bool CheckForNoMatches () {

		if (bombHandler.GetBombListCount () == 0 && !jewelCollectorLevel) {
			return false;
		}

		if (bombHandler.ZeroBombInList ()) {
			return false;
		}
		
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (CheckForSwipeUpHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeLeftVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeRightVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeDownHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeUpVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeLeftHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeRightHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeDownVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
			}
		}
		shuffle.ResetTheGameboard ();
		noMatchesSlider.SetMoveDown (true);
		//Debug.Log ("NO MAS MATCHES SON!!");
		return true;
	}

	public bool CheckForNoMatchesWithoutShuffle () {
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (CheckForSwipeUpHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeLeftVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeRightVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeDownHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeUpVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeLeftHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeRightHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeDownVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
			}
		}
		return true;
	}

	public bool CheckForNoMatchSingleJewel (int i, int j) {
		if (CheckForSwipeUpHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeLeftVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeRightVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeDownHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeUpVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeLeftHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeRightHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		if (CheckForSwipeDownVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
			return false;
		return true;
	}

	public bool CheckForMatchesOnInstantiation () {
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				if (CheckForSwipeUpHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeLeftVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeRightVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeDownHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeUpVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeLeftHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeRightHorizontalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
				if (CheckForSwipeDownVerticalMatch (i, j, instantiator.GetJewelGridGameObject (i, j)))
					return false;
			}
		}
		//Debug.Log ("NO MAS MATCHES SON!!");
		return true;
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
				matchList.Clear ();
				return true;
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
				matchList.Clear ();
				return true;
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
				matchList.Clear ();
				return true;
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
				matchList.Clear ();
				return true;
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
			matchList.Add (instantiator.GetJewelGridGameObject (oldRow, col));
			while (row - rowOffset >= 0 && instantiator.GetJewelGridGameObject (row - rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row - rowOffset, col).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row - rowOffset, col));
				rowOffset++;
			}
		}

		if (matchList.Count > 2) {
			matchList.Clear ();
			return true;
		}
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
			matchList.Add (instantiator.GetJewelGridGameObject (row, oldCol));
			while (col - colOffset >= 0 && instantiator.GetJewelGridGameObject (row, col - colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col - colOffset).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row, col - colOffset));
				colOffset++;
			}
			if (matchList.Count > 2) {
				matchList.Clear ();
				return true;
			}
		}
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
			matchList.Add (instantiator.GetJewelGridGameObject (row, oldCol));
			while (col + colOffset < 9 && instantiator.GetJewelGridGameObject (row, col + colOffset) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row, col + colOffset).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row, col + colOffset));
				colOffset++;
			}
			if (matchList.Count > 2) {
				matchList.Clear ();
				return true;
			}
		}
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
			matchList.Add (instantiator.GetJewelGridGameObject (oldRow, col));
			while (row + rowOffset >= 0 && instantiator.GetJewelGridGameObject (row + rowOffset, col) != null && JewelsAreTheSame (jewel.tag, instantiator.GetJewelGridGameObject (row + rowOffset, col).tag)) {
				matchList.Add (instantiator.GetJewelGridGameObject (row + rowOffset, col));
				rowOffset++;
			}
		}
		if (matchList.Count > 2) {
			matchList.Clear ();
			return true;
		}
		matchList.Clear ();
		return false;
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
