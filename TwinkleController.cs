using UnityEngine;
using System.Collections;

public class TwinkleController : MonoBehaviour {

	public GameObject smallTwinkle, mediumTwinkle, bigTwinkle;
	GameObject twinkleOne, twinkleTwo, twinkleThree;
	float alphaTwinkleOne, alphaTwinkleTwo, alphaTwinkleThree;
	bool increaseTwinkleOneAlpha, increaseTwinkleTwoAlpha, increaseTwinkleThreeAlpha;
	Vector3 twinkleOnePosition, twinkleTwoPosition, twinkleThreePosition;
	Quaternion twinkleOneRotation, twinkleTwoRotation, twinkleThreeRotation;
	Color twinkleOneOldColor, twinkleTwoOldColor, twinkleThreeOldColor;
	SpriteRenderer twinkleOneSpriteRenderer, twinkleTwoSpriteRenderer, twinkleThreeSpriteRenderer;
	float twinkleOneAlphaIncrement, twinkleTwoAlphaIncrement, twinkleThreeAlphaIncrement;
	// Use this for initialization
	void Start () {
		alphaTwinkleOne = 0;
		alphaTwinkleTwo = 0;
		alphaTwinkleThree = 0;
	}
	
	// Update is called once per frame
	void Update () {
		InstantiateTwinkle ();
		if (twinkleOne != null)
			twinkleOne.transform.position = new Vector3 (transform.position.x + twinkleOnePosition.x, transform.position.y + twinkleOnePosition.y, transform.position.z - 1);
		if (twinkleTwo != null)
			twinkleTwo.transform.position = new Vector3 (transform.position.x + twinkleTwoPosition.x, transform.position.y + twinkleTwoPosition.y, transform.position.z - 1);
		if (twinkleThree != null)
			twinkleThree.transform.position = new Vector3 (transform.position.x + twinkleThreePosition.x, transform.position.y + twinkleThreePosition.y, transform.position.z - 1);
		ChangeTwinkleOneAlpha ();
		ChangeTwinkleTwoAlpha ();
//		ChangeTwinkleThreeAlpha ();
//		if (twinkleTwo != null) {
//			twinkleTwo.transform.position = new Vector3 (transform.position.x + twinkleOnePosition.x, transform.position.y + twinkleOnePosition.y, transform.position.z - 1);
//			ChangeTwinkleTwoAlpha ();
//		}
	}

