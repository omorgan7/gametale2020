//v basic script that moves player as you would expect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    private char [] map = {'w', 'a', 's', 'd', ' '}; //

    /* CONTROLS
        left arrow: Input.GetAxis("Horizontal") < 0
        right arrow: Input.GetAxis("Horizontal") > 0
        up arrow: Input.GetAxis("Vertical") > 0

        Babove works for wasd
        down arrow: Input.GetAxis("Vertical") < 0 
        space bar: Input.GetKeyDown("space") == true OR Input.inputString == " "
        w: Input.GetKeyDown("w") == true
        a: Input.GetKeyDown("a") == true
        s: Input.GetKeyDown("s") == true
        d: Input.GetKeyDown("d") == true

        Input.inputString: gets key pressed
    */

    public float speed = 1.0f;
    private Rigidbody2D rigidBody ;
    private float jumpPower = 200.0f;
    private Vector2 jumpDirection = Vector2.up;
    
    void Start(){
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        print(rigidBody == null);    
        
    }

    // Update is called once per frame
    void Update(){    




        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * speed * Time.deltaTime;
        
        // if(Input.GetKeyDown("space") == true){ //jump - still can fly off
        //     rigidBody.AddForce(Vector2.up * jumpPower);
        // }

        // if(Input.GetAxis("Horizontal") != 0){
        //     print(Input.GetAxis("Horizontal"));
        // }
    }

    void OnBecameInvisible(){ //tests when out of camera view (i.e lost game, or do we just want if it goes to left?)
        print("Out of frame...");
    }
}
