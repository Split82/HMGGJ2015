using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager> {

	public AudioSource _audioSource;

	public AudioClip _soundHit;
	public AudioClip _soundJump;
	public AudioClip _soundPickup;
	public AudioClip _soundSelect;

	public AudioClip[] _soundShot;

	public void PlayHit() {
		_audioSource.PlayOneShot(_soundHit, 1.0f);
	}

	public void PlayJump() {
		_audioSource.PlayOneShot(_soundJump, 0.2f);
	}

	public void PlayPickup() {
		_audioSource.PlayOneShot(_soundPickup, 1.0f);
	}

	public void PlaySelect() {
		_audioSource.PlayOneShot(_soundSelect, 1.0f);
	}

	public void PlayShot() {
		int which = Random.Range(0, _soundShot.Length);
		float volume = Random.Range(0.5f, 1.0f);
		_audioSource.PlayOneShot(_soundShot[which], volume);
	}

}
