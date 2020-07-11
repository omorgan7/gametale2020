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
        if((Input.GetKeyDown(currentMap[0]) == true)&(rigidBody.velocity.y >= -0.001) & (jumps < 2)){ //jump 
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

        if((Input.GetKeyDown(currentMap[2]) == true)){
            rigidBody.AddForce(Vector2.down * fall, ForceMode2D.Impulse);
        }

        if(Input.GetKeyDown(currentMap[1]) == true){
            moveLeft = true;
            // var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            // transform.position += move * speed * Time.deltaTime;
          
        }
         if(Input.GetKeyUp(currentMap[1]) == true){
            moveLeft = false;
            // var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            // transform.position += move * speed * Time.deltaTime;
        }

        if(moveLeft){
            if((currentMap[1] == KeyCode.A)  | (currentMap[1] == KeyCode.D)){
                move = new Vector3(Mathf.Abs(Input.GetAxis("Horizontal")), 0, 0);
            }
            else{
                move = new Vector3(Mathf.Abs(Input.GetAxis("Vertical")), 0, 0);
            }
            transform.position -= move * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(currentMap[3]) == true){
            moveRight = true;
        }
        if(Input.GetKeyUp(currentMap[3]) == true){
            moveRight = false;
        }
        if(moveRight){
            if((currentMap[3] == KeyCode.A)  | (currentMap[3] == KeyCode.D)){
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
