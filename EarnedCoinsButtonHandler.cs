//#define RESET_SHOW_AD_BOX
using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System;

public class EarnedCoinsButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedOkayButton, coinReward, fiveThousandCoinReward, dontShowAdBox, cantPlayAdsDialog;
	GameObject instantiatedPressedButton, bottomBanner, instantiateShowAdBox, instantiatedCantPlayAdsDialog;
	public string zoneId = "rewardedVideo";
	public int rewardQty = 250;
	bool adStarted, giveDownloadReward;
	DateTime focusLostTime;


	// Use this for initialization
	void Start () {
		#if RESET_SHOW_AD_BOX
		PlayerPrefs.DeleteKey ("showAdBox");
		#endif
		if (!PlayerPrefs.HasKey ("showAdBox")) {
			PlayerPrefs.SetString ("showAdBox", "false");
		}
		bottomBanner = GameObject.Find ("Bottom Banner");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}
		else if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}
//		else if (instantiatedPressedButton != null) {
//			Destroy (instantiatedPressedButton);
//		}
	
	}

	void OnApplicationFocus (bool focused) {
		if (adStarted && !focused) {
			focusLostTime = DateTime.Now.ToUniversalTime ();
		}
		if (adStarted && focused) {
			long elapsedTicks = DateTime.Now.ToUniversalTime ().Ticks - focusLostTime.Ticks;
			TimeSpan elapsedTime = new TimeSpan (elapsedTicks);
			int elapsedSeconds = (elapsedTime.Minutes * 60) + elapsedTime.Seconds;
			#if UNITY_ANDROID
			if (elapsedSeconds >= 60) {
				giveDownloadReward = true;
			}
			#endif
			#if UNITY_IPHONE
			if (elapsedSeconds >= 20) {
				giveDownloadReward = true;
			}
			#endif
		}
	}

	private void HandleShowResult (ShowResult result)
	{
		adStarted = false;
		switch (result)
		{
		case ShowResult.Finished:
			if (giveDownloadReward) {
				giveDownloadReward = false;
				Instantiate (fiveThousandCoinReward, new Vector3 (transform.position.x, transform.position.y, transform.position.z + .1f), Quaternion.identity);
				bottomBanner.GetComponent<BottomBannerCoinHandler> ().UpdateCoinNumber ();
				bottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (true, bottomBanner.transform.position.z);
			}
			else {
				Debug.Log ("Video completed. User rewarded " + rewardQty + " credits.");
				Instantiate (coinReward, new Vector3 (transform.position.x, transform.position.y, transform.position.z + .1f), Quaternion.identity);
				bottomBanner.GetComponent<BottomBannerCoinHandler> ().UpdateCoinNumber ();
				bottomBanner.GetComponent<BottomBannerCoinHandler> ().Move (true, bottomBanner.transform.position.z);
			}
			break;
		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");
			break;
		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			if (GameObject.Find ("Earn Coins Button 1") != null) {
				instantiatedCantPlayAdsDialog = Instantiate (cantPlayAdsDialog, new Vector3 (-10, 0.95f, -50), Quaternion.identity) as GameObject;
				instantiatedCantPlayAdsDialog.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.11f);
			}
			break;
		}
	}


	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		hit = Physics2D.OverlapPoint(new Vector2 (wp.x, wp.y));

		if (hit.gameObject != null && hit.gameObject == gameObject) {
			if (instantiatedPressedButton == null) {
				instantiatedPressedButton = Instantiate (pressedOkayButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity) as GameObject;
			}
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		hit2 = Physics2D.OverlapPoint(new Vector2 (wp.x, wp.y));

		if (hit2.gameObject == null || (hit2.gameObject != null && hit2.gameObject != gameObject)) {
			if (instantiatedPressedButton != null) {
				Destroy (instantiatedPressedButton);
			}
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		hit3 = Physics2D.OverlapPoint(new Vector2 (wp.x, wp.y));

		if (hit3.gameObject != null && hit3.gameObject == gameObject) {
			if (PlayerPrefs.HasKey ("showAdBox") && PlayerPrefs.GetString ("showAdBox") == "true") {
				adStarted = true;
				if (string.IsNullOrEmpty (zoneId)) zoneId = null;

	//			Rect buttonRect = new Rect (10, 10, 150, 50);
				string buttonText = Advertisement.IsReady (zoneId) ? "Show Ad" : "Waiting...";
	//
				ShowOptions options = new ShowOptions();
				options.resultCallback = HandleShowResult;

	//			if (GUI.Button (buttonRect, buttonText)) {
				Advertisement.Show (zoneId, options);
			} else if (instantiateShowAdBox == null){
				instantiateShowAdBox = Instantiate (dontShowAdBox, new Vector3 (-10, 0, -50), Quaternion.identity) as GameObject;
				instantiateShowAdBox.GetComponent<LevelFailedScreenSlider> ().SetTargetX (0);
			}
		}
		if (instantiatedPressedButton != null) {
			Destroy (instantiatedPressedButton);
		}
	}

	public void StartAd () {
		if (GameObject.Find ("Earn Coins Explaination Banner(Clone)") != null) {
			Destroy (GameObject.Find ("Earn Coins Explaination Banner(Clone)"));
		}

		adStarted = true;
		if (string.IsNullOrEmpty (zoneId)) zoneId = null;

		//			Rect buttonRect = new Rect (10, 10, 150, 50);
		string buttonText = Advertisement.IsReady (zoneId) ? "Show Ad" : "Waiting...";
		//
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		//			if (GUI.Button (buttonRect, buttonText)) {
		Advertisement.Show (zoneId, options);
	}
}
