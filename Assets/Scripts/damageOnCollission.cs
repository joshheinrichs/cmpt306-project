using UnityEngine;
using System.Collections;

public class damageOnCollission : MonoBehaviour {


	public int damage = 10;

	GameObject player;
	PlayerHealth playerHealth;

	public void setDamage(int dmg)
	{
		damage = dmg;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		OnCollisionStay2D(collision);
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			player = GameObject.FindGameObjectWithTag("Player");
			playerHealth = player.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(damage);
		}
		if (collision.gameObject.tag == "enemy")
		{
			Debug.Log("enemy hit");
			EnemyHealth enemy;
			enemy = collision.gameObject.GetComponent<EnemyHealth>();
			enemy.TakeDamage(damage);
			Destroy(this.gameObject);
			

		}
	}

}



