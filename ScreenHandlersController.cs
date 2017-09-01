using UnityEngine;
using System.Collections;

public class ScreenHandlersController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
}
