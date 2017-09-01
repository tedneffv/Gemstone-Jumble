using UnityEngine;
using System.Collections;

public class BombNumberHandler : MonoBehaviour {

	GameObject parentObject;
	int bombNumber;
	bool bombDestroyed, vectorSet;
	Vector3 bombNumberPosition;
	public GameObject plusFive;

	void Awake () {
		SetBombNumber ();
	}
	// Use this for initialization
	void Start () {
		parentObject = transform.parent.gameObject;	
	}

	// Update is called once per frame
	void Update () {
		if (bombDestroyed) {
			if (!vectorSet) {
				bombNumberPosition = GameObject.Find ("Level Controller").GetComponent<LevelTwoBombRemainderController> ().GetCorrectVector ();

				vectorSet = true;
			}
			transform.Translate ((bombNumberPosition - transform.position) * Time.deltaTime * 6);
			if (Mathf.Abs (bombNumberPosition.x - transform.position.x) < .01f && Mathf.Abs (bombNumberPosition.x - transform.position.x) < .01f) {
				GameObject.Find ("Level Controller").GetComponent<LevelTwoBombRemainderController> ().AddToBombRemainder (bombNumber);
				Destroy (gameObject);
			}
		}
	}

	public void InstantiatePlusFive () {
		Instantiate (plusFive, new Vector3 (transform.position.x, transform.position.y, -20), Quaternion.identity);
	}

	public void SetBombNumber (int bombNumber) {
		this.bombNumber = bombNumber;
	}

	public int GetBombNumber () {
		return bombNumber;
	}

	public void SetBombDestroyed (bool bombDestroyed) {
		this.bombDestroyed = bombDestroyed;
	}

