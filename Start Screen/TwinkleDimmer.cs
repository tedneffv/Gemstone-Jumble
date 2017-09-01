using UnityEngine;
using System.Collections;

public class TwinkleDimmer : MonoBehaviour {

	bool increaseAlpha, decreaseAlpha;
	float alphaIncrement, timeStamp, cooldown;
	SpriteRenderer spriteRenderer;
	Color oldColor;

	// Use this for initialization
	void Start () {
		alphaIncrement = Random.Range (.01f, .02f);
		spriteRenderer = GetComponent<SpriteRenderer> ();
		increaseAlpha = true;
		cooldown = .01f;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timeStamp + cooldown && increaseAlpha) {
			oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + alphaIncrement);
			timeStamp = Time.time;
			if (spriteRenderer.color.a >= 1) {
				increaseAlpha = false;
				decreaseAlpha = true;
			}
		} else if (Time.time > timeStamp + cooldown && decreaseAlpha) {
			oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - alphaIncrement);
			timeStamp = Time.time;
			if (spriteRenderer.color.a <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
