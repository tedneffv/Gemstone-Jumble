using UnityEngine;
using System.Collections;

public class TimeBombBannerController : MonoBehaviour {

	bool firstStop, secondStop, thirdStop;
	float firstPosition, secondPosition, thirdPosition, startButtonTarget, currentPosition;
	GameObject startButton;

	// Use this for initialization
	void Start () {
		firstStop = true;
		firstPosition = 0f;
		secondPosition = -1f;
		thirdPosition = 11f;
		currentPosition = firstPosition;
		startButton = GameObject.Find ("Start Button");
		startButtonTarget = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (currentPosition - transform.position.x, 0, 0) * Time.deltaTime * 10f);
		if (firstStop && Mathf.Abs (transform.position.x - currentPosition) < .001f) {
			transform.position = new Vector3 (0, transform.position.y, transform.position.z);
		}
		if (firstStop && Mathf.Abs (transform.position.x - currentPosition) < 1f) {
			startButton.transform.Translate (new Vector3 (0, (startButtonTarget - startButton.transform.position.y), 0) * Time.deltaTime * 10f);
			if (Mathf.Abs (startButton.transform.position.x - (startButtonTarget)) < .001f)
				startButton.transform.position = new Vector3 (startButton.transform.position.x, startButtonTarget, startButton.transform.position.z);
		}

		if (secondStop && Mathf.Abs (transform.position.x - currentPosition) < .1f) {
			secondStop = false;
			thirdStop = true;
			currentPosition = thirdPosition;
		}
		if ((secondStop || thirdStop) && Mathf.Abs (startButton.transform.position.y - startButtonTarget) > .001f) {
			startButton.transform.Translate (new Vector3 (0, (startButtonTarget - startButton.transform.position.y), 0) * Time.deltaTime * 10f);
		}

		if (thirdStop && transform.position.x > 10) {
			Destroy (gameObject);
		}

	}

	public void GoToSecondStop () {
		firstStop = false;
		secondStop = true;
		currentPosition = secondPosition;
		startButtonTarget = .65f;
	}
}
