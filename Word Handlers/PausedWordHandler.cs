using UnityEngine;
using System.Collections;

public class PausedWordHandler : MonoBehaviour {

	bool leaveHome, goHome;
	float startingY, firstYCoefficient, secondYCoefficient, thirdYCoefficient;
	ButtonHandler buttonHandler;

	// Use this for initialization
	void Start () {
		startingY = GameObject.Find ("Pause Button(Clone)").transform.position.y;
		startingY += .2f;
		firstYCoefficient = .62f;
		secondYCoefficient = .5f;
		thirdYCoefficient = .45f;
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (leaveHome) {
			if (gameObject.name == "D(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + firstYCoefficient) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + firstYCoefficient) - transform.position.y)) < .001) {
//					transform.position = new Vector3 (transform.position.x, startingY + firstYCoefficient, transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "E(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.51f * 2)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.51f * 2)) - transform.position.y)) < .001) {
//					transform.position = new Vector3 (transform.position.x, startingY + (.49f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "S(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.47f * 3)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.47f * 3)) - transform.position.y)) < .001) {
//					transform.position = new Vector3 (transform.position.x, startingY + (.45f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "U(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.45f * 4)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.45f * 4)) - transform.position.y)) < .001) {
//					transform.position = new Vector3 (transform.position.x, startingY + (.43f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "A(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.44f * 5)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.44f * 5)) - transform.position.y)) < .001) {
//					transform.position = new Vector3 (transform.position.x, startingY + (.42f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Capital P(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.435f * 6)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.435f * 6)) - transform.position.y)) < .001) {
//					transform.position = new Vector3 (transform.position.x, startingY + (.415f * 2), transform.position.z);
					leaveHome = false;
				}
			}
		}

		else if (goHome) {
			transform.Translate (new Vector3 (0, (startingY - transform.position.y), 0) * .2f);
			if (Mathf.Abs (startingY - transform.position.y) < .01f) {
				if (gameObject.name == "Capital P(Clone)")
					buttonHandler.SetP (null);
				else if (gameObject.name == "A(Clone)")	
					buttonHandler.SetA (null);
				else if (gameObject.name == "U(Clone)")
					buttonHandler.SetU (null);
				else if (gameObject.name == "S(Clone)")
					buttonHandler.SetS (null);
				else if (gameObject.name == "E(Clone)")
					buttonHandler.SetE (null);
				else if (gameObject.name == "D(Clone)")
					buttonHandler.SetD (null);
				Destroy (gameObject);  
			}
		}
	}

	public void LeaveHome () {
		goHome = false;
		leaveHome = true;
	}

	public void GoHome () {
		leaveHome = false;
		goHome = true;
	}
}
