using UnityEngine;
using System.Collections;

public class MoveUpDown : MonoBehaviour {

	private float ySpeed;
	public float moveSpeed = 8.0f;
	float startY;
	public float range = 3.0f;
	
	// Use this for initialization
	void Start () {
		
		startY = transform.position.y;
		ySpeed = -moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (startY - transform.position.y > range) {
			ySpeed = moveSpeed;
		} else if (startY - transform.position.y < -range) {
			ySpeed = -moveSpeed;
		}
		transform.Translate (0, ySpeed * Time.deltaTime, 0);
		
	}
}
