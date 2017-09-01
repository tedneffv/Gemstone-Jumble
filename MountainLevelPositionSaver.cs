using UnityEngine;
using System.Collections;

public class MountainLevelPositionSaver : MonoBehaviour {

	float cameraPositionY;

	void Awake () {
		if (!PlayerPrefs.HasKey ("mountainSelectionCameraPositionY")) {
			PlayerPrefs.SetFloat ("mountainSelectionCameraPositionY", 4.95f);
		} else {
			GameObject mainCamera = GameObject.Find ("Main Camera");
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, PlayerPrefs.GetFloat ("mountainSelectionCameraPositionY"), mainCamera.transform.position.z);
		}
	}

	public void SetCameraPositionY (float cameraPositionY) {
		PlayerPrefs.SetFloat ("mountainSelectionCameraPositionY", cameraPositionY);
	}
}
