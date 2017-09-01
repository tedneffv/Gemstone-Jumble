using UnityEngine;
using System.Collections;

public class PressedPowerButtonHandler : MonoBehaviour {

	Vector3 tempPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePosition ();
	}

	void UpdatePosition () {
		switch (name) {
		case "Pressed Jewel Swap Button(Clone)": tempPosition = GameObject.Find ("Jewel Swap Button(Clone)").transform.position; break;
		case "Pressed Row Destruction Button(Clone)": tempPosition = GameObject.Find ("Row Destruction Button(Clone)").transform.position; break;
		case "Pressed Bomb Power Button(Clone)": tempPosition = GameObject.Find ("Bomb Power Button(Clone)").transform.position; break;
		case "Pressed Multi Star Power Button(Clone)": tempPosition = GameObject.Find ("Multi Star Power Button(Clone)").transform.position; break;
		case "Pressed Single Star Power Button(Clone)": tempPosition = GameObject.Find ("Single Star Power Button(Clone)").transform.position; break;
		}
		transform.position = new Vector3 (tempPosition.x, tempPosition.y, tempPosition.z - 1);
	}
}
