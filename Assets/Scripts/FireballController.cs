using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		print ("test");
		if (collision.gameObject.tag == "Player") {
			PlayerController player = collision.gameObject.GetComponent<PlayerController> ();
			player.Kill ();
		}
	}
}
