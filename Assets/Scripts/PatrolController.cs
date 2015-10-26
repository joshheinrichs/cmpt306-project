using UnityEngine;
using System.Collections;

/**
 * The PatrolController controls the movement of a GameObject, moving it
 * between the specified positions at the given times. The transforms and
 * moveTimes should be parallel arrays. This does not yet support rotation.
 */
public class PatrolController : MonoBehaviour {
	
	/** positions to move to */
	public Transform[] transforms;

	/** time to move from i to i+1 */
	public float[] moveTimes;

	int currPosition = 0;
	int nextPosition;
	float time;
	
	// Use this for initialization
	void Start () {
		nextPosition = (currPosition + 1) % transforms.Length;
	}
	
	/** Updates the target position, and calls to Move */
	void Update () {
		time += Time.deltaTime;
		if (time >= moveTimes [currPosition]) {
			time -= moveTimes [currPosition];
			currPosition = (currPosition + 1) % transforms.Length;
			nextPosition = (currPosition + 1) % transforms.Length;
		}
		Move ();
	}
	
	/**
	 * Moves this GameObject from the current position towards the next position.
	 */
	void Move () {
		float ratio = time / moveTimes[currPosition];
		transform.position = transforms[currPosition].position + ratio * (transforms[nextPosition].position - transforms[currPosition].position);
	}
}
