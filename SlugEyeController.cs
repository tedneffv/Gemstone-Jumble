using UnityEngine;
using System.Collections;

public class SlugEyeController : MonoBehaviour {

	public GameObject eye1, eye2, eye3, eye4, eye5, eye6, eye7, eye8, eye9;
	GameObject instantiatedEye;
	float cooldown, timestamp;

	// Use this for initialization
	void Start () {
		instantiatedEye = (GameObject)Instantiate (GetRandomEye (), new Vector3 (transform.position.x, transform.position.y + .12f, transform.position.z - .01f), Quaternion.identity);
		instantiatedEye.transform.parent = transform;
		cooldown = Random.Range (.5f, 3f);
		timestamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		if (Time.time > timestamp + cooldown) {
			timestamp = Time.time;
			cooldown = Random.Range (.5f, 3f);
			Destroy (instantiatedEye);
			instantiatedEye = (GameObject)Instantiate (GetRandomEye (), new Vector3 (transform.position.x, transform.position.y + .12f, transform.position.z - .01f), Quaternion.identity);
			instantiatedEye.transform.parent = transform;
		}
	}

	GameObject GetRandomEye () {
		switch (Random.Range (0, 9)) {
		case 0: return eye1;
		case 1: return eye2;
		case 2: return eye3;
		case 3: return eye4;
		case 4: return eye5;
		case 5: return eye6;
		case 6: return eye7;
		case 7: return eye8;
		case 8: return eye9;
		}
		return null;
	}
}
