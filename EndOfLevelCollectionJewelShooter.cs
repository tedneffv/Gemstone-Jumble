using UnityEngine;
using System.Collections;

public class EndOfLevelCollectionJewelShooter : MonoBehaviour {

	public GameObject blueHomingCollectorJewel, greenHomingCollectorJewel, orangeHomingCollectorJewel, purpleHomingCollectorJewel, redHomingCollectorJewel, whiteHomingCollectorJewel;
	Vector3 staticJewelCollectorOnePosition, staticJewelCollectorTwoPosition, staticJewelCollectorThreePosition, staticJewelCollectorFourPosition, staticJewelCollectorFivePosition, staticjewelCollectorSixPosition;
	Vector3 jewelCollectorOnePosition, jewelCollectorTwoPosition, jewelCollectorThreePosition, jewelCollectorFourPosition, jewelCollectorFivePosition, jewelCollectorSixPosition;
	float cooldown, timeStamp;
	bool startSpray, sprayingInProgress, collectorSprayed;
	JewelCollectorController jewelCollectorController;
	int randomCollector, randomJewelSprayNumber, cooldownCounter, originalCollector;

	// Use this for initialization
	void Start () {
		timeStamp = Time.time;
		cooldown = .01f;
		jewelCollectorController = GameObject.Find ("Jewel Collector").GetComponent<JewelCollectorController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (startSpray && cooldownCounter == 0) {
			if (!collectorSprayed) {
				randomCollector = Random.Range (1, 7);
				originalCollector = randomCollector;
				collectorSprayed = true;
			} else {
				randomCollector++;
				if (randomCollector == 7)
					randomCollector = 1;
				if (randomCollector == originalCollector) {
					startSpray = false;
					GameObject.Find ("Jewel Collector").GetComponent<MoveNumberHandler> ().StartLaunchingRowDestructionStars ();
				}
			}

			randomJewelSprayNumber = Random.Range (5, 11);
			if (jewelCollectorController.GetCollectionJewelNumerator (randomCollector) > randomJewelSprayNumber) {
				timeStamp = Time.time;
				collectorSprayed = false;
				cooldown = Random.Range (1f, 2f);
				jewelCollectorController.SubtractFromJewelCollectorNumerator (randomCollector, randomJewelSprayNumber, GetCorrectCollectorPosition (randomCollector));
				GameObject tempJewel;
				Vector3 tempVector = GetCorrectStaticJewelPosition (randomCollector);
				jewelCollectorController.SubtractFromTotalJewelGoal (randomJewelSprayNumber);
				for (int i = 0; i < randomJewelSprayNumber; i++) {
					tempJewel = (GameObject)Instantiate (GetCorrectHomingJewel (randomCollector), new Vector3 (tempVector.x, tempVector.y, -30), Quaternion.identity);
					tempJewel.GetComponent<CollectionJewelHomingMovement> ().SetTarget (Random.Range (0, 9), Random.Range (0, 9));
				}
			} else if (jewelCollectorController.GetCollectionJewelNumerator (randomCollector) != 0) {
				timeStamp = Time.time;
				collectorSprayed = false;
				cooldown = Random.Range (1f, 2f);
				int jewelCollectorNumerator = jewelCollectorController.GetCollectionJewelNumerator (randomCollector);
				jewelCollectorController.SubtractFromJewelCollectorNumerator (randomCollector, jewelCollectorNumerator, GetCorrectCollectorPosition (randomCollector));
				GameObject tempJewel;
				jewelCollectorController.SubtractFromTotalJewelGoal (randomJewelSprayNumber);
				Vector3 tempVector = GetCorrectStaticJewelPosition (randomCollector);
				for (int i = 0; i < jewelCollectorNumerator; i++) {
					tempJewel = (GameObject)Instantiate (GetCorrectHomingJewel (randomCollector), new Vector3 (tempVector.x, tempVector.y, -30), Quaternion.identity);
					tempJewel.GetComponent<CollectionJewelHomingMovement> ().SetTarget (Random.Range (0, 9), Random.Range (0, 9));
				}
			}
		}

		if (startSpray && !collectorSprayed) {
			cooldownCounter = ((cooldownCounter + 1) % 35);
		}
	}

	Vector3 GetCorrectStaticJewelPosition (int collectorNumber) {
		switch (collectorNumber) {
		case 1: return staticJewelCollectorOnePosition;
		case 2: return staticJewelCollectorTwoPosition;
		case 3: return staticJewelCollectorThreePosition;
		case 4: return staticJewelCollectorFourPosition;
		case 5: return staticJewelCollectorFivePosition;
		case 6: return staticjewelCollectorSixPosition;
		}
		return Vector3.zero;
	}

	GameObject GetCorrectHomingJewel (int collectorNumber) {
		switch (collectorNumber) {
		case 1: return whiteHomingCollectorJewel;
		case 2: return redHomingCollectorJewel;
		case 3: return greenHomingCollectorJewel;
		case 4: return blueHomingCollectorJewel;
		case 5: return purpleHomingCollectorJewel;
		case 6: return orangeHomingCollectorJewel;
		}
		return null;
	}

	Vector3 GetCorrectCollectorPosition (int randomCollector) {
		switch (randomCollector) {
		case 1: return jewelCollectorOnePosition;
		case 2: return jewelCollectorTwoPosition;
		case 3: return jewelCollectorThreePosition;
		case 4: return jewelCollectorFourPosition;
		case 5: return jewelCollectorFivePosition;
		case 6: return jewelCollectorSixPosition;
		}
		return Vector3.zero;
	}

	public void StartJewelSpray () {
		sprayingInProgress = true;
		GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ().SetCollectionJewelsLaunching (true);
		GameObject.Find ("Level Controller").GetComponent<RockLevelMatchAssistant> ().SetGameStarted (false);

		jewelCollectorOnePosition = GameObject.Find ("Jewel Collector One").transform.position;
		jewelCollectorTwoPosition = GameObject.Find ("Jewel Collector Two").transform.position;
		jewelCollectorThreePosition = GameObject.Find ("Jewel Collector Three").transform.position;
		jewelCollectorFourPosition = GameObject.Find ("Jewel Collector Four").transform.position;
		jewelCollectorFivePosition = GameObject.Find ("Jewel Collector Five").transform.position;
		jewelCollectorSixPosition = GameObject.Find ("Jewel Collector Six").transform.position;

		staticJewelCollectorOnePosition = GameObject.Find ("Static Jewel One").transform.position;
		staticJewelCollectorTwoPosition = GameObject.Find ("Static Jewel Two").transform.position;
		staticJewelCollectorThreePosition = GameObject.Find ("Static Jewel Three").transform.position;
		staticJewelCollectorFourPosition = GameObject.Find ("Static Jewel Four").transform.position;
		staticJewelCollectorFivePosition = GameObject.Find ("Static Jewel Five").transform.position;
		staticjewelCollectorSixPosition = GameObject.Find ("Static Jewel Six").transform.position;

		startSpray = true;
	}

	public bool GetSprayingInProgress () {
		return sprayingInProgress;
	}

	public bool GetStartSpray () {
		return startSpray;
	}
}
