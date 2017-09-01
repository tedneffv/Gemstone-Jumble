using UnityEngine;
using System.Collections;

public class ScoreNumberSizeIncreaser : MonoBehaviour {
	bool firstDigit, secondDigit, thirdDigit, fourthDigit, fifthDigit, sixthDigit, grow, shrink;
	float cooldown, timeStamp, growthSize, shrinkSize, startSize;
	
	
	// Use this for initialization
	void Start () {
		cooldown = .02f;
		timeStamp = Time.time;
		shrinkSize = gameObject.transform.localScale.x;
		//Debug.Log ("shrinkSize = " + shrinkSize);
		growthSize = shrinkSize + .2f;
//		grow = true;
//		if (tag == "Score Number") {
//			growthSize = .7f;
//			shrinkSize = .51f;
//		} else if (tag == "Coin Number") {
//			growthSize = .6f;
//			shrinkSize = .41f;
//
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (grow && transform.localScale.x < growthSize && Time.time >= timeStamp + cooldown) {
			transform.localScale = new Vector3 (transform.localScale.x + .1f, transform.localScale.y + .1f, transform.localScale.z);
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

	public void SetGrow (bool grow) {
		this.grow = grow;
	}
	
	public void SetFirstDigit () {
		firstDigit = true;
	}
}
