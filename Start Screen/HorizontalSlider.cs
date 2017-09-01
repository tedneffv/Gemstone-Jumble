using UnityEngine;
using System.Collections;

public class HorizontalSlider : MonoBehaviour {

	float timeStamp, cooldown, targetX, stutterStart, movementSpeed;
	bool slideHorizontal, wobble, doneWithSlide, mountainSoundPlayed, observationSoundPlayed, titleSoundPlayed, playSoundPlayed, settingSoundPlayed; 
	public GameObject facebookHandler;

	// Use this for initialization
	void Start () {
		movementSpeed = 7;
		stutterStart = 2f;
		timeStamp = Time.time;
		if (gameObject.name == "Mountain") {
			targetX = -1.77f;
			cooldown = stutterStart + .2f;
		} else if (gameObject.name == "Observation Deck") {
			targetX = 2.71f;
			cooldown = stutterStart + .4f;
		} else if (name == "Title") {
			targetX = .06f;
			cooldown = stutterStart + .8f;
		} else if (name == "Play Sign") {
			targetX = 1.96f;
			cooldown = stutterStart + 1.2f;
			gameObject.GetComponentInChildren<WobbleController> ().SetRotateSign (true);
		} else if (name == "Settings Button") {
			targetX = Camera.main.ViewportToWorldPoint (new Vector3 (.07f, 0, 0)).x;
			cooldown = stutterStart + 1f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!doneWithSlide) {
			if (slideHorizontal) {
				transform.Translate (new Vector3 (targetX - transform.position.x, 0, 0) * Time.deltaTime * movementSpeed);
				if (Mathf.Abs (targetX - transform.position.x) < .01f) {
					doneWithSlide = true;					
					if (name == "Title")
						Instantiate (facebookHandler);
				}
			}
			else if (Time.time > timeStamp + cooldown)
				slideHorizontal = true;
		} 
	}
}
