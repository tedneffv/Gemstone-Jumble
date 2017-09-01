using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla;

public class FacebookLikeButtonHandler : MonoBehaviour {

//	Collider2D hit, hit2, hit3;
//	public GameObject pressedLikeButton, coinExplosion;
//	GameObject instantiatedPressedLikeButton;
//	bool liked, getRidOfCoins;
//	float timeStamp, cooldown;
//
//	// Use this for initialization
//	void Start () {
//		cooldown = 1f;
//		if (PlayerPrefs.HasKey ("Facebook Liked")) {
//			if (PlayerPrefs.GetString ("Facebook Liked") == "true") {
//				liked = true;
//			} else {
//				liked = false;
//			}
//		} else {
//			PlayerPrefs.SetString ("Facebook Liked", "false");
//			liked = false;
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//		if (Input.GetMouseButtonDown (0)) 
//			CheckTouch (Input.mousePosition);
//		else if (Input.GetMouseButton (0))
//			CheckTouch2 (Input.mousePosition);
//		else if (Input.GetMouseButtonUp (0)) 
//			CheckTouch3 (Input.mousePosition);
//
//		if (getRidOfCoins && Time.time > timeStamp + cooldown) {
//			getRidOfCoins = false;
//			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade (true);
//			StoreInventory.GiveItem ("coin_currency_ID", 1000);
//			GameObject likeCoins = GameObject.Find ("Like Coins");
//			Instantiate (coinExplosion, new Vector3 (-1.41f, -4.08f, -208), Quaternion.Euler (-90, 0, 0));
//			Destroy (GameObject.Find ("Like Coins"));
//		}
//	}
//
//	void CheckTouch (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//		if (hit != null && hit.gameObject == gameObject) {
//			instantiatedPressedLikeButton = (GameObject)Instantiate (pressedLikeButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .01f), Quaternion.identity);
//			instantiatedPressedLikeButton.transform.parent = transform;
//		}
//	}
//
//	void CheckTouch2 (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit2 == null || (hit2 != null && hit2.gameObject != gameObject)) {
//			if (instantiatedPressedLikeButton != null) 
//				Destroy (instantiatedPressedLikeButton);
//		}
//	}
//
//	void CheckTouch3 (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit3 != null && hit3.gameObject == gameObject) {
//			if (instantiatedPressedLikeButton != null) {
//				VirtualCurrency coin = StoreAssets.COIN_CURRENCY;
//				Reward thousandCoinReward = new VirtualItemReward("thousandCoinReward_ID", "1000 Coin Reward", coin.ID, 1000);
//				SoomlaProfile.Like (
//					Provider.FACEBOOK,
//					"967017816669970",
//					null
////					thousandCoinReward
//					);
//				if (!liked) {
//					liked = true;
//					timeStamp = Time.time;
//					getRidOfCoins = true;
//					PlayerPrefs.SetString ("Facebook Liked", "true");
//				}
//			}
//		}
//
//		if (instantiatedPressedLikeButton != null)
//			Destroy (instantiatedPressedLikeButton);
//	}
//
//	public bool GetLiked () {
//		return (PlayerPrefs.GetString ("Facebook Liked") == "true");
//	}
}
