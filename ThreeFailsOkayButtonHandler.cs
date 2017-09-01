using UnityEngine;
using System.Collections;

public class ThreeFailsOkayButtonHandler : MonoBehaviour {

	Collider2D hit, hit2, hit3;
	public Sprite regularSprite, pressedSprite;
	SpriteRenderer spriteRenderer;
	int clickNumber;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		clickNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}
		if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		}
		if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}

	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject == gameObject) {
			spriteRenderer.sprite = pressedSprite;
		}
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		if (hit2 == null || hit2.gameObject != gameObject) {
			if (spriteRenderer.sprite == pressedSprite)
				spriteRenderer.sprite = regularSprite;
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		if (hit3 != null && hit3.gameObject == gameObject) {
			if (clickNumber == 0) {
				GameObject.Find ("Three Fails Shade(Clone)").GetComponent<ThreeFailsShadeOneController> ().NextSpeechBubble ();
				GameObject.Find ("Three Fails Shade(Clone)").GetComponent<ThreeFailsShadeOneController> ().InstantiateCoins ();
				clickNumber++;
			} else if (clickNumber == 1) {
				GameObject.Find ("Three Fails Shade(Clone)").GetComponent<ThreeFailsShadeOneController> ().LightenShade ();
				clickNumber++;
			}
		}
		if (spriteRenderer.sprite == pressedSprite) 
			spriteRenderer.sprite = regularSprite;
	}
}
