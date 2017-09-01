using UnityEngine;
using System.Collections;

public class CityHighScoreAdder : MonoBehaviour {

	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, comma;
	GameObject firstDigit, secondDigit, thirdDigit, fourthDigit, fifthDigit, sixthDigit, seventhDigit, eightDigit;
	GameObject firstComma, secondComma;
	HighScoreKeeper highScoreKeeper;
	
	// Use this for initialization
	void Start () {
		highScoreKeeper = GameObject.Find ("Game Manager").GetComponent<HighScoreKeeper> ();
		int highScore, length, digitPlacement, commaNumber, counter;
		bool even;
		GameObject block, tempNumber;
		string highScoreString;
		for (int i = 0; i < 30; i++) {
			highScore = highScoreKeeper.GetCityLevelHighScore (i);
			if (highScore == 0)
				return;
			block = GameObject.Find (i + " block");
			highScoreString = highScore.ToString ();
			length = highScoreString.Length;
			counter = 0;
			if (length > 6)
				commaNumber = 2;
			else if (length > 3)
				commaNumber = 1;
			else 
				commaNumber = 0;
			even = length % 2 == 0;
			digitPlacement = length / 2;
			float xPosition;
			if (even) {
				xPosition = (block.transform.position.x + .08f) - (digitPlacement * .16f);
				xPosition = xPosition - (commaNumber * .05f);
				int lengthCount = length;
				bool commas = length > 3;
				for (int j = 0; j < length; j++) {
					tempNumber = (GameObject)Instantiate (GetCorrectNumber (highScoreString[counter]), new Vector3 (xPosition, block.transform.position.y - .655f, block.transform.position.z), Quaternion.identity);
					tempNumber.transform.parent = block.transform;
					lengthCount--;
					counter++;
					if (lengthCount == 3 || lengthCount == 6) {
						xPosition += .1f;
						tempNumber = (GameObject)Instantiate (comma, new Vector3 (xPosition, block.transform.position.y - .75f, block.transform.position.z), Quaternion.identity);
						tempNumber.transform.parent = block.transform;
						xPosition += .1f;
					} else 
						xPosition += .16f;
				}
			} else {
				xPosition = (block.transform.position.x) - (digitPlacement * .16f);
				xPosition = xPosition - (commaNumber * .05f);
				int lengthCount = length;
				bool commas = length > 3;
				for (int j = 0; j < length; j++) {
					tempNumber = (GameObject)Instantiate (GetCorrectNumber (highScoreString[counter]), new Vector3 (xPosition, block.transform.position.y - .655f, block.transform.position.z), Quaternion.identity);
					tempNumber.transform.parent = block.transform;
					lengthCount--;
					counter++;
					if (lengthCount == 3 || lengthCount == 6) {
						xPosition += .1f;
						tempNumber = (GameObject)Instantiate (comma, new Vector3 (xPosition, block.transform.position.y - .75f, block.transform.position.z), Quaternion.identity);
						tempNumber.transform.parent = block.transform;
						xPosition += .1f;
					} else 
						xPosition += .16f;
				}
			}
		}
	}
	
	GameObject GetCorrectNumber (char count) {
		switch (count) {
		case '0': return zero;
		case '1': return one;
		case '2': return two;
		case '3': return three;
		case '4': return four;
		case '5': return five;
		case '6': return six;
		case '7': return seven;
		case '8': return eight;
		case '9': return nine;
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
