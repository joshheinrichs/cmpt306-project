using UnityEngine;
using System.Collections;

public class ShootForward : MonoBehaviour {
	 
	public GameObject projectile;
	public Transform spawn;
	public float cooldown = 1f;
	public float timer = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetButton("Fire1") && timer >= cooldown)
		{
			timer = 0f;
			GameObject bullet = Instantiate (projectile);
			bullet.transform.position = spawn.position;
			bullet.transform.rotation = transform.rotation;
			bullet.transform.Rotate (new Vector3(0, 0, -90));
			bullet.GetComponent<Rigidbody2D>().AddRelativeForce (new Vector3 (0, 1000));
			
//			Vector3 offset = transform.rotation * bulletOffset;
//			GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation * Quaternion.Euler(0,0,90));
//			bulletGO.layer = bulletLayer;
//			GameObject smoke = (GameObject)Instantiate(fireAnimation, transform.position + offset, transform.rotation * Quaternion.Euler(0, 0, 90));
//			
//			
//			AudioSource audio = GetComponent<AudioSource>();
//			audio.PlayOneShot(shotFired);
//			
//			Destroy(smoke, 0.9f); // get rid of smoke animation
//			Destroy(bulletGO, 10); // destroy bullet after it falls off the screen
//			cooldownTimer = 1;
		}
	}
}
