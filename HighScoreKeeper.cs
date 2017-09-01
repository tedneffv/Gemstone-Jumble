using UnityEngine;
using System.Collections;

public class HighScoreKeeper : MonoBehaviour {

	int[] mountainLevelHighScores, cityLevelHighScores, cabinLevelHighScores, launchpadLevelHighScores;
	MountainLevelsStatus mountainLevelStatus;
	CityLevelsStatus cityLevelsStatus;
	CabinLevelsStatus cabinLevelsStatus;
	LaunchpadLevelsStatus launchpadLevelsStatus;
	IOSSocialManager socialManager;
	int callNumber = 0;

	// Use this for initialization
	void Start () {
		mountainLevelStatus = GameObject.Find ("Game Manager").GetComponent<MountainLevelsStatus> ();
		cityLevelsStatus = GameObject.Find ("Game Manager").GetComponent<CityLevelsStatus> ();
		cabinLevelsStatus = GameObject.Find ("Game Manager").GetComponent<CabinLevelsStatus> ();
		launchpadLevelsStatus = GameObject.Find ("Game Manager").GetComponent<LaunchpadLevelsStatus> ();
		socialManager = GameObject.Find ("Game Manager").GetComponent<IOSSocialManager> ();
		mountainLevelHighScores = new int[30];
		cityLevelHighScores = new int[30];
		cabinLevelHighScores = new int[30];
		launchpadLevelHighScores = new int[30];

		if (!PlayerPrefs.HasKey ("mountainLevelThreeStarsNumber")) {
			PlayerPrefs.SetInt ("mountainLevelThreeStarsNumber", 0);
		}
		if (!PlayerPrefs.HasKey ("cityLevelThreeStarsNumber")) {
			PlayerPrefs.SetInt ("cityLevelThreeStarsNumber", 0);
		}
		if (!PlayerPrefs.HasKey ("cabinLevelThreeStarsNumber")) {
			PlayerPrefs.SetInt ("cabinLevelThreeStarsNumber", 0);
		}
		if (!PlayerPrefs.HasKey ("launchpadLevelThreeStarsNumber")) {
			PlayerPrefs.SetInt ("launchpadLevelThreeStarsNumber", 0);
		}

		// 0 is False and 1 is True for PlayerPref.GetInt when setting ThreeStarArrays;
		if (!PlayerPrefs.HasKey ("mountainLevelThreeStarArrayIndex 0")) {
			for (int i = 0; i < 30; i++) {
				PlayerPrefs.SetInt ("mountainLevelThreeStarArrayIndex " + i, 0);
				PlayerPrefs.SetInt ("cityLevelThreeStarArrayIndex " + i, 0);
				PlayerPrefs.SetInt ("cabinLevelThreeStarArrayIndex " + i, 0);
				PlayerPrefs.SetInt ("launchpadLevelThreeStarArrayIndex " + i, 0);
			}
		}

		if (!PlayerPrefs.HasKey ("mountainLevelHighScores 0")) {
			int temp = 0;
			for (int i = 0; i < 30; i++) {
				temp = mountainLevelStatus.GetStarNumber(i) * ((GetMountainLevelThreeStarScore (i) / 3) + Random.Range (0, 1000));
				PlayerPrefs.SetInt ("mountainLevelHighScores " + i, temp);
				mountainLevelHighScores[i] = temp;

			}
			for (int i = 0; i < 30; i++) {
				temp = cityLevelsStatus.GetStarNumber(i) * ((GetCityLevelThreeStarScore (i) / 3) + Random.Range (0, 1000));
				PlayerPrefs.SetInt ("cityLevelHighScores " + i, temp);
				cityLevelHighScores[i] = temp;
			}
			for (int i = 0; i < 30; i++) {
				temp = cabinLevelsStatus.GetStarNumber (i) * ((GetCabinLevelThreeStareScore (i) / 3) + Random.Range (0, 1000));
				PlayerPrefs.SetInt ("cabinLevelHighScores " + i, temp);
				cabinLevelHighScores[i] = temp;
			}
			for (int i = 0; i < 30; i++) {
				temp = launchpadLevelsStatus.GetStarNumber (i) * ((GetLaunchpadLevelThreeStarScore (i) / 3) + Random.Range (0, 1000));
				PlayerPrefs.SetInt ("launchpadLevelHighScores " + i, temp);
				launchpadLevelHighScores[i] = temp;
			}
		}
		else {
			for (int i = 0; i < 30; i++) {
				mountainLevelHighScores[i] = PlayerPrefs.GetInt ("mountainLevelHighScores " + i);
//				Debug.Log ("mountainLevelHighScores[" + i + "] = " + mountainLevelHighScores[i]);
			}
			for (int i = 0; i < 30; i++) {
				cityLevelHighScores[i] = PlayerPrefs.GetInt ("cityLevelHighScores " + i);
//				Debug.Log ("cityLevelHighScores[" + i + "] = " + cityLevelHighScores[i]);
			}
			for (int i = 0; i < 30; i++) {
				cabinLevelHighScores[i] = PlayerPrefs.GetInt ("cabinLevelHighScores " + i);
			}
			for (int i = 0; i < 30; i++) {
				launchpadLevelHighScores[i] = PlayerPrefs.GetInt ("launchpadLevelHighScores " + i);
			}
		}
	}
	
