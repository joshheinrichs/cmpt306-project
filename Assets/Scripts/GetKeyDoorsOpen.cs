using UnityEngine;
using System.Collections;

public class GetKeyDoorsOpen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	//called by gameobject which is needed to open doors
	public void GotKey()
	{
		GetComponent<RoomController> ().Completed ();
	}
}
