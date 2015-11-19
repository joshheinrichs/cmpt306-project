using UnityEngine;
using System.Collections;

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
