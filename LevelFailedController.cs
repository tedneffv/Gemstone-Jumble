using UnityEngine;
using System.Collections;

public class LevelFailedController : MonoBehaviour {

	Vector2 bounceForce = new Vector2 (0, 450);
	bool bounced;

	// Use this for initialization
	void Start () {
		bounced = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (!bounced) {
			GetComponent<Rigidbody2D>().AddForce (bounceForce);
			bounced = true;
		}
	}
}
