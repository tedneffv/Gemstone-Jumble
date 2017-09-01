using UnityEngine;
using System.Collections;

public class PowerCoinNumberMovementController : MonoBehaviour {
	Vector3 originalPosition;
	GameObject attachedButton;
	bool moveNumberAway, moveNumberBack;
	float yDistance;
	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		moveNumberAway = true;
		yDistance = .53f;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Mathf.Rad2Deg * transform.rotation.z < -30 && Mathf.Rad2Deg * transform.rotation.z != 0) {
//			PowerButtonHandler.decreaseCoinNumber ();
//			Destroy (gameObject);
//		}
		if (moveNumberAway) {
			if (name == "Power Coin One" && transform.position.y < originalPosition.y + yDistance)
				transform.Translate (new Vector3 (0, (originalPosition.y + yDistance) - transform.position.y, 0) * Time.deltaTime * 20, Space.Self);
			else if (name == "Power Coin One" && transform.position.y >= originalPosition.y + yDistance) {
				transform.position = new Vector3 (transform.position.x, originalPosition.y + yDistance, transform.position.z);
				moveNumberAway = false;				
			}

			if (name == "Power Coin Two" && transform.position.y < (originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 22.5f) * yDistance))) 
				transform.Translate (new Vector3 (0, (originalPosition.y + yDistance) - transform.position.y, 0) * Time.deltaTime * 15, Space.Self);
			else if (name == "Power Coin Two" && transform.position.y >= (originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 22.5f) * yDistance))) {
				transform.position = new Vector3 (transform.position.x, originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 22.5f) * yDistance), transform.position.z);
				moveNumberAway = false;
			}

			if (name == "Power Coin Three" && transform.position.y < (originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 45) * yDistance))) 
				transform.Translate (new Vector3 (0, (originalPosition.y + yDistance) - transform.position.y, 0) * Time.deltaTime * 10, Space.Self);
			else if (name == "Power Coin Three" && transform.position.y >= (originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 45) * yDistance))) {
				transform.position = new Vector3 (transform.position.x, originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 45) * yDistance), transform.position.z);
				moveNumberAway = false;
			}

			if (name == "Thousanth Power Coin" && transform.position.y < (originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 22.5f) * yDistance)))
				transform.Translate (new Vector3 (0, (originalPosition.y + yDistance) - transform.position.y, 0) * Time.deltaTime * 25, Space.Self);
			else if (name == "Thousanth Power Coin" && transform.position.y >= (originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 22.5f) * yDistance))) {
				transform.position = new Vector3 (transform.position.x, originalPosition.y + (Mathf.Cos (Mathf.Deg2Rad * 22.5f) * yDistance), transform.position.z);
				moveNumberAway = false;
			}
		}

		else if (moveNumberBack) {
			if (name == "Power Coin One")
				transform.Translate (new Vector3 (originalPosition.x - transform.position.x, originalPosition.y - transform.position.y, originalPosition.z - transform.position.z) * Time.deltaTime * 10, Space.World);

			else if (name == "Power Coin Two")
				transform.Translate (new Vector3 (originalPosition.x - transform.position.x, originalPosition.y - transform.position.y, originalPosition.z - transform.position.z) * Time.deltaTime * 15, Space.World);

			else if (name == "Power Coin Three")
				transform.Translate (new Vector3 (originalPosition.x - transform.position.x, originalPosition.y - transform.position.y, originalPosition.z - transform.position.z) * Time.deltaTime * 20, Space.World);

			else if (name == "Thousanth Power Coin")
				transform.Translate (new Vector3 (originalPosition.x - transform.position.x, originalPosition.y - transform.position.y, originalPosition.z - transform.position.z) * Time.deltaTime * 7.5f, Space.World);
			if (Mathf.Abs (attachedButton.transform.position.y - transform.position.y) < .1f) {
				PowerButtonHandler.decreaseCoinNumber ();
				Destroy (gameObject);
			}
		}
	}

	public void MoveNumberBack (bool moveNumberBack) {
		this.moveNumberAway = !moveNumberBack;
		this.moveNumberBack = moveNumberBack;
	}

	public void MoveNumberAway (bool moveNumberAway) {
		this.moveNumberBack = !moveNumberAway;
		this.moveNumberAway = moveNumberAway;
	}

	public void SetAttachedButton (GameObject attachedButton) {
		this.attachedButton = attachedButton;
	}
}
