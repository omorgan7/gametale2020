//v basic script that moves player as you would expect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    private KeyCode [] map = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}; 

    private BoxCollider2D myCollider;

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
    public float jumpPower = 10.0f, jumpBoost = 5.0f, fall = 30.0f;
    private int jumps = 0;
    private Vector2 jumpDirection = Vector2.up;
    private bool inAir = false, moveLeft = false, moveRight = false;
    private Vector3 move = new Vector3(0,0,0);
    
    public GameObject insideOf;

    void Start(){
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        print(rigidBody == null);    
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update(){    


        // var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        // transform.position += move * speed * Time.deltaTime;
        
        if((Input.GetKeyDown(map[0]) == true)&(rigidBody.velocity.y >= -0.001) & (jumps < 2)){ //jump 
            if(jumps == 0){
                rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else{
                rigidBody.AddForce(Vector2.up * jumpBoost, ForceMode2D.Impulse);
            }
            jumps++;
        }

        if((jumps > 0)&(rigidBody.velocity.y <= 0.001)){ //reset jump
            jumps = 0;
        }


        checkHit();

        if((Input.GetKeyDown(map[2]) == true)){
            rigidBody.AddForce(Vector2.down * fall, ForceMode2D.Impulse);
        }

        if(Input.GetKeyDown(map[1]) == true){
            moveLeft = true;
            // var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            // transform.position += move * speed * Time.deltaTime;
          
        }
         if(Input.GetKeyUp(map[1]) == true){
            moveLeft = false;
            // var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            // transform.position += move * speed * Time.deltaTime;
        }

        if(moveLeft){
            if((map[1] == KeyCode.A)  | (map[1] == KeyCode.D)){
                move = new Vector3(Mathf.Abs(Input.GetAxis("Horizontal")), 0, 0);
            }
            else{
                move = new Vector3(Mathf.Abs(Input.GetAxis("Vertical")), 0, 0);
            }
            transform.position -= move * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(map[3]) == true){
            moveRight = true;
        }
        if(Input.GetKeyUp(map[3]) == true){
            moveRight = false;
        }
        if(moveRight){
            if((map[3] == KeyCode.A)  | (map[3] == KeyCode.D)){
                move = new Vector3(Mathf.Abs(Input.GetAxis("Horizontal")), 0, 0);
            }
            else{
                move = new Vector3(Mathf.Abs(Input.GetAxis("Vertical")), 0, 0);
            }
            transform.position += move * speed * Time.deltaTime;
        }
    }

    private void checkHit()
    {

        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, 0.1f, layerMask);
        if (hit)
        {
            insideOf = hit.collider.gameObject;
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector3.left, 0.1f, layerMask);
            if (hit)
            {
                insideOf = hit.collider.gameObject;
            }
            else
            {
                insideOf = null;
            }
        }
    }

}
