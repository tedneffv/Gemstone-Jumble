using UnityEngine;
using System.Collections;

public class ArrowHolder : MonoBehaviour {

	GameObject moveArrow, orangeArrow, purpleArrow, blueArrow, greenArrow, redArrow, whiteArrow; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMoveArrow (GameObject arrow) {
		moveArrow = arrow;
	}

	public GameObject GetMoveArrow () {
		return moveArrow;
	}

	public void SetOrangeArrow (GameObject arrow) {
		orangeArrow = arrow;
	}

	public GameObject GetOrangeArrow () {
		return orangeArrow;
	}

	public void SetPurpleArrow (GameObject arrow) {
		purpleArrow = arrow;
	}

	public GameObject GetPurpleArrow () {
		return purpleArrow;
	}

	public void SetBlueArrow (GameObject arrow) {
		blueArrow = arrow;
	}

	public GameObject GetBlueArrow () {
		return blueArrow; 
	}

	public void SetGreenArrow (GameObject arrow) {
		greenArrow = arrow;
	}

	public GameObject GetGreenArrow () {
		return greenArrow;
	}

	public void SetRedArrow (GameObject arrow) {
		redArrow = arrow;
	}

	public GameObject GetRedArrow () {
		return redArrow;
	}

	public void SetWhiteArrow (GameObject arrow) {
		whiteArrow = arrow;
	}

	public GameObject GetWhiteArrow () {
		return whiteArrow;
	}
}
