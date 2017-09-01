using UnityEngine;
using System.Collections;

public class OwlieHandler : MonoBehaviour {

	public GameObject fullBlink, leftBlink, rightBlink, normal, lookDown, lookLeft, lookUp, lookRight, flapping;
	Collider2D hit;
	float cooldown, timeStamp;
	GameObject currentOwlie;
	bool owlieSet, missedHit, owlieFlapping;
	OwlieSlider owlieSlider;
	SoundHandler soundHandler;

	// Use this for initialization
	void Start () {
		soundHandler = GameObject.Find ("Game Manager").GetComponent<SoundHandler> ();
		cooldown = Random.Range (.5f, 3.0f);
		timeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			CheckTouch (Input.mousePosition);
		}

		else if (Input.GetMouseButton (0)) {
			CheckTouch2 (Input.mousePosition);
		}

		if (Input.GetMouseButtonUp (0)) {
			CheckTouch3 (Input.mousePosition);
		}

		if (owlieSet && !owlieSlider.Sliding () && Time.time > timeStamp + cooldown) {
			GetRandomOwlie ();
			cooldown = Random.Range (.5f, 3.0f);
			timeStamp = Time.time;
		}
	}

	void CheckTouch (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));

		if (hit != null && hit.gameObject.tag != "Owlie") 
			hit = null;
	}

	void CheckTouch2 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
			
		if (!missedHit && hit != null && hit.gameObject.tag != "Owlie") {
			missedHit = true;
		}
	}


	void CheckTouch3 (Vector3 pos) {
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		hit = Physics2D.OverlapPoint (new Vector2 (wp.x, wp.y));
			
		if (!missedHit && hit != null && hit.gameObject.tag == "Owlie") {
			//Debug.Log (hit.bounds.center.x);
			if (hit.bounds.center.y - hit.gameObject.transform.position.y < 0) 
				OwlieFlap ();
			else if (hit.bounds.center.y - hit.gameObject.transform.position.y > 0 && hit.bounds.center.x - hit.gameObject.transform.position.x < 0) 
				LeftEyeHit ();
			else if (hit.bounds.center.y - hit.gameObject.transform.position.y > 0 && hit.bounds.center.x - hit.gameObject.transform.position.x > 0)
				RightEyeHit ();
			
		}

		missedHit = false;
	}

	void LeftEyeHit () {
		timeStamp = Time.time;
		cooldown = Random.Range (.5f, 3.0f);
		GameObject tempOwlie = (GameObject)Instantiate (leftBlink, currentOwlie.transform.position, Quaternion.identity);
		tempOwlie.GetComponent<OwlieSlider> ().SetTargetY (owlieSlider.GetTargetY ());
		owlieSlider = tempOwlie.GetComponent<OwlieSlider> ();
		if (owlieFlapping) {
			soundHandler.StopOwlieFlapping ();
		}
		Destroy (currentOwlie);
		currentOwlie = tempOwlie;
	}

	void RightEyeHit () {
		timeStamp = Time.time;
		cooldown = Random.Range (.5f, 3.0f);
		GameObject tempOwlie = (GameObject)Instantiate (rightBlink, currentOwlie.transform.position, Quaternion.identity);
		tempOwlie.GetComponent<OwlieSlider> ().SetTargetY (owlieSlider.GetTargetY ());
		owlieSlider = tempOwlie.GetComponent<OwlieSlider> ();
		if (owlieFlapping) {
			soundHandler.StopOwlieFlapping ();
		}
		Destroy (currentOwlie);
		currentOwlie = tempOwlie;
	}

	public void SetCurrentOwlie (GameObject currentOwlie) {
		this.currentOwlie = currentOwlie;
		owlieSlider = currentOwlie.GetComponent<OwlieSlider> ();
		owlieSet = true;
	}

	void GetRandomOwlie () {
		int owlieNumber = Random.Range (0, 6);
		GameObject tempOwlie = null;
		switch (owlieNumber) {
		case 0: tempOwlie = fullBlink; break;
		case 1: tempOwlie = normal; break;
		case 2: tempOwlie = lookDown; break;
		case 3: tempOwlie = lookLeft; break;
		case 4: tempOwlie = lookUp; break;
		case 5: tempOwlie = lookRight; break;
		}
		if (tempOwlie != null) {
			tempOwlie = (GameObject)Instantiate (tempOwlie, currentOwlie.transform.position, Quaternion.identity);
			tempOwlie.GetComponent<OwlieSlider> ().SetTargetY (owlieSlider.GetTargetY ());
			owlieSlider = tempOwlie.GetComponent<OwlieSlider> ();
			if (owlieFlapping) {
				soundHandler.StopOwlieFlapping ();
				
			}
			Destroy (currentOwlie);
			currentOwlie = tempOwlie;
		}
	}

	public void OwlieFlap () {
		soundHandler.PlayOwlieFlapping ();
		GameObject tempOwlie = (GameObject)Instantiate (flapping, currentOwlie.transform.position, Quaternion.identity);
		tempOwlie.GetComponent<OwlieSlider> ().SetTargetY (owlieSlider.GetTargetY ());
		owlieSlider = tempOwlie.GetComponent<OwlieSlider> ();
		owlieFlapping = true;
		Destroy (currentOwlie);
		currentOwlie = tempOwlie;
		currentOwlie.GetComponent<Animator> ().enabled = true;
		cooldown = Random.Range (1f, 3f);
		timeStamp = Time.time;
	}

	public void GetRidOfOwlie () {
		OwlieFlap ();
		cooldown = 100;
		currentOwlie.GetComponent<OwlieFlyUp> ().FlyAway ();
	}

	public void SetNewCooldown () {
		timeStamp = Time.time;
		cooldown = Random.Range (1f, 3f);
	}
}
