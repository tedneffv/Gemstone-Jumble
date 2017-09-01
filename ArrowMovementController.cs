using UnityEngine;
using System.Collections;

public class ArrowMovementController : MonoBehaviour {

	float targetY, firstTarget, secondTarget; 
	bool targetSet, firstTargetActivated;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (targetSet) {
			transform.Translate (new Vector3 (0, targetY - transform.position.y, 0) * Time.deltaTime * 7, Space.World);
			if (Mathf.Abs (targetY - transform.position.y) < .01f) {
				GetNewTargetY ();
			}
		}
		if (transform.position.y > 20 || transform.position.y  < -20) {
//			Destroy (gameObject);
		}
	}

	public void SetYTargets (float firstTarget, float secondTarget) {
		firstTargetActivated = true;
		this.targetY = firstTarget;
		this.firstTarget = firstTarget;
		this.secondTarget = secondTarget;
		targetSet = true;
	}

	void GetNewTargetY () {
		if (firstTargetActivated) {
			targetY = secondTarget;
			firstTargetActivated = false;
		} else {
			targetY = firstTarget;
			firstTargetActivated = true;
		}
	}
}
