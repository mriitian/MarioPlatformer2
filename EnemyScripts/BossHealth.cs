using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private int health = 1;

    private bool CanDamage;
    void Awake()
    {
        anim = GetComponent<Animator>();
        CanDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        CanDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(CanDamage)
        {
            if (target.tag == "Bullet")
            {
                
                    health--;

                    CanDamage = false;
                    if (health == 0)
                    {
                        GetComponent<BossScript>().Deactivate();
                        anim.Play("BossDead");
                    }
                    StartCoroutine(WaitForDamage());
                
            }
        }
    }
}
