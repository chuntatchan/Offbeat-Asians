using UnityEngine;
using UnityEngine.UI;

public class UI_Icon : MonoBehaviour {

	[SerializeField]
	private	string name;
	[SerializeField]
	private Sprite sprite;

	public string GetName() {
		return name;
	}

	public Sprite GetSprite() {
		return sprite;
	}

}
