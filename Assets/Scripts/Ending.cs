using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ending : MonoBehaviour {

	[SerializeField]
	private DevProfile[] devs;

	[Header("UI")]
	[SerializeField]
	private Image picture;

	[SerializeField]
	private int counter = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			Application.Quit ();
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			leftArrow ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			rightArrow ();
		}
	}

	public void leftArrow() {
		if (counter == 0) {
			counter = devs.Length - 1;
		} else {
			counter = (counter - 1) % devs.Length;
		}
		changeUI (counter);
	}

	public void rightArrow() {
		counter = (counter + 1) % devs.Length;
		changeUI (counter);
	}

	private void changeUI (int i) {
		for (int j = 0; j < devs.Length; j++) {
			if (j == i) {
				devs [j].name.gameObject.SetActive (true);
				devs [j].description.gameObject.SetActive (true);
				picture.sprite = devs [j].image;
			} else {
				devs [j].name.gameObject.SetActive (false);
				devs [j].description.gameObject.SetActive (false);
			}
		}
	}

}

[System.Serializable]
public class DevProfile {

	public TMP_Text name, description;
	public Sprite image;

}
