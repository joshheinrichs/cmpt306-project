using UnityEngine;
using System.Collections;

/**
 * Kills the player upon collision.
 */
public class killOnCollission : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		OnCollisionStay2D (collision);
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth> ();
			player.TakeDamage(10);
//			player.Death ();
		}
	}
}
