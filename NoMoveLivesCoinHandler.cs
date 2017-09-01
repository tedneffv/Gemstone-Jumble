using UnityEngine;
using System.Collections;

public class NoMoveLivesCoinHandler : MonoBehaviour {

	public GameObject coin, zero, one, two, three, four, five, six, seven, eight, nine;
	GameObject[] buyLivesNumbers;
	bool moveLiveNumbersDown;

	// Use this for initialization
	void Start () {
		buyLivesNumbers = new GameObject[5];
	}
	
	// Update is called once per frame
	void Update () {

		if (moveLiveNumbersDown) {
			buyLivesNumbers[0].transform.Translate (new Vector3 (0, -1.5f - buyLivesNumbers[0].transform.position.y, 0) * Time.deltaTime * 9);
			buyLivesNumbers[1].transform.Translate (new Vector3 (0, -1.5f - buyLivesNumbers[1].transform.position.y, 0) * Time.deltaTime * 8);
			buyLivesNumbers[2].transform.Translate (new Vector3 (0, -1.5f - buyLivesNumbers[2].transform.position.y, 0) * Time.deltaTime * 7);
			buyLivesNumbers[3].transform.Translate (new Vector3 (0, -1.5f - buyLivesNumbers[3].transform.position.y, 0) * Time.deltaTime * 6);
			buyLivesNumbers[4].transform.Translate (new Vector3 (0, -1.5f - buyLivesNumbers[4].transform.position.y, 0) * Time.deltaTime * 5);
		}
	
	}

	public void InstantiateBuyLivesCoins () {
		buyLivesNumbers[0] = (GameObject)Instantiate (coin, new Vector3 (1.48f, -1f, -55f), Quaternion.identity);
		buyLivesNumbers[1] = (GameObject)Instantiate (five, new Vector3 (1.8f, -1f, -55f), Quaternion.identity);
		buyLivesNumbers[2] = (GameObject)Instantiate (zero, new Vector3 (2.02f, -1f, -55f), Quaternion.identity);
		buyLivesNumbers[3] = (GameObject)Instantiate (zero, new Vector3 (2.24f, -1f, -55f), Quaternion.identity);
		buyLivesNumbers[4] = (GameObject)Instantiate (zero, new Vector3 (2.46f, -1f, -55f), Quaternion.identity);
		moveLiveNumbersDown = true;
	}
}
