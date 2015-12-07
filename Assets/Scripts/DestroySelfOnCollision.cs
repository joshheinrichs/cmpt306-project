using UnityEngine;
using System.Collections;

/**
 * Destroys the GameObject to which this is attached once it has collided with
 * another GameObject.
 */
public class DestroySelfOnCollision : MonoBehaviour {

	public bool destoryOnlyOnPlayerCollision = false;

	/** The amount of delay before destroying the GameObject */
	public float delay = 0.0f;
	
	void OnCollisionEnter2D(Collision2D collision) {

		if(destoryOnlyOnPlayerCollision && collision.gameObject.tag == "Player")
			Destroy (this.gameObject, delay);
		else if(!destoryOnlyOnPlayerCollision)
			Destroy (this.gameObject, delay);
	}
}
