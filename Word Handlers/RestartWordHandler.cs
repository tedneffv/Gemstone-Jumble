using UnityEngine;
using System.Collections;

public class RestartWordHandler : MonoBehaviour {

	bool leaveHome, goHome, playButtonPressed;
	float startingY, startingX, firstYCoefficient, secondYCoefficient, thirdYCoefficient;
	ButtonHandler buttonHandler;
	
	// Use this for initialization
	void Start () {
		startingX = transform.position.x;
		startingY = GameObject.Find ("Restart Button(Clone)").transform.position.y;
		firstYCoefficient = .62f;
		secondYCoefficient = .5f;
		thirdYCoefficient = .45f;
		buttonHandler = GameObject.Find ("Button Handler").GetComponent<ButtonHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (leaveHome) {
			if (gameObject.name == "Restart Second T(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + firstYCoefficient) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + firstYCoefficient) - transform.position.y)) < .001) {
					//					transform.position = new Vector3 (transform.position.x, startingY + firstYCoefficient, transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Restart Second R(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.51f * 2)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + (.51f * 2)) - transform.position.y)) < .001) {
					//					transform.position = new Vector3 (transform.position.x, startingY + (.49f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Restart A(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.47f * 3)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + (.47f * 3)) - transform.position.y)) < .001) {
					//					transform.position = new Vector3 (transform.position.x, startingY + (.45f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Restart T(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.45f * 4)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + (.45f * 4)) - transform.position.y)) < .001) {
					//										transform.position = new Vector3 (transform.position.x, startingY + (.43f * 2), transform.position.z);
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Restart S(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.44f * 5)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + (.44f * 5)) - transform.position.y)) < .001) {
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Restart E(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.425f * 6)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + (.425f * 6)) - transform.position.y)) < .001) {
					leaveHome = false;
				}
			}
			else if (gameObject.name == "Restart Capital R(Clone)") {
				transform.Translate (new Vector3 (0, ((startingY + (.42f * 7)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingY + (.42f * 7)) - transform.position.y)) < .001) {
					leaveHome = false;
				}
			}
			
			else if (gameObject.name == "Restart Confirmation Button(Clone)") {
				transform.Translate (new Vector3 ((startingX + .5f) - transform.position.x, ((startingY + (.45f * 6.5f)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingX + .5f) - transform.position.x)) < .001 && Mathf.Abs (((startingY + (.45f * 6.5f)) - transform.position.y)) < .001)
					leaveHome = false;
			}
			
			else if (gameObject.name == "Restart Cancel Button(Clone)") {
				transform.Translate (new Vector3 ((startingX + .5f) - transform.position.x, ((startingY + (.45f * 5f)) - transform.position.y), 0) * .2f);
				if (Mathf.Abs (((startingX + .5f) - transform.position.x)) < .001 && Mathf.Abs (((startingY + (.45f * 5f)) - transform.position.y)) < .001)
					leaveHome = false;
			}
		}
		
		else if (goHome) {
			transform.Translate (new Vector3 (buttonHandler.GetRestartButton ().transform.position.x - transform.position.x, (startingY - transform.position.y), 0) * .3f);
			if (Mathf.Abs (startingY - transform.position.y) < .1f) {
				if (gameObject.name == "Restart Capital R(Clone)")
					buttonHandler.SetRestartFirstR (null);
				else if (gameObject.name == "Restart E(Clone)")	
					buttonHandler.SetRestartE (null);
				else if (gameObject.name == "Restart S(Clone)")
					buttonHandler.SetRestartS (null);
				else if (gameObject.name == "Restart First T(Clone)")
					buttonHandler.SetRestartFirstT (null);
				else if (gameObject.name == "Restart A(Clone)")
					buttonHandler.SetRestartA (null);
				else if (gameObject.name == "Restart Second R(Clone)")
					buttonHandler.SetRestartSecondR (null);
				else if (gameObject.name == "Restart Second T(Clone)")
					buttonHandler.SetRestartSecondT (null);
				else if (gameObject.name == "Restart Confirmation Button(Clone)")
					buttonHandler.SetRestartConfirmationButton (null);
				if (GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().GetWaitingForQuitDestruction () && buttonHandler.GetRestartFirstR () == null && buttonHandler.GetRestartE () == null && 
				    buttonHandler.GetRestartS () == null && buttonHandler.GetRestartSecondT () == null && buttonHandler.GetRestartA () == null && buttonHandler.GetRestartSecondR () == null && buttonHandler.GetRestartSecondT () == null) 
					GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().GoHome ();
				if (GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().GetWaitingForDestruction () && buttonHandler.GetRestartFirstR () == null && buttonHandler.GetRestartE () == null && 
				    buttonHandler.GetRestartS () == null && buttonHandler.GetRestartSecondT () == null && buttonHandler.GetRestartA () == null && buttonHandler.GetRestartSecondR () == null && buttonHandler.GetRestartSecondT () == null) 
					GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().TimeToLeave ();
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
