using UnityEngine;
using System.Collections;

public class PumpUpWordHolder : MonoBehaviour {

	public GameObject wowWord;
	float cooldown, timestamp;
	bool wordCountdownStarted;
	GameObject wordOne, wordTwo, wordThree, wordFour, wordFive, wordSix, wordSeven, wordEight, wordNine, wordTen;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		wordCountdownStarted = false;
		cooldown = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {

		if (wordCountdownStarted && Time.time > timestamp + cooldown) {
			wordCountdownStarted = false;
			wordOne.GetComponent<SloMoWordController> ().SetDestination (10, wordOne.transform.position.y);
			wordTwo.GetComponent<SloMoWordController> ().SetDestination (10, wordTwo.transform.position.y);
			wordThree.GetComponent<SloMoWordController> ().SetDestination (10, wordThree.transform.position.y);
		}
	
	}

	public void InstantiateWow () {
		if (wordCountdownStarted)
			return;

		wordOne = (GameObject)Instantiate (wowWord, new Vector3 (-10, 3.44f, -1.2f), Quaternion.identity);
		wordOne.GetComponent<SloMoWordController> ().SetDestination (-1, 3.44f);

		wordTwo = (GameObject)Instantiate (wowWord, new Vector3 (-10, 3.44f, -1.1f), Quaternion.identity);
		wordTwo.GetComponent<SloMoWordController> ().SetDestination (-1, 3.44f);
		wordTwo.GetComponent<SpriteRenderer> ().color = new Color (1, .8705882f, 0);

		wordThree = (GameObject)Instantiate (wowWord, new Vector3 (-10, 3.44f, -1f), Quaternion.identity);
		wordThree.GetComponent<SloMoWordController> ().SetDestination (-1, 3.44f);
		wordThree.GetComponent<SpriteRenderer> ().color = new Color (.63137f, .63137f, .63137f);
		
		wordCountdownStarted = true;
		timestamp = Time.time;
	}
}
