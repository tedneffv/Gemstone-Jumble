using UnityEngine;
using System.Collections;

public class HighScoreBannerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, 4.94f - transform.position.y, 0) * Time.deltaTime * 6);
	}
}
