using UnityEngine;
using System.Collections;

public class WalkToTarget : MonoBehaviour {

	public float speed = 250f;
	
	public GameObject target;
	
	Transform myTransform;
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		myTransform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		
		angle = myTransform.eulerAngles.magnitude * Mathf.Deg2Rad;
		
		Vector2 velocity;
		velocity.x = (Mathf.Cos (angle) * speed) * Time.deltaTime;
		velocity.y = (Mathf.Sin (angle) * speed) * Time.deltaTime;
		GetComponent<Rigidbody2D> ().velocity = velocity;
	}
}
