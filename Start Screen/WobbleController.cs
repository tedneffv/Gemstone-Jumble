using UnityEngine;
using System.Collections;

public class WobbleController : MonoBehaviour {

	bool rotateLeft, rotateRight, rotateSign;

	// Use this for initialization
	void Start () {
		rotateRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (rotateSign) {
			RotateSign ();
		}
	}

	void RotateSign () {
		if (rotateLeft) {
			transform.Rotate (new Vector3 (0, 0, 10 - Mathf.Rad2Deg * transform.rotation.z) * Time.deltaTime * 8);
			if (Mathf.Abs (10 - Mathf.Rad2Deg * transform.rotation.z) < .5f) {
				rotateLeft = false;
				rotateRight = true;
			}
		} else if (rotateRight) {
			transform.Rotate (new Vector3 (0, 0, -10 - Mathf.Rad2Deg * transform.rotation.z) * Time.deltaTime * 8);
			if (Mathf.Abs (-10 - Mathf.Rad2Deg * transform.rotation.z) < .5f) {
				rotateRight = false;
				rotateLeft = true;
			}
		}
	}

	public void SetRotateSign (bool rotateSign) {
		this.rotateSign = rotateSign;
	}
}
