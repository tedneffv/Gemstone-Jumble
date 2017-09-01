using UnityEngine;
using System.Collections;

public class EndOfLevelStarHandler : MonoBehaviour {

	bool starFalling, moveOneHundredCoins, instantiateCoins;
	public GameObject starExplosion, oneHundredCoins, coinNumberExplosion;
	GameObject instantiatedOneHundredCoins;
	float startingY;
	CoinTotalManager coinTotalManager;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		starFalling = true;
		GetComponent<Rigidbody2D>().AddTorque(-1000);
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (starFalling && transform.position.y < 1.27f) {
			if (name == "Blue Progress Star(Clone)") {
				soundHandler.PlayStarHit ();
				Instantiate (starExplosion, new Vector3 (transform.position.x, 1.217f, -202), Quaternion.identity);
			}
			else if (name == "Purple Progress Star(Clone)") {
				soundHandler.PlayStarHit2 ();
				Instantiate (starExplosion, new Vector3 (transform.position.x, 1.187f, -202), Quaternion.identity);
			}
			else if (name == "Green Progress Star(Clone)") {
				soundHandler.PlayStarHit3 ();
				Instantiate (starExplosion, new Vector3 (transform.position.x, 1.195f, -202), Quaternion.identity);
			}
			GetComponent<Rigidbody2D>().isKinematic = true;
			transform.position = (new Vector3 (transform.position.x, 1.27f, transform.position.z));
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			starFalling = false;
			if (instantiateCoins) {
				instantiatedOneHundredCoins = (GameObject)Instantiate (oneHundredCoins, new Vector3 (transform.position.x, transform.position.y, transform.position.z + .1f), Quaternion.identity);
				startingY = transform.position.y;
				instantiatedOneHundredCoins.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 600));
				moveOneHundredCoins = true;
			}
		}
		else if (moveOneHundredCoins) {
			if (instantiatedOneHundredCoins.transform.position.y < startingY) {
				soundHandler.PlayCoinDrop ();
				Instantiate (coinNumberExplosion, new Vector3 (instantiatedOneHundredCoins.transform.position.x, instantiatedOneHundredCoins.transform.position.y, instantiatedOneHundredCoins.transform.position.z - 2), Quaternion.Euler (-90, 0 ,0));
				Destroy (instantiatedOneHundredCoins);
				moveOneHundredCoins = false;
			}
		}
	}

	public void SetInstantiateCoins (bool instantiateCoins) {
		this.instantiateCoins = instantiateCoins;
	}
}
