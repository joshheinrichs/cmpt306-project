using UnityEngine;
using System.Collections;

// puts boulders(and only boulders) back to their starting location
public class ResetSwitch : MonoBehaviour
{
    public bool reset;
    Transform boulders; // the boulders in the room
        
    // Use this for initialization
    void Start()
    {
        reset = false;

        // get boulders
        foreach (Transform child in transform.parent)
        {
            if (child.name == "Boulders")
            {
                boulders = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(reset)
        {
            // reset each boulder
            foreach (Transform b in boulders)
            {
                b.position = b.GetComponent<Boulder>().startingpos();
            }

            reset = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            reset = true;
        }
    }
}    
