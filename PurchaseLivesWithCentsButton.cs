using UnityEngine;
using System.Collections;

public class PurchaseLivesWithCentsButton : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	public Sprite pressedSprite, regularSprite;
	Collider2D hit, hit2, hit3;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}
		else if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}
		else if (spriteRenderer.sprite == pressedSprite)
			spriteRenderer.sprite = regularSprite;
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
		if ((hit2 != null && hit2.gameObject != gameObject) || hit2 == null) {
			if (spriteRenderer.sprite == pressedSprite)
				spriteRenderer.sprite = regularSprite;
		}
	}

	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
		if (hit3 != null && hit3.gameObject == gameObject) {
			StoreAssets.BuyFiveLivesPack ();
		}
	}
}
