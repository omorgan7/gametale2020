//v basic script that moves player as you would expect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{

    /* CONTROLS
        left arrow: Input.GetAxis("Horizontal") < 0
        right arrow: Input.GetAxis("Horizontal") > 0
        up arrow: Input.GetAxis("Vertical") > 0
        down arrow: Input.GetAxis("Vertical") < 0 
        space bar: Input.GetKeyDown("space") == true
        w: Input.GetKeyDown("w") == true
        a: Input.GetKeyDown("a") == true
        s: Input.GetKeyDown("s") == true
        d: Input.GetKeyDown("d") == true
    */

    public float speed = 1.0f;
    
    void Start()    {
        
    }

    // Update is called once per frame
    void Update(){         
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
        
        if(Input.anyKeyDown){ //prints whichever key has been pushed - not good for space/arrows 
            print(Input.inputString);
        }
    }

    void OnBecameInvisible(){ //tests when out of camera view (i.e lost game, or do we just want if it goes to left?)
        print("Out of frame...");
    }
}
