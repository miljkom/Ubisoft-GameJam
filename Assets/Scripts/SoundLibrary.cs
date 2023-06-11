using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {

	public SoundGroup[] soundGroups;

	Dictionary<string, SoundGroup> groupDictionary = new Dictionary<string, SoundGroup>();

	void Awake() 
	{
		foreach (SoundGroup soundGroup in soundGroups) 
		{
			groupDictionary.Add (soundGroup.groupID, soundGroup);
		}
	}

	public AudioClip GetClipFromName(string name) 
	{
		if (groupDictionary.ContainsKey (name)) 
		{
			AudioClip[] sounds = groupDictionary [name].group;
			return sounds [Random.Range (0, sounds.Length)];
		}
		return null;
	}

	public float GetVolumeFromName(string name)
    {
		if (groupDictionary.ContainsKey(name))
		{
			float volume = groupDictionary[name].volume;
			return volume;
		}
		return 0;
	}

	[System.Serializable]
	public class SoundGroup {
		public string groupID;
		[Range(0, 1)]
		public float volume;
		public AudioClip[] group;
	}
}