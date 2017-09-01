using UnityEngine;
using System.Collections;

public class PowerPercentageController : MonoBehaviour {

	int powerPercentageNumerator, powerPercentageDenominator, jewelDropPercentageNumerator, jewelDropPercentageDenominator, bombColorDropPercentageNumerator, bombColorDropPercentageDenominator;

	// Use this for initialization
	void Start () {
		powerPercentageNumerator = 1;
		powerPercentageDenominator = 200;
		if (gameObject.GetComponent<RockLevelController> ().GetLevelNumber () == 61 || gameObject.GetComponent<RockLevelController> ().GetLevelNumber () == 1 || gameObject.GetComponent<RockLevelController> ().GetLevelNumber () == 2 ||
		    gameObject.GetComponent<RockLevelController> ().GetLevelNumber () == 3 || gameObject.GetComponent<RockLevelController> ().GetLevelNumber () == 4) {
			powerPercentageDenominator = 50;
			jewelDropPercentageNumerator = 20;
			bombColorDropPercentageNumerator = 20;
		}
		if (gameObject.GetComponent<RockLevelController> ().GetLevelNumber () < 31) {
			Debug.Log ("jewelDropPercentage && bombColorDropPercentage = 20");
			jewelDropPercentageNumerator = 20;
			bombColorDropPercentageNumerator = 20;
		} else if (gameObject.GetComponent<RockLevelController> ().GetLevelNumber () < 61){
			Debug.Log ("jewelDropPercentage && bombColorDropPercentage = 15");
			jewelDropPercentageNumerator = 15;
			bombColorDropPercentageNumerator = 15;
		} else {
			Debug.Log ("jewelDropPercentage && bombColorDropPercentage = 10");
			jewelDropPercentageNumerator = 10;
			bombColorDropPercentageNumerator = 10; 
		}

		jewelDropPercentageDenominator = 100;
		bombColorDropPercentageDenominator = 100;
	}
	
	// Update is called once per fraem
	void Update () {
	
	}

	public bool IsWithinPercentage () {
		int random = (Random.Range (0, powerPercentageDenominator));
		if (random > (powerPercentageDenominator - powerPercentageNumerator - 1))
			return true;
		return false;
	}

	public void IncreasePecentage (int increaseNumber) {
		powerPercentageNumerator += increaseNumber;
	}

	public bool IsJewelDropWithinPercentage () {
		int random = (Random.Range (0, jewelDropPercentageDenominator));
		if (random > (jewelDropPercentageDenominator - jewelDropPercentageNumerator - 1)) 
			return true;
		return false;
	}

	public void IncreaseJewelDropPercentage (int increaseNumber) {
		jewelDropPercentageNumerator += increaseNumber;
	}

	public bool IsBombColorDropWithinPercentage () {
		int random = (Random.Range (0, bombColorDropPercentageDenominator));
		if (random > (bombColorDropPercentageDenominator - bombColorDropPercentageNumerator - 1))
			return true;
		return false;
	}

	public void IncreaseBombColorDropPercentage (int increaseNumber) {
		bombColorDropPercentageNumerator += increaseNumber;
	}
}
