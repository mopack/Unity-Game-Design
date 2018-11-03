using UnityEngine;
using System.Collections;

public class _6_MapCollisionDetection : MonoBehaviour {
	void OnTriggerEnter( Collider CollisionDetection){
		if (CollisionDetection.tag == "Map") {
			CollisionDetection.tag = "RunMap";
		}
	}

	void OnTriggerExit(Collider CollisionDetection){
		if (CollisionDetection.tag == "RunMap") {
			Destroy (CollisionDetection.gameObject);
		}
	}
}
