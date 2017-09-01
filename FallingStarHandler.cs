using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingStarHandler : MonoBehaviour {

	Vector3 rotationVector;
	bool falling;
	int targetRow, targetCol;
	RockLevelInstantiator instantiator;
	RockLevelDeleteJewels deleteJewels;
	SoundHandler soundHandler;
	public GameObject bombExplosion;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 2500));
		if (tag != "Bomb") {
			GetComponent<Rigidbody2D>().AddTorque (-540f);
		}
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		targetRow = Random.Range (0, 9);
		targetCol = Random.Range (0, 9);
	}
	
	// Update is called once per frame
	void Update () {

		if (!falling && GetComponent<Rigidbody2D>().velocity.y < 0) {
			transform.position = new Vector3 (instantiator.GetJewelGridGameObject (targetRow, targetCol).transform.position.x, transform.position.y, transform.position.z);
			falling = true;
		}

		if (falling) {
			if (transform.position.y < instantiator.GetJewelGridGameObject (targetRow, targetCol).transform.position.y) {
				if (tag != "Bomb") {
					soundHandler.PlayJewelBreak ();
					if (PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (targetRow, targetCol))) {
						HashSet<GameObject> deleteList = new HashSet<GameObject> ();
						deleteList.Add (instantiator.GetJewelGridGameObject (targetRow, targetCol));
						deleteJewels.DeleteAllJewelsInList (deleteList, true);
					}
					PaidPowerTracker.RemovePowerFromList (gameObject);
					Destroy (gameObject);
				} else {
					DestroySurroundingJewels ();
					PaidPowerTracker.RemovePowerFromList (gameObject);
					Destroy (gameObject);
				}
			}
		}
	}

	void DestroySurroundingJewels () {
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
