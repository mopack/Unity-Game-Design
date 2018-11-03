using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float Speed;
	private Rigidbody rb;
	public AudioClip soundPickUp;

	private int Count;
	public Text CountText;
	public Text WinText;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		Count = 0;
		SetCounterText ();
		WinText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//rb.AddForce (transform.forward);
		rb.AddForce(movement * Speed);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("PickUp")) {
			Count = Count + 1;
			SetCounterText ();
			AudioSource.PlayClipAtPoint (soundPickUp, transform.position);
			other.gameObject.SetActive (false);
		}
	}

	void SetCounterText(){
		CountText.text = "Count = " + Count.ToString ();
		if (Count == 5) {
			WinText.text = "You win!";
		}
	}
}
