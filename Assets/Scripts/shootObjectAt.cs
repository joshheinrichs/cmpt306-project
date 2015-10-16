using UnityEngine;
using System.Collections;

public class shootObjectAt : MonoBehaviour {

	public GameObject projectile;
	int projectileLayer;
	public Vector3 projectileOffset = new Vector3 (0f, 0f, 0);
	public Transform target;

	public float expireTime = 2.0f;
	public float speed = 3.0f;
	public float cooldownTimer = 0;
	public float fireDelay = 0.25f; // maybe this could be randomized over a range of time in the future?

	
	GameObject[] orbiters;
	
	// Use this for initialization
	void Start () {
		projectileLayer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		if (cooldownTimer <= 0) {
			Vector3 offset = transform.rotation * projectileOffset;
			GameObject projectileGo = (GameObject)Instantiate (projectile, transform.position + offset +(target.position - transform.position).normalized, 
			                                                   Quaternion.LookRotation(target.position - transform.position));
			projectileGo.layer = projectileLayer;
			Destroy (projectileGo,expireTime);
			cooldownTimer = 1;
		}

	}
}
