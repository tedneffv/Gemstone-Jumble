using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {

	public GameObject music, soundHolder;
	GameObject instantiatedSoundHolder, instantiatedMusic;
	GameManagerScript gameManager;
	bool soundOn, owlieCheck, musicOn;
	AudioSource[] audioSources;
	int jewelBreakNumber;

	// Use this for initialization
	void Start () {
		GameObject soundHandler = GameObject.Find ("Sound Handler");
		gameManager = gameObject.GetComponent<GameManagerScript> ();
		soundOn = gameManager.GetSoundState ();
		instantiatedSoundHolder = (GameObject)Instantiate (soundHolder);
		instantiatedSoundHolder.transform.parent = soundHandler.transform;
		audioSources = instantiatedSoundHolder.GetComponents<AudioSource> ();
		jewelBreakNumber = 0;
		if (GameObject.Find ("Mountain Level One ID") != null || GameObject.Find ("Mountian Level Three ID") != null) {
			owlieCheck = true;
		}
		if (gameManager.GetMusicState ()) {
			PlayMusic ();
			musicOn = true;
		}
	}

	void Update () {
		if (owlieCheck && audioSources[3].isPlaying && audioSources[3].time > 3f) {
			audioSources[3].Play ();
		}

		if (musicOn && instantiatedMusic.GetComponent<AudioSource>().time >= 100f) {
			instantiatedMusic.GetComponent<AudioSource>().Play ();
		}
	}
	
	public void PlayMusic () {
		if (instantiatedMusic == null) {
			instantiatedMusic = (GameObject)Instantiate (music);
			instantiatedMusic.GetComponent<AudioSource>().Play ();
			DontDestroyOnLoad (instantiatedMusic);
		} else {
			instantiatedMusic.GetComponent<AudioSource>().mute = false;
		}
	}

	public void StopMusic () {
		if (instantiatedMusic == null) {
			instantiatedMusic = (GameObject)Instantiate (music);
		}
		instantiatedMusic.GetComponent<AudioSource>().mute = true;
	}

	public void PlayButtonClickDown () {
		if (soundOn) {
			audioSources[1].Play ();
//			instantiatedButtonClickDown.audio.Play ();
		}
	}

	public void PlayButtonClickUp () {
		if (soundOn) {
			audioSources[2].Play ();
			
//			instantiatedButtonClickUp.audio.Play ();
		}
	}

	public void SetSoundOn (bool soundOn) {
		this.soundOn = soundOn;
		if (!soundOn) {
			for (int i = 0; i < audioSources.Length; i++) {
				audioSources[i].Stop ();
			}
		}
	}

	public void PlayWoosh () {
		if (soundOn) {
			audioSources[0].Play ();
//			instantiatedWoosh.audio.Play ();
		}
	}

	public void PlayOwlieFlapping () {
		if (soundOn) {
			audioSources[3].volume = .5f;
			audioSources[3].Play ();
		}
	}

	public void PlayJewelSwap () {
		if (soundOn) {
			audioSources[4].Play ();
		}
	}

	public void PlayJewelBounce () {
		if (soundOn) {
			audioSources[4].Play ();
		}
	}

	public void PlayJewelBreak () {
		if (soundOn) {
			audioSources[5].Play ();
		}
	}

	public void PlayCornerStarPiano (int pianoNumber) {
		if (soundOn) {
			audioSources[6 + pianoNumber].Play ();
		}
	}

	public void PlayPowerUpSound () {
		if (soundOn) {
			audioSources[6].Play ();
		}
	}

	public void PlayPowerShot () {
		if (soundOn) {
			audioSources[7].Play ();
		}
	}

	public void PlayStarShot () {
		if (soundOn) {
			audioSources[8].Play ();
		}
	}

	public void PlayStarHit () {
		if (soundOn) {
			audioSources[9].Play ();
		}
	}

	public void PlayCoinDrop () {
		if (soundOn) 
			audioSources[10].Play ();
	}

	public void PlayCashRegister () {
		if (soundOn) {
			audioSources[11].Play ();
		}
	}

	public void PlayStarHit2 () {
		if (soundOn) {
			audioSources[12].Play ();
		}
	}

	public void PlayStarHit3 () {
		if (soundOn) {
			audioSources[13].Play ();
		}
	}

	public void PlayWoodCreak () {
		if (soundOn) {
			audioSources[15].Play ();
		}
	}

	public void PlayExplosion () {
		if (soundOn) {
			audioSources[15].Play ();
		}
	}

	public void PlaySmallExplosion () {
		if (soundOn) {
			audioSources[16].Play ();
		}
	}

	public void PlayHandbrake () {
		if (soundOn) {
			audioSources[17].Play ();
		}
	}

	public void Play321 () {
		if (soundOn) {
			audioSources[18].Play ();
		}
	}

	public void PlayGO () {
		if (soundOn) {
			audioSources[19].Play ();
		}
	}

	public void PlayAlarm () {
		if (soundOn) {
			audioSources[20].Play ();
		}
	}

	public void StopOwlieFlapping () {
		audioSources[3].Stop ();
	}

	public void QuietOwlieFlapping () {
		audioSources[3].volume = audioSources[3].volume - .0035f;
	}

	public void ResetJewelBreakNumber () {
		jewelBreakNumber = 0;
	}
}
