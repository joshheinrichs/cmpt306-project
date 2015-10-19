using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float maxSpeed = 5f;
	public Transform heatTarget = null; // fill this in if youd like head seaking bullets

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update() {
		if (heatTarget == null)
		{
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
			pos += transform.rotation * Quaternion.Euler(0, 0, 0) * velocity;
			transform.position = pos;
		}
		else
		{
			// code for heatseeking, do this later
			Debug.Log("Heat seaking not yet implemented");
		}
	}
}
