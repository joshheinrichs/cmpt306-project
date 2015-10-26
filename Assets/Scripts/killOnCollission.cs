﻿using UnityEngine;
using System.Collections;

/**
 * Kills the player upon collision.
 */
public class killOnCollission : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		print ("test");
		if (collision.gameObject.tag == "Player") {
			PlayerController player = collision.gameObject.GetComponent<PlayerController> ();
			player.Kill ();
		}
	}
}
