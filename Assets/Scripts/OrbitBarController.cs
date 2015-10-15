using UnityEngine;
using System.Collections;

public class OrbitBarController : MonoBehaviour {

	public GameObject orbitObject;
	public int length;
	public float orbitTime;

	GameObject[] orbiters;

	// Use this for initialization
	void Start () {
		orbiters = new GameObject[length];
		for (int i=0; i<orbiters.Length; i++) {
			GameObject orbiter = Instantiate (orbitObject);
			orbiter.AddComponent<OrbitController>();
			orbiter.GetComponent<OrbitController>().origin = transform;
			orbiter.GetComponent<OrbitController>().radius = i+1;
			orbiters[i] = orbiter;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
