using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using Facebook.Unity;
using System.Globalization;

public class FacebookShareButtonHandler : MonoBehaviour {

//	Collider2D hit, hit2, hit3;
//	GameObject instantiatedPressedShareButton;
//	public GameObject pressedShareButton, coinExplosion;
//	bool buttonPressed, shared;
//	float cooldown, timestamp;
//
//	// Use this for initialization
//	void Start () {
//		cooldown = 1f;
//		if (PlayerPrefs.HasKey ("Facebook Shared")) {
//			if (PlayerPrefs.GetString ("Facebook Shared") == "true") {
//				shared = true;
//			} else {
//				shared = false;
//			}
//		} else {
//			PlayerPrefs.SetString ("Facebook Shared", "false");
//			shared = false;
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (Input.GetMouseButtonDown (0)) 
//			CheckTouch (Input.mousePosition);
//		else if (Input.GetMouseButton (0))
//			CheckTouch2 (Input.mousePosition);
//		else if (Input.GetMouseButtonUp (0))
//			CheckTouch3 (Input.mousePosition);
//
//		if (buttonPressed && Time.time > timestamp + cooldown) {
//			buttonPressed = false;
//			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
//			StoreInventory.GiveItem ("coin_currency_ID", 1000);
//			GameObject likeCoins = GameObject.Find ("Share Coins");
//			Instantiate (coinExplosion, new Vector3 (1.41f, -4.08f, -208), Quaternion.Euler (-90, 0, 0));
//			Destroy (GameObject.Find ("Share Coins"));
//		}
//
//	}
//
//	void CheckTouch (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit != null && hit.gameObject == gameObject) {
//			instantiatedPressedShareButton = (GameObject)Instantiate (pressedShareButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .01f), Quaternion.identity);
//			instantiatedPressedShareButton.transform.parent = transform;
//		}
//	}
//
//	void CheckTouch2 (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit2 == null || (hit2 != null && hit2.gameObject != gameObject)) {
//			if (instantiatedPressedShareButton != null)
//				Destroy (instantiatedPressedShareButton);
//		}
//	}
//
//	void CheckTouch3 (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit3 != null && hit3.gameObject == gameObject) {
//			if (instantiatedPressedShareButton != null) {
//				if (!SoomlaProfile.IsLoggedIn (Provider.FACEBOOK)) {
//					SoomlaProfile.Login (Provider.FACEBOOK);
//				}
//				else {
//					VirtualCurrency coin = StoreAssets.COIN_CURRENCY;
//					Reward thousandCoinReward = new VirtualItemReward ("thousandCoinReward_ID", "1000 Coins", coin.ID, 1000);
//					RockLevelController levelController = GameObject.Find ("Level Controller").GetComponent<RockLevelController> ();
//					SoomlaProfile.UpdateStory(
//						Provider.FACEBOOK,                          // Provider
//						"I just scored " + GameObject.Find ("Level Controller").GetComponent<RockLevelScoreKeeper> ().GetScore ().ToString ("0,0", CultureInfo.InvariantCulture) + " points on level " + levelController.GetLevelNumber () +
//						" of World " + GetWorldString () + " in Gemstone Jumble. See if you can beat my high score!",                       // Text of the story to post
//						"Gemstone Jumble!",  // Name
//						"Try it today!",                            // Caption
//						"",                     // Description
//						"https://www.facebook.com/GemstoneJumble/?ref=aymt_homepage_panel",            // Link to post
//						"https://scontent-dfw1-1.xx.fbcdn.net/hphotos-xtp1/t31.0-8/12357203_991210584250693_7144578280525688761_o.png",    // Image URL
//						"",                                         // Payload
//						null                               // Reward for posting a story
//						);
//					Debug.Log (GameObject.Find ("Level Controller").GetComponent<RockLevelScoreKeeper> ().GetScore ().ToString ("0,0", CultureInfo.InvariantCulture));
//					if (!shared) {
//						shared = true;
//						timestamp = Time.time;
//						buttonPressed = true;
//						PlayerPrefs.SetString ("Facebook Shared", "true");
//					}
//				}
//			}
//		}
//
//		if (instantiatedPressedShareButton != null)
//			Destroy (instantiatedPressedShareButton);
//	}
//
//	string GetWorldString () {
//		if (GameObject.Find ("Level Controller").GetComponent<RockLevelController> ().GetLevelNumber () < 31) {
//			return "1";
//		} else if (GameObject.Find ("Level Controller").GetComponent<RockLevelController> ().GetLevelNumber () < 61) 
//			return "2";
//		else if (GameObject.Find ("Level Controller").GetComponent<RockLevelController> ().GetLevelNumber () < 91)
//			return "3";
//		return "3";
//	}
//
//	public bool GetShared () {
//		return (PlayerPrefs.GetString ("Facebook Shared") == "true");
//	}
}
