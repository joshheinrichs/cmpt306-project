using UnityEngine;
using System.Collections;

public class OnPlayerTouchTrigger : MonoBehaviour {

	public GameObject thisRoom; //this should be the room this key is in, used to open the doors when key is got.

	// Use this for initialization
	void Start () {
	
	}

	//when touching the key tile, tell room it is completed so that it will open doors.
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			//play a click type sound?
			thisRoom.SendMessage("GotKey", null, SendMessageOptions.RequireReceiver);
		}
	}
}
