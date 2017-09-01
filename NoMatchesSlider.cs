using UnityEngine;
using System.Collections;

public class NoMatchesSlider : MonoBehaviour {
	float targetY, errorDistance, timeStamp, cooldown;
	int targetYNumber, speed;
	bool moveUp, moveDown;

	// Use this for initialization
	void Start () {
		targetY = 4.39f;
		targetYNumber = 0;
		errorDistance = .001f;
		speed = 13;
		cooldown = .75f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (moveDown) {
			if (Mathf.Abs (targetY - transform.position.y) > errorDistance) {
				transform.Translate (new Vector3 (0, (targetY - transform.position.y), 0) * Time.deltaTime * speed);
			}
			else if (!moveUp){
				timeStamp = Time.time;
				moveUp = true;
			}
			
			if (moveUp && Time.time > timeStamp + cooldown) {
				speed = 3;
				targetY = 7;
			}
			if (transform.position.y > 6) {
				transform.position = new Vector3 (.67f, 6, -1.1f);
				speed = 13;
				errorDistance = .001f;
				targetYNumber = 0;
				targetY = 4.39f;
				moveUp = false;
				moveDown = false;
			}
		}
	}

	public void SetMoveDown (bool moveDown) {
		this.moveDown = moveDown;
	}
}
