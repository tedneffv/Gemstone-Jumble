using UnityEngine;
using System.Collections;

public class JewelClackCounter : MonoBehaviour {

	static int jewelClackNumber;

	// Use this for initialization
	void Start () {
		jewelClackNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void ResetJewelClackNumber () {
		//Debug.Log ("Reseting Jewel Clack Number");
		jewelClackNumber = 0;
	}

	public static int GetJewelClackNumber () {
		int temp = jewelClackNumber;
		jewelClackNumber++;
		return temp;
	}
}
