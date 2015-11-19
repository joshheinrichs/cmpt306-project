using UnityEngine;
using System.Collections;

public class OpenDoorsInstantly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<RoomController> ().Completed ();
	}
}
