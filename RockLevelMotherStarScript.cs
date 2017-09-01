using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockLevelMotherStarScript : MonoBehaviour {

	bool instantiateChildStars, moveRight, jewelPicked, startDecent, tutorialLevel, bombInstantiated;
	public GameObject greenChildStar, redChildStar, whiteChildStar, purpleChildStar, blueChildStar, orangeChildStar, target;
	GameObject childStar, instantiatedStar, instantiatedTarget, targetJewel;
	float timeStamp, cooldown, targetX, targetY;
	int childStarCount, row, col, tutorialNumber;
	List<GameObject> childStarArray, jewelList;
	RockLevelChildStarMovement childStarMovement;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement;
	RockLevelTouchHandler touchHandler;
	RockLevelFiveInARow fiveInARow;
	RockLevelDeleteJewels deleteJewels;
	PowerPercentageController powerPercentageController;
	RockLevelBombHandler bombHandler;
	SoundHandler soundHandler;
	
	// Use this for initialization
	void Start () {
		cooldown = .1f;
		childStarCount = 0;
		GetChildStar ();
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		GetComponent<Rigidbody2D>().AddTorque (540);
		deleteJewels = GameObject.Find ("Level Controller").GetComponent<RockLevelDeleteJewels> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		fiveInARow = GameObject.Find ("Level Controller").GetComponent<RockLevelFiveInARow> ();
		powerPercentageController = GameObject.Find ("Level Controller").GetComponent<PowerPercentageController> ();
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		bombHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelBombHandler> ();
		if (GameObject.Find ("Mountain Level Two ID") != null) {
			tutorialLevel = true;
		}
		tutorialNumber = 0;
		jewelList = new List<GameObject> ();
		jewelPicked = false;
		//		targetX = Random.Range (-2f, 2f);
		//		targetY = 4.5f;
		targetX = transform.position.x;
		targetY = transform.position.y;
		childStarArray = new List<GameObject> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!instantiateChildStars)
			transform.Translate ((targetX - transform.position.x) * .1f, (targetY - transform.position.y) * .1f, 0, Space.World);
		if (!instantiateChildStars && Mathf.Abs (targetX - transform.position.x) < .05f && Mathf.Abs (targetY - transform.position.y) < .05f) {
			instantiateChildStars = true;
			timeStamp = Time.time;
			moveRight = true;
		}
		
		if (instantiateChildStars && childStarCount < 12) {
			childStarCount++;
			if (!tutorialLevel && powerPercentageController.IsWithinPercentage () && !bombInstantiated) {
				bombInstantiated = true;
				InstantiateBombSeekingChildStars ();
			} else 
				InstantiateChildStars ();
			timeStamp = Time.time;
			if (childStarCount == 12) {
				cooldown = .4f;
				timeStamp = Time.time;
				startDecent = true;
			}
		} 
		
		if (startDecent && Time.time > timeStamp + cooldown) {
			foreach (GameObject a in childStarArray) {
				childStarMovement = a.GetComponent<RockLevelChildStarMovement> ();
				childStarMovement.StartDecent ();
			}
			soundHandler.PlayPowerShot ();
			fiveInARow.GetMotherStarList ().Remove (gameObject);
			Destroy (gameObject);
		}
	}

	void InstantiateBombSeekingChildStars () {
		GameObject randomBomb = bombHandler.GetRandomBomb ();
		if (randomBomb == null) {
			InstantiateChildStars ();
			return;
		}
		instantiatedStar = (GameObject)Instantiate (childStar, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, -15), Quaternion.identity);
		childStarMovement = instantiatedStar.GetComponent<RockLevelChildStarMovement> ();
		row = randomBomb.GetComponent<RockLevelJewelMovement> ().GetRow ();
		col = randomBomb.GetComponent<RockLevelJewelMovement> ().GetCol ();
		while (instantiator.GetJewelGridGameObject (row, col) == null) {
			row = Random.Range (0, 9);
			col = Random.Range (0, 9);
		}
		jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
		while (jewelMovement.GetMoving () || jewelList.Contains (instantiator.GetJewelGridGameObject (row, col))) {
			row = Random.Range (0, 9);
			col = Random.Range (0, 9);
			while (instantiator.GetJewelGridGameObject (row, col) == null) {
				row = Random.Range (0, 9);
				col = Random.Range (0, 9);
			}
			jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
		}

		childStarMovement = instantiatedStar.GetComponent<RockLevelChildStarMovement> ();
		childStarArray.Add (instantiatedStar);
		fiveInARow.GetChildStarList ().Add (instantiatedStar);
		jewelList.Add (instantiator.GetJewelGridGameObject (row, col));
		instantiatedTarget = (GameObject)Instantiate (target, new Vector3 (-2.45f + (col * .6125f), 2.591252f - (row * .6097268f), -23), Quaternion.identity);
		childStarMovement.SetTargetPosition (instantiator.GetJewelGridGameObject (row, col).transform.position, row, col, instantiatedTarget);
	}
	
	void InstantiateChildStars () {
		instantiatedStar = (GameObject)Instantiate (childStar, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, -15), Quaternion.identity);
		childStarMovement = instantiatedStar.GetComponent<RockLevelChildStarMovement> ();
		if (!tutorialLevel) {
			row = Random.Range (0, 9);
			col = Random.Range (0, 9);
			while (IsBomb (instantiator.GetJewelGridGameObject (row, col).tag) || instantiator.GetJewelGridGameObject (row, col) == null) {
				row = Random.Range (0, 9);
				col = Random.Range (0, 9);
			}
			jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
			while (IsBomb (instantiator.GetJewelGridGameObject (row, col).tag) || jewelMovement.GetMoving () || jewelList.Contains (instantiator.GetJewelGridGameObject (row, col))) {
				row = Random.Range (0, 9);
				col = Random.Range (0, 9);
				while (instantiator.GetJewelGridGameObject (row, col) == null) {
					row = Random.Range (0, 9);
					col = Random.Range (0, 9);
				}
				jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
			}
		} else {
			InstantiateTutorailChildStars ();
			jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
		}

		childStarMovement = instantiatedStar.GetComponent<RockLevelChildStarMovement> ();
		//		childStarMovement.StartDecent ();
		//		jewelMovement.targetForExplosion ();
		childStarArray.Add (instantiatedStar);
		fiveInARow.GetChildStarList ().Add (instantiatedStar);
		jewelList.Add (instantiator.GetJewelGridGameObject (row, col));
		instantiatedTarget = (GameObject)Instantiate (target, new Vector3 (-2.45f + (col * .6125f), 2.591252f - (row * .6097268f), -23), Quaternion.identity);
		childStarMovement.SetTargetPosition (instantiator.GetJewelGridGameObject (row, col).transform.position, row, col, instantiatedTarget);
	}

	void InstantiateTutorailChildStars () {
		switch (tutorialNumber) {
		case 0: row = 1; col = 8; break;
		case 1: row = 2; col = 2; break;
		case 2: row = 3; col = 2; break;
		case 3: row = 4; col = 3; break;
		case 4: row = 5; col = 1; break;
		case 5: row = 6; col = 7; break;
		case 6: row = 7; col = 5; break;
		case 7: row = 8; col = 3; break;
		case 8: row = 7; col = 7; break;
		case 9: row = 6; col = 2; break;
		case 10: row = 5; col = 4; break;
		case 11: row = 4; col = 2; break;
		}
		tutorialNumber++;
		jewelMovement = instantiator.GetJewelGridGameObject (row, col).GetComponent<RockLevelJewelMovement> ();
	}
	
	void GetChildStar () {
		switch (gameObject.name) {
		case "Red Mother Star(Clone)": childStar = redChildStar; break;
		case "Green Mother Star(Clone)": childStar = greenChildStar; break;
		case "White Mother Star(Clone)": childStar = whiteChildStar; break;
		case "Blue Mother Star(Clone)": childStar = blueChildStar; break;
		case "Orange Mother Star(Clone)": childStar = orangeChildStar; break; 
		case "Purple Mother Star(Clone)": childStar = purpleChildStar; break;
		}
	}
	
	public bool JewelListContains (GameObject jewel) {
		return jewelList.Contains (jewel);
	}

	bool IsBomb (string tag) {
		return (tag == "Blue Bomb" || tag == "Green Bomb" || tag == "Orange Bomb" || tag == "Purple Bomb" || tag == "Red Bomb" || tag == "White Bomb");
	}
}
