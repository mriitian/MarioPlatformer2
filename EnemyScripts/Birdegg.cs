using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdegg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
       // Debug.Log("Collision detected with: " + target.gameObject.name);

        if (target.gameObject.tag == "Player")
        {
            //Debug.Log("Player hit!");

            // Perform player damage here
            target.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }

        StartCoroutine(DestroyEgg());
    }
    IEnumerator DestroyEgg()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
