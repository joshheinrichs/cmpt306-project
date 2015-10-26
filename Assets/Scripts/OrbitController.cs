using UnityEngine;
using System.Collections;

/**
 * An OrbitController controls the orbit of an object around a point of origin.
 */
public class OrbitController : MonoBehaviour {

	public Transform origin;
	public float radius = 100.0f;
	public float angle = 0.0f;	
	public float orbitTime = 1.0f;
	public bool clockwise = false;
	
	/**
	 * Handles the rotation of the object around the origin every frame.
	 */
	void Update () {
		float radians = angle * (Mathf.PI / 180.0f); // Converting Degrees To Radians
		Vector2 position;
		position.x = origin.position.x + radius * Mathf.Cos(radians);
		position.y = origin.position.y + radius * Mathf.Sin (radians);
		transform.position = position; // Position The Orbiter Along x-axis

		if (clockwise) {
			angle += 360.0f / orbitTime * Time.deltaTime;
		} else {
			angle -= 360.0f / orbitTime * Time.deltaTime;
		}
	}
}
