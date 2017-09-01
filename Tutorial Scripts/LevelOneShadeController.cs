using UnityEngine;
using System.Collections;

public class LevelOneShadeController : MonoBehaviour {

	public GameObject owlieTheOwl, okayButton, speechBubble1, speechBubble2, speechBubble3, speechBubble4;
	GameObject instantiatedOwlie, instantiatedOkayButton, instantiatedSpeechBubble;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade;
	int owlieNumber, speechBubbleNumber;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		owlieNumber = 0;
		speechBubbleNumber = 0;
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (darkenShade && spriteRenderer.color.a < .75f) {
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
			oldColor = spriteRenderer.color;
		}

		else if (lightenShade && spriteRenderer.color.a > 0) {
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
			oldColor = spriteRenderer.color;
		}

		else if (lightenShade && spriteRenderer.color.a <= 0) {
			transform.Translate (new Vector3 (0, 0, 200));
			lightenShade = false;
		}
	}

	public void DarkenShade () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		oldColor = spriteRenderer.color;
		darkenShade = true;
		InstantiateOwlie ();
	}

	public void LightenShade () {
		darkenShade = false;
		lightenShade = true;
	}

	void InstantiateOwlie () {	
		switch (owlieNumber) {
		case 0: InstantiateFirstOwlie (); break;
		case 1: InstantiateSecondOwlie (); break;
		}
		owlieNumber++;
	}

	void InstantiateFirstOwlie () {
		soundHandler.PlayOwlieFlapping ();
		instantiatedOwlie = (GameObject)Instantiate (owlieTheOwl, new Vector3 (2, 10, -102), Quaternion.identity);
		instantiatedOwlie.GetComponent<OwlieSlider> ().SetTargetY (0);
		gameObject.GetComponent<OwlieHandler> ().SetCurrentOwlie (instantiatedOwlie);
		instantiatedOwlie.GetComponent<Animator> ().enabled = true;

		instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble1, new Vector3(-70, 1.84f, -101.1f), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.78f);
		speechBubbleNumber++;

		instantiatedOkayButton = (GameObject)Instantiate (okayButton, new Vector3 (-10, -1.78f, -101.1f), Quaternion.identity);
		instantiatedOkayButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.99f);
	}

	void InstantiateSecondOwlie () {
		soundHandler.PlayOwlieFlapping ();
		instantiatedOwlie = (GameObject)Instantiate (owlieTheOwl, new Vector3 (-1.5f, 10, -102), Quaternion.identity);
		instantiatedOwlie.GetComponent<OwlieSlider> ().SetTargetY (-1);
		gameObject.GetComponent<OwlieHandler> ().SetCurrentOwlie (instantiatedOwlie);
		instantiatedOwlie.GetComponent<Animator> ().enabled = true;

		instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble3, new Vector3(-70, .87f, -101.1f), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.22f);

		instantiatedOkayButton = (GameObject)Instantiate (okayButton, new Vector3 (-10, -2.78f, -101.1f), Quaternion.identity);
		instantiatedOkayButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-1.49f);

		GetComponent<OwlieHandler> ().SetNewCooldown ();

	}

	public void NextSpeechBubble () {
		switch (speechBubbleNumber) {
		case 1: instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble2, new Vector3 (-10, 1.84f, -101), Quaternion.identity);
			instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.78f);
			speechBubbleNumber++; break;
		case 2: instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			speechBubbleNumber++; break;
		case 3: instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble4, new Vector3 (-10, .87f, -101.1f), Quaternion.identity);
			instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.22f);
			speechBubbleNumber++; break;
		case 4: instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
			speechBubbleNumber++; break;
		}
	}

	public int GetOwlieNumber () {
		return owlieNumber;
	}

	public bool SpeechBubbleSliding () {
		return instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().Sliding ();
	}
}
