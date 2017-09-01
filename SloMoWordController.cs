using UnityEngine;
using System.Collections;

public class SloMoWordController : MonoBehaviour {

	float targetX, targetY;
	bool title;

	// Use this for initialization
	void Start () {
		if (gameObject.name == "Title") {
			title = true;
		}
	}
	
	void Update () {
		transform.Translate (new Vector3 ((targetX - transform.position.x), (targetY - transform.position.y), 0) * .4f);
		if (Mathf.Abs (transform.position.x - targetX) < .01f && Mathf.Abs (transform.position.y - targetY) < .01f) {
			if (!title) {
				targetX = Random.Range (-1.1f, -.9f);
				targetY = Random.Range (3.54f, 3.34f);
			} else {
				targetX = Random.Range (-.1f, .1f);
				targetY = Random.Range (.64f, .84f);
			}
		}
		if (transform.position.x > 9)
			Destroy (gameObject);
	}

	public void SetDestination (float targetX, float targetY) {
		this.targetX = targetX;
		this.targetY = targetY;
	}

	public void GetRidOfSloMo () {
		targetX = 10;
	}
}
