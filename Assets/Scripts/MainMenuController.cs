using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject title;
	public GameObject MC_Image;

	void Start() 
	{
		StartCoroutine (slideIn());
	}

	IEnumerator slideIn() {

		title.transform.position = new Vector3 (title.transform.position.x - 10f, title.transform.position.y, title.transform.position.z);
		MC_Image.transform.position = new Vector3 (MC_Image.transform.position.x + 7f, MC_Image.transform.position.y, MC_Image.transform.position.z);

		yield return new WaitForEndOfFrame ();

		for (int i = 0; i < 30; i++) {
			float delta = 10f / 30f;
			title.transform.position = new Vector3 (title.transform.position.x + delta, title.transform.position.y, title.transform.position.z);
			yield return new WaitForEndOfFrame ();
		}

		for (int i = 0; i < 3; i++) {
			yield return new WaitForEndOfFrame ();
		}

		for (int i = 0; i < 30; i++) {
			float delta = 7f / 30f;
			MC_Image.transform.position = new Vector3 (MC_Image.transform.position.x - delta, MC_Image.transform.position.y, MC_Image.transform.position.z);
			yield return new WaitForEndOfFrame ();
		}

		yield return null;

	}

	public void buttonClicked(int i) {
		if (i == 0) {
			SceneManager.LoadScene ("IntroScene");
		}
	}

}
