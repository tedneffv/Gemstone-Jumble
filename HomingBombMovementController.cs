using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomingBombMovementController : MonoBehaviour {

	int targetRow, targetCol;
	GameObject targetJewel;
	Vector3 targetPosition;
	float startingLeftJewelPosition, horizontalMultiplier;
	RockLevelInstantiator instantiator;
	RockLevelDeleteJewels deleteJewels;
	public GameObject bombExplosion;

	void Awake () {
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		startingLeftJewelPosition = -2.45f;
		horizontalMultiplier = .6125f;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0) * Time.deltaTime * 8);
		if (Mathf.Abs (targetPosition.x - transform.position.x) < .01f && Mathf.Abs (targetPosition.y - transform.position.y) < .01f) {
			ExplodeBomb ();
			PaidPowerTracker.RemovePowerFromList (gameObject);
			Destroy (gameObject);
		}
	}

	public void SetTargetRowAndCol (int row, int col) {
		this.targetRow = row;
		this.targetCol = col;
		targetPosition = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * col), 2.591258f - (.61f * row), (-1 * row) - 2);
		targetJewel = instantiator.GetJewelGridGameObject (row, col);
	}

	void ExplodeBomb () {
		HashSet<GameObject> deleteList = new HashSet<GameObject> ();
		GameObject tempJewel = null;
		if (PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (targetRow, targetCol))) 
			deleteList.Add (instantiator.GetJewelGridGameObject (targetRow, targetCol));
		if (targetRow - 1 >= 0) { 
			tempJewel = instantiator.GetJewelGridGameObject (targetRow - 1, targetCol);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
			if (targetCol + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (targetRow - 1, targetCol + 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
			if (targetCol - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (targetRow - 1, targetCol - 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
		}
		if (targetRow + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (targetRow + 1, targetCol);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
			if (targetCol + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (targetRow + 1, targetCol + 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
			if (targetCol - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (targetRow + 1, targetCol - 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
		}
		
		if (targetCol + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (targetRow, targetCol + 1);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
		}
		if (targetCol - 1 >= 0) {
			tempJewel = instantiator.GetJewelGridGameObject (targetRow, targetCol - 1);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
		}
		Instantiate (bombExplosion, transform.position, Quaternion.Euler (180, 0, 0));
		deleteJewels.DeleteAllJewelsInList (deleteList, true);
	}
}
