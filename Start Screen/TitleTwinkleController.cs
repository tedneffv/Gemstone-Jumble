using UnityEngine;
using System.Collections;

public class TitleTwinkleController : MonoBehaviour {

	public GameObject smallTwinkle, mediumTwinkle, bigTwinkle;
	float instantiationX, instantiationY, cooldown, timeStamp;
	float rotation;

	// Use this for initialization
	void Start () {
		rotation = Random.Range (0, 361);
		InstantiateTwinkle ();
		timeStamp = Time.time;
		cooldown = .25f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timeStamp + cooldown) {
			timeStamp = Time.time;
			InstantiateTwinkle ();
		}
	}

	void InstantiateTwinkle () {
		GameObject instantiatedTwinkle;
		instantiationX = Random.Range (-2.55f, 2.64f);
		instantiationY = Random.Range (1.25f, -1f);
		instantiatedTwinkle = (GameObject)Instantiate (GetRandomTwinkle (), new Vector3 (gameObject.transform.position.x + instantiationX, gameObject.transform.position.y + instantiationY, -5), Quaternion.identity);
		instantiatedTwinkle.transform.rotation = Quaternion.Euler (0, 0, rotation);
		instantiatedTwinkle.transform.parent = transform;
	}

	GameObject GetRandomTwinkle () {
		switch (Random.Range (0, 3)) {
		case 0: return smallTwinkle;
		case 1: return mediumTwinkle;
		case 2: return bigTwinkle;
		}
		return null;
	}
}
