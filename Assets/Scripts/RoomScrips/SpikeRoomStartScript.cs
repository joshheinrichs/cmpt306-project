using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Randomly deletes some spikes within the spike room.
 */
public class SpikeRoomStartScript : MonoBehaviour {

	/** %chance that spike in room is spawned(not removed) before play */
	public float spikeChance = 80; 

	// Use this for initialization
	void Start () {
		//called when room created, maybe call at diff time in future.
		InitializeRoom ();
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