	public int GetMountainLevelHighScore (int level) {
		return mountainLevelHighScores[level];
	}

	public int GetCityLevelHighScore (int level) {
		return cityLevelHighScores[level];
	}

	public int GetCabinLevelHighScore (int level) {
		return cabinLevelHighScores[level];
	}

	public int GetLaunchpadLevelHighScore (int level) {
		return launchpadLevelHighScores[level];
	}


	int GetMountainLevelThreeStarScore (int level) {
		level += 1;
		switch (level) {
			case 1: return 550300;
			case 2: return 550300; 
			case 3: return 19100; 
			case 4: return 19100; 
			case 5: return 55100; 
			case 6: return 26100; 
			case 7: return 177800; 
			case 8: return 550300; 
			case 9: return 177800; 
			case 10: return 198375; 
			case 11: return 368100; 
			case 12: return 55100; 
			case 13: return 1675000; 
			case 14: return 77800; 
			case 15: return 198375; 
			case 16: return 19100; 
			case 17: return 177800; 
			case 18: return 177800; 
			case 19: return 19100; 
			case 20: return 713625; 
			case 21: return 368100; 
			case 22: return 368100; 
			case 23: return 108000; 
			case 24: return 951500; 
			case 25: return 103875; 
			case 26: return 264500; 
			case 27: return 2762800; 
			case 28: return 108000; 
			case 29: return 368100; 
			case 30: return 4835700; 
		}
		return -1;
	}
	
	int GetCityLevelThreeStarScore (int level) {
		level += 1;
		switch (level) {
			case 1: return 19100; 
			case 2: return 45600; 
			case 3: return 264500; 
			case 4: return 310600; 
			case 5: return 1545750; 
			case 6: return 177800; 
			case 7: return 264500; 
			case 8: return 66000; 
			case 9: return 264500; 
			case 10: return 5192250; 
			case 11: return 108000; 
			case 12: return 368100; 
			case 13: return 626000; 
			case 14: return 368100; 
			case 15: return 1008450; 
			case 16: return 55100; 
			case 17: return 368100; 
			case 18: return 780300; 
			case 19: return 34000; 
			case 20: return 19575; 
			case 21: return 422000; 
			case 22: return 368100; 
			case 23: return 55100; 
			case 24: return 236600; 
			case 25: return 1545750; 
			case 26: return 780300; 
			case 27: return 177800; 
			case 28: return 264500; 
			case 29: return 780300; 
			case 30: return 2197125; 
		}
		return -1;
	}

