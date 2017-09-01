using UnityEngine;
using System.Collections;

public class TutorialLevelOneCreator : MonoBehaviour {

	public GameObject blueBlock, greenBlock, yellowBlock, whiteBlock, purpleBlock, redBlock, pauseButton;
	GameObject instantiatedJewel;
	float startingLeftJewelPosition, horizontalMultiplier;
	RockLevelInstantiator instantiator;
	RockLevelJewelMovement jewelMovement;

	void Start () {
		instantiator = GameObject.Find ("Level Controller").GetComponent<RockLevelInstantiator> ();
		startingLeftJewelPosition = -2.45f;
		horizontalMultiplier = .6125f;
		Instantiate (pauseButton, new Vector3 (2.3f, -4f, -100f), Quaternion.identity);
	}

	public void InstantiateJewels () {
		int jewelNumber = 0;
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				//Debug.Log ("Inside for loops");
				instantiatedJewel = (GameObject)Instantiate(GetTutorialBlocks (jewelNumber), new Vector3 (startingLeftJewelPosition + (horizontalMultiplier * j), Random.Range (10, 15), -.5f), Quaternion.identity);
				jewelMovement = instantiatedJewel.GetComponent<RockLevelJewelMovement> ();
				jewelMovement.SetRow (i);
				jewelMovement.SetCol (j);
				instantiator.SetJewelGridGameObject (i, j, instantiatedJewel);
				instantiatedJewel.layer = i + 17;
				jewelNumber++;
			}
		}

	}

	private GameObject GetTutorialBlocks (int jewelNumber) {
		switch (jewelNumber) {
		case 0: return blueBlock;
		case 1: return greenBlock;
		case 2: return yellowBlock;
		case 3: return blueBlock;
		case 4: return greenBlock; 
		case 5: return whiteBlock;
			
		case 6: return whiteBlock;
		case 7: return blueBlock;
		case 8: return yellowBlock;
		case 9: return blueBlock;
		case 10: return whiteBlock; 
		case 11: return greenBlock;
			
		case 12: return purpleBlock;
		case 13: return whiteBlock;
		case 14: return redBlock;
		case 15: return purpleBlock;
		case 16: return yellowBlock;
		case 17: return whiteBlock;
			
		case 18: return whiteBlock;
		case 19: return redBlock;
		case 20: return greenBlock;
		case 21: return blueBlock;
		case 22: return blueBlock; 
		case 23: return whiteBlock;
			
		case 24: return purpleBlock;
		case 25: return redBlock;
		case 26: return whiteBlock;
		case 27: return blueBlock;
		case 28: return purpleBlock;
		case 29: return yellowBlock;
			
		case 30: return purpleBlock;
		case 31: return redBlock;
		case 32: return purpleBlock;
		case 33: return redBlock;
		case 34: return whiteBlock;
		case 35: return greenBlock;
			
		case 36: return greenBlock;
		case 37: return yellowBlock;
		case 38: return purpleBlock;
		case 39: return blueBlock;
		case 40: return whiteBlock; 
		case 41: return purpleBlock;
			
		case 42: return blueBlock;
		case 43: return greenBlock;
		case 44: return greenBlock;
		case 45: return whiteBlock;
		case 46: return greenBlock;
		case 47: return redBlock;
			
		case 48: return greenBlock;
		case 49: return greenBlock;
		case 50: return redBlock;
		case 51: return whiteBlock;
		case 52: return redBlock;
		case 53: return blueBlock;
			
		case 54: return whiteBlock;
		case 55: return redBlock;
		case 56: return yellowBlock;
		case 57: return greenBlock;
		case 58: return blueBlock;
		case 59: return yellowBlock;
			
		case 60: return blueBlock;
		case 61: return whiteBlock;
		case 62: return purpleBlock;
		case 63: return blueBlock;
		case 64: return whiteBlock; 
		case 65: return redBlock;
			
		case 66: return blueBlock;
		case 67: return purpleBlock;
		case 68: return blueBlock;
		case 69: return whiteBlock;
		case 70: return purpleBlock; 
		case 71: return redBlock;
			
		case 72: return redBlock;
		case 73: return blueBlock;
		case 74: return greenBlock;
		case 75: return yellowBlock;
		case 76: return greenBlock;
		case 77: return redBlock;
			
		case 78: return yellowBlock;
		case 79: return yellowBlock;
		case 80: return redBlock;
		case 81: return redBlock;
		case 82: return purpleBlock;
		case 83: return greenBlock;
			
		case 84: return redBlock;
		case 85: return yellowBlock;
		case 86: return greenBlock;
		case 87: return purpleBlock;
		case 88: return redBlock;
		case 89: return yellowBlock;
			
		case 90: return purpleBlock;
		case 91: return greenBlock;
		case 92: return yellowBlock;
		case 93: return purpleBlock;
		case 94: return whiteBlock;
		case 95: return yellowBlock;
			
		case 96: return blueBlock;
		case 97: return redBlock;
		case 98: return purpleBlock;
		case 99: return greenBlock;
		case 100: return yellowBlock;
		case 101: return purpleBlock;
			
		case 102: return whiteBlock;
		case 103: return yellowBlock;
		case 104: return yellowBlock;
		case 105: return greenBlock;
		case 106: return blueBlock;
		case 107: return yellowBlock;
			
		case 108: return redBlock;
		case 109: return blueBlock;
		case 110: return greenBlock;
		case 111: return yellowBlock;
		case 112: return greenBlock;
		case 113: return redBlock;
			
		case 114: return blueBlock;
		case 115: return greenBlock;
		case 116: return greenBlock;
		case 117: return whiteBlock;
		case 118: return greenBlock;
		case 119: return redBlock;
			
		case 120: return blueBlock;
		case 121: return greenBlock;
		case 122: return greenBlock;
		case 123: return whiteBlock;
		case 124: return greenBlock;
		case 125: return redBlock;
			
		case 126: return blueBlock;
		case 127: return greenBlock;
		case 128: return greenBlock;
		case 129: return whiteBlock;
		case 130: return greenBlock;
		case 131: return redBlock;
			
		}
		//Debug.Log ("RandomBlock returned null from LevelOneInstantiator.cs");
		return null;
	}
}
