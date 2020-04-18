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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (updatePos) transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
        if (updateRotation) {
            Quaternion lookOnLook =
            Quaternion.LookRotation(target.transform.position - transform.position);

            transform.rotation =
            Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * rotateSpeed);
        }
    }
}
