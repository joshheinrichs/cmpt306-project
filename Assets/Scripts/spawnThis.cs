using UnityEngine;
using System.Collections;

public class spawnThis : MonoBehaviour {


	public GameObject spawnthis;
	// Use this for initialization
	void Start () {
		GameObject spawnObject = (GameObject)Instantiate(this.spawnthis, gameObject.transform.position,Quaternion.identity  );
		spawnObject.transform.parent = gameObject.transform.parent;
		this.Removethis ();
	}

	void Removethis(){
		Destroy (gameObject);
	}
	
	// Update is called once per frame

}
