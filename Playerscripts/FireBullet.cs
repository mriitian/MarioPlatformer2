using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 10f;

    private Animator anim;
    private bool canmove;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {

        canmove = true; 
        StartCoroutine(DisableBullet(10f));
        
    }

    // Update is called once per frame
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        if(canmove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Beetle" || target.gameObject.tag == "Snail" || target.gameObject.tag == "Spider" || target.gameObject.tag == "Boss" || target.gameObject.tag == "Frog")
        {
            anim.Play("Explode");
            canmove = false;
            StartCoroutine(DisableBullet(0.1f));
        }
    }
}
