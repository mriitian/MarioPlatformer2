using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool animation_started;
    private bool animation_finished;
    private int JumpTimes;
    private bool jumpleft = true;

    private string Coroutine_Name = "FrogJump";
    public LayerMask PlayerLayer;

    private GameObject Player;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine("FrogJump");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.6f, PlayerLayer))
        {
            Player.GetComponent<PlayerDamage>().DealDamage();
        }   
    }
    void LateUpdate()
    {
        if(animation_started && animation_finished) 
        {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;

        }
    }
     IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        animation_started = true;
        animation_finished = false;

        JumpTimes++;

        if (jumpleft)
        {
            anim.Play("FrogWalk");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }
        StartCoroutine("FrogJump");
    }
    void Animation_Finished()
    {
        animation_finished = true;

        if (jumpleft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }
        if(JumpTimes == 3)
        {
            JumpTimes = 0;
            Vector3 tempscale = transform.localScale;
            tempscale *= -1;
            transform.localScale = tempscale;

            jumpleft = !jumpleft;
        }
    }

    IEnumerator FrogDead()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
        StopCoroutine("FrogJump");
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Bullet")
        {
            anim.Play("FrogDead");
            StartCoroutine(FrogDead());
        }
    }
}
