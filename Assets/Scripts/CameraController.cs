using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;

    public float speed = 1.0f;
    public bool isMoving = true;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void LateUpdate()
    {
          if(isMoving){
            var move = new Vector3(1,0,0);
            transform.position += move * speed * Time.deltaTime;
        }
        else{
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 10.0f, transform.position.z);
        }
    }
}
