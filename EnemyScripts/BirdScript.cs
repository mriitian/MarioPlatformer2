using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Start is called before the first frame update\
    private Rigidbody2D mybody;
    private Animator anim;
    private Vector3 moveDirection = Vector3.left;
    private Vector3 OriginPos;
    private Vector3 MovePos;

    public GameObject BirdEgg;
    public LayerMask PlayerLayer;
    private bool attacked;
    private bool Canmove;

    void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        OriginPos = transform.position;
        OriginPos.x += 6f;
        MovePos = transform.position;
        MovePos.x -= 6f;

        Canmove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovetheBird();
        DroptheEGG();
    }
    void MovetheBird()
    {
        if(Canmove)
        {
            transform.Translate(moveDirection * 3f * Time.smoothDeltaTime);
            if(transform.position.x >= OriginPos.x )
            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);
            }
            else if(transform.position.x <= MovePos.x )
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }
    void ChangeDirection(float Direction)
    {
        Vector3 tempscale = transform.localScale;
        tempscale.x = Direction;
        transform.localScale = tempscale;

    }
    void DroptheEGG()
    {
        if(!attacked)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, PlayerLayer);
            if (hit.collider != null)
            {
                Instantiate(BirdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            anim.Play("Dead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            mybody.bodyType = RigidbodyType2D.Dynamic;
            Canmove = false;
            StartCoroutine(BirdDead());
        }
    }
}