	void SetBombNumber () {
		switch (name) {
		case "Bomb Number 105(Clone)": bombNumber = 105; break;
		case "Bomb Number 104(Clone)": bombNumber = 104; break;
		case "Bomb Number 103(Clone)": bombNumber = 103; break;
		case "Bomb Number 102(Clone)": bombNumber = 102; break;
		case "Bomb Number 101(Clone)": bombNumber = 101; break;
		case "Bomb Number 100(Clone)": bombNumber = 100; break;

		case "Bomb Number 99(Clone)": bombNumber = 99; break;
		case "Bomb Number 98(Clone)": bombNumber = 98; break;
		case "Bomb Number 97(Clone)": bombNumber = 97; break;
		case "Bomb Number 96(Clone)": bombNumber = 96; break;
		case "Bomb Number 95(Clone)": bombNumber = 95; break;
		case "Bomb Number 94(Clone)": bombNumber = 94; break;
		case "Bomb Number 93(Clone)": bombNumber = 93; break;
		case "Bomb Number 92(Clone)": bombNumber = 92; break;
		case "Bomb Number 91(Clone)": bombNumber = 91; break;
		case "Bomb Number 90(Clone)": bombNumber = 90; break;

		case "Bomb Number 89(Clone)": bombNumber = 89; break;
		case "Bomb Number 88(Clone)": bombNumber = 88; break;
		case "Bomb Number 87(Clone)": bombNumber = 87; break;
		case "Bomb Number 86(Clone)": bombNumber = 86; break;
		case "Bomb Number 85(Clone)": bombNumber = 85; break;
		case "Bomb Number 84(Clone)": bombNumber = 84; break;
		case "Bomb Number 83(Clone)": bombNumber = 83; break;
		case "Bomb Number 82(Clone)": bombNumber = 82; break;
		case "Bomb Number 81(Clone)": bombNumber = 81; break;
		case "Bomb Number 80(Clone)": bombNumber = 80; break;

		case "Bomb Number 79(Clone)": bombNumber = 79; break;
		case "Bomb Number 78(Clone)": bombNumber = 78; break;
		case "Bomb Number 77(Clone)": bombNumber = 77; break;
		case "Bomb Number 76(Clone)": bombNumber = 76; break;
		case "Bomb Number 75(Clone)": bombNumber = 75; break;
		case "Bomb Number 74(Clone)": bombNumber = 74; break;
		case "Bomb Number 73(Clone)": bombNumber = 73; break;
		case "Bomb Number 72(Clone)": bombNumber = 72; break;
		case "Bomb Number 71(Clone)": bombNumber = 71; break;
		case "Bomb Number 70(Clone)": bombNumber = 70; break;

		case "Bomb Number 69(Clone)": bombNumber = 69; break;
		case "Bomb Number 68(Clone)": bombNumber = 68; break;
		case "Bomb Number 67(Clone)": bombNumber = 67; break;
		case "Bomb Number 66(Clone)": bombNumber = 66; break;
		case "Bomb Number 65(Clone)": bombNumber = 65; break;
		case "Bomb Number 64(Clone)": bombNumber = 64; break;
		case "Bomb Number 63(Clone)": bombNumber = 63; break;
		case "Bomb Number 62(Clone)": bombNumber = 62; break;
		case "Bomb Number 61(Clone)": bombNumber = 61; break;
		case "Bomb Number 60(Clone)": bombNumber = 60; break;

		case "Bomb Number 59(Clone)": bombNumber = 59; break;
		case "Bomb Number 58(Clone)": bombNumber = 58; break;
		case "Bomb Number 57(Clone)": bombNumber = 57; break;
		case "Bomb Number 56(Clone)": bombNumber = 56; break;
		case "Bomb Number 55(Clone)": bombNumber = 55; break;
		case "Bomb Number 54(Clone)": bombNumber = 54; break;
		case "Bomb Number 53(Clone)": bombNumber = 53; break;
		case "Bomb Number 52(Clone)": bombNumber = 52; break;
		case "Bomb Number 51(Clone)": bombNumber = 51; break;
		case "Bomb Number 50(Clone)": bombNumber = 50; break;

		case "Bomb Number 49(Clone)": bombNumber = 49; break;
		case "Bomb Number 48(Clone)": bombNumber = 48; break;
		case "Bomb Number 47(Clone)": bombNumber = 47; break;
		case "Bomb Number 46(Clone)": bombNumber = 46; break;
		case "Bomb Number 45(Clone)": bombNumber = 45; break;
		case "Bomb Number 44(Clone)": bombNumber = 44; break;
		case "Bomb Number 43(Clone)": bombNumber = 43; break;
		case "Bomb Number 42(Clone)": bombNumber = 42; break;
		case "Bomb Number 41(Clone)": bombNumber = 41; break;
		case "Bomb Number 40(Clone)": bombNumber = 40; break;

		case "Bomb Number 39(Clone)": bombNumber = 39; break;
		case "Bomb Number 38(Clone)": bombNumber = 38; break;
		case "Bomb Number 37(Clone)": bombNumber = 37; break;
		case "Bomb Number 36(Clone)": bombNumber = 36; break;
		case "Bomb Number 35(Clone)": bombNumber = 35; break;
		case "Bomb Number 34(Clone)": bombNumber = 34; break;
		case "Bomb Number 33(Clone)": bombNumber = 33; break;
		case "Bomb Number 32(Clone)": bombNumber = 32; break;
		case "Bomb Number 31(Clone)": bombNumber = 31; break;
		case "Bomb Number 30(Clone)": bombNumber = 30; break;

		case "Bomb Number 29(Clone)": bombNumber = 29; break;
		case "Bomb Number 28(Clone)": bombNumber = 28; break;
		case "Bomb Number 27(Clone)": bombNumber = 27; break;
		case "Bomb Number 26(Clone)": bombNumber = 26; break;
		case "Bomb Number 25(Clone)": bombNumber = 25; break;
		case "Bomb Number 24(Clone)": bombNumber = 24; break;
		case "Bomb Number 23(Clone)": bombNumber = 23; break;
		case "Bomb Number 22(Clone)": bombNumber = 22; break;
		case "Bomb Number 21(Clone)": bombNumber = 21; break;
		case "Bomb Number 20(Clone)": bombNumber = 20; break;

		case "Bomb Number 19(Clone)": bombNumber = 19; break;
		case "Bomb Number 18(Clone)": bombNumber = 18; break;
		case "Bomb Number 17(Clone)": bombNumber = 17; break;
		case "Bomb Number 16(Clone)": bombNumber = 16; break;
		case "Bomb Number 15(Clone)": bombNumber = 15; break;
		case "Bomb Number 14(Clone)": bombNumber = 14; break;
		case "Bomb Number 13(Clone)": bombNumber = 13; break;
		case "Bomb Number 12(Clone)": bombNumber = 12; break;
		case "Bomb Number 11(Clone)": bombNumber = 11; break;
		case "Bomb Number 10(Clone)": bombNumber = 10; break;

		case "Bomb Number 9(Clone)": bombNumber = 9; break;
		case "Bomb Number 8(Clone)": bombNumber = 8; break;
		case "Bomb Number 7(Clone)": bombNumber = 7; break;
		case "Bomb Number 6(Clone)": bombNumber = 6; break;
		case "Bomb Number 5(Clone)": bombNumber = 5; break;
		case "Bomb Number 4(Clone)": bombNumber = 4; break;
		case "Bomb Number 3(Clone)": bombNumber = 3; break;
		case "Bomb Number 2(Clone)": bombNumber = 2; break;
		case "Bomb Number 1(Clone)": bombNumber = 1; break;
		case "Bomb Number 0(Clone)": bombNumber = 0; break;

		}
	}
}
