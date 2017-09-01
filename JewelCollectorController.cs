using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JewelCollectorController : MonoBehaviour {

	GameObject[] jewelCollectorOneNumbers, jewelCollectorTwoNumbers, jewelCollectorThreeNumbers, jewelCollectorFourNumbers, jewelCollectorFiveNumbers, jewelCollectorSixNumbers;
	GameObject firstSlash, secondSlash, thirdSlash, fourthSlash, fifthSlash, sixthSlash;
	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, slash, finishedJewelCollector;
	Vector3 firstNumberOffset, secondNumberOffset, thirdNumberOffset, fourthNumberOffset, slashOffset;
	HashSet<GameObject> collectionJewelsSet;
	int jewelCollectorOneNumerator, jewelCollectorTwoNumerator, jewelCollectorThreeNumerator, jewelCollectorFourNumerator, jewelCollectorFiveNumerator, jewelCollectorSixNumerator;
	int jewelCollectorOneDenominator, jewelCollectorTwoDenominator, jewelCollectorThreeDenominator, jewelCollectorFourDenominator, jewelCollectorFiveDenominator, jewelCollectorSixDenominator;
	int jewelCollectorsFinishedNumber, totalJewelCollectionGoal;
	int jewelCollectorOneCap, jewelCollectorTwoCap, jewelCollectorThreeCap, jewelCollectorFourCap, jewelCollectorFiveCap, jewelCollectorSixCap;
	bool jewelCollectorOneFinished, jewelCollectorTwoFinished, jewelCollectorThreeFinished, jewelCollectorFourFinished, jewelCollectorFiveFinished, jewelCollectorSixFinished;
	RockLevelTouchHandler touchHandler;
	RockLevelMovementChecker movementChecker;

	void Awake () {
		jewelCollectorsFinishedNumber = 0;
		jewelCollectorOneNumbers = new GameObject[4];
		jewelCollectorTwoNumbers = new GameObject[4];
		jewelCollectorThreeNumbers = new GameObject[4];
		jewelCollectorFourNumbers = new GameObject[4];
		jewelCollectorFiveNumbers = new GameObject[4];
		jewelCollectorSixNumbers = new GameObject[4];
		firstNumberOffset = new Vector3 (-.195f, .13f, -.01f);
		secondNumberOffset = new Vector3 (-.055f, .13f, -.01f);
		thirdNumberOffset = new Vector3 (.045f, -.15f, -.01f);
		fourthNumberOffset = new Vector3 (.185f, -.15f, -.01f);
		slashOffset = new Vector3 (-.005f, -.02f, -.01f);
		collectionJewelsSet = new HashSet<GameObject> ();
		touchHandler = GameObject.Find ("Level Controller").GetComponent<RockLevelTouchHandler> ();
		movementChecker = GameObject.Find ("Level Controller").GetComponent<RockLevelMovementChecker> ();
		totalJewelCollectionGoal = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetJewelCollectorOneNumbers (GameObject button, Vector3 buttonPosition, int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int denominator) {
		jewelCollectorOneNumerator = 0;
		jewelCollectorOneDenominator = denominator;
		if (firstNumber != 0) 
			jewelCollectorOneNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (firstNumber), buttonPosition + firstNumberOffset, Quaternion.identity);
		jewelCollectorOneNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (secondNumber), buttonPosition + secondNumberOffset, Quaternion.identity);
		firstSlash = (GameObject)Instantiate (slash, buttonPosition + slashOffset, Quaternion.identity);
		firstSlash.transform.parent = button.transform;
		if (thirdNumber != 0) 
			jewelCollectorOneNumbers[2] = (GameObject)Instantiate (GetGameObjectFromNumber (thirdNumber), buttonPosition + thirdNumberOffset, Quaternion.identity);
		jewelCollectorOneNumbers[3] = (GameObject)Instantiate (GetGameObjectFromNumber (fourthNumber), buttonPosition + fourthNumberOffset, Quaternion.identity);
		AttachNumbersToParents (button, jewelCollectorOneNumbers);
	}

	public void SetJewelCollectorTwoNumbers (GameObject button, Vector3 buttonPosition, int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int denominator) {
		jewelCollectorTwoNumerator = 0;
		jewelCollectorTwoDenominator = denominator;
		if (firstNumber != 0) 
			jewelCollectorTwoNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (firstNumber), buttonPosition + firstNumberOffset, Quaternion.identity);
		jewelCollectorTwoNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (secondNumber), buttonPosition + secondNumberOffset, Quaternion.identity);
		secondSlash = (GameObject)Instantiate (slash, buttonPosition + slashOffset, Quaternion.identity);
		secondSlash.transform.parent = button.transform;
		if (thirdNumber != 0)
			jewelCollectorTwoNumbers[2] = (GameObject)Instantiate (GetGameObjectFromNumber (thirdNumber), buttonPosition + thirdNumberOffset, Quaternion.identity);
		jewelCollectorTwoNumbers[3] = (GameObject)Instantiate (GetGameObjectFromNumber (fourthNumber), buttonPosition + fourthNumberOffset, Quaternion.identity);
		AttachNumbersToParents (button, jewelCollectorTwoNumbers);
	}

	public void SetJewelCollectorThreeNumbers (GameObject button, Vector3 buttonPosition, int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int denominator) {
		jewelCollectorThreeNumerator = 0;
		jewelCollectorThreeDenominator = denominator;
		if (firstNumber != 0) 
			jewelCollectorThreeNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (firstNumber), buttonPosition + firstNumberOffset, Quaternion.identity);
		jewelCollectorThreeNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (secondNumber), buttonPosition + secondNumberOffset, Quaternion.identity);
		thirdSlash = (GameObject)Instantiate (slash, buttonPosition + slashOffset, Quaternion.identity);
		thirdSlash.transform.parent = button.transform;
		if (thirdNumber != 0) 
			jewelCollectorThreeNumbers[2] = (GameObject)Instantiate (GetGameObjectFromNumber (thirdNumber), buttonPosition + thirdNumberOffset, Quaternion.identity);
		jewelCollectorThreeNumbers[3] = (GameObject)Instantiate (GetGameObjectFromNumber (fourthNumber), buttonPosition + fourthNumberOffset, Quaternion.identity);
		AttachNumbersToParents (button, jewelCollectorThreeNumbers);
	}

	public void SetJewelCollectorFourNumbers (GameObject button, Vector3 buttonPosition, int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int denominator) {
		jewelCollectorFourNumerator = 0;
		jewelCollectorFourDenominator = denominator;
		if (firstNumber != 0) 
			jewelCollectorFourNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (firstNumber), buttonPosition + firstNumberOffset, Quaternion.identity);
		jewelCollectorFourNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (secondNumber), buttonPosition + secondNumberOffset, Quaternion.identity);
		fourthSlash = (GameObject)Instantiate (slash, buttonPosition + slashOffset, Quaternion.identity);
		fourthSlash.transform.parent = button.transform;
		if (thirdNumber != 0) 
			jewelCollectorFourNumbers[2] = (GameObject)Instantiate (GetGameObjectFromNumber (thirdNumber), buttonPosition + thirdNumberOffset, Quaternion.identity);
		jewelCollectorFourNumbers[3] = (GameObject)Instantiate (GetGameObjectFromNumber (fourthNumber), buttonPosition + fourthNumberOffset, Quaternion.identity);
		AttachNumbersToParents (button, jewelCollectorFourNumbers);
	}

	public void SetJewelCollectorFiveNumbers (GameObject button, Vector3 buttonPosition, int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int denominator) {
		jewelCollectorFiveNumerator = 0;
		jewelCollectorFiveDenominator = denominator;
		if (firstNumber != 0) 
			jewelCollectorFiveNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (firstNumber), buttonPosition + firstNumberOffset, Quaternion.identity);
		jewelCollectorFiveNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (secondNumber), buttonPosition + secondNumberOffset, Quaternion.identity);
		fifthSlash = (GameObject)Instantiate (slash, buttonPosition + slashOffset, Quaternion.identity);
		fifthSlash.transform.parent = button.transform;
		if (thirdNumber != 0) 
			jewelCollectorFiveNumbers[2] = (GameObject)Instantiate (GetGameObjectFromNumber (thirdNumber), buttonPosition + thirdNumberOffset, Quaternion.identity);
		jewelCollectorFiveNumbers[3] = (GameObject)Instantiate (GetGameObjectFromNumber (fourthNumber), buttonPosition + fourthNumberOffset, Quaternion.identity);
		AttachNumbersToParents (button, jewelCollectorFiveNumbers);
	}

	public void SetJewelCollectorSixNumbers (GameObject button, Vector3 buttonPosition, int firstNumber, int secondNumber, int thirdNumber, int fourthNumber, int denominator) {
		jewelCollectorSixNumerator = 0;
		jewelCollectorSixDenominator = denominator;
		if (firstNumber != 0) 
			jewelCollectorSixNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (firstNumber), buttonPosition + firstNumberOffset, Quaternion.identity);
		jewelCollectorSixNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (secondNumber), buttonPosition + secondNumberOffset, Quaternion.identity);
		sixthSlash = (GameObject)Instantiate (slash, buttonPosition + slashOffset, Quaternion.identity);
		sixthSlash.transform.parent = button.transform;
		if (thirdNumber != 0) 
			jewelCollectorSixNumbers[2] = (GameObject)Instantiate (GetGameObjectFromNumber (thirdNumber), buttonPosition + thirdNumberOffset, Quaternion.identity);
		jewelCollectorSixNumbers[3] = (GameObject)Instantiate (GetGameObjectFromNumber (fourthNumber), buttonPosition + fourthNumberOffset, Quaternion.identity);
		AttachNumbersToParents (button, jewelCollectorSixNumbers);
	}

	void SetNewJewelCollectorNumerator (Vector3 collectorPosition, int newNumerator, GameObject[] collectorArray) {
		Destroy (collectorArray[0]);
		Destroy (collectorArray[1]);
		if (newNumerator / 10 > 0) {
			collectorArray[0] = (GameObject)Instantiate (GetGameObjectFromNumber (newNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
		}
		collectorArray[1] = (GameObject)Instantiate (GetGameObjectFromNumber (newNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);
	}

	public void AddOneToJewelCollectorOne (Vector3 collectorPosition) {
		if (jewelCollectorOneNumerator == jewelCollectorOneCap) {
			jewelCollectorOneFinished = true;
			return;
		}
		jewelCollectorOneNumerator++; 
		string jewelCollectionNumeratorString = jewelCollectorOneNumerator.ToString ();
		if (jewelCollectorOneNumerator <= jewelCollectorOneDenominator) {
			if (jewelCollectionNumeratorString.Length > 1) {
				Destroy (jewelCollectorOneNumbers[0]);
				jewelCollectorOneNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorOneNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
			}	
			Destroy (jewelCollectorOneNumbers[1]);
			jewelCollectorOneNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorOneNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);
			if (!jewelCollectorOneFinished && jewelCollectorOneNumerator == jewelCollectorOneDenominator) {
				jewelCollectorOneFinished = true;
				jewelCollectorsFinishedNumber++;
				if (jewelCollectorsFinishedNumber == 6) {
					touchHandler.PauseTouch ();
					movementChecker.SetCollectingDone ();
				}
				GameObject temp = GameObject.Find ("Jewel Collector One");
				temp.transform.DetachChildren ();
				Destroy (temp);
				temp = (GameObject)Instantiate (finishedJewelCollector, collectorPosition, Quaternion.identity);
				temp.name = "Jewel Collector One";
			}
		}
	}

	public void AddOneToJewelCollectorTwo (Vector3 collectorPosition) {
		if (jewelCollectorTwoNumerator == jewelCollectorTwoCap) {
			jewelCollectorTwoFinished = true;
			return;
		}
		jewelCollectorTwoNumerator++;
		string jewelCollectionNumeratorString = jewelCollectorTwoNumerator.ToString ();
		if (jewelCollectorTwoNumerator <= jewelCollectorTwoDenominator) {
			if (jewelCollectionNumeratorString.Length > 1) {
				Destroy (jewelCollectorTwoNumbers[0]);
				jewelCollectorTwoNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorTwoNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
			}
			Destroy (jewelCollectorTwoNumbers[1]);
			jewelCollectorTwoNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorTwoNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);
			if (!jewelCollectorTwoFinished && jewelCollectorTwoNumerator == jewelCollectorTwoDenominator) {
				jewelCollectorTwoFinished = true;
				jewelCollectorsFinishedNumber++;
				if (jewelCollectorsFinishedNumber == 6) {
					touchHandler.PauseTouch ();
					movementChecker.SetCollectingDone ();
				}
				GameObject temp = GameObject.Find ("Jewel Collector Two");
				temp.transform.DetachChildren ();
				Destroy (temp);
				temp = (GameObject)Instantiate (finishedJewelCollector, collectorPosition, Quaternion.identity);
				temp.name = "Jewel Collector Two";
			}
		}
	}

	public void AddOneToJewelCollectorThree (Vector3 collectorPosition) {
		if (jewelCollectorThreeNumerator == jewelCollectorThreeCap) {
			jewelCollectorThreeFinished = true;
			return;
		}
		jewelCollectorThreeNumerator++;
		string jewelCollectionNumeratorString = jewelCollectorThreeNumerator.ToString ();
		if (jewelCollectorThreeNumerator <= jewelCollectorThreeDenominator) {
			if (jewelCollectionNumeratorString.Length > 1) {
				Destroy (jewelCollectorThreeNumbers[0]);
				jewelCollectorThreeNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorThreeNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
			} 
			Destroy (jewelCollectorThreeNumbers[1]);
			jewelCollectorThreeNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorThreeNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);
			if (!jewelCollectorThreeFinished && jewelCollectorThreeNumerator == jewelCollectorThreeDenominator) {
				jewelCollectorThreeFinished = true;
				jewelCollectorsFinishedNumber++;
				if (jewelCollectorsFinishedNumber == 6) {
					touchHandler.PauseTouch ();
					movementChecker.SetCollectingDone ();
				}
				GameObject temp = GameObject.Find ("Jewel Collector Three");
				temp.transform.DetachChildren ();
				Destroy (temp);
				temp = (GameObject)Instantiate (finishedJewelCollector, collectorPosition, Quaternion.identity);
				temp.name = "Jewel Collector Three";
			}
		}
	}

	public void AddOneToJewelCollectorFour (Vector3 collectorPosition) {
		if (jewelCollectorFourNumerator == jewelCollectorFourCap) {
			jewelCollectorFourFinished = true;
			return;
		}
		jewelCollectorFourNumerator++;
		string jewelCollectionNumeratorString = jewelCollectorFourNumerator.ToString ();
		if (jewelCollectorFourNumerator <= jewelCollectorFourDenominator) {
			if (jewelCollectionNumeratorString.Length > 1) {
				Destroy (jewelCollectorFourNumbers[0]);
				jewelCollectorFourNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorFourNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
			} 
			Destroy (jewelCollectorFourNumbers[1]);
			jewelCollectorFourNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorFourNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);
			if (!jewelCollectorFourFinished && jewelCollectorFourNumerator == jewelCollectorFourDenominator) {
				jewelCollectorFourFinished = true;
				jewelCollectorsFinishedNumber++;
				if (jewelCollectorsFinishedNumber == 6) {
					touchHandler.PauseTouch ();
					movementChecker.SetCollectingDone ();
				}
				GameObject temp = GameObject.Find ("Jewel Collector Four");
				temp.transform.DetachChildren ();
				Destroy (temp);
				temp = (GameObject)Instantiate (finishedJewelCollector, collectorPosition, Quaternion.identity);
				temp.name = "Jewel Collector Four";
			}
		}
	}

	public void AddOneToJewelCollectorFive (Vector3 collectorPosition) {
		if (jewelCollectorFiveNumerator == jewelCollectorFiveCap) {
			jewelCollectorFiveFinished = true;
			return;
		}
		jewelCollectorFiveNumerator++;
		string jewelCollectionNumeratorString = jewelCollectorFiveNumerator.ToString ();
		if (jewelCollectorFiveNumerator <= jewelCollectorFiveDenominator) {
			if (jewelCollectionNumeratorString.Length > 1) {
				Destroy (jewelCollectorFiveNumbers[0]);
				jewelCollectorFiveNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorFiveNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
			} 
			Destroy (jewelCollectorFiveNumbers[1]);
			jewelCollectorFiveNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorFiveNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);
		    
			if (!jewelCollectorFiveFinished && jewelCollectorFiveNumerator == jewelCollectorFiveDenominator) {
				jewelCollectorFiveFinished = true;
				jewelCollectorsFinishedNumber++;
				if (jewelCollectorsFinishedNumber == 6) {
					touchHandler.PauseTouch ();
					movementChecker.SetCollectingDone ();
				}
				GameObject temp = GameObject.Find ("Jewel Collector Five");
				temp.transform.DetachChildren ();
				Destroy (temp);
				temp = (GameObject)Instantiate (finishedJewelCollector, collectorPosition, Quaternion.identity);
				temp.name = "Jewel Collector Five";
			}
		}
	}

	public void AddOneToJewelCollectorSix (Vector3 collectorPosition) {
		if (jewelCollectorSixNumerator == jewelCollectorSixCap) {
			jewelCollectorSixFinished = true;
			return;
		}
		jewelCollectorSixNumerator++;
		string jewelCollectionNumeratorString = jewelCollectorSixNumerator.ToString ();
		if (jewelCollectorSixNumerator <= jewelCollectorSixDenominator) {
			if (jewelCollectionNumeratorString.Length > 1) {
				Destroy (jewelCollectorSixNumbers[0]);
				jewelCollectorSixNumbers[0] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorSixNumerator / 10), collectorPosition + firstNumberOffset, Quaternion.identity);
			} 
			Destroy (jewelCollectorSixNumbers[1]);
			jewelCollectorSixNumbers[1] = (GameObject)Instantiate (GetGameObjectFromNumber (jewelCollectorSixNumerator % 10), collectorPosition + secondNumberOffset, Quaternion.identity);

			if (!jewelCollectorSixFinished && jewelCollectorSixNumerator == jewelCollectorSixDenominator) {
				jewelCollectorSixFinished = true;
				jewelCollectorsFinishedNumber++;
				if (jewelCollectorsFinishedNumber == 6) {
					touchHandler.PauseTouch ();
					movementChecker.SetCollectingDone ();
				}
				GameObject temp = GameObject.Find ("Jewel Collector Six");
				temp.transform.DetachChildren ();
				Destroy (temp);
				temp = (GameObject)Instantiate (finishedJewelCollector, collectorPosition, Quaternion.identity);
				temp.name = "Jewel Collector Six";
			}
		}
	}


	void AttachNumbersToParents (GameObject currentButton, GameObject[] currentArray) {
		for (int i = 0; i < currentArray.Length; i++) {
			if (currentArray[i] != null) {
				currentArray[i].transform.parent = currentButton.transform;	
			}
		}
	}

	GameObject GetGameObjectFromNumber (int number) {
		switch (number) {
		case 0: return zero;
		case 1: return one;
		case 2: return two;
		case 3: return three;
		case 4: return four;
		case 5: return five;
		case 6: return six;
		case 7: return seven;
		case 8: return eight;
		case 9: return nine;
		}
		return null;
	}

	public void AddToCollectionJewelsList (GameObject collectionJewel) {
		collectionJewelsSet.Add (collectionJewel);
	}

	public int GetCollectionJewelNumerator (int collectorPosition) {
		switch (collectorPosition) {
		case 1: return jewelCollectorOneNumerator;
		case 2: return jewelCollectorTwoNumerator;
		case 3: return jewelCollectorThreeNumerator;
		case 4: return jewelCollectorFourNumerator;
		case 5: return jewelCollectorFiveNumerator;
		case 6: return jewelCollectorSixNumerator;
		}
		return 0;
	}

	public void SubtractFromJewelCollectorNumerator (int collectorNumber, int subtractionNumber, Vector3 collectorPosition) {
		switch (collectorNumber) {
		case 1: jewelCollectorOneNumerator -= subtractionNumber; SetNewJewelCollectorNumerator (collectorPosition, jewelCollectorOneNumerator, jewelCollectorOneNumbers); break;
		case 2: jewelCollectorTwoNumerator -= subtractionNumber; SetNewJewelCollectorNumerator (collectorPosition, jewelCollectorTwoNumerator, jewelCollectorTwoNumbers); break;
		case 3: jewelCollectorThreeNumerator -= subtractionNumber; SetNewJewelCollectorNumerator (collectorPosition, jewelCollectorThreeNumerator, jewelCollectorThreeNumbers); break;
		case 4: jewelCollectorFourNumerator -= subtractionNumber; SetNewJewelCollectorNumerator (collectorPosition, jewelCollectorFourNumerator, jewelCollectorFourNumbers); break;
		case 5: jewelCollectorFiveNumerator -= subtractionNumber; SetNewJewelCollectorNumerator (collectorPosition, jewelCollectorFiveNumerator, jewelCollectorFiveNumbers); break;
		case 6: jewelCollectorSixNumerator -= subtractionNumber; SetNewJewelCollectorNumerator (collectorPosition, jewelCollectorSixNumerator, jewelCollectorSixNumbers); break;
		}
	}

	public void AddToTotalJewelGoal (int jewelNumberToAdd) {
		totalJewelCollectionGoal += jewelNumberToAdd;
	}

	public void SubtractFromTotalJewelGoal (int jewelNumberToSubtract) {
		totalJewelCollectionGoal -= jewelNumberToSubtract;
		Debug.Log ("totalJewelCollectionGoal = " + totalJewelCollectionGoal);
	}

	public int GetTotalJewelCollectionGoalNumber () {
		return totalJewelCollectionGoal;
	}

	public void SetJewelCollectorCap (int collectorNumber, int collectorCap) {
		switch (collectorNumber) {
		case 1: jewelCollectorOneCap = collectorCap; break;
		case 2: jewelCollectorTwoCap = collectorCap; break;
		case 3: jewelCollectorThreeCap = collectorCap; break;
		case 4: jewelCollectorFourCap = collectorCap; break;
		case 5: jewelCollectorFiveCap = collectorCap; break;
		case 6: jewelCollectorSixCap = collectorCap; break;
		}
	}

	public bool GetCollectorOneFinished () {
		return jewelCollectorOneFinished;
	}

	public bool GetCollectorTwoFinished () {
		return jewelCollectorTwoFinished;
	}

	public bool GetCollectorThreeFinished () {
		return jewelCollectorThreeFinished;
	}

	public bool GetCollectorFourFinished () {
		return jewelCollectorFourFinished;
	}

	public bool GetCollectorFiveFinished () {
		return jewelCollectorFiveFinished;
	}

	public bool GetCollectorSixFinished () {
		return jewelCollectorSixFinished;
	}

	public bool GetJewelCollectorsFinshedCollecting () {
		return jewelCollectorsFinishedNumber >= 6;
	}
}
