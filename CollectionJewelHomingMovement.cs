using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionJewelHomingMovement : MonoBehaviour {

	Vector3 targetPosition;
	float startingLeftJewelPosition, horizontalMultiplier, speed, yMin;
	bool move;
	int row, col;
	RockLevelDeleteJewels deleteJewels;
	RockLevelInstantiator instantiator;
	HashSet<GameObject> deleteList;

	// Use this for initialization
	void Awake () {
		startingLeftJewelPosition = -2.45f;
		horizontalMultiplier = .6125f;
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteList = new HashSet<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			transform.Translate (new Vector3 (targetPosition.x - transform.position.x, 0, 0) * Time.deltaTime * speed);
			if (transform.position.y < yMin) {
				if (instantiator.GetJewelGridGameObject (row, col) != null) {
					deleteList.Add (instantiator.GetJewelGridGameObject (row, col));
					deleteJewels.DeleteAllJewelsInList (deleteList, false);
				}
				Destroy (gameObject);
			}
		}
	}

	public void SetTarget (int row, int col) {
		this.row = row;
		this.col = col;
		targetPosition = new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * col), 2.591252f - (row * .6097268f), -90);
		yMin = 2.591252f - (row * .6097268f);
		speed = 10;
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, Random.Range (200, 600)));
		move = true;
	}
}
