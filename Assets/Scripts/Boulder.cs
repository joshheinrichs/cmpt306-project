using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

    public Vector2 startpos;  // starting position of the boulder
    
    // Use this for initialization
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Vector2 startingpos()
    {
        return startpos;
    }


}
