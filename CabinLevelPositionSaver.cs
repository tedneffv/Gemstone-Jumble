using UnityEngine;
using System.Collections;

public class CabinLevelPositionSaver : MonoBehaviour {

	float cameraPositionY;

	void Start () {
		if (!PlayerPrefs.HasKey ("cabinSelectionCameraPositionY")) {
			PlayerPrefs.SetFloat ("cabinSelectionCameraPositionY", 4.95f);
		} else {
			GameObject mainCamera = GameObject.Find ("Main Camera");
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, PlayerPrefs.GetFloat ("cabinSelectionCameraPositionY"), mainCamera.transform.position.z);
		}
	}
	
	public void SetCameraPositionY (float cameraPositionY) {
		PlayerPrefs.SetFloat ("cabinSelectionCameraPositionY", cameraPositionY);
	}
}
