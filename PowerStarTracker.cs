using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerStarTracker : MonoBehaviour {

	static HashSet<GameObject> destructionJewels;
	static bool starsShooting;
	float timestamp, cooldown;
	static int addedCount, subtractCount;

	// Use this for initialization
	void Start () {
		destructionJewels = new HashSet<GameObject> ();
		cooldown = 1;
		timestamp = Time.time;
		addedCount = 0;
		subtractCount = 0;
	}

	public static bool AddToHashSet (GameObject jewel) {
		addedCount++;
		if (starsShooting) {
			return true;
		}
		return destructionJewels.Add (jewel);
	}

	public static bool RemoveFromHashSet (GameObject jewel) {
		subtractCount++;
		return destructionJewels.Remove (jewel);
	}

	public static void SetStarsShooting (bool shooting) {
		starsShooting = shooting;
	}

	public static bool ContainsJewel (GameObject jewel) {
		return destructionJewels.Contains (jewel);
	}

	public static void Clear () {
		destructionJewels.Clear ();
	}
}
