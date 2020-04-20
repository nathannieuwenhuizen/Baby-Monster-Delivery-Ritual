using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{

    [SerializeField]
    private Transform target;


    [SerializeField]
    private bool updatePos = false;
    [SerializeField]
    private bool updateRotation = false;

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotateSpeed = 2f;


    void Update()
    {
        if (target != null)
        {
            if (updatePos) transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
            if (updateRotation)
            {
                Quaternion lookOnLook =
                Quaternion.LookRotation((target.transform.position  + new Vector3(0,0,5f)) - transform.position);

                transform.rotation =
                Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * rotateSpeed);
            }
        }
    }
}
