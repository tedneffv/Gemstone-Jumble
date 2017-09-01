using UnityEngine;
using System.Collections;

public class LevelFailedScreenSlider : MonoBehaviour {

	float targetX, slideSpeed;
	bool slide, explodeNumber;

	// Use this for initialization
	void Start () {
		slideSpeed = .15f;
		GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetFirstMoveMade (false);
		if (name == "No ads to display(Clone)")
			StartCoroutine (GetRidOfOwlie ());
	}
	
	void Update () {
		transform.Translate (new Vector3 ((targetX - transform.position.x), 0, 0) * slideSpeed);
		if (tag == "Heart Number" && explodeNumber && Mathf.Abs (targetX - transform.position.x) < .01f)
			GameObject.Find ("Screen Handlers").GetComponent<LevelFailedScreenHandler> ().ExplodeNumber ();
		if (Mathf.Abs (targetX - transform.position.x) < .001f) {
			if (targetX == 10)
				Destroy (gameObject);
			slide = false;
		}
		if (targetX == 10 && Mathf.Abs (targetX - transform.position.x) < 1) {
			Destroy (gameObject);
		}
	}

	public void SetTargetX (float targetX) {
		this.targetX = targetX;
		slide = true;
	}

	public float GetTargetX () {
		return targetX;
	}

	public void SetExplodeNumber (bool explodeNumber) {
		this.explodeNumber = explodeNumber;
	}

	public bool Sliding () {
		return slide;
	}

	public void SetSlideSpeed (float slideSpeed) {
		this.slideSpeed = slideSpeed;
	}

	IEnumerator GetRidOfOwlie () {
		yield return new WaitForSeconds(3);
		SetTargetX (10);
	}
}
