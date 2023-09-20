using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Firebullet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void shootBullet()
    {
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            GameObject bullet = Instantiate(Firebullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }
    void Update()
    {
        shootBullet();
    }
}
