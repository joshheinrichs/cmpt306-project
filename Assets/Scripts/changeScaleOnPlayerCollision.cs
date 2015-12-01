using UnityEngine;
using System.Collections;

public class changeScaleOnPlayerCollision : MonoBehaviour {

	private bool hasActivated = false; //used to only call makesmaller once

	public float newXScale = 75; //the new xscale to chage this object to
	public float newYScale = 75; // the new yscale to change this object to.
	public float newZScale = 1; //the new zscale to change this object to(should not be needed 2d! leave at 1)

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			this.makeSmaller ();
			this.hasActivated = true;
		}
	}

	//if it has not yet been called(hasActivated is false), will change the scale of the object to 60% of normal value
	void makeSmaller()
	{

		if (!this.hasActivated)
			transform.localScale = new Vector3 (this.newXScale, this.newYScale, this.newZScale);
	}

}
