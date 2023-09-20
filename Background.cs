using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer Sr = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.one;

        float width = Sr.sprite.bounds.size.x;
        float height = Sr.sprite.bounds.size.y;

        float WorldSceenHeight = Camera.main.orthographicSize * 2f;
        float WorldScreenWidth = WorldSceenHeight / Screen.height * Screen.width;

        Vector3 tempScale = transform.localScale;
        tempScale.x = WorldScreenWidth / width + 0.1f;
        tempScale.y = WorldSceenHeight / height + 0.1f;

        transform.localScale = tempScale;
    }

    
}//class
