using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour {

	float cooldown, timestamp;
	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool dim;

	// Use this for initialization
	void Start () {
		timestamp = Time.time;
		cooldown = 1.8f;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		oldColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dim && Time.time > timestamp + cooldown) {
			dim = true;
		}	
		if (dim) {
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, (oldColor.a - .02f));
			oldColor = spriteRenderer.color;
			if (oldColor.a <= 0) {
				Destroy (gameObject);
				Destroy (GameObject.Find ("No Extras Shade"));
				GameObject.Find ("Game Manager").GetComponent<GameManagerScript> ().StartDialogBox ();
			}
		}
	}
}
