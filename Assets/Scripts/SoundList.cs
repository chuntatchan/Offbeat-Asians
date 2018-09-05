using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour {

	[SerializeField]
	private soundContainer[] list;

	public AudioClip FindSound(string name) {
		for (int i = 0; i < list.Length; i++) {
			if (list [i].name == name) {
				return list [i].sound;
			}
		}
		print ("No sound found.");
		return null;
	}

	[System.Serializable]
	public struct soundContainer
	{
		public string name;
		public AudioClip sound;
	}

}
