using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdController : MonoBehaviour {

	void Awake () {
		Object.DontDestroyOnLoad (gameObject);
	}

	IEnumerator StartAdCoroutine () {
		yield return new WaitForSeconds(10);
		ShowAd ();
	}

	public void ShowAd () {
		if (!PlayerPrefs.HasKey ("removeAds") && Advertisement.IsReady ()) {
			Advertisement.Show ("video");
		}
	}

	public void ShowRewardedVideo () {
		if (!PlayerPrefs.HasKey ("removeAds") && Advertisement.IsReady("rewardedVideo"))
		{
			Advertisement.Show("rewardedVideo");
		}
	}

}
