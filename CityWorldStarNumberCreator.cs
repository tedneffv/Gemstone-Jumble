using UnityEngine;
using System.Collections;

public class CityWorldStarNumberCreator : MonoBehaviour {

	public GameObject padlock, zero, one, two, three, four, five, six, seven, eight, nine;
	Vector3 firstDigitPosition, secondDigitPosition;
	bool touchOn, finishedAllMountainLevels;

	void Awake () {
		firstDigitPosition = new Vector3 (-1.12f, -.71f, -.5f);
		secondDigitPosition = new Vector3 (-.72f, -.71f, -.5f);
		int totalStars = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetTotalStarNumber ();
		string starString = totalStars.ToString ();

		if (totalStars >= 45 || GameObject.Find ("Game Manager").GetComponent<CityLevelsStatus> ().AreCityLevelsOpen ()) {
			touchOn = true;
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
			Color oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
		}

		else if (totalStars < 45) {
			GameObject instantiatedPadlock = (GameObject)Instantiate (padlock, new Vector3 (0, 0, 0), Quaternion.identity);

			if (starString.Length == 1) {
				GameObject temp = (GameObject)Instantiate (GetStarNumber (starString[0]), secondDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
			} else if (starString.Length == 2) {
				GameObject temp = (GameObject)Instantiate (GetStarNumber (starString[0]), firstDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
				temp = (GameObject)Instantiate (GetStarNumber (starString[1]), secondDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
			}

			instantiatedPadlock.transform.position = new Vector3 (6.504f, .304f, -5);
			instantiatedPadlock.transform.parent = gameObject.transform;
		} 

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	GameObject GetStarNumber (char number) {
		switch (number) {
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

	public bool GetTouchOn () {
		return touchOn;
	}
}