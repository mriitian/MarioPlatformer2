using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpiderScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D mybody;
    private Animator anim;
    private Vector3 MoveDirection = Vector3.down;
    private string Couroutine_name = "ChangeMovement";

    void Awake()
    {
        anim = GetComponent<Animator>();
        mybody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(Couroutine_name);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(MoveDirection * Time.smoothDeltaTime);
    }
    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        if (MoveDirection == Vector3.down)
        {
            MoveDirection = Vector3.up;
        }
        else
        {
            MoveDirection = Vector3.down;
        }

        StartCoroutine(Couroutine_name);
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            anim.Play("SpiderDead");
            mybody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDead());
            StopCoroutine(Couroutine_name);
        }

        if(target.tag == "Player")
        {
            target.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
