using UnityEngine;
using System.Collections;

public class EndOfLevelNumberHandler : MonoBehaviour {

	bool firstDigit, secondDigit, thirdDigit, fourthDigit, fifthDigit, sixthDigit, grow, shrink;
	float cooldown, timeStamp, growthSize, shrinkSize;


	// Use this for initialization
	void Start () {
		cooldown = .02f;
		timeStamp = Time.time;
		grow = true;
		growthSize = .6f;
		shrinkSize = .41f;
	}
	
	// Update is called once per frame
	void Update () {
		if (grow && transform.localScale.x < growthSize && Time.time >= timeStamp + cooldown) {
			transform.localScale = new Vector3 (transform.localScale.x + .1f, transform.localScale.y + .1f, transform.localScale.z + 1f);
			timeStamp = Time.time;
		} else if (grow && transform.localScale.x >= growthSize) {
			shrink = true;
			grow = false;
		}

		if (shrink && transform.localScale.x > shrinkSize && Time.time >= timeStamp + cooldown) {
			transform.localScale = new Vector3 (transform.localScale.x - .1f, transform.localScale.y - .1f, transform.localScale.z);
			timeStamp = Time.time;
		}
	}

	public void SetFirstDigit () {
		firstDigit = true;
	}
}
