using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform spawnPoint;
	public float speed = 500.0f;

	Rigidbody2D body;
	SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		transform.position = spawnPoint.position;
		body = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
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
