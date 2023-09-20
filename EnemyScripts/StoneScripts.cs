using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactivate", 4f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            target.GetComponent<PlayerDamage>().DealDamage();
            gameObject.SetActive(false );
        }
    }

}//class