	int GetCabinLevelThreeStareScore (int level) {
		level += 1;
		switch (level) {
		case 1: return 24100; 
		case 2: return 14500; 
		case 3: return 106600; 
		case 4: return 127000; 
		case 5: return 153500; 
		case 6: return 1607100; 
		case 7: return 509500; 
		case 8: return 1867800; 
		case 9: return 11067000; 
		case 10: return 509500; 
		case 11: return 708600; 
		case 12: return 153500; 
		case 13: return 1500300; 
		case 14: return 708600; 
		case 15: return 6070300; 
		case 16: return 873100; 
		case 17: return 208500; 
		case 18: return 6567600; 
		case 19: return 3764100; 
		case 20: return 27567100; 
		case 21: return 1500300; 
		case 22: return 395600; 
		case 23: return 12374100; 
		case 24: return 1158600; 
		case 25: return 8300600; 
		case 26: return 19877000; 
		case 27: return 7819500; 
		case 28: return 24466600; 
		case 29: return 1500300; 
		case 30: return 4481600; 
		}
		return -1;
	}

	int GetLaunchpadLevelThreeStarScore (int level) {
		level += 1;
		switch (level) {
		case 1: return 1162000; 
		case 2: return 723600; 
		case 3: return 55150; 
		case 4: return 352800; 
		case 5: return 65087; 
		case 6: return 33600; 
		case 7: return 723600; 
		case 8: return 33600; 
		case 9: return 75600; 
		case 10: return 1352800; 
		case 11: return 2478000; 
		case 12: return 61392; 
		case 13: return 2613600; 
		case 14: return 52298; 
		case 15: return 985600; 
		case 16: return 16696000; 
		case 17: return 42651; 
		case 18: return 2433600; 
		case 19: return 70379; 
		case 20: return 13356000; 
		case 21: return 3502800; 
		case 22: return 66184; 
		case 23: return 424000; 
		case 24: return 43887; 
		case 25: return 424000; 
		case 26: return 154000; 
		case 27: return 60647; 
		case 28: return 142800; 
		case 29: return 54460; 
		case 30: return 522000; 
		}
		return -1;
	}

	public bool SetMountainLevelHighScore (int level, int score) {
		callNumber++;
		Debug.Log ("callNumber = " + callNumber);
		if (mountainLevelHighScores[level] < score) {
			socialManager.AddToAndUpdateTotalScoreLeaderboard (score - mountainLevelHighScores[level]);
			socialManager.AddToAndUpdateWorldScoreLeaderboard (score - mountainLevelHighScores[level], 1);
			if (level == 29 && mountainLevelHighScores[level] == 0) {
				socialManager.IncreaseWorldFinishedAchievement (1);
			}
			mountainLevelHighScores[level] = score;
			PlayerPrefs.SetInt ("mountainLevelHighScores " + level, score);
			Debug.Log ("Returning true");
			return true;
		} 
		return false;
	}

	public bool SetCityLevelHighScore (int level, int score) {
		if (cityLevelHighScores[level] < score) {
			socialManager.AddToAndUpdateTotalScoreLeaderboard (score - cityLevelHighScores[level]);
			socialManager.AddToAndUpdateWorldScoreLeaderboard (score - cityLevelHighScores[level], 2);
			if (level == 29 && cityLevelHighScores[level] == 0) 
				socialManager.IncreaseWorldFinishedAchievement (2);
			cityLevelHighScores[level] = score;
			PlayerPrefs.SetInt ("cityLevelHighScores " + level, score);
			Debug.Log ("Returning true");
			return true;
		} else {
			return false;
		}
	}

	public bool SetCabinLevelHighScore (int level, int score) {
		if (cabinLevelHighScores[level] < score) {
			socialManager.AddToAndUpdateTotalScoreLeaderboard (score - cabinLevelHighScores[level]);
			socialManager.AddToAndUpdateWorldScoreLeaderboard (score - cabinLevelHighScores[level], 3);
			if (level == 29 && cabinLevelHighScores[level] == 0)
				socialManager.IncreaseWorldFinishedAchievement (3);
			cabinLevelHighScores[level] = score;
			PlayerPrefs.SetInt ("cabinLevelHighScores " + level, score);
			Debug.Log ("Returning true");
			return true;
		} else {
			return false;
		}
	}

