using UnityEngine;
using System.Collections;

public class JewelCollectionSeekingScript : MonoBehaviour {

	Vector3 targetPosition;
	float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTargetPosition (Vector3 targetPosition, float speed) {
		this.targetPosition = targetPosition;
		this.speed = speed;
	}

}
