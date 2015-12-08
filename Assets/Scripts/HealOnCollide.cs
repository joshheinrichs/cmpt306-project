using UnityEngine;
using System.Collections;

public class HealOnCollide : MonoBehaviour {

	public int healAmmount = 50;
	GameObject player;
	PlayerHealth playerHealth;
	public GameObject getSound; //use for destruction sound

	// Use this for initialization
	void Start () {
	
	}

	//if player touches, heal them and destory, else nothing.
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			player = GameObject.FindGameObjectWithTag("Player");
			playerHealth = player.GetComponent<PlayerHealth>();
			playerHealth.HealDamage(healAmmount);
			GameObject powerUpSound = (GameObject)Instantiate(this.getSound,this.transform.position,Quaternion.identity);
			Destroy(this.gameObject);
		}
	}

	/**
	void OnCollisionStay2D(Collision2D collision)
	{
	}
	**/
}
