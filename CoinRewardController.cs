using UnityEngine;
using System.Collections;
using Soomla.Store;

public class CoinRewardController : MonoBehaviour {

	GameObject bottomBanner;
	public GameObject coinExplosion;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 250));
		bottomBanner = GameObject.Find ("Bottom Banner");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 ((bottomBanner.transform.position.x - transform.position.x) * Time.deltaTime * 5, 0, 0));
		if (transform.position.y < bottomBanner.transform.position.y) {
			GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().SetPurchaseMade ();
			if (name == "Coin Reward(Clone)")
				StoreInventory.GiveItem ("coin_currency_ID", 250);
			else 
				StoreInventory.GiveItem ("coin_currency_ID", 5000);
			Instantiate (coinExplosion, transform.position, Quaternion.Euler (-90, 0, 0));
			bottomBanner.GetComponent<BottomBannerCoinHandler> ().MoveBannerDownCoroutine ();
			Destroy (gameObject);
		}
	}
}
