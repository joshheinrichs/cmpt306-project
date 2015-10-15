using UnityEngine;
using System.Collections;

public class OrbitController : MonoBehaviour {

	public Transform origin;
	public float radius = 100.0f;
	public float angle = 0.0f;	
	public float orbitTime = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float radians = angle * (Mathf.PI / 180.0f); // Converting Degrees To Radians
		Vector2 position;
		position.x = origin.position.x + radius * Mathf.Cos(radians);
		position.y = origin.position.y + radius * Mathf.Sin (radians);
		transform.position = position; // Position The Orbiter Along x-axis

		angle += 360.0f / orbitTime * Time.deltaTime;
	}
}
