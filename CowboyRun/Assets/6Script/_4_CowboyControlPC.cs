using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _4_CowboyControlPC : MonoBehaviour {

	bool Challenge;
	public float CowboyRunSpeed;
	bool OnFloor;
	public float JumpHight;
	int CowboyRunway; //-1,0,1
	public float ChangingRunwaySpeed;
	public GameObject JumpSound, RunSmoke;

	public GameObject CoinsEffects, GetCoinSound, HitSound;
	public AudioClip PickCoinSound;

	public static float StartReciprocalTime;
	public GameObject StartRecipreocalTimeText;


	// Use this for initialization
	void Start () {
		Challenge = true;
		transform.position = new Vector3 (0, 0.25F, 0);
		CowboyRunway = 0;

		StartReciprocalTime = 3;
		SetEmission (RunSmoke, false); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Challenge == true) {
			if (StartReciprocalTime > 0) {
				StartReciprocalTime -= Time.deltaTime;
				StartRecipreocalTimeText.GetComponent<Text> ().text = "" + Mathf.CeilToInt (StartReciprocalTime);
			} else {
				StartRecipreocalTimeText.GetComponent<Text> ().text = "";
				ChallengeStart ();
			}
		}
	}

	void SetEmission(GameObject gameobject, bool SettingBool){
		ParticleSystem ps = gameobject.GetComponent<ParticleSystem> ();
		var em = ps.emission;
		em.enabled = SettingBool;
	}

	void ChallengeStart(){
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (0, 0, CowboyRunSpeed * Time.deltaTime);
			GetComponent<_2_CowboyAnim> ().BoolRun = true;
			JumpMove ();
			LeftRight ();
			if (transform.position.x != CowboyRunway) {
				CheckCowboyPosX ();
			}


			if (OnFloor == true) {
				SetEmission (RunSmoke, true); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = true;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
			} else { 
				SetEmission (RunSmoke, false); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;
			}
		} else {
			GetComponent< _2_CowboyAnim> ().BoolRun = false;
			SetEmission (RunSmoke, false); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;

		}
	}

	void JumpMove(){
		if( Input.GetKeyDown( KeyCode.Space) && OnFloor == true){
			GetComponent<_2_CowboyAnim> ().BoolJump = true;
			GetComponent<Rigidbody> ().AddForce (0, JumpHight, 0);
			OnFloor = false;
			Instantiate (JumpSound, transform.position, Quaternion.identity);
		}
	}

	void LeftRight(){
		if (Input.GetKeyDown (KeyCode.A) && CowboyRunway != -1) {
			GetComponent<_2_CowboyAnim> ().BoolJumpLeft = true;
			CowboyRunway--;
			Instantiate (JumpSound, transform.position, Quaternion.identity);
		}

		if (Input.GetKeyDown (KeyCode.D) && CowboyRunway != 1) {
			GetComponent<_2_CowboyAnim> ().BoolJumpRight = true;
			CowboyRunway++;
			Instantiate (JumpSound, transform.position, Quaternion.identity);
		}
	}

	void CheckCowboyPosX(){
		float ChangingRunway = Mathf.Lerp (transform.position.x, CowboyRunway, ChangingRunwaySpeed * Time.deltaTime);
		transform.position = new Vector3 (ChangingRunway, transform.position.y, transform.position.z);
	}

	void OnCollisionEnter(Collision CowboyCollision){
		if (CowboyCollision.collider.tag == "Floor") {
			GetComponent<_2_CowboyAnim> ().BoolJump = false;
			OnFloor = true;
		}

		if (CowboyCollision.collider.tag == "Wall") {
			GetComponent<_2_CowboyAnim> ().BoolOver = true;
			Challenge = false;
			_1_GameControl.GameOverBool = true;
			Instantiate (HitSound, transform.position, Quaternion.identity);
			SetEmission (RunSmoke, false); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;
		}

		if (CowboyCollision.collider.tag == "Coin") {
			_1_GameControl.CoinNumber++;
			Destroy (CowboyCollision.gameObject);
			Instantiate (GetCoinSound, transform.position, Quaternion.identity); //AudioSource.PlayClipAtPoint (PickCoinSound, transform.position);
			Instantiate (CoinsEffects, transform.position + new Vector3(0F, 1F, 2F), Quaternion.identity);
		}
	}

}
