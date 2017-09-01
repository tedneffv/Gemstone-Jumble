using UnityEngine;
using System.Collections;

public class RockLevelTouchHandler : MonoBehaviour {

	Collider2D hit;
	public GameObject firstPressed, secondPressed;
	RockLevelSwapJewel swapJewels;
	RockLevelJewelMovement jewelMovement;
	RockLevelCheckForMatches checkForMatches;
	RockLevelBombHandler bombHandler;
	bool tutorial, gameStarted, pauseTouch, timeStampPunched, tutorialLevel2, swapCooldown;
	float timeStamp, cooldown;
	RockLevelFiveInARow fiveInARow;
	RockLevelFourInARow fourInARow;
	RockLevelCorners corners;
	RockLevelDeleteJewels deleteJewels;
	System.DateTime initialTime;
	long elapsedTicks; 

	// Use this for initialization
	void Start () {
		firstPressed = null;
		secondPressed = null;
		tutorial = true;
		gameStarted = false;
		cooldown = 1;
		swapJewels = gameObject.GetComponent<RockLevelSwapJewel> ();
		checkForMatches = gameObject.GetComponent<RockLevelCheckForMatches> ();
		bombHandler = gameObject.GetComponent<RockLevelBombHandler> ();
		fiveInARow = gameObject.GetComponent<RockLevelFiveInARow> ();
		fourInARow = gameObject.GetComponent<RockLevelFourInARow> ();
		corners = gameObject.GetComponent<RockLevelCorners> ();
		deleteJewels = gameObject.GetComponent<RockLevelDeleteJewels> ();
		initialTime = System.DateTime.Now;
		timeStamp = Time.time;
		cooldown = .625f;
		if (GameObject.Find ("Mountain Level Two ID") != null)
			tutorialLevel2 = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted) {
			if (swapCooldown && Time.time > timeStamp + cooldown) {
				swapCooldown = false;
			}
			if (Input.GetMouseButton (0)) {
				if (!pauseTouch)
					CheckTouch (Input.mousePosition);
			}
			if (Input.GetMouseButtonUp (0)) {
				firstPressed = null;
				secondPressed = null;
			}
			//			}
		}
	}

	private void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	

		if (swapCooldown)
			return;
		
		if (/*!swapJewels.GetPauseTouchForSwap () &&*/ hit != null && !PowerStarTracker.ContainsJewel (hit.gameObject) /*&& !checkForMatches.JewelOkayToMove (hit.gameObject)*/ && HasCorrectTags (hit.gameObject) && hit.gameObject.name != "Slug Meal" && fiveInARow.GetMotherStarList ().Count == 0 && corners.GetCornerStarGreaterThan10 () && fiveInARow.GetChildStarList ().Count == 0 && fourInARow.GetHomingStarList ().Count == 0 && firstPressed == null /*&& deleteJewels.OkayToMoveAgain ()*/) {
			jewelMovement = hit.gameObject.GetComponent<RockLevelJewelMovement> ();
			if (!jewelMovement.GetToBeDestroyed () && !jewelMovement.GetMoving () && jewelMovement.GetOnPlatform () && !checkForMatches.deleteList.Contains (hit.gameObject)) {
				if (tutorialLevel2 && hit.gameObject.name != "Level Two Tutorial Jewel") 
					return;
				firstPressed = hit.gameObject;
			}
		}else if (/*!swapJewels.GetPauseTouchForSwap () &&*/ hit != null && !PowerStarTracker.ContainsJewel (hit.gameObject) /*&& !checkForMatches.JewelOkayToMove (hit.gameObject) */&& HasCorrectTags (hit.gameObject) && hit.gameObject.name != "Slug Meal"  /*&& deleteJewels.OkayToMoveAgain () */&& corners.GetCornerStarGreaterThan10 () && fiveInARow.GetMotherStarList ().Count == 0 && fiveInARow.GetChildStarList ().Count == 0 && firstPressed != null && secondPressed == null && hit.gameObject != firstPressed && fourInARow.GetHomingStarList ().Count == 0 &&
                  //((jewelMovement.GetRow () == hit.gameObject.GetComponent<RockLevelJewelMovement> ().GetRow () && Mathf.Abs (jewelMovement.GetCol () - hit.gameObject.GetComponent<RockLevelJewelMovement> ().GetCol ()) == 1) ||
                  //(jewelMovement.GetCol () == hit.gameObject.GetComponent<RockLevelJewelMovement> ().GetCol () && Mathf.Abs (jewelMovement.GetRow () - hit.gameObject.GetComponent<RockLevelJewelMovement> ().GetRow ()) == 1)))
                  Mathf.Abs(hit.gameObject.transform.position.x - firstPressed.transform.position.x) < .66 &&
                  Mathf.Abs(hit.gameObject.transform.position.y - firstPressed.transform.position.y) < .66 && (Mathf.Abs(hit.gameObject.transform.position.x - firstPressed.transform.position.x) < .1 || Mathf.Abs(hit.gameObject.transform.position.y - firstPressed.transform.position.y) < .1))
        {
            //{
			
			jewelMovement = hit.gameObject.GetComponent<RockLevelJewelMovement> ();
			if (!jewelMovement.GetToBeDestroyed () && !jewelMovement.GetMoving () && jewelMovement.GetOnPlatform () && !checkForMatches.deleteList.Contains (hit.gameObject)) {
				if (tutorialLevel2 && hit.gameObject.name != "Level Two Tutorial Jewel") 
					return;
				secondPressed = hit.gameObject;
				swapCooldown = true;
				timeStamp = Time.time;
				swapJewels.Swap (firstPressed, secondPressed, false);
			}
		}
	}

	private bool HasCorrectTags (GameObject jewel) {
		string tempTag = jewel.tag;
		return (tempTag == "Blue Block" || tempTag == "Blue Bomb" || tempTag == "Green Block" || tempTag == "Green Bomb" || tempTag == "Red Block" || tempTag == "Red Bomb" 
		        || tempTag == "White Block" || tempTag == "White Bomb" || tempTag == "Yellow Block" || tempTag == "Orange Bomb" || tempTag == "Purple Block" || tempTag == "Purple Bomb");
	}
	
	public void SetTutorial (bool tutorial) {
		this.tutorial = tutorial;
	}
	
	public bool GetTutorial () {
		return tutorial;
	}

	public void SetTutorialTwo (bool tutorial2) {
		this.tutorialLevel2 = tutorial2;
	}
	
	public void PunchTimeCard () {
		timeStamp = Time.time;
	}
	
	public void SetGameStarted (bool gameStarted) {
		this.gameStarted = gameStarted;
	}
	
	public bool GetGameStarted () {
		return gameStarted;
	}

	public void PauseTouch () {
		pauseTouch = true;
	}

	public void UnpauseTouch () {
		pauseTouch = false;
	}
}
