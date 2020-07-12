using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour{

    public Vector3 start = new Vector3(-7, -0.03f, -0.8f), end = new Vector3(-4, -0.03f, -0.08f);
    public float speed = 100.0f;
    private Vector3 diff;
    private bool inc = true;
    public bool beginAtStartPosition = true; //if true begin platform at start vector


    
    // Start is called before the first frame update
    void Start(){
        if(beginAtStartPosition){
            transform.position = start;
        }
        else{
            transform.position = end;
        }
        diff = end - start;
    }

    // Update is called once per frame
    void Update()
    {
        float x = start.x;
        float y = start.y;
        if (diff.x != 0.0f)
        {
            x += Mathf.Sign(diff.x) * Mathf.PingPong(speed * Time.time, Mathf.Abs(diff.x));
        }
        if (diff.y != 0.0f)
        {
            y += Mathf.Sign(diff.y) * Mathf.PingPong(speed * Time.time, Mathf.Abs(diff.y));
        }

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
