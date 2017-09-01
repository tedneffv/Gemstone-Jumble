using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndOfLevelBombMovementController : MonoBehaviour {

	int row, col;
	GameObject targetJewel;
	public GameObject bombExplosion;
	RockLevelInstantiator instantiator;
	RockLevelDeleteJewels deleteJewels;
	Vector3 targetPositon;
	float startingLeftJewelPosition = -2.45f, horizontalMultiplier = .6125f, targetXPosition, targetYPosition;
	Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		row = Random.Range (1, 8);
		col = Random.Range (1, 8);
		targetJewel = instantiator.GetJewelGridGameObject (row, col); 
		targetPositon = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * col), 2.591258f - (.61f * row), (-1 * row) - 2);	
		targetXPosition = targetPositon.x;
		targetYPosition = targetPositon.y;
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.AddForce (new Vector2 (0, 1000));
		rigidBody.AddTorque (Random.Range (-10000, 10000));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (targetXPosition - transform.position.x, 0, 0) * Time.deltaTime * 3);
		if (rigidBody.velocity.y < 0 && transform.position.y < targetYPosition) {
			ExplodeBomb ();
			PaidPowerTracker.RemovePowerFromList (gameObject);
			Destroy (gameObject);
		}
	}

	void ExplodeBomb () {
		HashSet<GameObject> deleteList = new HashSet<GameObject> ();
		GameObject tempJewel = null;
		if (PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (row, col))) 
			deleteList.Add (instantiator.GetJewelGridGameObject (row, col));
		if (row - 1 >= 0) { 
			tempJewel = instantiator.GetJewelGridGameObject (row - 1, col);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
			if (col + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (row - 1, col + 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
			if (col - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (row - 1, col - 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
		}
		if (row + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (row + 1, col);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
			if (col + 1 <= 8) {
				tempJewel = instantiator.GetJewelGridGameObject (row + 1, col + 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
			if (col - 1 >= 0) {
				tempJewel = instantiator.GetJewelGridGameObject (row + 1, col - 1);
				if (PowerStarTracker.AddToHashSet (tempJewel)) 
					deleteList.Add (tempJewel);
			}
		}
		
		if (col + 1 <= 8) {
			tempJewel = instantiator.GetJewelGridGameObject (row, col + 1);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
		}
		if (col - 1 >= 0) {
			tempJewel = instantiator.GetJewelGridGameObject (row, col - 1);
			if (PowerStarTracker.AddToHashSet (tempJewel)) 
				deleteList.Add (tempJewel);
		}
		Instantiate (bombExplosion, transform.position, Quaternion.Euler (180, 0, 0));
		deleteJewels.DeleteAllJewelsInList (deleteList, true);
	}
}
