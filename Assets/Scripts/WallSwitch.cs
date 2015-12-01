using UnityEngine;
using System.Collections;

public class WallSwitch : MonoBehaviour {

    public bool pressed;
    
    // Use this for initialization
    void Start()
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            pressed = true;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
