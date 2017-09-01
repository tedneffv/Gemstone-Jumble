using UnityEngine;
using System.Collections;
using Soomla.Store;

public class FallingObjectPositionController : MonoBehaviour {

	float targetX, targetY;
	Rigidbody2D rigidBody;
	public GameObject coinExplosion;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (new Vector3 (targetX - transform.position.x, 0, 0) * Time.deltaTime * 3);
		if (rigidBody.velocity.y < 0 && transform.position.y <= targetY) {
			Debug.Log ("Coin final y position = " + transform.position.y);
			Instantiate (coinExplosion, transform.position, Quaternion.Euler (-90, 0, 0));
			StoreInventory.GiveItem ("coin_currency_ID", 10000);
			Destroy (gameObject);
		}
	
	}

	public void SetTarget (float targetX, float targetY) {
		this.targetX = targetX;
		this.targetY = targetY;
	}
}
