using UnityEngine;
using System.Collections;

public class _5_CoinRotation : MonoBehaviour {

	public float RotationSpeed;

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, RotationSpeed * Time.deltaTime, 0);
	}
}
