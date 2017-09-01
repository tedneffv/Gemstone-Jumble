using UnityEngine;
using System.Collections;

public class LeaveLevelScreenHandler : MonoBehaviour {

	bool darken;
	TransitionShadeController transitionShadeController;

	// Use this for initialization
	void Start () {
		transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			darken = true;
			transitionShadeController.DarkenShade ();
		} else if (darken && transitionShadeController.GetAlpha () >= 1) {
			if (GameObject.Find ("The Mountain Level Progression") != null) {
				GameObject.Find ("Level Selecton Position Saver").GetComponent<MountainLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
			}
			else if (GameObject.Find ("The City Level Progression") != null) {
				GameObject.Find ("Level Selection Position Saver").GetComponent<CityLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
			}
			else if (GameObject.Find ("The Cabin Level Progression") != null) {
				GameObject.Find ("Level Selection Position Saver").GetComponent<CabinLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
			}
			else if (GameObject.Find ("Launchpad Level Progression") != null) {
				GameObject.Find ("Level Selection Position Saver").GetComponent<LaunchpadLevelPositionSaver> ().SetCameraPositionY (GameObject.Find ("Main Camera").transform.position.y);
			}
			Application.LoadLevel ("New Level Map");
		}
	}
}
