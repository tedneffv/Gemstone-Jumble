using UnityEngine;
using System.Collections;

public class TouchToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void TurnTouchOn () {
		if (GameObject.Find ("Play Button(Clone)") != null)
			GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().SetTouchOn (true);
		if (GameObject.Find ("Restart Button(Clone)") != null)
			GameObject.Find ("Restart Button(Clone)").GetComponent<RestartButtonHandler> ().SetTouchOn (true);
		if (GameObject.Find ("Quit Button(Clone)") != null)
			GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().SetTouchOn (true);
	}

	public void TurnTouchOff () {
		GameObject.Find ("Play Button(Clone)").GetComponent<PlayButtonHandler> ().SetTouchOn (false);
		GameObject.Find ("Restart Button(Clone)").GetComponent<RestartButtonHandler> ().SetTouchOn (false);
		GameObject.Find ("Quit Button(Clone)").GetComponent<QuitButtonHandler> ().SetTouchOn (false);
	}
}
