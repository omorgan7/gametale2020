using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAnimator : MonoBehaviour
{
    // Start is called before the first frame update

    public float frametime = 0.33f;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        int index = 0;
        while (true)
        {
            spriteRenderer.sprite = sprites[index];
            ++index;
            index = index % sprites.Length;
            yield return new WaitForSecondsRealtime(frametime);
        }
    }
}
