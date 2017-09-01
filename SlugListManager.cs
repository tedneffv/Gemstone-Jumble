using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SlugListManager {

	static HashSet<GameObject> slugList = new HashSet<GameObject> ();

	public static bool AddToSlugList (GameObject slugBlock) {
		return slugList.Add (slugBlock);
	}

	public static void RemoveFromSlugList (GameObject slugBlock) {
		slugList.Remove (slugBlock);
	}

	public static int SlugListSize () {
		return slugList.Count;
	}

	public static void MoveAllSlugs () {
		foreach (GameObject a in slugList) {
			if (a != null) {
				a.GetComponent<SlugMovementController> ().MoveSlug ();
			}
		}
		GameObject.Find ("Level Controller").GetComponent<RockLevelMovementChecker> ().SetGridStaticToFalse ();
	}
}
