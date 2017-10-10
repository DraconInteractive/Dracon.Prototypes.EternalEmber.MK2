using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    Player player;
    Vector3 cameraVelocity;
    public Vector3 playerOffset, lookOffset;
    public float cameraMovementDelta;
    [HideInInspector]
    public Transform headTransform;

    bool rDown;
    Vector3 scrollOffset;
    public float scrollPower;
    // Use this for initialization
    void Start()
    {
        player = Player.player;
        headTransform = player.head;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (1))
        {
            rDown = true;
        }
        if (Input.GetMouseButtonUp (1))
        {
            rDown = false;
        }

        if (rDown)
        {
            float mX = Input.GetAxis ("Mouse X");
            float mY = Input.GetAxis ("Mouse Y");

            scrollOffset.y += mY * scrollPower;
            scrollOffset.x += mX * scrollPower;
        }
        else
        {
            if (scrollOffset.magnitude > 0)
            {
                scrollOffset = Vector3.Lerp (scrollOffset, Vector3.zero, 0.2f);
            }
        }
        Vector3 targetPos = player.transform.position + player.transform.forward * playerOffset.z + player.transform.right * playerOffset.x + player.transform.up * playerOffset.y;
        transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref cameraVelocity, cameraMovementDelta);
//        transform.LookAt (headTransform.position + Vector3.up * scrollOffset.y + headTransform.right * scrollOffset.x);
		transform.LookAt (player.transform.position + Vector3.up * (scrollOffset.y + lookOffset.y) + player.transform.right * (scrollOffset.x + lookOffset.x) + player.transform.forward * lookOffset.z);
    }
}
