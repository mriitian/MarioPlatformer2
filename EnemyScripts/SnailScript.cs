using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float movespeed = 1f;
    private Rigidbody2D mybody;
    private Animator anim;
    public LayerMask PlayerLayer;
    private bool Move_left;
    public Transform Down_Collision, left_Collsion, right_Collision, top_Collision;
    private Vector3 Left_CollisonPosition, Right_CollisonPosition;
    private bool canmove;
    private bool stunned;

    void Awake()
    {
       mybody =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Left_CollisonPosition = left_Collsion.position;
        Right_CollisonPosition = right_Collision.position;
    }
    void Start()
    {
        Move_left = true;
        canmove = true;
        stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(canmove)
        {
            if (Move_left)
            {
                mybody.velocity = new Vector2(-movespeed, mybody.velocity.y);
            }
            else
            {
                mybody.velocity = new Vector2(movespeed, mybody.velocity.y);
            }
        }
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D lefthit = Physics2D.Raycast(left_Collsion.position, Vector2.left, 0.5f, PlayerLayer);
        RaycastHit2D righthit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, PlayerLayer);

        Collider2D tophit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, PlayerLayer);

        if(tophit != null)
        {
            if(tophit.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    tophit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(tophit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 6f);
                    canmove = false;
                    mybody.velocity = new Vector2(0f, 0f);

                    anim.Play("Stunned");
                    stunned = true;

                    //BEETLE CODE HERE
                    if(tag == "Beetle")
                    {

                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }
        if (lefthit)
        {
            if(lefthit.collider.gameObject.tag == "Player")
            {
                if(!stunned)
                {
                    //Aplly damage to Player
                    lefthit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        mybody.velocity = new Vector2(15f, mybody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if(righthit)
        {
            if(righthit.collider.gameObject.tag == "Player")
            {
                if(!stunned)
                {
                    //player damage
                    righthit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        mybody.velocity = new Vector2(-15f, mybody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if(!Physics2D.Raycast(Down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection ();
        }
    }
     
    void ChangeDirection()
    {
        Move_left = !Move_left;
        Vector3 tempscale = transform.localScale;
        if(Move_left)
        {
            tempscale.x = Mathf.Abs(tempscale.x);
            left_Collsion.position = Left_CollisonPosition;
            right_Collision.position = Right_CollisonPosition;
        }
        else
        {
            tempscale.x = -Mathf.Abs(tempscale.x);
            left_Collsion.position = Right_CollisonPosition;
            right_Collision.position = Left_CollisonPosition;
        }
        transform.localScale = tempscale;
    }
     IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            if(tag == "Beetle")
            {
                anim.Play("Stunned");
                canmove = false;

                mybody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }

            if(tag == "Snail")
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canmove = false;
                    mybody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false); 
                }
            }
        }
    }
}
