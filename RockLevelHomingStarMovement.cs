using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelHomingStarMovement : MonoBehaviour {

	Vector3 targetPosition;
	public GameObject crosshairs;
	GameObject instantiatedCrosshairs, jewelToDestroy;
	RockLevelScoreKeeper scoreKeeper;
	int targetJewelRow, targetJewelCol;
	HashSet<GameObject> deleteList;
	RockLevelDeleteJewels deleteJewels;
	RockLevelInstantiator instantiator;
	RockLevelFourInARow fourInARow;
	RockLevelController controller;
	RockLevelJewelMovement jewelMovement;
	RockLevelSwapJewel swapJewel;
	float timeStamp, cooldown1, cooldown2, oppositeY, oppositeX;
	bool fourInARowStar, fireStar, readyToFire, playAudio, checkForRepeatJewel, soundPlayed;
	AudioSource[] audioSources;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddTorque (-540);
		deleteList = new HashSet<GameObject> ();
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		fourInARow = GameObject.Find ("Level Controller").GetComponent<RockLevelFourInARow> ();
		scoreKeeper = GameObject.Find ("Level Controller").GetComponent<RockLevelScoreKeeper> ();
		jewelToDestroy = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol);
		controller = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		audioSources = GameObject.Find ("SoundHandler").GetComponents<AudioSource>();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		swapJewel = GameObject.Find ("Level Controller").GetComponent<RockLevelSwapJewel> ();
		if (tag != "End Star") {
			instantiatedCrosshairs = (GameObject)Instantiate (crosshairs, new Vector3 (-2.45f + (targetJewelCol * .6125f), 2.591252f - (targetJewelRow * .6097268f), -90), Quaternion.identity);
			targetPosition = instantiatedCrosshairs.transform.position;
		} else
			targetPosition = new Vector3 (-2.45f + (targetJewelCol * .6125f), 2.591252f - (targetJewelRow * .6097268f), -90);
		timeStamp = Time.time;
		cooldown1 = .25f;
		cooldown2 = .15f;
		if (transform.position.y > -.5) {
			oppositeY = Random.Range (4f, 4.5f);
		} else 
			oppositeY = Random.Range (-4f, -4.65f);
		oppositeX = Random.Range (-2.5f, 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (fourInARowStar) {
			if (!fireStar) {
				transform.Translate (new Vector3 (oppositeX - transform.position.x, oppositeY - transform.position.y, 0) * Time.deltaTime * 8f, Space.World);
				if (Mathf.Abs (oppositeY - transform.position.y) < .05f) {
					if (!readyToFire) 
						readyToFire = true;
					if (fourInARow.AllStarsReadyToFire ()) {
						jewelToDestroy = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol);
						while (!PowerStarTracker.AddToHashSet (jewelToDestroy)) {
							targetJewelRow = Random.Range (0, 9);
							targetJewelCol = Random.Range (0, 9);
							jewelToDestroy = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol);
							targetPosition = (new Vector3 (-2.45f + (targetJewelCol * .6125f), 2.591252f - (targetJewelRow * .6097268f), -90));
							if (instantiatedCrosshairs != null) {
								Destroy (instantiatedCrosshairs);
								instantiatedCrosshairs = (GameObject)Instantiate (crosshairs, new Vector3 (-2.45f + (targetJewelCol * .6125f), 2.591252f - (targetJewelRow * .6097268f), -79), Quaternion.identity);
							}
						}
						fireStar = true;
						timeStamp = Time.time;
					}
				}
			} else if (fireStar && Time.time > timeStamp + cooldown2) {
				if (!soundPlayed) {
					soundPlayed = true;
					soundHandler.PlayPowerShot ();
				}
				transform.Translate ((targetPosition - transform.position) * Time.deltaTime * 20f, Space.World);
				int tempRow = 0, tempCol = 0;
				if ((Mathf.Abs (targetPosition.x - transform.position.x) < .2f && Mathf.Abs (targetPosition.y - transform.position.y) < .2f) || transform.position.y > 3f) {
					if (jewelToDestroy != null) {
						tempRow = jewelToDestroy.GetComponent<RockLevelJewelMovement> ().GetRow ();
						tempCol = jewelToDestroy.GetComponent<RockLevelJewelMovement> ().GetCol ();
						if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
							jewelMovement = jewelToDestroy.GetComponent<RockLevelJewelMovement> ();
							jewelMovement.StartDestroyCountdown ();
						}
						if (jewelToDestroy != null && !jewelToDestroy.GetComponent<RockLevelJewelMovement> ().GetMoving ()) {
							deleteList.Add (jewelToDestroy);
							deleteJewels.DeleteAllJewelsInList (deleteList, true);
						}
					}
					if (instantiatedCrosshairs != null)
						Destroy (instantiatedCrosshairs);
					fourInARow.RemoveHomingStarFromList (gameObject);
					Destroy (gameObject);
				}
			}
		}
		else if (Time.time > timeStamp + cooldown1) {
//			if (!checkForRepeatJewel) {
//				while (!PowerStarTracker.AddToHashSet (jewelToDestroy)) {
//					targetJewelRow = Random.Range (0, 9);
//					targetJewelCol = Random.Range (0, 9);
//					jewelToDestroy = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol);
//					targetPosition = new Vector3 (-2.45f + (targetJewelCol * .6125f), 2.591252f - (targetJewelRow * .6097268f), -23);
//				}
//				checkForRepeatJewel = true;
//			}
			transform.Translate ((targetPosition - transform.position) * Time.deltaTime * 7.5f, Space.World);
			if ((Mathf.Abs (targetPosition.x - transform.position.x) < .1f && Mathf.Abs (targetPosition.y - transform.position.y) < .1f) || transform.position.y > 3f) {
				Destroy (instantiatedCrosshairs);
				if (IsJewel (gameObject.tag) || IsBomb (gameObject)) {
					jewelMovement = instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).GetComponent<RockLevelJewelMovement> ();
					jewelMovement.StartDestroyCountdown ();
				}
				if (jewelToDestroy != null && !instantiator.GetJewelGridGameObject (targetJewelRow, targetJewelCol).GetComponent<RockLevelJewelMovement> ().GetMoving ()) {
					deleteList.Add (jewelToDestroy);
					deleteJewels.DeleteAllJewelsInList (deleteList, true);
				}
				//Debug.Log ("Shooting Star From Second Method");
				Destroy (gameObject);
			}
		}
	}
	
	public void FireStar () {
		fireStar = true;
	}
	
	public bool ReadyToFire () {
		return readyToFire;
	}
		
	public void SetRow (int row) {
		targetJewelRow = row;
	}
	
	public void SetCol (int col) {
		targetJewelCol = col;
	}

	public int GetRow () {
		return targetJewelRow;
	}

	public int GetCol () {
		return targetJewelCol;
	}
	
	public void SetFourInARowStar () {
		fourInARowStar = true;
	}

	public void SetJewelToDestroy (GameObject jewelToDestroy) {
		this.jewelToDestroy = jewelToDestroy;
	}

	public GameObject GetJewelToDestroy () {
		return jewelToDestroy;
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
