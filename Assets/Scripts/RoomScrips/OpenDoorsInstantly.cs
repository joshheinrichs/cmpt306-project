using UnityEngine;
using System.Collections;

/**
 * Instantly calls RoomController's Completed method.
 * This is useful for rooms that do not have an object,
 * other than simply traversing it.
 */
public class OpenDoorsInstantly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<RoomController> ().Completed ();
	}
}
