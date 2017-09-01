using UnityEngine;
using System.Collections;

public class PowerButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public GameObject pressedPowerButton, plusFiveBombButton, jewelSwapButton, rowDestructionButton, bombPowerButton, multiStarButton;
	GameObject instantiatedPressedPowerButton, instantiatedPlusFivePowerButton, instantiatedJewelSwapButton, instantiatedRowDestructionButton, instantiatedBombPowerButton, instantiatedMultiStarButton, instantiatedSingleStarButton, bottomBanner;
	bool leaveHome, bottomBannerUp, goHome, touchOn, moveBanner;
	float totalCircleAngle, incrementalCircleAngle, circleRadius, targetX, targetY;
	static int coinNumberCount;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		leaveHome = true;
		coinNumberCount = 0;
		touchOn = true;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (touchOn) {
			if (Input.GetMouseButtonDown (0)) {
				CheckTouch (Input.mousePosition);
			} 
			else if (Input.GetMouseButton (0)) {
				CheckTouch2 (Input.mousePosition);
			}
			else if (Input.GetMouseButtonUp (0) && instantiatedPressedPowerButton != null) {
				CheckTouch3 (Input.mousePosition);
				Destroy (instantiatedPressedPowerButton);
			}
		}
		if (bottomBanner != null && moveBanner) {
			if (bottomBannerUp && Mathf.Abs (-5 - bottomBanner.transform.position.y) > .01f) {
				bottomBanner.transform.Translate (new Vector3 (0, (-5 - bottomBanner.transform.position.y), 0) * Time.deltaTime * 9);
			} else if (!bottomBannerUp && Mathf.Abs (-6 - bottomBanner.transform.position.y) > .01f) {
				bottomBanner.transform.Translate (new Vector3 (0, (-6 - bottomBanner.transform.position.y), 0) * Time.deltaTime * 5);
			} else if (!bottomBannerUp && Mathf.Abs (-6 - bottomBanner.transform.position.y) <= .01f) {
				moveBanner = false;
			}
		}
	}

	public void MoveBottomBannerDown () {
		if (bottomBanner !=  null) {
			moveBanner = true;
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit = Physics2D.OverlapPoint(touchPos);	

		if (hit != null && hit.gameObject == gameObject) {
			if (instantiatedPressedPowerButton == null) {
				soundHandler.PlayButtonClickDown ();
				instantiatedPressedPowerButton = (GameObject)Instantiate (pressedPowerButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1), Quaternion.identity);
			}
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit2 = Physics2D.OverlapPoint(touchPos);	

		if (hit != null && hit.gameObject == gameObject && ((hit2 != null && hit2.gameObject != gameObject) || hit2 == null)) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPowerButton);
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		hit3 = Physics2D.OverlapPoint(touchPos);
		if (instantiatedPressedPowerButton != null) {
			soundHandler.PlayButtonClickUp ();
			Destroy (instantiatedPressedPowerButton);
			if (GameObject.Find ("Mountain Level One ID") != null || GameObject.Find ("Mountain Level Two ID") != null || GameObject.Find ("Mountain Level Three ID") != null || GameObject.Find ("Mountain Level Four ID") != null)
				return;
		}

		if (hit != null && hit.gameObject == gameObject && hit3 != null && hit3.gameObject == gameObject) {
			if (leaveHome) {
				soundHandler.PlayWoosh ();
				bottomBanner = GameObject.Find ("Bottom Banner");
//				moveBanner = true;
				GameObject.Find ("Bottom Banner").GetComponent<BottomBannerCoinHandler> ().Move (true, GameObject.Find ("Bottom Banner").transform.position.z);
				GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ().UpdateCoinNumber ();
				bottomBannerUp = true;
				totalCircleAngle = Mathf.Rad2Deg * (2 * (Mathf.Atan (((transform.position.x)/(transform.position.x * .8930232558f)))));
				incrementalCircleAngle = totalCircleAngle / 6;
				circleRadius = (-transform.position.x / (Mathf.Sin ((totalCircleAngle / 2) * Mathf.Deg2Rad)));
				if (instantiatedPlusFivePowerButton == null) {
					GameObject instantitedPlusFiveCircle = new GameObject ();
					instantitedPlusFiveCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
					instantitedPlusFiveCircle.name = "Plus Five Circle";
					instantiatedPlusFivePowerButton = (GameObject)Instantiate (plusFiveBombButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5), Quaternion.identity);
					instantiatedPlusFivePowerButton.transform.parent = instantitedPlusFiveCircle.transform;
					instantiatedPlusFivePowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle));
				}
				if (instantiatedJewelSwapButton == null) {
					GameObject instantiatedJewelSwapCircle = new GameObject ();
					instantiatedJewelSwapCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
					instantiatedJewelSwapCircle.name = "Jewel Swap Circle";
					instantiatedJewelSwapButton = (GameObject)Instantiate (jewelSwapButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10), Quaternion.identity);
					instantiatedJewelSwapButton.transform.parent = instantiatedJewelSwapCircle.transform;
					instantiatedJewelSwapButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 2));
					instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (-targetX, targetY);
				}
				if (instantiatedRowDestructionButton == null) {
					GameObject instantiatedRowDestructionCircle = new GameObject ();
					instantiatedRowDestructionCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
					instantiatedRowDestructionCircle.name = "Row Destruction Circle";
					instantiatedRowDestructionButton = (GameObject)Instantiate (rowDestructionButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 15), Quaternion.identity);
					instantiatedRowDestructionButton.transform.parent = instantiatedRowDestructionCircle.transform;
					instantiatedRowDestructionButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 3));
				}
				if (instantiatedBombPowerButton == null) {
					GameObject instantiatedBombPowerCircle = new GameObject ();
					instantiatedBombPowerCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
					instantiatedBombPowerCircle.name = "Bomb Power Circle";
					instantiatedBombPowerButton = (GameObject)Instantiate (bombPowerButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 20), Quaternion.identity);
					instantiatedBombPowerButton.transform.parent = instantiatedBombPowerCircle.transform;
					instantiatedBombPowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 4));
				}
				if (instantiatedMultiStarButton == null) {
					GameObject instantiatedMultiStarCircle = new GameObject ();
					instantiatedMultiStarCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
					instantiatedMultiStarCircle.name = "Multi Star Circle";
					instantiatedMultiStarButton = (GameObject)Instantiate (multiStarButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 25), Quaternion.identity);
					instantiatedMultiStarButton.transform.parent = instantiatedMultiStarCircle.transform;
					instantiatedMultiStarButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 5));
				}
