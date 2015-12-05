using UnityEngine;
using System.Collections;

/**
 * Spawns a bar of GameObjects which orbit around this GameObject's position
 * at increasing distance from the origin.
 */
public class OrbitBarController : MonoBehaviour {

	public GameObject orbitObject;
	public int length = 3;
	public float orbitTime = 2.0f;
	public float angle = 0.0f;
	public bool clockwise = true;

	GameObject[] orbiters;

	/**
	 * Spawns the GameObjects from the given prefab and sets them to orbit
	 * around this GameObject.
	 */
	void Start () {
		orbiters = new GameObject[length];
		for (int i=0; i<orbiters.Length; i++) {
			GameObject orbiter = Instantiate (orbitObject);
			orbiter.AddComponent<OrbitController>();
			orbiter.AddComponent<DestroySelfOnCollision>();
			OrbitController orbitController = orbiter.GetComponent<OrbitController>();
			orbitController.origin = transform;
			orbitController.radius = i+1;
			orbitController.orbitTime = orbitTime;
			orbitController.angle = angle;
			orbitController.clockwise = clockwise;
			orbiters[i] = orbiter;
		}
	}
}
