using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeRoomStartScript : MonoBehaviour {

	public float spikeChance = 80; //%chance that spike in room is spawned(not removed) before play

	// Use this for initialization
	void Start () {
		InitializeRoom ();//called when room created, maybe call at diff time in future.
	}

	/**
	 * called to initialize/activate the room, for now just calling it when room created
	 * may be called upon entering the room in the futrue?
	 * */
	void InitializeRoom()
	{
		GameObject obs = gameObject.transform.Find ("Obstacles").gameObject;

		Transform[] childList = obs.GetComponentsInChildren<Transform> ();
		for (int i =0; i < childList.Length; i++) {
			if (childList [i].gameObject.tag == "spikes") {
				if (Random.Range (0, 100) > spikeChance)//if ran roll > spikechance destroy spike
					GameObject.Destroy (childList [i].gameObject);
			}
		}

	}

}