	void InstantiateTwinkle () {
		if ((twinkleOne == null && twinkleTwo == null) || (twinkleOne == null && twinkleTwo != null && alphaTwinkleTwo < .1f && !increaseTwinkleTwoAlpha)) {
			twinkleOnePosition = GetTwinklePosition ();
			twinkleOneRotation = Quaternion.Euler (0, 0, Random.Range (0, 180));
			if (twinkleOne != null)
				Destroy (twinkleOne);
			switch (Random.Range (0, 3)) {
			case 0: twinkleOne = (GameObject)Instantiate (smallTwinkle, new Vector3 (transform.position.x + twinkleOnePosition.x, transform.position.y + twinkleOnePosition.y, transform.position.z - 6), twinkleOneRotation); break;
			case 1: twinkleOne = (GameObject)Instantiate (mediumTwinkle, new Vector3 (transform.position.x + twinkleOnePosition.x, transform.position.y + twinkleOnePosition.y, transform.position.z - 6), twinkleOneRotation); break;
			case 2: twinkleOne = (GameObject)Instantiate (bigTwinkle, new Vector3 (transform.position.x + twinkleOnePosition.x, transform.position.y + twinkleOnePosition.y, transform.position.z - 6), twinkleOneRotation); break;
			}
			increaseTwinkleOneAlpha = true;
			twinkleOneAlphaIncrement = Random.Range (.01f, .04f);
			alphaTwinkleOne = 0;
			twinkleOneSpriteRenderer = twinkleOne.GetComponent<SpriteRenderer> ();
			twinkleOneOldColor = twinkleOneSpriteRenderer.color;
			twinkleOneSpriteRenderer.color = new Color (twinkleOneOldColor.r, twinkleOneOldColor.g, twinkleOneOldColor.b, alphaTwinkleOne);
			increaseTwinkleOneAlpha = true;
		}
		if ((twinkleTwo == null && twinkleOne != null && alphaTwinkleOne < .1f && !increaseTwinkleOneAlpha)) {
			twinkleTwoPosition = GetTwinklePosition ();
			twinkleTwoRotation = Quaternion.Euler (0, 0, Random.Range (0, 180));
			if (twinkleTwo != null) 
				Destroy (twinkleTwo);
			switch (Random.Range (0, 3)) {
			case 0: twinkleTwo = (GameObject)Instantiate (smallTwinkle, new Vector3 (transform.position.x + twinkleTwoPosition.x, transform.position.y + twinkleTwoPosition.y, transform.position.z - 6), twinkleTwoRotation); break;
			case 1: twinkleTwo = (GameObject)Instantiate (mediumTwinkle, new Vector3 (transform.position.x + twinkleTwoPosition.x, transform.position.y + twinkleTwoPosition.y, transform.position.z - 6), twinkleTwoRotation); break;
			case 2: twinkleTwo = (GameObject)Instantiate (bigTwinkle, new Vector3 (transform.position.x + twinkleTwoPosition.x, transform.position.y + twinkleTwoPosition.y, transform.position.z - 6), twinkleTwoRotation); break;
			}
			increaseTwinkleTwoAlpha = true;
			twinkleTwoAlphaIncrement = Random.Range (.01f, .04f);
			alphaTwinkleTwo = 0;
			twinkleTwoSpriteRenderer = twinkleTwo.GetComponent<SpriteRenderer> ();
			twinkleTwoOldColor = twinkleTwoSpriteRenderer.color;
			twinkleTwoSpriteRenderer.color = new Color (twinkleTwoOldColor.r, twinkleTwoOldColor.g, twinkleTwoOldColor.b, alphaTwinkleTwo);
			increaseTwinkleOneAlpha = true;
		}
		if ((twinkleOne == null && twinkleTwo == null) || (alphaTwinkleOne < .25f && !increaseTwinkleOneAlpha && alphaTwinkleTwo <.25f && !increaseTwinkleTwoAlpha)) {
			//Debug.Log ("Instantiating twinkle Three");
			twinkleThreePosition= GetTwinklePosition ();
			twinkleThreeRotation = Quaternion.Euler (0, 0, Random.Range (0, 180));
			if (twinkleThree != null)
				Destroy (twinkleThree);
			switch (Random.Range (0, 3)) {
			case 0: twinkleThree = (GameObject)Instantiate (smallTwinkle, new Vector3 (transform.position.x + twinkleThreePosition.x, transform.position.y + twinkleThreePosition.y, transform.position.z - 6), twinkleThreeRotation); break;
			case 1: twinkleThree = (GameObject)Instantiate (mediumTwinkle, new Vector3 (transform.position.x + twinkleThreePosition.x, transform.position.y + twinkleThreePosition.y, transform.position.z - 6), twinkleThreeRotation); break;
			case 2: twinkleThree = (GameObject)Instantiate (bigTwinkle, new Vector3 (transform.position.x + twinkleThreePosition.x, transform.position.y + twinkleThreePosition.y, transform.position.z - 6), twinkleThreeRotation); break;
			}
			increaseTwinkleThreeAlpha = true;
			twinkleThreeAlphaIncrement = Random.Range (.01f, .04f);
			alphaTwinkleThree = 0;
			twinkleThreeSpriteRenderer = twinkleThree.GetComponent<SpriteRenderer> ();
			twinkleThreeOldColor = twinkleThreeSpriteRenderer.color;
			twinkleThreeSpriteRenderer.color = new Color (twinkleThreeOldColor.r, twinkleThreeOldColor.g, twinkleThreeOldColor.b, alphaTwinkleThree);
			increaseTwinkleThreeAlpha = true;
		}
	}

	void OnDestroy () {
		if (twinkleOne != null)
			Destroy (twinkleOne);
		if (twinkleTwo != null)
			Destroy (twinkleTwo);
		if (twinkleThree != null)
			Destroy (twinkleThree);
	}

