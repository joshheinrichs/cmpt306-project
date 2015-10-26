using UnityEngine;
using System.Collections;

/**
 * Basic controls for the player.
 */
public class PlayerController : MonoBehaviour {

	public Transform spawnPoint;
	public float speed = 500.0f;

	Rigidbody2D body;
	SpriteRenderer sprite;
	
	/**
	 * Initializes the required fields for the PlayerController.
	 */
	void Start () {
		transform.position = spawnPoint.position;
		body = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	/**
	 * Checks to see which buttons are pressed and updates the Player's velocity
	 * accordingly.
	 */
	void Update () {
		Vector2 velocity = Vector2.zero;
		if (Input.GetKey (KeyCode.UpArrow)) {
			velocity.y += speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			velocity.y -= speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			velocity.x += speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			velocity.x -= speed * Time.deltaTime;
		}
		body.velocity = velocity;
//		sprite.sortingOrder = (int) -transform.position.y;
	}

	public void Kill () {
		transform.position = spawnPoint.position;
	}
}
