using UnityEngine;
using System.Collections;

public class MultiOrbitController : MonoBehaviour {

	public GameObject orbitObject;
	public int numObjects = 3;
	public float orbitTime = 2.0f;
	
	GameObject[] orbiters;
	
	// Use this for initialization
	void Start () {
		orbiters = new GameObject[numObjects];
		for (int i=0; i<orbiters.Length; i++) {
			GameObject orbiter = Instantiate (orbitObject);
			orbiter.AddComponent<OrbitController>();
			OrbitController orbitController = orbiter.GetComponent<OrbitController>();
			orbitController.origin = transform;
			orbitController.radius = 5;
			orbitController.orbitTime = orbitTime;
			orbitController.angle = i * 360.0f / orbiters.Length;
			orbiters[i] = orbiter;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
