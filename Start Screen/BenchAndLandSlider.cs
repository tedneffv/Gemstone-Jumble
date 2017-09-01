using UnityEngine;
using System.Collections;

public class BenchAndLandSlider : MonoBehaviour {
	float timeStamp, cooldown, targetY, stutterStart;
	bool slideUp;

	// Use this for initialization
	void Start () {
		stutterStart = 2f;
		timeStamp = Time.time;
		if (name == "Bench and Land") {
			targetY = -3.14f;
			cooldown = stutterStart + 0;
		} else if (name == "Planet") {
			targetY = 3.89f;
			cooldown = stutterStart + .6f;
		} 
	}
	
	// Update is called once per frame
	void Update () {

		if (slideUp) {
			transform.Translate (new Vector3 (0, targetY - transform.position.y, 0) * Time.deltaTime * 7);
		}
		else if (Time.time > timeStamp + cooldown) {
			slideUp = true;
		}
	}
}
