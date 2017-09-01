using UnityEngine;
using System.Collections;

public class OwliePowerUpTutorialController : MonoBehaviour {

	public GameObject tutorialShade;
	int lossesInARow;
	bool playedTutorial;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("lossesInARow")) {
			lossesInARow = PlayerPrefs.GetInt ("lossesInARow");
		}
		else {
			lossesInARow = 0;
			PlayerPrefs.SetInt ("lossesInARow", lossesInARow);
		}

		if (PlayerPrefs.HasKey ("playedTutorial")) {
			if (PlayerPrefs.GetString ("playedTutorial") == "true")
				playedTutorial = true;
			else
				playedTutorial = false;
		}
		else {
			playedTutorial = false;
			PlayerPrefs.SetString ("playedTutorial", "false");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseLossesInARowByOne () {
		lossesInARow++;
		PlayerPrefs.SetInt ("lossesInARow", lossesInARow);
	}

	public int GetLossesInARow () {
		return lossesInARow;
	}

	public void ResetLossesInARow () {
		lossesInARow = 0;
		PlayerPrefs.SetInt ("lossesInARow", lossesInARow);
	}

	public void SetPlayedTutorialTrue () {
		playedTutorial = true;
		PlayerPrefs.SetString ("playedTutorial", "true");
	}

	public bool GetPlayedTutorial () {
		return playedTutorial;
	}
}