//				if (instantiatedSingleStarButton == null) {
//					GameObject instantiatedSingleStarCircle = new GameObject ();
//					instantiatedSingleStarCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
//					instantiatedSingleStarCircle.name = "Single Star Circle";
//					instantiatedSingleStarButton = (GameObject)Instantiate (singleStarButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 25), Quaternion.identity);
//					instantiatedSingleStarButton.transform.parent = instantiatedSingleStarCircle.transform;
//					instantiatedSingleStarButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 5));
//				}

				targetX = (circleRadius * Mathf.Sin (((totalCircleAngle / 2) - incrementalCircleAngle) * Mathf.Deg2Rad));
				targetY = (transform.position.y + (transform.position.x * .8930232558f)) + (circleRadius * Mathf.Cos (((totalCircleAngle / 2) - incrementalCircleAngle) * Mathf.Deg2Rad));
				instantiatedPlusFivePowerButton.GetComponent<PlusFiveButtonHandler> ().SetTarget (-targetX, targetY);
				instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().SetTarget (targetX, targetY);
//				instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (-targetX, targetY);
//				instantiatedSingleStarButton.GetComponent<SingleStarPowerButtonHandler> ().SetTarget (targetX, targetY);

				targetX = (circleRadius * Mathf.Sin (((totalCircleAngle / 2) - incrementalCircleAngle * 2) * Mathf.Deg2Rad));
				targetY = (transform.position.y + (transform.position.x * .8930232558f)) + (circleRadius * Mathf.Cos (((totalCircleAngle / 2) - incrementalCircleAngle * 2) * Mathf.Deg2Rad));
				instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (-targetX, targetY);
				instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().SetTarget (0, (transform.position.y + (transform.position.x * .8930232558f)) + circleRadius);
				instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().SetTarget (targetX, targetY);
//				instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().SetTarget (-targetX, targetY);
//				instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().SetTarget (targetX, targetY);
//				instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().SetTarget (0, (transform.position.y + (transform.position.x * .8930232558f)) + circleRadius);

				instantiatedPlusFivePowerButton.GetComponent<PlusFiveButtonHandler> ().LeaveHome ();
				instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().LeaveHome ();
				instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().LeaveHome ();
				instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().LeaveHome ();
				instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().LeaveHome ();
