using UnityEngine;
using System.Collections;

public class MoveLeftRight : MonoBehaviour {

	private float xSpeed;
	public float moveSpeed = 2.0f;
	float startX;
	public float range = 5.0f;
	
	// Use this for initialization
	void Start () {
		
		startX = transform.position.x;
		xSpeed = -moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (startX - transform.position.x > range) {
			xSpeed = moveSpeed;
		} else if (startX - transform.position.x < -range) {
			xSpeed = -moveSpeed;
		}
		transform.Translate (xSpeed * Time.deltaTime, 0, 0);
		
	}
}
