using UnityEngine;
using System.Collections;

public class MoveLeftRight : MonoBehaviour {

	private float xSpeed;
	public float moveSpeed = 2.0f;
	float startX;
	public float range = 5.0f;
	public bool centered = true;
	public bool goRight = true; //only used if centered is false, if true, moves between startX and startX + range, otherwise startX - range
	private float endX;	//used if centered is false
	private bool toEnd = true; //used if centered is false, is it currently traveling to endX? otherwise going to startX
	
	// Use this for initialization
	void Start () {
		
		startX = transform.position.x;
		endX = transform.position.x + range;
		xSpeed = -moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (centered)
			centeredMove ();
		else
			NonCenteredMove ();
		
	}

	/**
	 * used if centered is FALSE
	 * moves between startX and !Either! range, OR -range, but not both
	 * */
	void NonCenteredMove()
	{
		//if endpoint left of start point
		if (endX < startX) {
			if (transform.position.x < endX)
				goRight = true;
			else if (transform.position.x > startX)
				goRight = false;
		} 
		//else endpoint to right of start point
		else {
			if (transform.position.x > endX)
				goRight = false;
			else if(transform.position.x < startX)
				goRight = true;
		}
		if (goRight)
			xSpeed = moveSpeed;
		else
			xSpeed = -moveSpeed;
		transform.Translate (xSpeed * Time.deltaTime, 0, 0);

	}

	/**
	 * used if centered is TRUE
	 * the orignal moveleftright script is in this function.
	 * starts at startX, moves right to range, then left to -range.
	 * */
	void centeredMove()
	{
		if (startX - transform.position.x > range) {
			xSpeed = moveSpeed;
		} else if (startX - transform.position.x < -range) {
			xSpeed = -moveSpeed;
		}
		transform.Translate (xSpeed * Time.deltaTime, 0, 0);
	}
}
