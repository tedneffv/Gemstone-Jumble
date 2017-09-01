using UnityEngine;
using System.Collections;

public class SlugAnimationController : MonoBehaviour {

	public Sprite regularSlug, animationStage1, animationStage2, animationStage3, animationStage4;
	Sprite currentSprite;
	float timestamp, cooldown;
	bool playAnimation;

	// Use this for initialization
	void Start () {
		currentSprite = animationStage1;
		timestamp = Time.time;
		cooldown = .8f;
	}
	
	// Update is called once per frame
	void Update () {
		if (playAnimation && Time.time > timestamp + cooldown) {
			if (currentSprite == regularSlug) 
				playAnimation = false;
			timestamp = Time.time;
			gameObject.GetComponent<SpriteRenderer> ().sprite = currentSprite;
			currentSprite = GetNextSprite (currentSprite);
		}
	}

	public void PlayAnimation () {
		playAnimation = true;
	}

	Sprite GetNextSprite (Sprite sprite) {
		switch (sprite.name) {
		case "White Chomp 1": return animationStage2;
		case "White Chomp 2": return animationStage3;
		case "White Chomp 3": return animationStage4;
		case "White Chomp 4": return regularSlug;
		case "White Candy Block": return animationStage1;
		}
		return regularSlug;
	}
}
