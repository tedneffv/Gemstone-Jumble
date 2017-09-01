using UnityEngine;
using System.Collections;

public class StarsEarnedManager : MonoBehaviour {

	int starNumber, levelNumber;
	MountainLevelsStatus mountainLevelStatus;
	CityLevelsStatus cityLevelStatus;
	RockLevelController levelController;

	// Use this for initialization
	void Start () {
		levelController = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
		mountainLevelStatus = GameObject.Find ("Game Manager").GetComponent<MountainLevelsStatus> ();
		if (GameObject.Find ("Right Star(Clone)") != null) {
			//Debug.Log ("3 Stars");
			levelNumber = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ().GetLevelNumber ();
			//Debug.Log ("LEVEL NUMBER = " + levelNumber);
			if (levelNumber < 31) {
				mountainLevelStatus.SetStarNumberInArray (3, levelNumber - 1);
				if (mountainLevelStatus.GetLevelProgressionNumber () == 3 && levelController.GetLevelNumber () == 4)
					mountainLevelStatus.IncrementLevelProgressionNumber ();
				else if (mountainLevelStatus.GetLevelProgressionNumber () == 4 && levelController.GetLevelNumber () == 5)
					mountainLevelStatus.IncrementLevelProgressionNumber ();
			}
			else if (levelNumber < 61) {
				cityLevelStatus.SetStarNumberInArray (3, levelNumber - 31);
			}
		}
		else if (GameObject.Find ("Middle Star(Clone)") != null) {
			if (levelNumber < 31) 
				mountainLevelStatus.SetStarNumberInArray (2, levelNumber - 1);
			else if (levelNumber < 61)
				cityLevelStatus.SetStarNumberInArray (2, levelNumber - 31);
			//Debug.Log ("2 Stars");
		}
		else if (GameObject.Find ("Left Star(Clone)") != null) {
			if (levelNumber < 31)
				mountainLevelStatus.SetStarNumberInArray (1, levelNumber - 1);
			else if (levelNumber < 61)
				cityLevelStatus.SetStarNumberInArray (1, levelNumber - 1);
			//Debug.Log ("1 Star");
		}
	}
	
}
