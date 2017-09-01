using UnityEngine;
using System.Collections;

public class NoLivesScreenMovement : MonoBehaviour {

	float targetX, movementSpeed;
	bool coinsInstantiated;
	public GameObject coin, five, zero;
	GameObject[] instantiatedCoins;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (targetX - transform.position.x) > .001f) {
			transform.Translate (new Vector3 (targetX - transform.position.x, 0, 0) * Time.deltaTime * movementSpeed);
			if (!coinsInstantiated && gameObject.name == "Purchase Lives Button(Clone)") {
				if (Mathf.Abs (targetX - transform.position.x) < .25f) {
					InstantiateCoins ();
					coinsInstantiated = true;
				}
			}
		}
		if (coinsInstantiated) {
			for (int i = 0; i < 5; i++) {
				instantiatedCoins[i].transform.Translate (new Vector3 (0, -.07f - instantiatedCoins[i].transform.position.y, 0) * Time.deltaTime * (9 - i));
			}
		}
	}

	void InstantiateCoins () {
		instantiatedCoins = new GameObject[5];
		instantiatedCoins[0] = (GameObject)Instantiate (coin, new Vector3 (1.11f, .5f, -55f), Quaternion.identity);
		instantiatedCoins[1] = (GameObject)Instantiate (five, new Vector3 (1.45f, .5f, -55f), Quaternion.identity);
		instantiatedCoins[2] = (GameObject)Instantiate (zero, new Vector3 (1.45f + (.22f * 1), .5f, -55f), Quaternion.identity);
		instantiatedCoins[3] = (GameObject)Instantiate (zero, new Vector3 (1.45f + (.22f * 2), .5f, -55f), Quaternion.identity);
		instantiatedCoins[4] = (GameObject)Instantiate (zero, new Vector3 (1.45f + (.22f * 3), .5f, -55f), Quaternion.identity);
	}

	public void SetTargetX (float targetX) {
		this.targetX = targetX;
	}

	public void SetMovementSpeed (float movementSpeed) {
		this.movementSpeed = movementSpeed;
	}
}
