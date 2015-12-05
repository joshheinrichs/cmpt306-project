using UnityEngine;
using System.Collections;

/**
 * Spawns a circle of equally spaced GameObjects around the attached GameObject,
 * which orbit at the specified speed.
 */
public class MultiOrbitController : MonoBehaviour {

	public GameObject orbitObject;
	public int numObjects = 3;
	public float orbitTime = 2f;
	public float radius = 2f;

	public bool expire = false;
	public float expire_time = 5f;
	
	GameObject[] orbiters;
	
	/**
	 * Spawns the GameObjects from the given prefab and sets them to orbit
	 * around this GameObject.
	 */
	void Start () {
		orbiters = new GameObject[numObjects];
		for (int i=0; i<orbiters.Length; i++) {
			GameObject orbiter = Instantiate (orbitObject);
			orbiter.AddComponent<OrbitController>();
			orbiter.AddComponent<DestroySelfOnCollision>();
			OrbitController orbitController = orbiter.GetComponent<OrbitController>();
			orbitController.origin = transform;
			orbitController.radius = radius;
			orbitController.orbitTime = orbitTime;
			orbitController.angle = i * 360.0f / orbiters.Length;
			orbiters[i] = orbiter;
		}
		if (expire)
		{
			for (int i = 0; i < orbiters.Length; i++)
				Destroy(orbiters[i],expire_time);
		}
    }
}
