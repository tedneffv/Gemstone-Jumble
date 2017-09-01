using UnityEngine;
using System.Collections;

public class LifeTimerHandler : MonoBehaviour {

	public GameObject zero, one, two, three, four, five, six, seven, eight, nine, timeHolder;
	GameObject tenthMinDigit, minDigit, tenthSecDigit, secDigit, first, second, third, fourth;
	Vector3 tenthMinDigitPosition, minDigitPosition, tenthSecDigitPosition, secDigitPosition;
	float timeStamp, cooldown;
	int minRemaining, secRemaining, firstDigitInt, secondDigitInt;
	bool timerStarted, setTargetToTen, testing;
	GameManagerScript gameManagerScript;

	// Use this for initialization
	void Start () {
		tenthMinDigitPosition = new Vector3 (-1.113162f, 1.2f, -200f);
		minDigitPosition = new Vector3 (-.483161f, 1.2f, -200f);
		tenthSecDigitPosition = new Vector3 (.483161f, 1.2f, -200f);
		secDigitPosition = new Vector3 (1.113162f, 1.2f, -200f);
		cooldown = 1f;
		gameManagerScript = GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ();
		testing = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerStarted && Time.time >= timeStamp + cooldown) {
			DecreaseSecondsByOne ();
			timeStamp = Time.time;
		}
	
	}

	public bool GetSetTargetToTen () {
		return setTargetToTen;
	}

	public void SetTenthMinDigit (GameObject tenthMinDigit) {
		this.tenthMinDigit = tenthMinDigit;
	}

	public void SetMinDigit (GameObject minDigit) {
		this.minDigit = minDigit;
	}

	public void SetTenthSecDigit (GameObject tenthSecDigit) {
		this.tenthSecDigit = tenthSecDigit;
	}

	public void SetSecDigit (GameObject secDigit) {
		this.secDigit = secDigit;
	}

	public GameObject GetTenthMinDigit () {
		return tenthMinDigit;
	}

	public GameObject GetMinDigit () {
		return minDigit;
	}

	public GameObject GetTenthSecDigit () {
		return tenthSecDigit;
	}

	public GameObject GetSecDigit () {
		return secDigit;
	}

	public void SetTargetToTen (bool setTargetToTen) {
		this.setTargetToTen = setTargetToTen;
		if (setTargetToTen = true) {
			tenthMinDigitPosition = new Vector3 (10, 1.2f, -200f);
			minDigitPosition = new Vector3 (10, 1.2f, -200f);
			tenthSecDigitPosition = new Vector3 (10, 1.2f, -200f);
			secDigitPosition = new Vector3 (10, 1.2f, -200f);
		}
		else {
			tenthMinDigitPosition = new Vector3 (-1.113162f, 1.2f, -200f);
			minDigitPosition = new Vector3 (-.483161f, 1.2f, -200f);
			tenthSecDigitPosition = new Vector3 (.483161f, 1.2f, -200f);
			secDigitPosition = new Vector3 (1.113162f, 1.2f, -200f);
		}
	}

	public void StartTimer () {
		if (!timerStarted) {
			timeStamp = Time.time;
			timerStarted = true;
		}
	}

	public void StopTimer () {
		if (timerStarted) {
			timerStarted = false;
		}
	}

	public void SetMinRemaining (int minRemaining) {
		this.minRemaining = minRemaining;
	}

	public void SetSecRemaining (int secRemaining) {
		this.secRemaining = secRemaining;
	}

	public int GetMinRemaining () {
		return minRemaining;
	}

	public int GetSecRemaining () {
		return secRemaining;
	}

	void DecreaseSecondsByOne () {
		if (secRemaining == 0) {
			secRemaining = 60;
			DecreaseMinuteByOne ();
		}
		secRemaining--;
		firstDigitInt = secRemaining / 10;
		secondDigitInt = secRemaining % 10;
		Destroy (tenthSecDigit);
		Destroy (secDigit);
		switch (firstDigitInt) {
		case 0: tenthSecDigit = (GameObject)Instantiate (zero, tenthSecDigitPosition, Quaternion.identity); break;
		case 1: tenthSecDigit = (GameObject)Instantiate (one, tenthSecDigitPosition, Quaternion.identity); break;
		case 2: tenthSecDigit = (GameObject)Instantiate (two, tenthSecDigitPosition, Quaternion.identity); break;
		case 3: tenthSecDigit = (GameObject)Instantiate (three, tenthSecDigitPosition, Quaternion.identity); break;
		case 4: tenthSecDigit = (GameObject)Instantiate (four, tenthSecDigitPosition, Quaternion.identity); break;
		case 5: tenthSecDigit = (GameObject)Instantiate (five, tenthSecDigitPosition, Quaternion.identity); break;
		case 6: tenthSecDigit = (GameObject)Instantiate (six, tenthSecDigitPosition, Quaternion.identity); break;
		case 7: tenthSecDigit = (GameObject)Instantiate (seven, tenthSecDigitPosition, Quaternion.identity); break;
		case 8: tenthSecDigit = (GameObject)Instantiate (eight, tenthSecDigitPosition, Quaternion.identity); break;
		case 9: tenthSecDigit = (GameObject)Instantiate (nine, tenthSecDigitPosition, Quaternion.identity); break;
		}
		if (setTargetToTen) 
			tenthSecDigit.GetComponent<SlideInFromLeft> ().SetTargetX (10);
		else 
			tenthSecDigit.GetComponent<SlideInFromLeft> ().SetTargetX (tenthSecDigit.transform.position.x);
		tenthSecDigit.transform.parent = GameObject.Find ("Time Holder").transform;

		switch (secondDigitInt) {
		case 0: secDigit = (GameObject)Instantiate (zero, secDigitPosition, Quaternion.identity); break;
		case 1: secDigit = (GameObject)Instantiate (one, secDigitPosition, Quaternion.identity); break;
		case 2: secDigit = (GameObject)Instantiate (two, secDigitPosition, Quaternion.identity); break;
		case 3: secDigit = (GameObject)Instantiate (three, secDigitPosition, Quaternion.identity); break;
		case 4: secDigit = (GameObject)Instantiate (four, secDigitPosition, Quaternion.identity); break;
		case 5: secDigit = (GameObject)Instantiate (five, secDigitPosition, Quaternion.identity); break;
		case 6: secDigit = (GameObject)Instantiate (six, secDigitPosition, Quaternion.identity); break;
		case 7: secDigit = (GameObject)Instantiate (seven, secDigitPosition, Quaternion.identity); break;
		case 8: secDigit = (GameObject)Instantiate (eight, secDigitPosition, Quaternion.identity); break;
		case 9: secDigit = (GameObject)Instantiate (nine, secDigitPosition, Quaternion.identity); break;
		}
		if (setTargetToTen) 
			secDigit.GetComponent<SlideInFromLeft> ().SetTargetX (10);
		else
			secDigit.GetComponent<SlideInFromLeft> ().SetTargetX (secDigit.transform.position.x);
		secDigit.transform.parent = GameObject.Find ("Time Holder").transform;
		timeStamp = Time.time;
	}

	void DecreaseMinuteByOne () {
		if (minRemaining == 0) {
			minRemaining = 30;
//			GameObject.Find ("Tutorial Shade 6(Clone)").GetComponent<DarkenOnInstantiaton> ().InstantiateHeart ();
			if (gameManagerScript.GetHeartNumber () >= 5) {
				StopTimer ();
				Destroy (tenthMinDigit);
				Destroy (minDigit);
				Destroy (tenthSecDigit);
				Destroy (secDigit);
				tenthMinDigit = (GameObject)Instantiate (three, tenthMinDigitPosition, Quaternion.identity);
				minDigit = (GameObject)Instantiate (zero, minDigitPosition, Quaternion.identity);
				tenthSecDigit = (GameObject)Instantiate (zero, tenthSecDigitPosition, Quaternion.identity);
				secDigit = (GameObject)Instantiate (zero, secDigitPosition, Quaternion.identity);
				return;
			}
		}
		minRemaining--;
		int secondMinuteInt = minRemaining / 10;
		int firstMinuteInt = minRemaining % 10;

		Destroy (tenthMinDigit);
		Destroy (minDigit);
		
		switch (firstMinuteInt) {
		case 0: minDigit = (GameObject)Instantiate (zero, minDigitPosition, Quaternion.identity); break;
		case 1: minDigit = (GameObject)Instantiate (one, minDigitPosition, Quaternion.identity); break;
		case 2: minDigit = (GameObject)Instantiate (two, minDigitPosition, Quaternion.identity); break;
		case 3: minDigit = (GameObject)Instantiate (three, minDigitPosition, Quaternion.identity); break;
		case 4: minDigit = (GameObject)Instantiate (four, minDigitPosition, Quaternion.identity); break;
		case 5: minDigit = (GameObject)Instantiate (five, minDigitPosition, Quaternion.identity); break;
		case 6: minDigit = (GameObject)Instantiate (six, minDigitPosition, Quaternion.identity); break;
		case 7: minDigit = (GameObject)Instantiate (seven, minDigitPosition, Quaternion.identity); break;
		case 8: minDigit = (GameObject)Instantiate (eight, minDigitPosition, Quaternion.identity); break;
		case 9: minDigit = (GameObject)Instantiate (nine, minDigitPosition, Quaternion.identity); break;
		}
		if (setTargetToTen)
			minDigit.GetComponent<SlideInFromLeft> ().SetTargetX (10);
		else 
			minDigit.GetComponent<SlideInFromLeft> ().SetTargetX (minDigit.transform.position.x);
		minDigit.transform.parent = GameObject.Find ("Time Holder").transform;
		
		switch (secondMinuteInt) {
		case 0: tenthMinDigit = (GameObject)Instantiate (zero, tenthMinDigitPosition, Quaternion.identity); break;
		case 1: tenthMinDigit = (GameObject)Instantiate (one, tenthMinDigitPosition, Quaternion.identity); break;
		case 2: tenthMinDigit = (GameObject)Instantiate (two, tenthMinDigitPosition, Quaternion.identity); break;
		case 3: tenthMinDigit = (GameObject)Instantiate (three, tenthMinDigitPosition, Quaternion.identity); break;
		case 4: tenthMinDigit = (GameObject)Instantiate (four, tenthMinDigitPosition, Quaternion.identity); break;
		case 5: tenthMinDigit = (GameObject)Instantiate (five, tenthMinDigitPosition, Quaternion.identity); break;
		case 6: tenthMinDigit = (GameObject)Instantiate (six, tenthMinDigitPosition, Quaternion.identity); break;
		case 7: tenthMinDigit = (GameObject)Instantiate (seven, tenthMinDigitPosition, Quaternion.identity); break;
		case 8: tenthMinDigit = (GameObject)Instantiate (eight, tenthMinDigitPosition, Quaternion.identity); break;
		case 9: tenthMinDigit = (GameObject)Instantiate (nine, tenthMinDigitPosition, Quaternion.identity); break;
		}
		if (setTargetToTen)
			tenthMinDigit.GetComponent<SlideInFromLeft> ().SetTargetX (10);
		else 
			tenthMinDigit.GetComponent<SlideInFromLeft> ().SetTargetX (tenthMinDigit.transform.position.x);
		tenthMinDigit.transform.parent = GameObject.Find ("Time Holder").transform;
	}
}