	public bool SetLaunchpadLevelHighScore (int level, int score) {
		if (launchpadLevelHighScores[level] < score) {
			socialManager.AddToAndUpdateTotalScoreLeaderboard (score - launchpadLevelHighScores[level]);
			socialManager.AddToAndUpdateWorldScoreLeaderboard (score - launchpadLevelHighScores[level], 4);
			if (level == 29 && launchpadLevelHighScores[level] == 0) 
				socialManager.IncreaseWorldFinishedAchievement (4);
			launchpadLevelHighScores[level] = score;
			PlayerPrefs.SetInt ("launchpadLevelHighScores " + level, score);
			return true;
		} 
		return false;
	}

	public void UpdateThreeStarScore (int worldNumber, int levelNumber) {
		switch (worldNumber) {
		case 1: if (PlayerPrefs.GetInt ("mountainLevelThreeStarArrayIndex " + levelNumber) == 0) {
				Debug.Log ("Setting mountainLevelThreeStarArrayIndex " + levelNumber + " to 1");
				PlayerPrefs.SetInt ("mountainLevelThreeStarArrayIndex " + levelNumber, 1);
				PlayerPrefs.SetInt ("mountainLevelThreeStarsNumber", (PlayerPrefs.GetInt ("mountainLevelThreeStarsNumber") + 1));
				Debug.Log ("mountainLevelThreeStarsNumber = " + PlayerPrefs.GetInt ("mountainLevelThreeStarsNumber"));
				if (PlayerPrefs.GetInt ("mountainLevelThreeStarsNumber") == 30) 
					socialManager.ThreeStarsForWorldAchievementComplete (1);
			} break;
		case 2: if (PlayerPrefs.GetInt ("cityLevelThreeStarArrayIndex " + levelNumber) == 0) {
				Debug.Log ("Setting cityLevelThreeStarArrayIndex " + levelNumber + " to 1");
				PlayerPrefs.SetInt ("cityLevelThreeStarArrayIndex " + levelNumber, 1);
				PlayerPrefs.SetInt ("cityLevelThreeStarsNumber", (PlayerPrefs.GetInt ("cityLevelThreeStarsNumber") + 1));
				Debug.Log ("cityLevelThreeStarsNumber = " + PlayerPrefs.GetInt ("cityLevelThreeStarsNumber"));
				if (PlayerPrefs.GetInt ("cityLevelThreeStarsNumber") == 30) 
					socialManager.ThreeStarsForWorldAchievementComplete (2);
			} break;
		case 3: if (PlayerPrefs.GetInt ("cabinLevelThreeStarArrayIndex " + levelNumber) == 0) {
				Debug.Log ("Setting cabinLevelThreeStarArrayIndex " + levelNumber + " to 1");
				PlayerPrefs.SetInt ("cabinLevelThreeStarArrayIndex " + levelNumber, 1);
				PlayerPrefs.SetInt ("cabinLevelThreeStarsNumber", (PlayerPrefs.GetInt ("cabinLevelThreeStarsNumber") + 1));
				Debug.Log ("cabinLevelThreeStarsNumber = " + PlayerPrefs.GetInt ("cabinLevelThreeStarsNumber"));
				if (PlayerPrefs.GetInt ("cabinLevelThreeStarsNumber") == 30) 
					socialManager.ThreeStarsForWorldAchievementComplete (3);
			} break;
		case 4: if (PlayerPrefs.GetInt ("launchpadLevelThreeStarArrayIndex " + levelNumber) == 0) {
				Debug.Log ("Setting launchpadLevelThreeStarArrayIndex " + levelNumber + " to 1");
				PlayerPrefs.SetInt ("launchpadLevelThreeStarArrayIndex " + levelNumber, 1);
				PlayerPrefs.SetInt ("launchpadLevelThreeStarsNumber", (PlayerPrefs.GetInt ("launchpadLevelThreeStarsNumber") + 1));
				Debug.Log ("launchpadLevelThreeStarsNumber = " + PlayerPrefs.GetInt ("launchpadLevelThreeStarsNumber"));
				if (PlayerPrefs.GetInt ("launchpadLevelThreeStarsNumber") == 30) 
					socialManager.ThreeStarsForWorldAchievementComplete (4);
			} break;
		}
	}
}
