//#define iOS_BUILD
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class IOSSocialManager : MonoBehaviour
{
	
	ILeaderboard totalScoreLeaderboard;
	float worldOneFinishedAchievement, worldTwoFinishedAchievement, worldThreeFinishedAchievement, worldFourFinishedAchievement;

	// Use this for initialization
	void Start ()
	{
		#if (iOS_BUILD)
		AuthenticateUser ();
		if (!PlayerPrefs.HasKey ("Leaderboard: Total Score")) {
			PlayerPrefs.SetInt ("Leaderboard: Total Score", 0);
		}

		if (!PlayerPrefs.HasKey ("Leaderboard: World 1")) {
			PlayerPrefs.SetInt ("Leaderboard: Total Score", 0);
		}

		if (!PlayerPrefs.HasKey ("Leaderboard: World 2")) {
			PlayerPrefs.SetInt ("Leaderboard: Total Score", 0);
		}

		if (!PlayerPrefs.HasKey ("Leaderboard: World 3")) {
			PlayerPrefs.SetInt ("Leaderboard: Total Score", 0);
		}

		if (!PlayerPrefs.HasKey ("Leaderboard: World 4")) {
			PlayerPrefs.SetInt ("Leaderboard: Total Score", 0);
		}

		if (!PlayerPrefs.HasKey ("worldOneFinished")) {
			PlayerPrefs.SetString ("worldOneFinished", "true");
		} 

		if (!PlayerPrefs.HasKey ("worldTwoFinishedDouble")) {
			PlayerPrefs.SetFloat ("worldTwoFinishedDouble", 0.0f);
			worldTwoFinishedAchievement = 0;
		} else {
			worldTwoFinishedAchievement = PlayerPrefs.GetFloat ("worldTwoFinishedDouble");
		}

		if (!PlayerPrefs.HasKey ("worldThreeFinishedDouble")) {
			PlayerPrefs.SetFloat ("worldThreeFinishedDouble", 0.0f);
			worldThreeFinishedAchievement = 0;
		} else {
			worldThreeFinishedAchievement = PlayerPrefs.GetFloat ("worldThreeFinishedDouble");
		}

		if (!PlayerPrefs.HasKey ("worldFourFinishedDouble")) {
			PlayerPrefs.SetFloat ("worldFourFinishedDouble", 0.0f);
			worldFourFinishedAchievement = 0;
		} else {
			worldFourFinishedAchievement = PlayerPrefs.GetFloat ("worldFourFinishedDouble");
		}

//		gameObject.GetComponent<HighScoreKeeper> ().IncreaseAchievementCountForPreviousPlayers ();
		#endif
	}
	
	void AuthenticateUser ()
	{
		#if (iOS_BUILD)
		Social.localUser.Authenticate (success => {
			if (success) {
				Debug.Log ("Authentication successful");
				string userInfo = "Username: " + Social.localUser.userName + "\nUser ID: " + Social.localUser.id + "\nIsUnderage: " + Social.localUser.underage;
				Debug.Log (userInfo);
			} else
				Debug.Log ("Authentication failded");
		});
		#endif
	}

	void ReportScore (long score, string leaderboardID)
	{
		#if (iOS_BUILD)		
		Debug.Log ("Reportiung score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			Debug.Log (success ? "Reported score successfully" : "Failed to report score");
		});
		#endif
	}

	public void AddToAndUpdateTotalScoreLeaderboard (long scoreToAdd)
	{
		#if (iOS_BUILD)
		long tempScore = PlayerPrefs.GetInt ("Leaderboard: Total Score");
		tempScore += scoreToAdd;
		PlayerPrefs.SetInt ("Leaderboard: Total Score", (int)tempScore);
		ReportScore (tempScore, "grp.TotalScoreLeaderboard");
		#endif
	}

	public void AddToAndUpdateWorldScoreLeaderboard (int scoreToAdd, int world) {
		#if (iOS_BUILD)
		int tempScore = PlayerPrefs.GetInt ("Leaderboard: World " + world);
		tempScore += scoreToAdd;
		PlayerPrefs.SetInt ("Leaderboard: World " + world, tempScore);
		ReportScore (tempScore, "grp.World" + world + "Leaderboard");
		#endif
	}

	public void ThreeStarsForWorldAchievementComplete (int world) {
		#if (iOS_BUILD)
		string tempString = "";
		switch (world) {
		case 1: tempString = "grp.ThreeStarsAllLevelsWorldOne"; break;
		case 2: tempString = "grp.ThreeStarsAllLevelsWorldTwo"; break;
		case 3: tempString = "grp.ThreeStarsAllLevelsWorldThree"; break;
		case 4: tempString = "grp.ThreeStarsAllLevelsWorldFour"; break;
		}

		Social.ReportProgress (tempString, 100.0, success => {
			Debug.Log (success ? "ThreeStarsForWorldAchievementComplete" : "Failed to ThreeSrarsForWorldAchievementComplete");
		});
		#endif		
	}

	public void IncreaseWorldFinishedAchievement (int world) {
		#if (iOS_BUILD)
		string tempString = "";
		switch (world) {
		case 1: tempString = "grp.WorldOneFinished"; break;
		case 2: tempString = "grp.WorldTwoFinished"; break;
		case 3: tempString = "grp.WorldThreeFinished"; break;
		case 4: tempString = "grp.WorldFourFinished"; break;
		}

		Social.ReportProgress (tempString, 100.0, success => {
			Debug.Log (success ? "Increased World Achievement" : "Failed To Increase World Achievement");
		});
		#endif
	}
}
