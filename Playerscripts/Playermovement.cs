using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    private Rigidbody2D mybody;
    private Animator anim;

    public Transform Groundcheckposition;
    public LayerMask Groundlayer;

    private bool IsGrounded;
    private bool Jumped;

    private float Jumppower = 16f;
    void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Physics2D.Raycast(Groundcheckposition.position, Vector2.down, 0.5f, Groundlayer))
        //{
          //  print("Collided with ground");
        //}
        CheckIfGrounded();
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempscale = transform.localScale;
        tempscale.x = direction;
        transform.localScale = tempscale;
    }
    void PlayerWalk()
    {
        
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            mybody.velocity = new Vector2 (speed, mybody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            mybody.velocity = new Vector2 (-speed, mybody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            mybody.velocity = new Vector2 (0f, mybody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)mybody.velocity.x));

    }

    

    void CheckIfGrounded()
    {
        IsGrounded = Physics2D.Raycast(Groundcheckposition.position, Vector2.down, 0.1f, Groundlayer);
        if(IsGrounded )
        {
            if (Jumped)
            {
                Jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }
    void PlayerJump()
    {
        if(IsGrounded)
        {
            if (Input.GetKey (KeyCode.Space))
            {
                Jumped = true;
                mybody.velocity = new Vector2(mybody.velocity.x, Jumppower);
                anim.SetBool("Jump", true);
            }
        }
    }
}//class
