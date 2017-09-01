using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaidPowerTracker : MonoBehaviour {

	static int size;
	static List<GameObject> powerList;
	static float timeStamp, coolDown;

	// Use this for initialization
	void Start () {
		powerList = new List<GameObject> ();
		size = 0;
		timeStamp = Time.time; 
		coolDown = 10;
	}

	void Update () {
		if (powerList.Count > 0 && Time.time > timeStamp + coolDown) {
			powerList.Clear ();
		}
	}
	
	public static void AddPowerToList (GameObject power) {
		if (powerList == null) {
			powerList = new List<GameObject> ();
			size = 0;
		}
		timeStamp = Time.time;
		powerList.Add (power);
		size++;
	}

	public static void RemovePowerFromList (GameObject power) {
		if (power == null)
			return;
		powerList.Remove (power);
		size--;
		//Debug.Log ("powerList.Count = " + powerList.Count);
	}

	public static int GetPowerListSize () {
		if (powerList == null)
			return 0;
		return powerList.Count;
	}
}
