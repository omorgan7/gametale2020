using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour{

    public Vector3 start = new Vector3(-7, -0.03f, -0.8f), end = new Vector3(-4, -0.03f, -0.08f);//, dir = new Vector3(1,0,0);
    public float speed = 100.0f;
    private float xChange, yChange, zChange;
    private bool inc = true;


    
    // Start is called before the first frame update
    void Start(){
        transform.position = start;

        xChange = (end.x - start.x)/speed;
        yChange = (end.y - start.y)/speed;
        zChange = (end.z - start.z)/speed;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position.x <= end.x)&(transform.position.y <= end.y)&(transform.position.z <= end.z)&(inc)){
            transform.position += new Vector3(xChange, yChange, zChange);
            print("in here");
        }
        else if(((transform.position.x >= end.x)|(transform.position.y >= end.y)|(transform.position.z >= end.z))&(inc)){
            inc = false;
        }
        else if((transform.position.x >= start.x)&(transform.position.y >= start.y)&(transform.position.z >= start.z)&(!inc)){
            transform.position -= new Vector3(xChange, yChange, zChange);
        }
        else if(((transform.position.x <= start.x)|(transform.position.y <= start.y)|(transform.position.z <= start.z))&(!inc)){
            inc = true;
        }


        
    }
}
