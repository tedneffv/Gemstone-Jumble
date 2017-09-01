using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	static int clackNumber; 
	float timeStamp, cooldown;
	bool gameStarted, gameEnded;
	// Use this for initialization
	void Start () {
		clackNumber = 1;	
		timeStamp = Time.time;
	}
	
	public bool AddToClack () {
		if (!gameStarted) {
			if (clackNumber % 20 == 0) {
				clackNumber++;
				return true;
			}
			clackNumber++;
			return false;
		}
		if (gameEnded) {
			if (clackNumber % 6 == 0) {
				clackNumber++;
				return true;
			}
			clackNumber++;
			return false;
		}
		if (clackNumber % 3 == 0) {
			clackNumber++;
			return true;
		}
		clackNumber++;
		return false;
	}

	public void SetGameStarted (bool gameStarted) {
		this.gameStarted = gameStarted;
	}

	public bool GetGameStarted () {
		return gameStarted;
	} 

	public void SetGameEnded (bool gameEnded) {
		this.gameEnded = gameEnded;
	}
}
