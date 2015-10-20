using UnityEngine;
using System.Collections;

public class PatrolController : MonoBehaviour {
	
	public Transform[] transforms;
	
	public float[] moveTimes;
//	public float[] waitTime;
	
//	public bool waiting = true;
//	public bool start = true;

	int currPosition = 0;
	int nextPosition;
	float time;
	
	// Use this for initialization
	void Start () {
		nextPosition = (currPosition + 1) % transforms.Length;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= moveTimes [currPosition]) {
			time -= moveTimes [currPosition];
			currPosition = (currPosition + 1) % transforms.Length;
			nextPosition = (currPosition + 1) % transforms.Length;
		}
		
//		if (waiting && time > waitTime) {
//			waiting = false;
//			time -= waitTime;
//		} else if (!waiting && time > moveTime) {
//			waiting = true;
//			time -= moveTime;
//			start = !start;
//		}
		Move ();
	}
	
	void Move () {
		float ratio = time / moveTimes[currPosition];
		transform.position = transforms[currPosition].position + ratio * (transforms[nextPosition].position - transforms[currPosition].position);
	}
}
