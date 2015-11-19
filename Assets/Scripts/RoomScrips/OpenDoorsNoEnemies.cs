using UnityEngine;
using System.Collections;

/**
 * Calls RoomController's Completed function once there are
 * no remaining enemies within the room. This script only
 * works if all enemies are placed within an "Enemies" child.
 */
public class OpenDoorsNoEnemies : MonoBehaviour {

	Transform enemies;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			if (child.name == "Enemies") {
				enemies = child;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enemies.childCount == 0) {
			GetComponent<RoomController>().Completed ();
		}
	}
}
