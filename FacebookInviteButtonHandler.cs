using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;

public class FacebookInviteButtonHandler : MonoBehaviour {

//	Collider2D hit, hit2, hit3;
//	public GameObject pressedInviteButton;
//	GameObject instantiatedPressedInviteButton;
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//		if (Input.GetMouseButtonDown (0)) {
//			CheckTouch (Input.mousePosition);
//		} else if (Input.GetMouseButton (0)) {
//			CheckTouch2 (Input.mousePosition);
//		} else if (Input.GetMouseButtonUp (0)) {
//			CheckTouch3 (Input.mousePosition);
//		}
//	}
//
//	void CheckTouch (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit != null && hit.gameObject == gameObject) {
//			instantiatedPressedInviteButton = (GameObject)Instantiate (pressedInviteButton, new Vector3 (transform.position.x, transform.position.y, transform.position.z - .01f), Quaternion.identity);
//		}
//	}
//
//	void CheckTouch2 (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit2 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit2 == null || (hit2 != null && hit2.gameObject != gameObject)) {
//			if (instantiatedPressedInviteButton != null) {
//				Destroy (instantiatedPressedInviteButton);
//			}
//		}
//	}
//
//	void CheckTouch3 (Vector3 pos) {
//		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
//		hit3 = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
//
//		if (hit3 != null && hit3.gameObject == gameObject) {
//			if (instantiatedPressedInviteButton != null) {
//				VirtualCurrency heart = StoreAssets.HEART_CURRENCY;
//				Reward heartReward = new VirtualItemReward ("heartReward_ID", "1 heart Reward", heart.ID, 1);
//				SoomlaProfile.Invite (
//					Provider.FACEBOOK,
//					"Come play Gemstone Jumble with me!",
//					"Gemstone Jumble Invitation",
//					"",
//					heartReward
//					);
//			}
//		}
//	}
}
