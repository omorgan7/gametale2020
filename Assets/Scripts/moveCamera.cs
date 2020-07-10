using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{

    public bool isMoving = true;
    private float speed = 1.0f, max_speed = 4.5f; //max_speed < player_speed
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            var move = new Vector3(1,0,0);
            transform.position += move * speed * Time.deltaTime;
        }
    }
}
