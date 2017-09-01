using UnityEngine;
using System.Collections;

public class LaunchPadWorldStarNumberCreator : MonoBehaviour {

	public GameObject padlock, zero, one, two, three, four, five, six, seven, eight, nine;
	Vector3 firstDigitPosition, secondDigitPosition, thirdDigitPosition;
	bool touchOn;

	void Awake () {
		firstDigitPosition = new Vector3 (-1.54f, -.71f, -.5f);
		secondDigitPosition = new Vector3 (-1.12f, -.71f, -.5f);
		thirdDigitPosition = new Vector3 (-.72f, -.71f, -.5f);
		int totalStars = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().GetTotalStarNumber ();
		string starString = totalStars.ToString ();
		
		if (totalStars < 135) {
			GameObject instantiatedPadlock = (GameObject)Instantiate (padlock, Vector3.zero, Quaternion.identity);

			if (starString.Length == 1) {
				GameObject temp = (GameObject)Instantiate (GetStarNumber (starString[0]), thirdDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
			} else if (starString.Length == 2) {
				GameObject temp = (GameObject)Instantiate (GetStarNumber (starString[0]), secondDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
				temp = (GameObject)Instantiate (GetStarNumber (starString[1]), thirdDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
			} else if (starString.Length == 3) {
				GameObject temp = (GameObject)Instantiate (GetStarNumber (starString[0]), firstDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
				temp = (GameObject)Instantiate (GetStarNumber (starString[1]), secondDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
				temp = (GameObject)Instantiate (GetStarNumber (starString[2]), thirdDigitPosition, Quaternion.identity);
				temp.gameObject.transform.parent = instantiatedPadlock.transform;
			}

			instantiatedPadlock.transform.position = new Vector3 (19.412f, -.365f, -5);
			instantiatedPadlock.transform.parent = transform;
		} else if (totalStars >= 135 || PlayerPrefs.HasKey ("finishedAllCabinLevels")){
			touchOn = true;
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
			Color oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
		}
		
	}

	public void FinishedAllCabinLevels () {
		if (!PlayerPrefs.HasKey ("finishedAllCabinLevels")) 
			PlayerPrefs.SetString ("finishedAllCabinLevels", "true");
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
