using UnityEngine;
using System.Collections;

public class OwlieSlider : MonoBehaviour {

	float targetY;
	bool slide, explodeNumber;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, targetY - transform.position.y, 0) * Time.deltaTime * 5); 
		if (Mathf.Abs (targetY - transform.position.y) < .001f) {
			if (targetY == 10)
				Destroy (gameObject);
			transform.position = new Vector3 (transform.position.x, targetY, transform.position.z);
			slide = false;
		}
		if (targetY == 10 && Mathf.Abs (targetY - transform.position.y) < 1) {
			Destroy (gameObject);
		}
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
}
