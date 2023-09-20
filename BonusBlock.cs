using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Bottom;
    private Animator anim;
    public LayerMask PlayerLayer;
    private Vector3 MoveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
    private bool Startanim;
    private bool Cananimate = true;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckforCollision();
        AnimateUpDown();
    }

    void CheckforCollision()
    {
        if (Cananimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(Bottom.position, Vector2.down, 0.1f, PlayerLayer);
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    //inc score
                    anim.Play("Idle");
                    Startanim = true;
                    Cananimate = false;
                }
            }
        }
    }

    void AnimateUpDown()
    {
        if (Startanim)
        {
            transform.Translate(MoveDirection * Time.smoothDeltaTime);
            if(transform.position.y >= animPosition.y)
            {
                MoveDirection = Vector2.down;
            }
            else if(transform.position.y <= originPosition.y)
            {
                Startanim = false;
            }
        }
    }
}//class
