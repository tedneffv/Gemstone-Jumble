using UnityEngine;
using System.Collections;

public class OwlieFlyUp : MonoBehaviour {

	float targetY, slideSpeed;
	bool slide, explodeNumber, flyAway, buttonSlid;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		slideSpeed= .001f;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (flyAway) {
			transform.Translate (new Vector3 (0, targetY - transform.position.y, 0) * Time.deltaTime * slideSpeed); 
			if (Mathf.Abs (targetY - transform.position.y) < .001f) {
				if (targetY == 10)
					Destroy (gameObject);
				transform.position = new Vector3 (transform.position.x, targetY, transform.position.z);
				slide = false;
			}
			if (targetY == 10) {
				soundHandler.QuietOwlieFlapping ();
			}
			if (targetY == 10 && Mathf.Abs (targetY - transform.position.y) < 1) {
				Destroy (gameObject);
			}
			if (slideSpeed < 3f) {
				slideSpeed = (slideSpeed + (slideSpeed * .1f));
			}
		}
	}

	void OnDestroy () {
		GameObject.Find ("Game Manager").GetComponent<SoundHandler> ().StopOwlieFlapping ();
	}
	
	public void SetTargetY (float targetY) {
		this.targetY = targetY;
		slide = true;
	}
	
	public float GetTargetY () {
		return targetY;
	}
	
	public void SetExplodeNumber (bool explodeNumber) {
		this.explodeNumber = explodeNumber;
	}
	
	public bool Sliding () {
		return slide;
	}

	public void FlyAway () {
		Destroy (gameObject.GetComponent<OwlieSlider> ());
		flyAway = true;
		targetY = 10;
	}

}
