using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlugMovementController : MonoBehaviour {

	public GameObject boulder, boulderStage2, slug, boulderStage3, levelCompleteShade, boulderStage4;
	public Sprite whiteSprite, yellowSprite, redSprite;
	int row, col, initialDirection, directionCount, oldRow, oldCol;
	RockLevelInstantiator instantiator;
	bool move, beingDeleted;
	float targetY, targetX;
	GameObject tempBoulder, destructionJewel, currentSlug, currentBoulder;
	RockLevelJewelMovement jewelMovement;
	RockLevelSwapJewel swapJewel;
	slugDirection movementDirection;

	enum slugDirection {
		up,
		right,
		down,
		left
	};
	
	// Use this for initialization
	void Start () {
		if (name == "Slug(Clone)") 
			currentBoulder = boulder;
		else if (name == "Slug Stage 2(Clone)")
			currentBoulder = boulderStage2;
		else if (name == "Slug Stage 3(Clone)")
			currentBoulder = boulderStage3;
		else if (name == "Slug Stage 4(Clone)")
			currentBoulder = boulderStage4;
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		jewelMovement = gameObject.GetComponent<RockLevelJewelMovement> ();
		swapJewel = GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ();
	}

	void OnDestroy () {
		if (destructionJewel != null)
			Destroy (destructionJewel);
	}
	
	void FixedUpdate () {
		if (move) {
			transform.Translate (new Vector3 (targetX - transform.position.x, targetY - transform.position.y, 0) * Time.deltaTime * 15);
			if (Mathf.Abs (targetX - transform.position.x) < .01f && Mathf.Abs (targetY - transform.position.y) < .01f) {
				transform.position = new Vector3 (targetX, targetY, -.5f);
				move = false;
				switch (movementDirection) {
				case slugDirection.up: gameObject.layer--;/* transform.Translate (new Vector3 (0, 0, 1));*/ break;
				case slugDirection.down: gameObject.layer++; break;
				}
				Destroy (destructionJewel);
				gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
		}
	}

	bool IsBomb (GameObject possibleBomb) {
		if (possibleBomb.tag == "Blue Bomb" || possibleBomb.tag == "Green Bomb" || possibleBomb.tag == "Orange Bomb" || possibleBomb.tag == "Purple Bomb" || possibleBomb.tag == "Red Bomb" || possibleBomb.tag == "White Bomb")
			return true;
		return false;
	}

	public void MoveSlug () {
		if (move || beingDeleted) {
			return;
		}
		directionCount = 0;
		movementDirection = GetRandomSlugDirection ();

		if (jewelMovement == null)
			jewelMovement = GetComponent<RockLevelJewelMovement> ();

		row = jewelMovement.GetRow ();
		col = jewelMovement.GetCol ();
		oldRow = row;
		oldCol = col;

		directionCount = 0;

		do {
			if (movementDirection == slugDirection.up) {
				if (row - 1 >= 0 && instantiator.GetJewelGridGameObject (row - 1, col) != null && IsJewel (instantiator.GetJewelGridGameObject (row - 1, col)) && instantiator.GetJewelGridGameObject (row - 1, col).name != "Slug Meal" 
				    && !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row - 1, col))) {
					instantiator.GetJewelGridGameObject (row - 1, col).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x;
					targetY = transform.position.y + .61f;
					jewelMovement.SetRow (row - 1);
					row--;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.right;
			} else if (movementDirection == slugDirection.right) {
				if (col + 1 < 9 && instantiator.GetJewelGridGameObject (row, col + 1) != null && IsJewel (instantiator.GetJewelGridGameObject (row, col + 1)) && instantiator.GetJewelGridGameObject (row, col + 1).name != "Slug Meal"
				    && !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row, col + 1))) {
					instantiator.GetJewelGridGameObject (row, col + 1).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x + .6125f;
					targetY = transform.position.y;
					jewelMovement.SetCol (col + 1);
					col++;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.down;
			} else if (movementDirection == slugDirection.down) {
				if (row + 1 < 9 && instantiator.GetJewelGridGameObject (row + 1, col) != null && IsJewel (instantiator.GetJewelGridGameObject (row + 1, col)) && instantiator.GetJewelGridGameObject (row + 1, col).name != "Slug Meal"
				    && !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row + 1, col))) {
					instantiator.GetJewelGridGameObject (row + 1, col).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x;
					targetY = transform.position.y - .61f;
					jewelMovement.SetRow (row + 1);
					row++;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.left;
			} else if (movementDirection == slugDirection.left) {
				if (col - 1 >= 0 && instantiator.GetJewelGridGameObject (row, col - 1) != null && IsJewel (instantiator.GetJewelGridGameObject (row, col - 1)) && instantiator.GetJewelGridGameObject (row, col - 1).name != "Slug Meal"
				    && !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row , col - 1))) {
					instantiator.GetJewelGridGameObject (row, col - 1).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x - .6125f;
					targetY = transform.position.y;
					jewelMovement.SetCol (col - 1);
					col--;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.up;
			}
			directionCount++;
		} while (directionCount < 4);

		MoveNonJewelDirection ();

	}

	void MoveNonJewelDirection () {
		directionCount = 0;
		while (true) {
			if (movementDirection == slugDirection.up) {
				if (row - 1 >= 0 && instantiator.GetJewelGridGameObject (row - 1, col) != null && instantiator.GetJewelGridGameObject (row - 1, col).tag != "Steel Block" && instantiator.GetJewelGridGameObject (row - 1, col).name != "Slug(Clone)" 
				    && instantiator.GetJewelGridGameObject (row - 1, col).name != "Slug Stage 2(Clone)" && instantiator.GetJewelGridGameObject (row - 1, col).name != "Slug Stage 3(Clone)"  && instantiator.GetJewelGridGameObject (row - 1, col).name != "Slug Stage 4(Clone)" && 
				    !Bomb (instantiator.GetJewelGridGameObject (row - 1, col))
				    && instantiator.GetJewelGridGameObject (row - 1, col).tag != "Slug Meal" && 
				    !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row - 1, col)) && instantiator.GetJewelGridGameObject (row - 1, col).transform.childCount == 0) {
					instantiator.GetJewelGridGameObject (row - 1, col).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x;
					targetY = transform.position.y + .61f;
					jewelMovement.SetRow (row - 1);
					row--;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.right;
			} else if (movementDirection == slugDirection.right) {
				if (col + 1 < 9 && instantiator.GetJewelGridGameObject (row, col + 1) != null && instantiator.GetJewelGridGameObject (row, col + 1).tag != "Steel Block" && instantiator.GetJewelGridGameObject (row, col + 1).name != "Slug(Clone)" 
				    && instantiator.GetJewelGridGameObject (row, col + 1).name != "Slug Stage 2(Clone)" && instantiator.GetJewelGridGameObject (row, col + 1).name != "Slug Stage 3(Clone)" && instantiator.GetJewelGridGameObject (row, col + 1).name != "Slug Stage 4(Clone)" && 
				    !Bomb (instantiator.GetJewelGridGameObject (row, col + 1))
				    && instantiator.GetJewelGridGameObject (row, col + 1).tag != "Slug Meal" && 
				    !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row, col + 1)) && instantiator.GetJewelGridGameObject (row, col + 1).transform.childCount == 0) {
					instantiator.GetJewelGridGameObject (row, col + 1).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x + .6125f;
					targetY = transform.position.y;
					jewelMovement.SetCol (col + 1);
					col++;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.down;
			} else if (movementDirection == slugDirection.down) {
				if (row + 1 < 9 && instantiator.GetJewelGridGameObject (row + 1, col) != null && instantiator.GetJewelGridGameObject (row + 1, col).tag != "Steel Block" && instantiator.GetJewelGridGameObject (row + 1, col).name != "Slug(Clone)" 
				    && instantiator.GetJewelGridGameObject (row + 1, col).name != "Slug Stage 2(Clone)" && instantiator.GetJewelGridGameObject (row + 1, col).name != "Slug Stage 3(Clone)" && instantiator.GetJewelGridGameObject (row + 1, col).name != "Slug Stage 4(Clone)" &&
				    !Bomb (instantiator.GetJewelGridGameObject (row + 1, col))
				    && instantiator.GetJewelGridGameObject (row + 1, col).tag != "Slug Meal" &&
				    !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row + 1, col)) && instantiator.GetJewelGridGameObject (row + 1, col).transform.childCount == 0) {
					instantiator.GetJewelGridGameObject (row + 1, col).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x;
					targetY = transform.position.y - .61f;
					jewelMovement.SetRow (row + 1);
					row++;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.left;
			} else if (movementDirection == slugDirection.left) {
				if (col - 1 >= 0 && instantiator.GetJewelGridGameObject (row, col - 1) != null && instantiator.GetJewelGridGameObject (row, col - 1).tag != "Steel Block" && instantiator.GetJewelGridGameObject (row, col - 1).name != "Slug(Clone)" 
				    && instantiator.GetJewelGridGameObject (row, col - 1).name != "Slug Stage 2(Clone)" && instantiator.GetJewelGridGameObject (row, col - 1).name != "Slug Stage 3(Clone)" && instantiator.GetJewelGridGameObject (row, col - 1).name != "Slug Stage 4(Clone)" && 
				    !Bomb (instantiator.GetJewelGridGameObject (row, col - 1))
				    && instantiator.GetJewelGridGameObject (row, col - 1).tag != "Slug Meal" && 
				    !swapJewel.IsASwapJewel (instantiator.GetJewelGridGameObject (row, col - 1)) && instantiator.GetJewelGridGameObject (row, col - 1).transform.childCount == 0) {
					instantiator.GetJewelGridGameObject (row, col - 1).tag = "Slug Meal";
					tempBoulder = (GameObject)Instantiate (currentBoulder, new Vector3 (transform.position.x, transform.position.y, -.5f), Quaternion.identity);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetBounced (true);
					tempBoulder.layer = gameObject.layer;
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetRow (row);
					tempBoulder.GetComponent<RockLevelJewelMovement> ().SetCol (col);
					instantiator.AddToJewelGrid (tempBoulder, row, col);
					targetX = transform.position.x - .6125f;
					targetY = transform.position.y;
					jewelMovement.SetCol (col - 1);
					col--;
					destructionJewel = instantiator.GetJewelGridGameObject (row, col);
					instantiator.AddToJewelGrid (gameObject, row, col);
					gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
					transform.position = (new Vector3 (transform.position.x, transform.position.y, -20));
					move = true;
					return;
				}
				movementDirection = slugDirection.up;
			}
			directionCount++;
			if (directionCount > 4) {
				return;
			}
		}
	}

	slugDirection GetRandomSlugDirection () {
		initialDirection = Random.Range (0, 4);
		if (initialDirection == 0) 
			return slugDirection.up;
		else if (initialDirection == 1)
			return slugDirection.right;
		else if (initialDirection == 2)
			return slugDirection.down;
		else if (initialDirection == 3)
			return slugDirection.left;
		return slugDirection.up;
	}

	bool IsJewel (GameObject possibleJewel) {
		switch (possibleJewel.tag) {
		case "Blue Block": 
		case "Green Block": 
		case "Yellow Block": 
		case "Purple Block": 
		case "Red Block": 
		case "White Block": 
			return true;
		}
		return false;
	}

	bool Bomb (GameObject possiblebomb) {
		switch (possiblebomb.tag) {
		case "Blue Bomb":
		case "Green Bomb":
		case "Orange Bomb":
		case "Purple Bomb":
		case "Red Bomb":
		case "White Bomb":
			return true;
		}
		return false;
	}

	public void StageThreeToStageTwo () {
		name = "Slug Stage 2(Clone)";
		currentBoulder = boulderStage2;
		gameObject.GetComponent<SpriteRenderer> ().sprite = yellowSprite;
		PowerStarTracker.RemoveFromHashSet (gameObject);
	}

	public void StageFourToStageThree () {
		name = "Slug Stage 3(Clone)";
		currentBoulder = boulderStage3;
		gameObject.GetComponent<SpriteRenderer> ().sprite = redSprite;
		PowerStarTracker.RemoveFromHashSet (gameObject);
	}

	public void StageTwoToStageOne () {
//		SlugListManager.RemoveFromSlugList (gameObject);
//		GameObject tempSlug = (GameObject)Instantiate (slug, transform.position, Quaternion.identity);
//		RockLevelJewelMovement tempJewelMovement = tempSlug.GetComponent<RockLevelJewelMovement> ();
//		SlugListManager.AddToSlugList (tempSlug);
//		tempJewelMovement.SetBounced (true);
//		tempJewelMovement.SetRow (jewelMovement.GetRow ());
//		tempJewelMovement.SetCol (jewelMovement.GetCol ());
//		tempSlug.layer = gameObject.layer;
//		instantiator.SetJewelGridGameObject (tempJewelMovement.GetRow (), tempJewelMovement.GetCol (), tempSlug);
//		Destroy (gameObject);
		name = "Slug(Clone)";
		currentBoulder = boulder;
		gameObject.GetComponent<SpriteRenderer> ().sprite = whiteSprite;
		PowerStarTracker.RemoveFromHashSet (gameObject);
	}

	public void SetRow (int row) {
		Debug.Log ("row = " + row);
		this.row = row;
	}

	public void SetCol (int col) {
		Debug.Log ("col = " + col);
		this.col = col;
	}

	public void DestroyBoulder () {
		Destroy (tempBoulder);
	}

	public void BeingDeleted (bool beingDeleted) {
		this.beingDeleted = beingDeleted;
	}

	public bool GetBeingDeleted () {
		return beingDeleted;
	}

	public bool GetMoving () {
		return move;
	}
}
