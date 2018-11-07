using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class _1_GameControl : MonoBehaviour {

	public static bool GameOverBool;
	public static int CoinNumber;
	public GameObject CoinNumberText;

	public GameObject[] Map;
	int MapDistance;

	public GameObject Cowboy;
	public GameObject RunDistaneText;
	public GameObject MessageMaskUI, PlayStopButton;
	public Sprite UI_02, UI_03;
	int Score;
	public GameObject EndMaskUI, EndCoinNumberText, EndRunDistanceText, EndScoreText;

	// Use this for initialization
	void Start () {
		GameOverBool = false;
		CoinNumber = 0;
		MapDistance = 0;

		MessageMaskUI.SetActive (false);
		EndMaskUI.SetActive (false);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		UguiShow ();
		ProduceMap ();
		if (GameOverBool == true) {
			print ("GameOver");
			EndMaskUI.SetActive (true);
			if (Score > PlayerPrefs.GetInt ("Score")) {
				PlayerPrefs.SetInt ("Score", Score);
			}
		}
	}

	void UguiShow(){
		CoinNumberText.GetComponent<Text> ().text = "" + CoinNumber;

		EndRunDistanceText.GetComponent<Text> ().text = "" + Mathf.FloorToInt (Cowboy.transform.position.z);
		EndCoinNumberText.GetComponent<Text> ().text = "" + CoinNumber;
		EndRunDistanceText.GetComponent<Text> ().text = "" + Mathf.FloorToInt (Cowboy.transform.position.z); 
		Score = CoinNumber * 10 + Mathf.FloorToInt (Cowboy.transform.position.z);
		EndScoreText.GetComponent<Text> ().text = "" + Score;
	}

	void ProduceMap(){
		GameObject FindMap = GameObject.FindGameObjectWithTag ("Map");
		if (FindMap == null) {
			MapDistance += 16;
			int RandomMap = Random.Range (0, Map.Length);
			Instantiate (Map [RandomMap], new Vector3 (0, 0, MapDistance), 
				Quaternion.identity);
		}
	}

	public void PlayStop(){
		if (PlayStopButton.GetComponent<Image> ().sprite.name == "UI_02") { //Start->Stop
			PlayStopButton.GetComponent<Image> ().sprite = UI_03;
			MessageMaskUI.SetActive (true);

			_9_CowboyControlMK.StartReciprocalTime = 3;
			//_8_CowboyControlMouse.StartReciprocalTime = 3; 
			//_4_CowboyControlPC.StartReciprocalTime = 3;

			Time.timeScale = 0;
		} else { // UI_03 Stop->Start
			PlayStopButton.GetComponent<Image>().sprite = UI_02;
			MessageMaskUI.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void MenuButton(){
		SceneManager.LoadScene ("Menu");
		//Application.LoadLevel ("Menu");
	}

	public void MessagePlayButton(){

		MessageMaskUI.SetActive (false);
		PlayStopButton.GetComponent<Image> ().sprite = UI_02;
		Time.timeScale = 1;
	}

	public void EndAgainButton(){
		
		SceneManager.LoadScene ("MainScene");
		//Application.LoadLevel ("MainScene");
	}
}
