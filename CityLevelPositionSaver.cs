using UnityEngine;
using System.Collections;

public class CityLevelPositionSaver : MonoBehaviour {

	float cameraPositionY;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("citySelectionCameraPositionY")) {
			PlayerPrefs.SetFloat ("citySelectionCameraPositionY", 4.95f);
		} else {
			GameObject mainCamera = GameObject.Find ("Main Camera");
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, PlayerPrefs.GetFloat ("citySelectionCameraPositionY"), mainCamera.transform.position.z);
		}
	}
	
	public void SetCameraPositionY (float cameraPositionY) {
		PlayerPrefs.SetFloat ("citySelectionCameraPositionY", cameraPositionY);
	}
}
