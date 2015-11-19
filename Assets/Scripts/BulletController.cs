using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public int damage = 10;

	void OnCollisionEnter2D(Collision2D col) {
		EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth> ();
		if (enemyHealth != null) {
			print ("zombie collided with bullet");
		 	enemyHealth.TakeDamage (damage);
		}
		Destroy (gameObject);
	}
}
