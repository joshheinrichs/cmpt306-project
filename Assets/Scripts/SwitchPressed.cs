using UnityEngine;
using System.Collections;

public class SwitchPressed : MonoBehaviour {

    public bool pressed;
    
	// Use this for initialization
	void Start ()
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // is there a boulder on the switch?
    public bool get()
    {
        return pressed;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody.tag == "boulder")
            pressed = true;
        else
            pressed = false;    
    }
}
