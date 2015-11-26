using UnityEngine;
using System.Collections;

public class HealOnCollide : MonoBehaviour {

	public int healAmmount = 50;
	GameObject player;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
			playerHealth.HealDamage(healAmmount);
		}
		Destroy(this.gameObject);

	}
}
