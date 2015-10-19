using UnityEngine;
using System.Collections;

public class DestroySelfOnCollision : MonoBehaviour {

	public float delay = 0.0f;
	
	void OnCollisionEnter2D(Collision2D collision) {
		Destroy (gameObject, delay);
	}
}
