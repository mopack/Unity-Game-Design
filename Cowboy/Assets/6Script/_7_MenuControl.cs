using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class _7_MenuControl : MonoBehaviour {

	public GameObject MenuScoreText;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("Score") == false) {
			PlayerPrefs.SetInt ("Score", 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		MenuScoreText.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("Score");
	}

	public void MenuStartButtom(){
		SceneManager.LoadScene ("MainScene");
		//Application.LoadLevel ("MainScene");
	}

	public void MenuExitButton(){
		Application.Quit ();
	}
}
