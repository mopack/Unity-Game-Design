using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _8_CowboyControlMouse : MonoBehaviour {

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

	Vector2 MouseDownPos, MouseUpPos;
	float HorizontalDistance, VerticalDistance;

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

				//5-5
				SetEmission (RunSmoke, false);
				GetComponent<_2_CowboyAnim> ().BoolRun = false;
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
		transform.Translate (0, 0, CowboyRunSpeed * Time.deltaTime);
		GetComponent<_2_CowboyAnim> ().BoolRun = true;

		ControlMouse (); //JumpMove (); //LeftRight ();

		if (transform.position.x != CowboyRunway) {
			CheckCowboyPosX ();
		}


		if (OnFloor == true) {
			SetEmission (RunSmoke, true); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = true;

		} else { 
			SetEmission (RunSmoke, false); //RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;
		}
	}

	void ControlMouse(){
		if (Input.GetMouseButtonDown (0)) {
			MouseDownPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			MouseUpPos = Input.mousePosition;
			DirectionChoose ();
		}
	}

	void DirectionChoose(){
		HorizontalDistance = MouseUpPos.y - MouseDownPos.y;
		VerticalDistance = MouseUpPos.x - MouseDownPos.x;
		if (Mathf.Abs (HorizontalDistance) > Mathf.Abs (VerticalDistance)) {
			JumpMove ();
		} else {
			LeftRight ();
		}
	}

	void JumpMove(){
		if( HorizontalDistance > 0 && OnFloor == true){
			GetComponent<_2_CowboyAnim> ().BoolJump = true;
			GetComponent<Rigidbody> ().AddForce (0, JumpHight, 0);
			OnFloor = false;
			Instantiate (JumpSound, transform.position, Quaternion.identity);
		}
	}

	void LeftRight(){
		if (VerticalDistance < 0 && CowboyRunway > -1) {
			GetComponent<_2_CowboyAnim> ().BoolJumpLeft = true;
			CowboyRunway--;
			Instantiate (JumpSound, transform.position, Quaternion.identity);
		}

		if (VerticalDistance > 0 && CowboyRunway < 1) {
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
			Instantiate (GetCoinSound, transform.position, Quaternion.identity);
			//AudioSource.PlayClipAtPoint (PickCoinSound, transform.position);
			Instantiate (CoinsEffects, transform.position + new Vector3(0F, 1F, 2F), Quaternion.identity);
		}
	}

}
