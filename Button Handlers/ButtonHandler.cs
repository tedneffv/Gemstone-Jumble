using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	GameObject pauseButton, playButton, musicButton, soundButton, restartButton, quitButton, buyCoinsButton;
	GameObject letterP, letterA, letterU, letterS, letterE, letterD;
	GameObject quitQ, quitU, quitI, quitT;
	GameObject quitConformationButton, quitCancelButton, restartConfirmationButton, restartCancelButton;
	GameObject restartFirstR, restartE, restartS, restartFirstT, restartA, restartSecondR, restartSecondT;

	// Use this for initialization
	void Start () {
	
	}
	
	public GameObject GetP () {
		return letterP;
	}
	
	public GameObject GetA () {
		return letterA;
	}
	
	public GameObject GetU () {
		return letterU;
	}
	
	public GameObject GetS () {
		return letterS;
	}
	
	public GameObject GetE () {
		return letterE;
	}
	
	public GameObject GetD () {
		return letterD;
	}

	public GameObject GetRestartFirstR () {
		return restartFirstR;
	}

	public GameObject GetRestartE () {
		return restartE;
	}

	public GameObject GetRestartS () {
		return restartS;
	}

	public GameObject GetRestartFirstT () {
		return restartFirstT;
	}

	public GameObject GetRestartA () {
		return restartA;
	}

	public GameObject GetRestartSecondR () {
		return restartSecondR;
	}

	public GameObject GetRestartSecondT () {
		return restartSecondT;
	}

	public GameObject GetRestartConfirmationButton () {
		return restartConfirmationButton;
	}

	public GameObject GetRestartCancelButton () {
		return restartCancelButton;
	}

	public GameObject GetQuitQ () {
		return quitQ;
	}

	public GameObject GetQuitU () {
		return quitU;
	}

	public GameObject GetQuitI () {
		return quitI;
	}

	public GameObject GetQuitT () {
		return quitT;
	}

	public GameObject GetQuitConformationButton () {
		return quitConformationButton;
	}

	public GameObject GetQuitCancelButton () {
		return quitCancelButton;
	}

	public void SetP (GameObject letterP) {
		this.letterP = letterP;
	}

	public void SetA (GameObject letterA) {
		this.letterA = letterA;
	}

	public void SetU (GameObject letterU) {
		this.letterU = letterU;
	}

	public void SetS (GameObject letterS) {
		this.letterS = letterS;
	}

	public void SetE (GameObject letterE) {
		this.letterE = letterE;
	}

	public void SetD (GameObject letterD) {
		this.letterD = letterD;
	}

	public void SetQuitQ (GameObject quitQ) {
		this.quitQ = quitQ;
	}

	public void SetQuitU (GameObject quitU) {
		this.quitU = quitU;
	}

	public void SetQuitI (GameObject quitI) {
		this.quitI = quitI;
	}

	public void SetQuitT (GameObject quitT) {
		this.quitT = quitT;
	}

	public void SetRestartFirstR (GameObject restartFirstR) {
		this.restartFirstR = restartFirstR;
	}

	public void SetRestartE (GameObject restartE) {
		this.restartE = restartE;
	}

	public void SetRestartS (GameObject restartS) {
		this.restartS = restartS;
	}

	public void SetRestartFirstT (GameObject restartFirstT) {
		this.restartFirstT = restartFirstT;
	}

	public void SetRestartA (GameObject restartA) {
		this.restartA = restartA;
	}

	public void SetRestartSecondR (GameObject restartSecondR) {
		this.restartSecondR = restartSecondR;
	}

	public void SetRestartSecondT (GameObject restartSecondT) {
		this.restartSecondT = restartSecondT;
	}

	public void SetRestartButton (GameObject restartButton) {
		this.restartButton = restartButton;
	}

	public void SetQuitButton (GameObject quitButton) {
		this.quitButton = quitButton;
	}

	public void SetPauseButton (GameObject pauseButton) {
		this.pauseButton = pauseButton;
	}

	public void SetPlayButton (GameObject playButton) {
		this.playButton = playButton;
	}

	public void SetMusicButton (GameObject musicButton) {
		this.musicButton = musicButton;
	}

	public void SetSoundButton (GameObject soundButton) {
		this.soundButton = soundButton;
	}

	public void SetBuyCoinsButton (GameObject buyCoinsButton) {
		this.buyCoinsButton = buyCoinsButton;
	}


	public void SetQuitConformationButton (GameObject quitConformationButton) {
		this.quitConformationButton = quitConformationButton;
	}

	public void SetQuitCancelButton (GameObject quitCancelButton) {
		this.quitCancelButton = quitCancelButton;
	}

	public void SetRestartConfirmationButton (GameObject restartConfirmationButton) {
		this.restartConfirmationButton = restartConfirmationButton;
	}

	public void SetRestartCancelButton (GameObject restartCancelButton) {
		this.restartCancelButton = restartCancelButton;
	}

	public GameObject GetRestartButton () {
		return restartButton;
	}

	public GameObject GetQuitButton () {
		return quitButton;
	}

	public GameObject GetPauseButton () {
		return pauseButton;
	}

	public GameObject GetPlayButton () {
		return playButton;
	}

	public GameObject GetMusicButton () {
		return musicButton;
	}

	public GameObject GetSoundButton () {
		return soundButton;
	}

	public GameObject GetBuyCoinsButton () {
		return buyCoinsButton;
	}
}
