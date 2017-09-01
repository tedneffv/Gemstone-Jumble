using UnityEngine;
using System.Collections;

public class ShadeController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	bool darkenShade, lightenShade;
	Color oldColor; 

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (darkenShade && spriteRenderer.color.a <= .75f) {
			oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a + .02f);
		}
		else if (lightenShade && spriteRenderer.color.a >= 0) {
			oldColor = spriteRenderer.color;
			spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .02f);
		} else if (lightenShade) 
			Destroy (gameObject);
	}

	public void DarkenShade () {
		darkenShade = true;
		lightenShade = false;
	}

	public void LightenShade () {
		lightenShade = true;
		darkenShade = false;
	}
}
