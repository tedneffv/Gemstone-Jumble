using UnityEngine;
using System.Collections;

public class NoLivesScreenButtonHandler : MonoBehaviour {

	public GameObject pressedPurchaseLivesButton, pressedPurchaseCoinsButton, pressedGoHomeButton;
	GameObject instantiatedPressedButton;

	// Use this for initialization
	void Start () {
	
	}
	
	public void ButtonPressed () {
		if (instantiatedPressedButton == null) {
			switch (name) {
			case "Purchase Lives Button(Clone)": instantiatedPressedButton = (GameObject)Instantiate (pressedPurchaseLivesButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity); break;
			case "Purchase Coins Button(Clone)": instantiatedPressedButton = (GameObject)Instantiate (pressedPurchaseCoinsButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity); break;
			case "Return Home Button(Clone)": instantiatedPressedButton = (GameObject)Instantiate (pressedGoHomeButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z -1), Quaternion.identity); break;
			}
		}
	}

	public void ButtonUnpressed () {
		if (instantiatedPressedButton != null)
			Destroy (instantiatedPressedButton);
	}

	public bool IsPressed () {
		return instantiatedPressedButton == null;
	}
}
