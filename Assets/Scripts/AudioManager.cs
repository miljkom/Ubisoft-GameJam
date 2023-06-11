using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SoundLibrary))]
[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour {

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance ??= new AudioManager(); }
    }
	[SerializeField] private SoundLibrary _library;
	[SerializeField] private AudioSource _musicSource;
	
	public float MasterVolumePercent { get; private set; }
	public float SfxVolumePercent { get; private set; }
	public float MusicVolumePercent { get; private set; }

	AudioSource sfx2DSource, sfxShootingSource;

	List<AudioSource> longSounds=new List<AudioSource>();
    private AudioManager()
    {
        instance = this;
    }
    private void Awake()
    {
		OnLevelWasLoad();
	}

	void LevelLoadPlayMusic()
	{
        PlayMusic("MenuMusic", 0.5f);
	}

    private void OnLevelWasLoad()
    {
		// Shooting with random pitch
		GameObject newSfxShootingSource = new GameObject("Sfx Shooting Source");
		sfxShootingSource= newSfxShootingSource.AddComponent<AudioSource> ();
		sfxShootingSource.transform.parent = transform;

		// 2D Sfx
		GameObject newSfx2Dsource = new GameObject ("2D sfx source");
		sfx2DSource = newSfx2Dsource.AddComponent<AudioSource> ();
		newSfx2Dsource.transform.parent = transform;

		MasterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
		SfxVolumePercent = PlayerPrefs.GetFloat ("sfx vol", 1);
		MusicVolumePercent = PlayerPrefs.GetFloat ("music vol", 1);

		int sfxMuteValue = PlayerPrefs.GetInt("sfx mute", 0);
		if (sfxMuteValue == 1) { sfx2DSource.mute = true; sfxShootingSource.mute = true; } else { sfx2DSource.mute = false; sfxShootingSource.mute = false; }

		int musicMuteValue = PlayerPrefs.GetInt("music mute", 0);
		if (musicMuteValue == 1)  
			_musicSource.mute = true;
		else  
			_musicSource.mute = false; 
	}
	public void SetVolume(float volumePercent, AudioChannel channel) {
		switch (channel) {
			case AudioChannel.Master:
				MasterVolumePercent = volumePercent;
				break;
			case AudioChannel.Sfx:
				SfxVolumePercent = volumePercent;
				break;
			case AudioChannel.Music:
				MusicVolumePercent = volumePercent;
				break;
		}

		if (longSounds != null)
		{
			for (int i = 0; i < longSounds.Count; i++)
			{
				longSounds[i].volume = SfxVolumePercent * MasterVolumePercent;
			}
		}
		
		sfxShootingSource.volume = SfxVolumePercent * MasterVolumePercent;
		sfx2DSource.volume = SfxVolumePercent * MasterVolumePercent;
		_musicSource.volume = MusicVolumePercent * MasterVolumePercent;

		PlayerPrefs.SetFloat ("master vol", MasterVolumePercent);
		PlayerPrefs.SetFloat ("music vol", MusicVolumePercent);
		PlayerPrefs.SetFloat("sfx vol", SfxVolumePercent);
		PlayerPrefs.Save ();
	}

	private void PlayMusic(string soundName, float fadeDuration = 1) 
	{
		AudioClip clip = _library.GetClipFromName(soundName);
		float volume = _library.GetVolumeFromName(soundName);

		StartCoroutine(AnimateMusicCrossfade(clip, fadeDuration, volume));
	}
	IEnumerator AnimateMusicCrossfade(AudioClip clip, float duration,float volume) 
	{
		float percent = 0;

		while (percent < volume) 
		{
			percent += Time.deltaTime * volume / duration;
			_musicSource.volume = Mathf.Lerp (MusicVolumePercent * MasterVolumePercent, 0, percent);
			yield return null;
		}
		percent = 0;
		_musicSource.clip = clip;
		_musicSource.Play();
		while (percent < volume) 
		{
			percent += Time.deltaTime * volume / duration;
			_musicSource.volume = Mathf.Lerp (0, MusicVolumePercent * MasterVolumePercent, percent);
			yield return null;
		}
	}

	public void PlaySound2D(string soundName) 
	{
		AudioClip clip = _library.GetClipFromName(soundName);
		float volume = _library.GetVolumeFromName(soundName);
		if (clip != null)
        {
			sfx2DSource.PlayOneShot(clip, SfxVolumePercent * MasterVolumePercent * volume);
		}
	}

	public void PlayShootingSound(string soundName)
	{
		AudioClip clip = _library.GetClipFromName(soundName);
		float volume = Random.Range(_library.GetVolumeFromName(soundName) - .3f, _library.GetVolumeFromName(soundName));
		sfxShootingSource.pitch = Random.Range(1 - 0.15f, 1 + 0.15f);
		sfxShootingSource.PlayOneShot(clip, SfxVolumePercent * MasterVolumePercent * volume);
	}

	public void PlayLongSound(string soundName, Transform transform)
    {
		AudioClip clip=_library.GetClipFromName(soundName);
		float volume= _library.GetVolumeFromName(soundName);
		AudioSource source = transform.GetComponent<AudioSource>();
		source.clip = clip;
		source.volume = SfxVolumePercent * MasterVolumePercent * volume;
		transform.GetComponent<AudioSource>().Play();
		longSounds.Add(source);
	}

	public void StopLongSound(Transform transform)
    {
		AudioSource source = transform.GetComponent<AudioSource>();
		source.Stop();
		longSounds.Remove(source);
    }

	public void ToggleMusic()
    {
		_musicSource.mute = !_musicSource.mute;

		if(_musicSource.mute)
			PlayerPrefs.SetInt("music mute", 1);
        else
			PlayerPrefs.SetInt("music mute", 0);

		PlayerPrefs.Save();
	}
	public void ToggleSFX()
    {
		sfx2DSource.mute = !sfx2DSource.mute;

		if (sfx2DSource.mute)
			PlayerPrefs.SetInt("sfx mute", 1);
		else
			PlayerPrefs.SetInt("sfx mute", 0);

		PlayerPrefs.Save();
	}
}

public enum AudioChannel 
{ 
	Master,
 	Sfx,
	Music 
};