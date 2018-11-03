using UnityEngine;
using System.Collections;

public class _3_DestroyMe : MonoBehaviour {

	public float DestroyTime;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, DestroyTime);
	}
}
