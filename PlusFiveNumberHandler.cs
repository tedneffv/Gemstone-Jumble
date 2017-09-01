using UnityEngine;
using System.Collections;

public class PlusFiveNumberHandler : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	float targetY;
	Color oldColor;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		targetY = transform.position.y + 1;
		oldColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, targetY - transform.position.y, 0) * Time.deltaTime);
		spriteRenderer.color = new Color (oldColor.r, oldColor.g, oldColor.b, oldColor.a - .01f);
		oldColor = spriteRenderer.color;
		if (oldColor.a <= 0) {
			Destroy (gameObject);
		}
	}
}
