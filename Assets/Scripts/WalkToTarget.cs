using UnityEngine;
using System.Collections;

/**
 * Moves the GameObject to which this is attached towards the specified
 * GameObject.
 */
public class WalkToTarget : MonoBehaviour {

	public float speed = 250f;
	
	public GameObject target;
	
	Transform myTransform;
	
	/**
	 * Initializes the WalkToTarget fields.
	 */
	void Start () {
		myTransform = transform;
	}
	
	/**
	 * Moves this GameObject towards the specified target at the given speed.
	 * This GameObject will rotate to face its target as it moves towards it.
	 */
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
