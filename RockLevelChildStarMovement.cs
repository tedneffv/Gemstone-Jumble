using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelChildStarMovement : MonoBehaviour {

	int row, col;
	float targetX, targetY, timeStamp;
	bool targetPosSet, startDecent, fireStar, targetJewelSet;
	GameObject crosshair, jewelToDestroy;
	HashSet<GameObject> deleteList;
	RockLevelInstantiator instantiator;
	RockLevelDeleteJewels deleteJewels;
	RockLevelFiveInARow fiveInARow;
	RockLevelJewelMovement jewelMovement;
	RockLevelSwapJewel swapJewel;
	
	// Use this for initialization
	void Start () {
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		fiveInARow = GameObject.Find ("Level Controller").GetComponent<RockLevelFiveInARow> ();
		swapJewel = GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ();
		deleteList = new HashSet<GameObject> ();
		GetComponent<Rigidbody2D>().AddTorque (540);
	}
	
	// Update is called once per frame
	void Update () {
		if (startDecent) {
			transform.Translate ((targetX - transform.position.x) * Time.deltaTime * 15f, (targetY - transform.position.y) * Time.deltaTime * 15f, 0, Space.World);
			if (!targetJewelSet) {
				deleteJewels.StampTimeCard ();
				jewelToDestroy = instantiator.GetJewelGridGameObject (row, col);
				while (!PowerStarTracker.AddToHashSet (jewelToDestroy)) {
					row = Random.Range (0, 9);
					col = Random.Range (0, 9);
					jewelToDestroy = instantiator.GetJewelGridGameObject (row, col);
					crosshair.transform.position = new Vector3 (-2.45f + (col * .6125f), 2.591252f - (row * .6097268f), -23);
				}
				targetJewelSet = true;
			}
		}
		
		if (startDecent && Mathf.Abs (targetX - transform.position.x) < .05f && Mathf.Abs (targetY - transform.position.y) < .05f) {
			if (jewelToDestroy != null) {
				if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
					jewelMovement = jewelToDestroy.GetComponent<RockLevelJewelMovement> ();
					jewelMovement.StartDestroyCountdown ();
				}
				if (jewelToDestroy != null && !swapJewel.IsASwapJewel (jewelToDestroy)) {
					deleteList.Add  (jewelToDestroy);
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
				}
			}
			Destroy (crosshair);
			fiveInARow.GetChildStarList ().Remove (gameObject);
			if (fiveInARow.TutorialListContainsStar (gameObject))
				fiveInARow.RemoveStarFromTutorialList (gameObject);
			Destroy (gameObject);
		}
	}
	
	public void SetTargetPosition (Vector3 targetPos, int i, int j, GameObject target) {
		row = i;
		col = j;
		targetX = targetPos.x;
		targetY = targetPos.y;
		targetPosSet = true;
		crosshair = target;
		timeStamp = Time.time;
	}
	
	public void StartDecent () {
		startDecent = true;
	}

	public GameObject GetJewelToDestroy () {
		return jewelToDestroy;
	}

	public void SetJewelToDestroy (GameObject jewelToDestroy) {
		this.jewelToDestroy = jewelToDestroy;
	}

	private bool IsJewel (string jewelTag) {
		return (jewelTag == "Red Block" || jewelTag == "Green Block" || jewelTag == "Yellow Block" || jewelTag == "Purple Block" || jewelTag == "White Block" || jewelTag == "Blue Block");
	}
	
	bool IsBomb (GameObject jewel) {
		if (jewel != null && (jewel.tag == "Blue Bomb" || jewel.tag == "Red Bomb" || jewel.tag == "Green Bomb" || jewel.tag == "White Bomb" || jewel.tag == "Orange Bomb" || jewel.tag == "Purple Bomb"))
			return true;
		return false;
	}
}