	Vector3 GetTwinklePosition () {
		switch (Random.Range (0, 10)) {
		case 0: return new Vector3 (Random.Range (-1.3f, -.33f), 1.5f, transform.position.z - 1); break;
		case 1: return new Vector3 (Random.Range (.33f, 1.3f), 1.5f, transform.position.z - 1); break;
		case 2: return new Vector3 (Random.Range (-1.62f, 1.62f), 1.12f, transform.position.z - 1); break;
		case 3: return new Vector3 (Random.Range (-1.7f, 1.7f), .8f, transform.position.z - 1); break;
		case 4: return new Vector3 (Random.Range (-1.62f, 1.62f), .4f, transform.position.z - 1); break;
		case 5: return new Vector3 (Random.Range (-1.41f, 1.41f), .02f, transform.position.z - 1); break;
		case 6: return new Vector3 (Random.Range (-1.19f, 1.19f), -.25f, transform.position.z - 1); break;
		case 7: return new Vector3 (Random.Range (-.92f, .92f), -.57f, transform.position.z - 1); break;
		case 8: return new Vector3 (Random.Range (-.62f, .62f), -.89f, transform.position.z - 1); break;
		case 9: return new Vector3 (Random.Range (-.26f, .26f), -1.31f, transform.position.z - 1); break;
		case 10: return new Vector3 (Random.Range (-.1f, .1f), -1.47f, transform.position.z - 1); break;
		case 11: return new Vector3 (-.82f, 1.71f, transform.position.z - 1); break;
		case 12: return new Vector3 (.82f, 1.71f, transform.position.z - 1); break;
		case 13: return new Vector3 (0, -1.69f, transform.position.z - 1); break;
		}
		return new Vector3 (-10, -10, -10);
	}

	void ChangeTwinkleOneAlpha () {
		if (twinkleOne == null)
			return;
		twinkleOneOldColor = twinkleOneSpriteRenderer.color;
		if (increaseTwinkleOneAlpha && alphaTwinkleOne <= 1) {
			alphaTwinkleOne += twinkleOneAlphaIncrement;
			twinkleOneSpriteRenderer.color = new Color (twinkleOneOldColor.r, twinkleOneOldColor.g, twinkleOneOldColor.b, alphaTwinkleOne);
			if (alphaTwinkleOne >= 1)
				increaseTwinkleOneAlpha = false;
		} else if (!increaseTwinkleOneAlpha && alphaTwinkleOne >= 0) {
			alphaTwinkleOne -= twinkleOneAlphaIncrement;
			twinkleOneSpriteRenderer.color = new Color (twinkleOneOldColor.r, twinkleOneOldColor.g, twinkleOneOldColor.b, alphaTwinkleOne);
			if (alphaTwinkleOne <= 0)
				Destroy (twinkleOne);
		}
	}

	void ChangeTwinkleTwoAlpha () {
		if (twinkleTwo == null)
			return;
		twinkleTwoOldColor = twinkleTwoSpriteRenderer.color;
		if (increaseTwinkleTwoAlpha && alphaTwinkleTwo <= 1) {
			alphaTwinkleTwo += twinkleTwoAlphaIncrement;
			twinkleTwoSpriteRenderer.color = new Color (twinkleTwoOldColor.r, twinkleTwoOldColor.g, twinkleTwoOldColor.b, alphaTwinkleTwo);
			if (alphaTwinkleTwo >= 1)
				increaseTwinkleTwoAlpha = false;
		} else if (!increaseTwinkleTwoAlpha && alphaTwinkleTwo >= 0) {
			alphaTwinkleTwo -= twinkleTwoAlphaIncrement;
			twinkleTwoSpriteRenderer.color = new Color (twinkleTwoOldColor.r, twinkleTwoOldColor.g, twinkleTwoOldColor.b, alphaTwinkleTwo);
			if (alphaTwinkleTwo <= 0)
				Destroy (twinkleTwo);
		}
	}

	void ChangeTwinkleThreeAlpha () {
		if (twinkleThree == null)
			return;
		twinkleThreeOldColor = twinkleThreeSpriteRenderer.color;
		if (increaseTwinkleThreeAlpha && alphaTwinkleThree <= 1) {
			alphaTwinkleThree += twinkleThreeAlphaIncrement;
			twinkleThreeSpriteRenderer.color = new Color (twinkleThreeOldColor.r, twinkleThreeOldColor.g, twinkleThreeOldColor.b, alphaTwinkleThree);
			if (alphaTwinkleThree >= 1)
				increaseTwinkleThreeAlpha = false;
		} else if (!increaseTwinkleThreeAlpha && alphaTwinkleThree >= 0) {
			alphaTwinkleThree -= twinkleThreeAlphaIncrement;
			twinkleThreeSpriteRenderer.color = new Color (twinkleThreeOldColor.r, twinkleThreeOldColor.g, twinkleThreeOldColor.b, alphaTwinkleThree);
			if (alphaTwinkleThree <= 0)
				Destroy (twinkleThree);
		}
	}
}
