using UnityEngine;
using System.Collections;

public class FallingJewelsGenerator : MonoBehaviour {

	public GameObject blueJewel, greenJewel, orangeJewel, purpleJewel, redJewel, whiteJewel;
	int timeCount, modNumber;
	GameObject instantiatedJewel;

	// Use this for initialization
	void Start () {
		timeCount = 0;
		modNumber = Random.Range (0, 21);
	}
	
	// Update is called once per frame
	void Update () {
		if (timeCount > modNumber) {
			timeCount = 0;
			modNumber = Random.Range (0, 21);
			instantiatedJewel = (GameObject)Instantiate (GetRandomJewel (), new Vector3 (Random.Range (-2.8f, 2.8f), Random.Range (7f, 10f), -.1f), Quaternion.identity);
			instantiatedJewel.GetComponent<Rigidbody2D> ().gravityScale = Random.Range (.1f, .25f);
			instantiatedJewel.GetComponent<Rigidbody2D> ().AddTorque (Random.Range (-300, 300));
		}
		else
			timeCount++;
	}

	GameObject GetRandomJewel () {
		switch (Random.Range (0, 6)) {
		case 0: return blueJewel;
		case 1: return greenJewel;
		case 2: return orangeJewel;
		case 3: return purpleJewel;
		case 4: return redJewel;
		case 5: return whiteJewel;
		}
		return null;
	}
}
