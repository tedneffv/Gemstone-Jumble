using UnityEngine;
using System.Collections;

public class QuitWordHandler : MonoBehaviour {

	bool leaveHome, goHome, playButtonPressed;
	float startingY, startingX, firstYCoefficient, secondYCoefficient, thirdYCoefficient;
	ButtonHandler buttonHandler;
	
	// Use this for initialization
	void Start () {
		startingX = transform.position.x;
		startingY = GameObject.Find ("Quit Button(Clone)").transform.position.y;
		firstYCoefficient = .62f;
		secondYCoefficient = .5f;
		thirdYCoefficient = .45f;
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (leaveHome) {
			if (gameObject.name == "T(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + firstYCoefficient) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + firstYCoefficient) - transform.position.y)) < .001) {
					//					transform.position = new Vector3 (transform.position.x, startingY + firstYCoefficient, transform.position.z);
					leaveHome = false;
				}
			}

			else if (gameObject.name == "I(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.51f * 2)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.51f * 2)) - transform.position.y)) < .001) {
					//					transform.position = new Vector3 (transform.position.x, startingY + (.49f * 2), transform.position.z);
					leaveHome = false;
				}
			}

			else if (gameObject.name == "U(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.47f * 3)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.47f * 3)) - transform.position.y)) < .001) {
					//					transform.position = new Vector3 (transform.position.x, startingY + (.45f * 2), transform.position.z);
					leaveHome = false;
				}
			}

			else if (gameObject.name == "Capital Q(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.45f * 4)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingY + (.45f * 4)) - transform.position.y)) < .001) {
//										transform.position = new Vector3 (transform.position.x, startingY + (.43f * 2), transform.position.z);
					leaveHome = false;
				}
			}

			else if (gameObject.name == "Quit Confirmation Button(Clone)") {
				transform.Translate (new Vector3 ((startingX + .5f) - transform.position.x, ((startingY + (.45f * 4f)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingX + .5f) - transform.position.x)) < .001 && Mathf.Abs (((startingY + (.45f * 4)) - transform.position.y)) < .001)
					leaveHome = false;
			}

			else if (gameObject.name == "Quit Cancel Button(Clone)") {
				transform.Translate (new Vector3 ((startingX + .5f) - transform.position.x, ((startingY + (.45f * 2.5f)) - transform.position.y), 0) * .0003f * Screen.width);
				if (Mathf.Abs (((startingX + .5f) - transform.position.x)) < .001 && Mathf.Abs (((startingY + (.45f * 2.5f)) - transform.position.y)) < .001)
					leaveHome = false;
			}
		}
		
		else if (goHome) {
			transform.Translate (new Vector3 (buttonHandler.GetQuitButton ().transform.position.x - transform.position.x, (startingY - transform.position.y), 0) * .0003f * Screen.width);
			if (Mathf.Abs (startingY - transform.position.y) < .1f) {
				if (gameObject.name == "Capital Q(Clone)")
					buttonHandler.SetQuitQ (null);
				else if (gameObject.name == "U(Clone)")	
					buttonHandler.SetQuitU (null);
				else if (gameObject.name == "I(Clone)")
					buttonHandler.SetQuitI (null);
				else if (gameObject.name == "T(Clone)")
					buttonHandler.SetQuitT (null);
				else if (gameObject.name == "Quit Confirmation Button(Clone)")
					buttonHandler.SetQuitConformationButton (null);
				if (GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().GetWaitingForQuitDestruction () && buttonHandler.GetQuitQ () == null && buttonHandler.GetQuitU () == null && 
				    buttonHandler.GetQuitI () == null && buttonHandler.GetQuitT () == null) 
					GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().GoHome ();
//				if (GameObject.Find ("Restart Button(Clone)").GetComponent<RestartButtonHandler> ().GetWaitingForDestruction () && buttonHandler.GetQuitQ () == null && buttonHandler.GetQuitU () == null && 
//				    buttonHandler.GetQuitI () == null && buttonHandler.GetQuitT () == null) 
//					GameObject.Find ("Restart Button(Clone)").GetComponent<RestartButtonHandler> ().TimeToLeave ();
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
