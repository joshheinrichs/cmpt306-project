using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		Camera camera = Camera.main;
		float cameraDistance = camera.transform.position.y - transform.position.y;

		Vector3 mouse = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, 
		                                                        Input.mousePosition.y, 
		                                                        cameraDistance));
		float angle = Mathf.Atan2 (mouse.y - transform.position.y, 
		                           mouse.x - transform.position.x);
		this.transform.rotation = Quaternion.Euler (0, 0, angle * Mathf.Rad2Deg);
	}
}
