using UnityEngine;
using System.Collections;

public class damageOnCollission : MonoBehaviour {


	public int damage = 10;
	public bool damage_player = true;
	public bool damage_enemy = false;

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
		if (collision.gameObject.tag == "Player" && damage_player)
		{
			player = GameObject.FindGameObjectWithTag("Player");
			playerHealth = player.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(damage);
		} else
		{
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider);
		}
		if (collision.gameObject.tag == "enemy" && damage_enemy)
		{
			Debug.Log("enemy hit");
			EnemyHealth enemy;
			enemy = collision.gameObject.GetComponent<EnemyHealth>();
			enemy.TakeDamage(damage);
			Destroy(this.gameObject);
		} else
		{
			Debug.Log("hit else");
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.collider);
		}
	}

}