//				instantiatedSingleStarButton.GetComponent<SingleStarPowerButtonHandler> ().LeaveHome ();

				leaveHome = false;
				goHome = true;
			}

			else if (goHome) {
				soundHandler.PlayWoosh ();
//				bottomBannerUp = false;
				GameObject.Find ("Bottom Banner").GetComponent<BottomBannerCoinHandler> ().Move (false, GameObject.Find ("Bottom Banner").transform.position.z);
				instantiatedPlusFivePowerButton.GetComponent<PlusFiveButtonHandler> ().SetTarget (transform.position.x, transform.position.y);
				instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (transform.position.x, transform.position.y);
				instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().SetTarget(transform.position.x, transform.position.y);
				instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().SetTarget (transform.position.x, transform.position.y);
				instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().SetTarget (transform.position.x, transform.position.y);
//				instantiatedSingleStarButton.GetComponent<SingleStarPowerButtonHandler> ().SetTarget (transform.position.x, transform.position.y);

				instantiatedPlusFivePowerButton.GetComponent<PlusFiveButtonHandler> ().GoHome ();
				instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().GoHome ();
				instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().GoHome ();
				instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().GoHome ();
				instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().GoHome ();
//				instantiatedSingleStarButton.GetComponent<SingleStarPowerButtonHandler> ().GoHome ();
				goHome = false;
				leaveHome = true;
			}
		}
	}

	public void LeaveHome () {
//		if (leaveHome) {
	if (soundHandler == null) {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
		soundHandler.PlayWoosh ();
		bottomBanner = GameObject.Find ("Bottom Banner");
//		moveBanner = true;
		GameObject.Find ("Coin Handler").GetComponent<CoinTotalManager> ().UpdateCoinNumber ();
		GameObject.Find ("Bottom Banner").GetComponent<BottomBannerCoinHandler> ().Move (true, GameObject.Find ("Bottom Banner").transform.position.z);
//		bottomBannerUp = true; 
		totalCircleAngle = Mathf.Rad2Deg * (2 * (Mathf.Atan (((transform.position.x)/(transform.position.x * .8930232558f)))));
		incrementalCircleAngle = totalCircleAngle / 6;
		circleRadius = (-transform.position.x / (Mathf.Sin ((totalCircleAngle / 2) * Mathf.Deg2Rad)));
		if (instantiatedPlusFivePowerButton == null) {
			GameObject instantitedPlusFiveCircle = new GameObject ();
			instantitedPlusFiveCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
			instantitedPlusFiveCircle.name = "Plus Five Circle";
			instantiatedPlusFivePowerButton = (GameObject)Instantiate (plusFiveBombButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 5), Quaternion.identity);
			instantiatedPlusFivePowerButton.transform.parent = instantitedPlusFiveCircle.transform;
			instantiatedPlusFivePowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle));
		}
		if (instantiatedJewelSwapButton == null) {
			GameObject instantiatedJewelSwapCircle = new GameObject ();
			instantiatedJewelSwapCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
			instantiatedJewelSwapCircle.name = "Jewel Swap Circle";
			instantiatedJewelSwapButton = (GameObject)Instantiate (jewelSwapButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10), Quaternion.identity);
			instantiatedJewelSwapButton.transform.parent = instantiatedJewelSwapCircle.transform;
			instantiatedJewelSwapButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 2));
			instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (-targetX, targetY);
		}
		if (instantiatedRowDestructionButton == null) {
			GameObject instantiatedRowDestructionCircle = new GameObject ();
			instantiatedRowDestructionCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
			instantiatedRowDestructionCircle.name = "Row Destruction Circle";
			instantiatedRowDestructionButton = (GameObject)Instantiate (rowDestructionButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 15), Quaternion.identity);
			instantiatedRowDestructionButton.transform.parent = instantiatedRowDestructionCircle.transform;
			instantiatedRowDestructionButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 3));
		}
		if (instantiatedBombPowerButton == null) {
			GameObject instantiatedBombPowerCircle = new GameObject ();
			instantiatedBombPowerCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
			instantiatedBombPowerCircle.name = "Bomb Power Circle";
			instantiatedBombPowerButton = (GameObject)Instantiate (bombPowerButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 20), Quaternion.identity);
			instantiatedBombPowerButton.transform.parent = instantiatedBombPowerCircle.transform;
			instantiatedBombPowerButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 4));
		}
		if (instantiatedMultiStarButton == null) {
			GameObject instantiatedMultiStarCircle = new GameObject ();
			instantiatedMultiStarCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
			instantiatedMultiStarCircle.name = "Multi Star Circle";
			instantiatedMultiStarButton = (GameObject)Instantiate (multiStarButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 25), Quaternion.identity);
			instantiatedMultiStarButton.transform.parent = instantiatedMultiStarCircle.transform;
			instantiatedMultiStarButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 5));
		}
		//				if (instantiatedSingleStarButton == null) {
		//					GameObject instantiatedSingleStarCircle = new GameObject ();
		//					instantiatedSingleStarCircle.transform.position = new Vector3 (0, (transform.position.y + (transform.position.x * .8930232558f)), 0);
		//					instantiatedSingleStarCircle.name = "Single Star Circle";
		//					instantiatedSingleStarButton = (GameObject)Instantiate (singleStarButton, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 25), Quaternion.identity);
		//					instantiatedSingleStarButton.transform.parent = instantiatedSingleStarCircle.transform;
		//					instantiatedSingleStarButton.transform.Rotate (new Vector3 (0, 0, incrementalCircleAngle * 5));
		//				}
		
		targetX = (circleRadius * Mathf.Sin (((totalCircleAngle / 2) - incrementalCircleAngle) * Mathf.Deg2Rad));
		targetY = (transform.position.y + (transform.position.x * .8930232558f)) + (circleRadius * Mathf.Cos (((totalCircleAngle / 2) - incrementalCircleAngle) * Mathf.Deg2Rad));
		instantiatedPlusFivePowerButton.GetComponent<PlusFiveButtonHandler> ().SetTarget (-targetX, targetY);
		instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().SetTarget (targetX, targetY);
		//				instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (-targetX, targetY);
		//				instantiatedSingleStarButton.GetComponent<SingleStarPowerButtonHandler> ().SetTarget (targetX, targetY);
		
		targetX = (circleRadius * Mathf.Sin (((totalCircleAngle / 2) - incrementalCircleAngle * 2) * Mathf.Deg2Rad));
		targetY = (transform.position.y + (transform.position.x * .8930232558f)) + (circleRadius * Mathf.Cos (((totalCircleAngle / 2) - incrementalCircleAngle * 2) * Mathf.Deg2Rad));
		instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().SetTarget (-targetX, targetY);
		instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().SetTarget (0, (transform.position.y + (transform.position.x * .8930232558f)) + circleRadius);
		instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().SetTarget (targetX, targetY);
		//				instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().SetTarget (-targetX, targetY);
		//				instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().SetTarget (targetX, targetY);
		//				instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().SetTarget (0, (transform.position.y + (transform.position.x * .8930232558f)) + circleRadius);
		
		instantiatedPlusFivePowerButton.GetComponent<PlusFiveButtonHandler> ().LeaveHome ();
		instantiatedJewelSwapButton.GetComponent<JewelSwapHandler> ().LeaveHome ();
		instantiatedRowDestructionButton.GetComponent<RowDestructionButtonHandler> ().LeaveHome ();
		instantiatedBombPowerButton.GetComponent<PowerBombButtonHandler> ().LeaveHome ();
		instantiatedMultiStarButton.GetComponent<MultiStarPowerHandler> ().LeaveHome ();
		//				instantiatedSingleStarButton.GetComponent<SingleStarPowerButtonHandler> ().LeaveHome ();
		
		leaveHome = false;
		goHome = true;
//		}
	}

	public float GetIncrementalCircleAngle () {
		return incrementalCircleAngle;
	}

	public static void increaseCoinNumber () {
		coinNumberCount++;
	}

	public static void decreaseCoinNumber () {
		coinNumberCount--;
	}

	public static int GetCoinNumberCount () {
		return coinNumberCount;
	}

	public void SetTouchOn (bool touchOn) {
		this.touchOn = touchOn;
	}

	public void SetLeaveHome (bool leaveHome) {
		this.leaveHome = leaveHome;
	}
}
