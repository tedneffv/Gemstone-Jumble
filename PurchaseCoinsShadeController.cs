using UnityEngine;
using System.Collections;

public class PurchaseCoinsShadeController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	Color oldColor;
	bool darkenShade, lightenShade;

	// Use this for initialization
	void Start () {
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
			Destroy (gameObject);
		}
	}

	public void DarkenShade () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		oldColor = spriteRenderer.color;
		darkenShade = true;
	}
	
	public void LightenShade () {
		darkenShade = false;
		lightenShade = true;
	}
}
