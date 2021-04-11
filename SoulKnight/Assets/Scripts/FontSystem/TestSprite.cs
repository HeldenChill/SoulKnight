using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSprite : MonoBehaviour
{
    // Start is called before the first frame update
    Sprite sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture2D = sprite.texture;
        //Debug.Log(texture2D.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
