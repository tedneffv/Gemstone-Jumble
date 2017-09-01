using UnityEngine;
using System.Collections;

public class LevelThreeShadeController : MonoBehaviour {

	public GameObject owlieTheOwl, okayButton, speechBubble;
	GameObject instantiatedShade, instantiatedOwlie, instantiatedOkayButton, instantiatedSpeechBubble;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade;
	int owlieNumber, speechBubbleNumber;
	SoundHandler soundHandler;
	
	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		owlieNumber = 0;
		speechBubbleNumber = 0;
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
		}
		owlieNumber++;
	}
	
	void InstantiateFirstOwlie () {
		soundHandler.PlayOwlieFlapping ();
		instantiatedOwlie = (GameObject)Instantiate (owlieTheOwl, new Vector3 (2, 10, -102), Quaternion.identity);
		instantiatedOwlie.GetComponent<OwlieSlider> ().SetTargetY (0);
		gameObject.GetComponent<OwlieHandler> ().SetCurrentOwlie (instantiatedOwlie);
		instantiatedOwlie.GetComponent<Animator> ().enabled = true;
		
		instantiatedSpeechBubble = (GameObject)Instantiate (speechBubble, new Vector3(-70, 1.84f, -101.1f), Quaternion.identity);
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (-.78f);
		speechBubbleNumber++;
		
		instantiatedOkayButton = (GameObject)Instantiate (okayButton, new Vector3 (-10, -1.78f, -101.1f), Quaternion.identity);
		instantiatedOkayButton.GetComponent<LevelFailedScreenSlider> ().SetTargetX (1.99f);
	}
	
	public void NextSpeechBubble () {
		instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().SetTargetX (10);
	}
	
	public int GetOwlieNumber () {
		return owlieNumber;
	}
	
	public bool SpeechBubbleSliding () {
		return instantiatedSpeechBubble.GetComponent<LevelFailedScreenSlider> ().Sliding ();
	}
}
