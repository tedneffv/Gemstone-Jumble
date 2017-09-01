using UnityEngine;
using System.Collections;

public class PositionHolder : MonoBehaviour {

	int row, col;

	public PositionHolder (int row, int col) {
		this.row = row;
		this.col = col;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	public void SetRowAndCol (int row, int col) {
		this.row = row;
		this.col = col;
	}

	public int GetRow () {
		return row;
	}

	public int GetCol () {
		return col;
	}
}
