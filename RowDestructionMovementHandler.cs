using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowDestructionMovementHandler : MonoBehaviour {

	int directionInt, targetJewelRow, targetJewelCol;
	GameObject[] deleteRowArray;
	bool moveToInitialPosition, moveToFinalPosition, soundPlayed;
	float initialX, initialY;
	RockLevelInstantiator instantiator;
	Vector3 initialPositionVector, finalPositionVector;
	HashSet<GameObject> deleteList;
	RockLevelDeleteJewels deleteJewels;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		deleteList = new HashSet<GameObject> ();
		deleteRowArray = new GameObject[9];
		//Debug.Log ("deleteRowArray.Length = " + deleteRowArray.Length);
		GetComponent<Rigidbody2D>().AddTorque (-540);
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		directionInt = Random.Range(0, 4);
		targetJewelRow = Random.Range (0, 9);
		targetJewelCol = Random.Range (0, 9);
		while (instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).GetComponent<RockLevelJewelMovement> ().GetMoving ()) {
			targetJewelRow = Random.Range (0, 9);
			targetJewelCol = Random.Range (0, 9);
		}
		GetInitialPosition ();
		moveToInitialPosition = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (moveToInitialPosition) {
			transform.Translate ((initialPositionVector - transform.position) * Time.deltaTime * 5, Space.World);
			if (Mathf.Abs (initialPositionVector.x - transform.position.x) < .01f || Mathf.Abs (initialPositionVector.y - transform.position.y) < .01f) {
				FillDeleteRowArray ();
				moveToInitialPosition = false;
				moveToFinalPosition = true;
			}
		}
		else if (moveToFinalPosition) {
			if (!soundPlayed) {
				soundPlayed = true;
				soundHandler.PlayPowerShot ();
			}
			transform.Translate ((finalPositionVector - transform.position) * Time.deltaTime / 2, Space.World);
			for (int i = 0; i < deleteRowArray.Length; i++) {
				if (directionInt == 0 && deleteRowArray[i] != null && deleteRowArray[i].transform.position.y > transform.position.y) {
					deleteList.Add (deleteRowArray[i]);
					deleteRowArray[i] = null;
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
					deleteList.Clear ();
				}
				else if (directionInt == 1 && deleteRowArray[i] != null && deleteRowArray[i].transform.position.y < transform.position.y) {
					deleteList.Add (deleteRowArray[i]);
					deleteRowArray[i] = null;
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
					deleteList.Clear ();
				}
				else if (directionInt == 2 && deleteRowArray[i] != null && deleteRowArray[i].transform.position.x > transform.position.x) {
					deleteList.Add (deleteRowArray[i]);
					deleteRowArray[i] = null;
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
					deleteList.Clear ();
				}
				else if (directionInt == 3 && deleteRowArray[i] != null && deleteRowArray[i].transform.position.x < transform.position.x) {
					deleteList.Add (deleteRowArray[i]);
					deleteRowArray[i] = null;
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
					deleteList.Clear ();
				}
			}
			if ((Mathf.Abs (transform.position.x) > 50 || Mathf.Abs (transform.position.y) > 50)) {
				PaidPowerTracker.RemovePowerFromList (gameObject);
				Destroy (gameObject);
			}
		}
	}

	void GetInitialPosition () {
		switch (directionInt) {
		case 0: initialY = 3.45f; initialX = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).transform.position.x; 
			finalPositionVector = new Vector3 (initialX, -100f, transform.position.z); break;
		case 1: initialY = -3.2f; initialX = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).transform.position.x; 
			finalPositionVector = new Vector3 (initialX, 100f, transform.position.z); break;
		case 2: initialY = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).transform.position.y; initialX = 3.2f; 
			finalPositionVector = new Vector3 (-100, initialY, transform.position.z); break;
		case 3: initialY = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).transform.position.y; initialX = -3.2f; 
			finalPositionVector = new Vector3 (100, initialY, transform.position.z); break;
		}
		initialPositionVector = new Vector3 (initialX, initialY, transform.position.z);
	}

	void FillDeleteRowArray () {
		if (directionInt == 0 || directionInt == 1) {
			for (int i = 0; i < 9; i++) {
				if (instantiator.GetJewelGridGameObject (i, targetJewelCol) != null && instantiator.GetJewelGridGameObject (i, targetJewelCol).tag != "Steel Block" &&
				    PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (i, targetJewelCol))) {
					deleteRowArray[i] = instantiator.GetJewelGridGameObject (i, targetJewelCol);
				}
			}
		} else if (directionInt == 2 || directionInt == 3) {
			for (int i = 0; i < 9; i++) {
				if (instantiator.GetJewelGridGameObject (targetJewelRow, i) != null && instantiator.GetJewelGridGameObject (targetJewelRow, i).tag != "Steel Block" && 
				    PowerStarTracker.AddToHashSet (instantiator.GetJewelGridGameObject (targetJewelRow, i))) {
					deleteRowArray[i] = instantiator.GetJewelGridGameObject (targetJewelRow, i);
				}
			}
		}
	}
}
