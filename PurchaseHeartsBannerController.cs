using UnityEngine;
using System.Collections;

public class PurchaseHeartsBannerController : MonoBehaviour {

	GameObject heart1, heart2, heart3, heart4, heart5, heartPurchaseButton, centButton, orButton, coinsButton;
	float heart1Y, heart2Y, heart3Y, heart4Y, heart5Y, heartPurchaseButtonY, centButtonY, orButtonY, coinsButtonY;
	float heart1Target, heart2Target, heart3Target, heart4Target, heart5Target, heartPurchaseButtonTarget, targetY, centButtonTarget, orButtonTarget, coinsButtonTarget;
	bool getRidOfScreen, stage1, stage2, instantiatedNoLivesScreen, heartsPurchased, instantiateCoinsScreen;
	public GameObject bottomBanner;
	// Use this for initialization
	void Start () {
		heart1 = GameObject.Find ("Heart 1");
		heart2 = GameObject.Find ("Heart 2");
		heart3 = GameObject.Find ("Heart 3");
		heart4 = GameObject.Find ("Heart 4");
		heart5 = GameObject.Find ("Heart 5");
//		heartPurchaseButton = GameObject.Find ("Heart Purchase Button");
		centButton = GameObject.Find ("Purchase Lives $0.99 Button");
		orButton = GameObject.Find ("Purchase lives Or Button");
		coinsButton = GameObject.Find ("Purchase Lives Coin Button");

		heart1Y = heart1.transform.position.y;
		heart2Y = heart2.transform.position.y;
		heart3Y = heart3.transform.position.y;
		heart4Y = heart4.transform.position.y;
		heart5Y = heart5.transform.position.y;
//		heartPurchaseButtonY = heartPurchaseButton.transform.position.y;
		centButtonY = centButton.transform.position.y;
		orButtonY = orButton.transform.position.y;
		coinsButtonY = coinsButton.transform.position.y;

		heart1Target = transform.position.y + .75f;
		heart2Target = transform.position.y + .75f;
		heart3Target = transform.position.y + .75f;
		heart4Target = transform.position.y + .75f;
		heart5Target = transform.position.y + .75f;
//		heartPurchaseButtonTarget = heartPurchaseButtonY - .625f;
		centButtonTarget = centButtonY - 1f;
		orButtonTarget = orButtonY - 2f;
		coinsButtonTarget = coinsButtonY - 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!getRidOfScreen) {
			transform.Translate (new Vector3 (0 - transform.position.x, 0, 0) * Time.deltaTime * 7f);
			targetY = transform.position.y + .75f;
			if (Mathf.Abs (transform.position.x - 0) < 1.25f) {
				heart1.transform.Translate (new Vector3 (0, targetY - heart1.transform.position.y, 0) * Time.deltaTime * 12);
			}
			if (Mathf.Abs (transform.position.x - 0) < 1f){
				heart2.transform.Translate (new Vector3 (0, (targetY + .1f) - heart2.transform.position.y, 0) * Time.deltaTime * 12);
			}
			if (Mathf.Abs (transform.position.x - 0) < .75f){
				heart3.transform.Translate (new Vector3 (0, (targetY + .13f) - heart3.transform.position.y, 0) * Time.deltaTime * 12);
//				heartPurchaseButton.transform.Translate (new Vector3 (0, (targetY - .75f - .525f) - heartPurchaseButton.transform.position.y, 0) * Time.deltaTime * 12);
				centButton.transform.Translate (new Vector3 (0, (targetY - .75f - 1) - centButton.transform.position.y, 0) * Time.deltaTime * 12);
				orButton.transform.Translate (new Vector3 (0, (targetY - .75f - 2) - orButton.transform.position.y, 0) * Time.deltaTime * 12);
				coinsButton.transform.Translate (new Vector3 (0, (targetY - .75f - 3) - coinsButton.transform.position.y, 0) * Time.deltaTime * 12);
			}
			if (Mathf.Abs (transform.position.x - 0) < .5f){
				heart4.transform.Translate (new Vector3 (0, (targetY + .1f) - heart4.transform.position.y, 0) * Time.deltaTime * 12);
			}
			if (Mathf.Abs (transform.position.x - 0) < .25f){
				heart5.transform.Translate (new Vector3 (0, targetY - heart5.transform.position.y, 0) * Time.deltaTime * 12);
			}
		}
		else if (getRidOfScreen && stage1){
			transform.Translate (new Vector3 (-1 - transform.position.x, 0, 0) * Time.deltaTime * 7f);
			if (Mathf.Abs (-1 - transform.position.x) < .05f) {
				stage1 = false;
				stage2 = true;
			}
		}
		else if (getRidOfScreen && stage2){
			if (!instantiatedNoLivesScreen && heartsPurchased) {
				GameObject.Find ("Screen Handlers").GetComponent<NoMoreLivesScreenHandler> ().InstantiateNoLivesScreen ();
				instantiatedNoLivesScreen = true;
			}
			else if (instantiateCoinsScreen) {
				instantiateCoinsScreen = false;
				GameObject.Find ("Screen Handlers").GetComponent<PurchaseCoinsScreenHandler> ().InstantiateCameraOffsetScreen ();
			}
			transform.Translate (new Vector3 (10 - transform.position.x, 0, 0) * Time.deltaTime * 7f);
			if (transform.position.x > 9) {
				Destroy (gameObject);
			}
		}

		if (getRidOfScreen) {
			targetY = transform.position.y;
//			if (stage1 || stage2) 
			heart1.transform.Translate (new Vector3 (0, targetY - .015f - heart1.transform.position.y, 0) * Time.deltaTime * 30);
//			if ((stage1 && transform.position.x < -.5f) || stage2)
			heart2.transform.Translate (new Vector3 (0, targetY + .06f- heart2.transform.position.y, 0) * Time.deltaTime * 25);
//			if ((stage1 && transform.position.x < -.65f) || stage2) {
			heart3.transform.Translate (new Vector3 (0, targetY + .09f - heart3.transform.position.y, 0) * Time.deltaTime * 20);
			centButton.transform.Translate (new Vector3 (0, (targetY + .1f) - centButton.transform.position.y, 0) * Time.deltaTime * 20);
			orButton.transform.Translate (new Vector3 (0, (targetY + .1f) - orButton.transform.position.y, 0) * Time.deltaTime * 20);
			coinsButton.transform.Translate (new Vector3 (0, (targetY + .1f) - coinsButton.transform.position.y, 0) * Time.deltaTime * 20);
//				heartPurchaseButton.transform.Translate (new Vector3 (0, (targetY + .17f) - heartPurchaseButton.transform.position.y, 0) * Time.deltaTime * 20);

//			}
//			if ((stage1 && transform.position.x < -.7f) || stage2)
			heart4.transform.Translate (new Vector3 (0, targetY + .06f - heart4.transform.position.y, 0) * Time.deltaTime * 15);
//			if ((stage1 && transform.position.x < -.725f) || stage2)
			heart5.transform.Translate (new Vector3 (0, targetY - .015f - heart5.transform.position.y, 0) * Time.deltaTime * 10);
		}
	}
	public void GetRidOfScreen (bool getRidOfScreen) {
		this.getRidOfScreen = getRidOfScreen;
		stage1 = true;
	}
	public void SetHeartsPurchased () {
		heartsPurchased = true;
	}
	public void SetInstantiateCoinsScreen (bool instantiateCoinsScreen) {
		this.instantiateCoinsScreen = instantiateCoinsScreen;
	}
}
