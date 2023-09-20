using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Resetspeed = 0.5f;
    public float camSpeed = 0.3f;

    public Bounds CameraBounds;
    public Transform CameraTarget;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 CurrentVelocity;

    private bool Followsplayer;

    void Awake()
    {
        BoxCollider2D mycoll = GetComponent<BoxCollider2D>();
        mycoll.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        CameraBounds = mycoll.bounds;
    }
    void Start()
    {
        CameraTarget = GameObject.FindGameObjectWithTag ("Player").transform;
        lastTargetPosition = CameraTarget.transform.position;
        offsetZ = (transform.position - CameraTarget.transform.position).z;
        Followsplayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Followsplayer)
        {
            Vector3 aheadTargetPos = CameraTarget.transform.position + Vector3.forward * offsetZ;
            if(aheadTargetPos.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref CurrentVelocity, camSpeed);
                transform.position = new Vector3(newCameraPosition.x,transform.position.y,newCameraPosition.z);
                lastTargetPosition = CameraTarget.position;
            }
        }
    }
}//class
