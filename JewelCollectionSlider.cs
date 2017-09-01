using UnityEngine;
using System.Collections;

public class JewelCollectionSlider : MonoBehaviour {

	Vector3 targetPosition;
	bool move, addedToTotal;
	float speed, timeStamp, cooldown;

	// Use this for initialization
	void Start () {
		timeStamp = Time.time;
		cooldown = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			transform.Translate ((new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0) * Time.deltaTime * speed));
			if (Mathf.Abs (targetPosition.y - transform.position.y) < .001f && Mathf.Abs (targetPosition.x - transform.position.x) < .001f) {
				move = false;
				transform.position = targetPosition;
				if (IsSeekingJewel (name)) {
					Destroy (gameObject);
				}
			}
			if (!addedToTotal && Mathf.Abs (targetPosition.y - transform.position.y) < .1f && IsSeekingJewel (name)) {
				addedToTotal = true;
				AddOneToCorrectJewelCollector (name);
			}
		}
	}

	void AddOneToCorrectJewelCollector (string name) {
		switch (name) {
		case "Blue Seeking Jewel": GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().AddOneToJewelCollectorFour (GameObject.Find ("Jewel Collector Four").transform.position); break;
		case "Green Seeking Jewel": GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().AddOneToJewelCollectorThree (GameObject.Find ("Jewel Collector Three").transform.position); break;
		case "Orange Seeking Jewel": GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().AddOneToJewelCollectorSix (GameObject.Find ("Jewel Collector Six").transform.position); break;
		case "Purple Seeking Jewel": GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().AddOneToJewelCollectorFive (GameObject.Find ("Jewel Collector Five").transform.position); break;
		case "Red Seeking Jewel": GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().AddOneToJewelCollectorTwo (GameObject.Find ("Jewel Collector Two").transform.position); break;
		case "White Seeking Jewel": GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ().AddOneToJewelCollectorOne (GameObject.Find ("Jewel Collector One").transform.position); break;
		}
	}

	bool IsSeekingJewel (string name) {
		switch (name) {
		case "Blue Seeking Jewel":
		case "Green Seeking Jewel":
		case "Orange Seeking Jewel":
		case "Purple Seeking Jewel":
		case "Red Seeking Jewel":
		case "White Seeking Jewel":
			return true;
		}
		return false;
	}

	public void SetTargetPosition (Vector3 targetPosition, float speed) {
		this.targetPosition = targetPosition;
		this.speed = speed;
	}

	public void Move () {
		move = true;
	}
}
