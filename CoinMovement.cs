using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, Random.Range (0, 361), 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 60, 0) * Time.deltaTime);
	}
}
