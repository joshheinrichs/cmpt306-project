using UnityEngine;
using System.Collections;

/**
 * Destroys the GameObject to which this is attached once it has collided with
 * another GameObject.
 */
public class DestroySelfOnCollision : MonoBehaviour {

	/** The amount of delay before destroying the GameObject */
	public float delay = 0.0f;
	
	void OnCollisionEnter2D(Collision2D collision) {
		Destroy (this.gameObject, delay);
	}
}
