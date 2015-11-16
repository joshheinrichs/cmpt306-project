﻿using UnityEngine;
using System.Collections;

public class damageOnCollission : MonoBehaviour {


	public int damage = 0;

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
		print("test");
		if (collision.gameObject.tag == "Player")
		{
			player = GameObject.FindGameObjectWithTag("Player");
			playerHealth = player.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(damage);
		}
	}

}


