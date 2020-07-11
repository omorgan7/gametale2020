//v basic script that moves player as you would expect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    private KeyCode [] currentMap = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}; 

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

    public float speed = 30.0f;
    public float maxSpeed = 7.0f;
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
    void Update()
    {    
        checkHit();
        swapKeys();
        handleMovement();
    }

    private void swapKeys()
    {
        // restore the default.
        currentMap[0] = KeyCode.W;
        currentMap[1] = KeyCode.A;
        currentMap[2] = KeyCode.S;
        currentMap[3] = KeyCode.D;

        if (insideOf == null)
        {  
            return;
        }

        switch (insideOf.tag)
        {
            case "red":
            {
                // SWAP LR
                currentMap[1] = KeyCode.D;
                currentMap[3] = KeyCode.A;
            } break;
            case "yellow":
            {
                // SWAP LU
                currentMap[0] = KeyCode.A;
                currentMap[1] = KeyCode.W;
            } break;
            case "green":
            {
                // SWAP RU
                currentMap[0] = KeyCode.D;
                currentMap[3] = KeyCode.W;
            } break;
            case "blue":
            {
                // SWAP L->U->R->L
                currentMap[0] = KeyCode.D;
                currentMap[1] = KeyCode.W;
                currentMap[3] = KeyCode.A;
            } break;
            case "purple":
            {
                // SWAP R->U->L->R
                currentMap[0] = KeyCode.A;
                currentMap[1] = KeyCode.D;
                currentMap[3] = KeyCode.W;
            } break;
        }
    }
    private void handleMovement()
    {

        if((Input.GetKey(currentMap[0]) == true)&(rigidBody.velocity.y >= -0.001) & (jumps < 2)){ //jump 
            if(jumps == 0){
                rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            jumps++;
        }

        if((jumps > 0)&(rigidBody.velocity.y <= 0.001)){ //reset jump
            jumps = 0;
        }

        if((Input.GetKey(currentMap[2]) == true)){
            rigidBody.AddForce(Vector2.down * fall, ForceMode2D.Impulse);
        }

        if(Input.GetKey(currentMap[1]) == true){
            rigidBody.AddForce(Vector2.left * speed);
          
        }

        if(moveLeft){
            rigidBody.AddForce(Vector2.left * speed);
        }

        if(Input.GetKey(currentMap[3]) == true){
            rigidBody.AddForce(Vector2.right * speed);
        }

        if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);
        }
    }

    private void checkHit()
    {

        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, 0.5f, layerMask);
        if (hit)
        {
            insideOf = hit.collider.gameObject;
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector3.left, 0.5f, layerMask);
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
