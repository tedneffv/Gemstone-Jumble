using UnityEngine;
using System.Collections;

public class LaunchpadLevelPositionSaver : MonoBehaviour {

	float cameraPositionY;
	
	void Start () {
		if (!PlayerPrefs.HasKey ("launchpadSelectionCameraPositionY")) {
			PlayerPrefs.SetFloat ("launchpadSelectionCameraPositionY", 4.95f);
		} else {
			GameObject mainCamera = GameObject.Find ("Main Camera");
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, PlayerPrefs.GetFloat ("launchpadSelectionCameraPositionY"), mainCamera.transform.position.z);
		}
	}
	
	public void SetCameraPositionY (float cameraPositionY) {
		PlayerPrefs.SetFloat ("launchpadSelectionCameraPositionY", cameraPositionY);
	}
}
