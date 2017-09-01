using UnityEngine;
using System.Collections;

public class LevelMapPositionSaver : MonoBehaviour {

	GameObject levelWorlds, background;
	TransitionShadeController transitionShadeController;
	float worldsStartingPosition, backgroundStartingPosition, worldsTargetX, backgroundTargetX, movementSpeed;
	bool firstTimeSeeingWorld;

	// Use this for initialization
	void Awake () {
		if (!PlayerPrefs.HasKey ("firstTimeSeeingWorld") && PlayerPrefs.GetInt ("totalMountainStars") == 0) {
			firstTimeSeeingWorld = true;
			PlayerPrefs.SetString ("firstTimeSeeingWorld", "false");
			levelWorlds = GameObject.Find ("Level Worlds");
			background = GameObject.Find ("Level Map Background");
			transitionShadeController = GameObject.Find ("Transition Shade").GetComponent<TransitionShadeController> ();
			worldsTargetX = 14.3f;
			backgroundTargetX = .52f;
			movementSpeed = .25f;
		} else {
			firstTimeSeeingWorld = false;
		}

		if (firstTimeSeeingWorld) {
			if (!PlayerPrefs.HasKey ("worldsStartingPosition")) {
				PlayerPrefs.SetFloat ("worldsStartingPosition", -17.88556f);
			}
			if (!PlayerPrefs.HasKey ("backgroundStartingPosition"))
				PlayerPrefs.SetFloat ("backgroundStartingPosition", -15.5f);
		}

		worldsStartingPosition = PlayerPrefs.GetFloat ("worldsStartingPosition");
		backgroundStartingPosition = PlayerPrefs.GetFloat ("backgroundStartingPosition");
	}

	void Update () {
		if (firstTimeSeeingWorld && transitionShadeController.GetAlpha () <= 0f) {
			levelWorlds.transform.Translate (new Vector3 (worldsTargetX - levelWorlds.transform.position.x, 0, 0) * Time.deltaTime * movementSpeed);
			background.transform.Translate (new Vector3 (backgroundTargetX - background.transform.position.x, 0, 0) * Time.deltaTime * movementSpeed);
			movementSpeed += .01f;
		}
	}

	public void TurnOffMovement () {
		if (firstTimeSeeingWorld && Mathf.Abs (worldsTargetX - levelWorlds.transform.position.x) < 1 && Mathf.Abs (backgroundTargetX - background.transform.position.x) < 1)
			firstTimeSeeingWorld = false;
	}

	public bool MovementReadyToTurnOff () {
		if (!firstTimeSeeingWorld)
			return true;
		return (Mathf.Abs (worldsTargetX - levelWorlds.transform.position.x) < .25f && Mathf.Abs (backgroundTargetX - background.transform.position.x) < .25f);
	}

	public float GetWorldsStartingPosition () {
		return worldsStartingPosition;
	}

	public float GetBackgroundStartingPosition () {
		return backgroundStartingPosition;
	}

	public void SetWorldsStartingPosition (float startingPosition) {
		PlayerPrefs.SetFloat ("worldsStartingPosition", startingPosition);
	}

	public void SetBackgroundStartingPosition (float startingPosition) {
		PlayerPrefs.SetFloat ("backgroundStartingPosition", startingPosition);
	}
}
