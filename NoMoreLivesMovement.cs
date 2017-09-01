using UnityEngine;
using System.Collections;

public class NoMoreLivesMovement : MonoBehaviour {

	public GameObject noMoreLives, noLivesButtons, coin, five, zero;
	GameObject instantiatedNoMoreLives, instantiatedNoLivesButtons, instantiatedCoin,  instantiatedFive, instantiatedFirstZero, instantiatedSecondZero, instantiatedThirdZero;
	bool noMoreLivesInstantiated, coinsInstantiated, livesPurchased;

	// Use this for initialization
	void Start () {
		noMoreLivesInstantiated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (instantiatedNoMoreLives != null && instantiatedNoLivesButtons != null) {
			if (noMoreLivesInstantiated && Mathf.Abs (0 - instantiatedNoMoreLives.transform.position.x) > .001f) {
				instantiatedNoMoreLives.transform.Translate (new Vector3 (0 - instantiatedNoMoreLives.transform.position.x, 0, 0) * Time.deltaTime * 10f);
			}
			if (noMoreLivesInstantiated && Mathf.Abs (0 - instantiatedNoLivesButtons.transform.position.x) > .001f) {
				instantiatedNoLivesButtons.transform.Translate (new Vector3 (0 - instantiatedNoLivesButtons.transform.position.x, 0, 0) * Time.deltaTime * 10f);
				if (!coinsInstantiated && Mathf.Abs (0 - instantiatedNoLivesButtons.transform.position.x) < .5f) {
					instantiatedCoin = (GameObject)Instantiate (coin, new Vector3 (.54f, -.85f, -54), Quaternion.identity);
					instantiatedFive = (GameObject)Instantiate (five, new Vector3 (.85f, -0.85f, -54), Quaternion.identity);
					instantiatedFirstZero = (GameObject)Instantiate (zero, new Vector3 (.85f + .22f, -0.85f, -54), Quaternion.identity);
					instantiatedSecondZero = (GameObject)Instantiate (zero, new Vector3 (.85f + (2 * .22f), -0.85f, -54), Quaternion.identity);
					instantiatedThirdZero = (GameObject)Instantiate (zero, new Vector3 (.85f + (3 * .22f), -0.85f, -54), Quaternion.identity);
					coinsInstantiated = true;
				}
			}
			if (coinsInstantiated) {
				if (Mathf.Abs (-.12227f - instantiatedCoin.transform.position.y) > .001f) {
					instantiatedCoin.transform.Translate (new Vector3 (0, (-.12227f - instantiatedCoin.transform.position.y), 0) * Time.deltaTime * 9);
				}
				if (Mathf.Abs (-.12227f - instantiatedFive.transform.position.y) > .001f) {
					instantiatedFive.transform.Translate (new Vector3 (0, (-.12227f - instantiatedFive.transform.position.y), 0) * Time.deltaTime * 8f);
				}
				if (Mathf.Abs (-.12227f - instantiatedFirstZero.transform.position.y) > .001f) {
					instantiatedFirstZero.transform.Translate (new Vector3 (0, (-.12227f - instantiatedFirstZero.transform.position.y), 0) * Time.deltaTime * 7);
				}
				if (Mathf.Abs (-.12227f - instantiatedSecondZero.transform.position.y) > .001f) {
					instantiatedSecondZero.transform.Translate (new Vector3 (0, (-.12227f - instantiatedSecondZero.transform.position.y), 0) * Time.deltaTime * 6f);
				}
				if (Mathf.Abs (-.12227f - instantiatedThirdZero.transform.position.y) > .001f) {
					instantiatedThirdZero.transform.Translate (new Vector3 (0, (-.12227f - instantiatedThirdZero.transform.position.y), 0) * Time.deltaTime * 5);
				}
			}
		}

		if (livesPurchased) {
			MoveCoinsBack ();
		}
	}
	public void SetLivesPurchased (bool livesPurchased) {
		this.livesPurchased = livesPurchased;
	}

	void MoveCoinsBack () {
		if (instantiatedCoin != null) {
			instantiatedCoin.transform.Translate (new Vector3 (0, -.85f - instantiatedCoin.transform.position.y, 0) * Time.deltaTime * 9);
			if (Mathf.Abs (-.85f - instantiatedCoin.transform.position.y) < .01) 
				Destroy (instantiatedCoin);
		}
		if (instantiatedFive != null) {
			instantiatedFive.transform.Translate (new Vector3 (0, -.85f - instantiatedFive.transform.position.y, 0) * Time.deltaTime * 8);
			if (Mathf.Abs (-.85f - instantiatedFive.transform.position.y) < .01)
				Destroy (instantiatedFive);
		}
		if (instantiatedFirstZero != null) {
			instantiatedFirstZero.transform.Translate (new Vector3 (0, -.85f - instantiatedFirstZero.transform.position.y, 0) * Time.deltaTime * 7);
			if (Mathf.Abs (-.85f - instantiatedFirstZero.transform.position.y) < .01)
				Destroy (instantiatedFirstZero);
		}
		if (instantiatedSecondZero != null) {
			instantiatedSecondZero.transform.Translate (new Vector3 (0, -.85f - instantiatedSecondZero.transform.position.y, 0) * Time.deltaTime * 6);
			if (Mathf.Abs (-.85f - instantiatedSecondZero.transform.position.y) < .01)
				Destroy (instantiatedSecondZero);
		}
		if (instantiatedThirdZero != null) {
			instantiatedThirdZero.transform.Translate (new Vector3 (0, -.85f - instantiatedThirdZero.transform.position.y, 0) * Time.deltaTime * 5);
			if (Mathf.Abs (-.85f - instantiatedThirdZero.transform.position.y) < .01)
				Destroy (instantiatedThirdZero);
		}

		if (instantiatedCoin == null && instantiatedFive == null && instantiatedFirstZero == null && instantiatedSecondZero == null && instantiatedThirdZero == null)  {
			livesPurchased = false;
		}
	}

	public void InstantiateNoMoreLivesSlide () {
		instantiatedNoMoreLives = (GameObject)Instantiate (noMoreLives, new Vector3 (-10, 0, -55), Quaternion.identity);
		instantiatedNoLivesButtons = (GameObject)Instantiate (noLivesButtons, new Vector3 (-10, 0, -55), Quaternion.identity);
		noMoreLivesInstantiated = true;
	}
}
