//v basic script that moves player as you would expect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    private KeyCode [] currentMap = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}; 
    private KeyCode [] currentArrowMap  =  {KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow};

    private BoxCollider2D myCollider;


    public float speed = 30.0f;
    public float maxSpeed = 7.0f;
    private Rigidbody2D rigidBody ;
    public float jumpPower = 10.0f, jumpBoost = 5.0f, fall = 30.0f;
    private int jumps = 0;
    private Vector2 jumpDirection = Vector2.up;
    private bool inAir = false, moveLeft = false, moveRight = false;
    private Vector3 move = new Vector3(0,0,0);
    
    public GameObject insideOf;

    public Vector3 startingPos;

    public Vector3 spawnPos;

    void Start(){
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        print(rigidBody == null);    
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        startingPos = transform.position;
        spawnPos = startingPos;
        
    }

    // Update is called once per frame
    void Update()
    {    
        checkHit();
        swapKeys();
        handleMovement();
        checkQuit();
    }

    private void swapKeys()
    {
        // restore the default.
        currentMap[0] = KeyCode.W;
        currentMap[1] = KeyCode.A;
        currentMap[2] = KeyCode.S;
        currentMap[3] = KeyCode.D;

        currentArrowMap[0] = KeyCode.UpArrow;
        currentArrowMap[1] = KeyCode.LeftArrow;
        currentArrowMap[2] = KeyCode.DownArrow;
        currentArrowMap[3] = KeyCode.RightArrow;

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

                currentArrowMap[1] = KeyCode.RightArrow;
                currentArrowMap[3] = KeyCode.LeftArrow;

            } break;
            case "yellow":
            {
                // SWAP LU
                currentMap[0] = KeyCode.A;
                currentMap[1] = KeyCode.W;

                currentArrowMap[0] = KeyCode.LeftArrow;
                currentArrowMap[1] = KeyCode.UpArrow;

            } break;
            case "green":
            {
                // SWAP RU
                currentMap[0] = KeyCode.D;
                currentMap[3] = KeyCode.W;

                currentArrowMap[0] = KeyCode.RightArrow;
                currentArrowMap[3] = KeyCode.UpArrow;

            } break;
            case "blue":
            {
                // SWAP L->U->R->L
                currentMap[0] = KeyCode.D;
                currentMap[1] = KeyCode.W;
                currentMap[3] = KeyCode.A;

                currentArrowMap[0] = KeyCode.RightArrow;
                currentArrowMap[1] = KeyCode.UpArrow;
                currentArrowMap[3] = KeyCode.LeftArrow;


            } break;
            case "purple":
            {
                // SWAP R->U->L->R
                currentMap[0] = KeyCode.A;
                currentMap[1] = KeyCode.D;
                currentMap[3] = KeyCode.W;

                currentArrowMap[0] = KeyCode.LeftArrow;
                currentArrowMap[1] = KeyCode.RightArrow;
                currentArrowMap[3] = KeyCode.UpArrow;

            } break;
        }
    }
    private void handleMovement()
    {

        if(((Input.GetKey(currentMap[0]) == true)|(Input.GetKey(currentArrowMap[0]) == true))&(rigidBody.velocity.y >= -0.001) & (jumps < 2)){ //jump 
            if(jumps == 0){
                rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            jumps++;
        }

        if((jumps > 0)&(rigidBody.velocity.y <= 0.001)){ //reset number of jumps
            jumps = 0;
        }


        if((Input.GetKey(currentMap[2]) == true)|((Input.GetKey(currentArrowMap[2]) == true))){
            rigidBody.AddForce(Vector2.down * fall, ForceMode2D.Impulse);
        }

        if((Input.GetKey(currentMap[1]) == true)|(Input.GetKey(currentArrowMap[1]) == true)){
            rigidBody.AddForce(Vector2.left * speed);
          
        }

        if((Input.GetKey(currentMap[3]) == true)|(Input.GetKey(currentArrowMap[3]) == true)){
            rigidBody.AddForce(Vector2.right * speed);
        }

        float xVelocity = Mathf.Abs(rigidBody.velocity.x);
        if (xVelocity > maxSpeed)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);
        }

        if (xVelocity > 0.0f)
        {
            rigidBody.AddForce(-20.0f * Mathf.Sign(rigidBody.velocity.x) * Vector2.right);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "transporter") //transport to start
        {
            transform.position = startingPos;
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0.0f;
        }

        if (collision.gameObject.tag == "checkpoint")
        {
            startingPos = transform.position;
        }

        if (collision.gameObject.tag == "gotostart")
        {
            transform.position = spawnPos;
            rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
            rigidBody.angularVelocity = 0.0f;
        }
    }

    void checkQuit(){
        if (Input.GetKey("escape")){
            Application.Quit();
        }
    }

}
