using UnityEngine;
using System.Collections;

public class TotalStarsEarnedInstantiator : MonoBehaviour {

	public GameObject zero, one, two, three, four, five, six, seven, eight, nine;
	Vector3 firstDigitPosition, secondDigitPosition;
	int totalStarNumber;

	// Use this for initialization
	void Start () {
		firstDigitPosition = new Vector3 (1.838f, 8.16f, -1f);
		secondDigitPosition = new Vector3 (2.073f, 8.16f, -1f);
		if (GameObject.Find ("The Mountain Level Progression") != null)
			totalStarNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetTotalMountainStars ();
		else if (GameObject.Find ("The City Level Progression") != null)
			totalStarNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetTotalCityStars ();
		else if (GameObject.Find ("The Cabin Level Progression") != null) {
			totalStarNumber = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetTotalCabinStars ();
			Debug.Log ("totalStarNumber = " + totalStarNumber);
		}
		InstantiateStarNumbers ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstantiateStarNumbers () {
		string tempString = totalStarNumber.ToString ();
//		if (tempString.Length == 2 && tempString[0] != '0') {
//			firstDigit = InstantiateDigit (firstDigitPosition, tempString[0]);
//		}
//		secondDigit = InstantiateDigit (secondDigitPosition, tempString[1]);

		if (tempString.Length == 1) {
			InstantiateDigit (secondDigitPosition, tempString[0]);
		} else if (tempString.Length == 2) {
			InstantiateDigit (firstDigitPosition, tempString[0]);
			InstantiateDigit (secondDigitPosition, tempString[1]);
		}
	}

	GameObject InstantiateDigit (Vector3 position, char number) {
		switch (number) {
		case '0': return (GameObject)Instantiate (zero, position, Quaternion.identity);
		case '1': return (GameObject)Instantiate (one, position, Quaternion.identity);
		case '2': return (GameObject)Instantiate (two, position, Quaternion.identity);
		case '3': return (GameObject)Instantiate (three, position, Quaternion.identity);
		case '4': return (GameObject)Instantiate (four, position, Quaternion.identity);
		case '5': return (GameObject)Instantiate (five, position, Quaternion.identity);
		case '6': return (GameObject)Instantiate (six, position, Quaternion.identity);
		case '7': return (GameObject)Instantiate (seven, position, Quaternion.identity);
		case '8': return (GameObject)Instantiate (eight, position, Quaternion.identity);
		case '9': return (GameObject)Instantiate (nine, position, Quaternion.identity);
		}
		return null;
	}
}
