using UnityEngine;
using System.Collections;

public class PowerButtonCurrentStates : MonoBehaviour {

	bool singleStarPowerPressed, multiStarPowerPressed, bombPowerPressed, rowDestructionPowerPressed, jewelSwapPowerPressed, plusFivePowerPressed;

	// Use this for initialization
	void Start () {
	
	}
	
	public void SetSingleStarPowerPressed (bool singleStarPowerPressed) {
		this.singleStarPowerPressed = singleStarPowerPressed;
	}

	public bool GetSingleStarPowerPressed () {
		return singleStarPowerPressed;
	}

	public void SetMultiStarPowerPressed (bool multiStarPowerPressed) {
		this.multiStarPowerPressed = multiStarPowerPressed;
	}

	public bool GetMultiStarPowerPressed () {
		return multiStarPowerPressed;
	}

	public void SetBombPowerPressed (bool bombPowerPressed) {
		this.bombPowerPressed = bombPowerPressed;
	}

	public bool GetBombPowerPressed () {
		return bombPowerPressed;
	}

	public void SetRowDestructionPowerPressed (bool rowDestructionPowerPressed) {
		this.rowDestructionPowerPressed = rowDestructionPowerPressed;
	}

	public bool GetRowDestructionPowerPressed () {
		return rowDestructionPowerPressed;
	}

	public void SetJewelSwapPowerPressed (bool jewelSwapPowerPressed) {
		this.jewelSwapPowerPressed = jewelSwapPowerPressed;
	}

	public bool GetJewelSwapPowerPressed () {
		return jewelSwapPowerPressed;
	}

	public void SetPlusFivePowerPressed (bool plusFivePowerPressed) {
		this.plusFivePowerPressed = plusFivePowerPressed;
	}

	public bool GetPlusFivePowerPressed () {
		return plusFivePowerPressed;
	}
}
