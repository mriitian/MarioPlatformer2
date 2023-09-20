using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stone;
    public Transform attackInstantiate;
    private Animator anim;
    private string Coroutine_name = "StartAttack";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine("StartAttack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce (new Vector2(Random.Range(-300f,-700f), 0f));
    }
    void BacktoIdle()
    {
        anim.Play("BossIdle");
    }

    public void Deactivate()
    {
        StopCoroutine("StartAttack");
        enabled = false;
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f,5f));
        anim.Play("BossAttack");
        StartCoroutine("StartAttack");
    }
}//class
