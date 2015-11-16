using UnityEngine;
using System.Collections;

public class MoveUpDown : MonoBehaviour {

	private float ySpeed;
	public float moveSpeed = 8.0f;
	float startY;
	public float range = 3.0f;
	public bool centered = true; //default way it works, goes back and forth from center based on range, false goes a 
								//direction based of godown bool and comes back based on range
	public bool goDown = true; //only used if centered is false
	private float endY; // used if centered is false, is range + startY goes from startY to EndY
	
	// Use this for initialization
	void Start () {
		
		startY = transform.position.y;
		endY = transform.position.y + range;
		ySpeed = -moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (centered)
			CenteredMove ();
		else
			NonCenteredMove ();

	}

	/**
	 * used if centered is False
	 * moves between startY and !Either! range, OR -range, but not both
	 * */
	void NonCenteredMove()
	{
		//if endpoint is below start point
		if (endY < startY) {
			if (transform.position.y < endY)
				goDown = false;
			else if (transform.position.y > startY)
				goDown = true;
		}
		//else endpoint above start point
		else {
			if (transform.position.y > endY)
				goDown = true;
			else if (transform.position.y < startY)
				goDown = false;
		}

		if (goDown)
			ySpeed = -moveSpeed;
		else
			ySpeed = moveSpeed;
		transform.Translate (0, ySpeed * Time.deltaTime, 0);
	}

	/**
	 * used if centered is TRUE
	 * the original moveupdown scrip is in this fucntion.
	 * starts at startY, moves to range, then from there to -range
	 * */
	void CenteredMove()
	{
		if (startY - transform.position.y > range) {
			ySpeed = moveSpeed;
		} else if (startY - transform.position.y < -range) {
			ySpeed = -moveSpeed;
		}
		transform.Translate (0, ySpeed * Time.deltaTime, 0);
	}


}