using UnityEngine;
using System.Collections;

/**
 * Calls RoomController's Completed function once all the
 * switches have boulders on them. This script only works 
 * if all switches are placed within an "WallSwitches" 
 * child.
 */
public class OpenDoorsWallSwitch : MonoBehaviour
{
    bool done;
    Transform switches;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "WallSwitches")
            {
                switches = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        done = true;

        foreach (Transform child in switches)
        {
            if (!child.GetComponent<WallSwitch>().get())
            {
                done = false;
            }
        }

        // all switches are covered
        if (done)
        {
            GetComponent<RoomController>().Completed();
        }
        // not all covered
        else
        {
            GetComponent<RoomController>().Unfinished();
        }
    }
}
