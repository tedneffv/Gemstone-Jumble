using UnityEngine;
using System.Collections;

public class OwlieSwapper : MonoBehaviour {

	public GameObject owlieNormal, owlieBlink, owlieLeftBlink, owlieRightBlink, owlieLookDown, owlieLookRight, owlieLookUp, owlieLookLeft, owlieFlapping;
	GameObject currentOwlie;
	float timeStamp, cooldown, targetX, targetY;
	bool owlieSwap, translateOwlie; 

	// Use this for initialization
	void Start () {
		cooldown = Random.Range (1.0f, 2.5f);
		timeStamp = Time.time;
	}
	
	void FixedUpdate () {
		if (owlieSwap && Time.time > timeStamp + cooldown) {
			timeStamp = Time.time;
			cooldown = Random.Range (1.0f, 2.5f);
			GameObject tempOwlie;
			tempOwlie = (GameObject)Instantiate (GetNewOwlie (), currentOwlie.transform.position, Quaternion.identity);
			tempOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (currentOwlie.GetComponent<LevelFailedScreenSlider> ().GetTargetX ());
			Destroy (currentOwlie);
			currentOwlie = tempOwlie;
		}

		if (translateOwlie) {
			if (Mathf.Abs (currentOwlie.transform.position.y - targetY) > .001f) {
				currentOwlie.transform.Translate (new Vector3 (0, targetY - currentOwlie.transform.position.y, 0) * Time.deltaTime * 5);
			} else 
				translateOwlie = false;
		}
	}

	public void SetCurrentOwlie (GameObject owlie) {
		currentOwlie = owlie;
		timeStamp = Time.time;
		owlieSwap = true;
	}

	GameObject GetNewOwlie () {
		switch (Random.Range (0, 6)) {
		case 0: return owlieNormal;
		case 1: return owlieBlink;
		case 2: return owlieLookDown;
		case 3: return owlieLookRight;
		case 4: return owlieLookUp;
		case 5: return owlieLookLeft;
		}
		return null;
	}

	public void LeftEyePoked () {
		timeStamp = Time.time;
		cooldown = Random.Range (1.0f, 2.5f);
		GameObject tempOwlie;
		tempOwlie = (GameObject)Instantiate (owlieLeftBlink, currentOwlie.transform.position, Quaternion.identity);
		tempOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (currentOwlie.GetComponent<LevelFailedScreenSlider> ().GetTargetX ());
		Destroy (currentOwlie);
		currentOwlie = tempOwlie;
	}

	public void RightEyePoked () {
		timeStamp = Time.time;
		cooldown = Random.Range (1.0f, 2.5f);
		GameObject tempOwlie;
		tempOwlie = (GameObject)Instantiate (owlieRightBlink, currentOwlie.transform.position, Quaternion.identity);
		tempOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (currentOwlie.GetComponent<LevelFailedScreenSlider> ().GetTargetX ());
		Destroy (currentOwlie);
		currentOwlie = tempOwlie;
	}

	public void BellyPoked () {
		timeStamp = Time.time;
		cooldown = Random.Range (1.0f, 2.5f);
		GameObject tempOwlie;
		tempOwlie = (GameObject)Instantiate (owlieFlapping, currentOwlie.transform.position, Quaternion.identity);
		tempOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (currentOwlie.GetComponent<LevelFailedScreenSlider> ().GetTargetX ());
		Destroy (currentOwlie);
		currentOwlie = tempOwlie;
	}

	public void TranslateOwlie (float targetX, float targetY) {
		currentOwlie.GetComponent<LevelFailedScreenSlider> ().SetTargetX (targetX);
		this.targetY = targetY;
		translateOwlie = true;
	}

	public void SetOwlieSwap (bool owlieSwap) {
		this.owlieSwap = owlieSwap;
	}

	public GameObject GetCurrentOwlie () {
		return currentOwlie;
	}
}
