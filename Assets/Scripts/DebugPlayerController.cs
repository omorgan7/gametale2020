using UnityEngine;

public class DebugPlayerController : MonoBehaviour
{
    // Update is called once per frame
    float size;

    private Vector3 mousePosition;
    void Start()
    {
        size = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize / 2.0f;
    }
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(
            mousePosition.x,
            mousePosition.y,
            transform.position.z
        );
    }
}
