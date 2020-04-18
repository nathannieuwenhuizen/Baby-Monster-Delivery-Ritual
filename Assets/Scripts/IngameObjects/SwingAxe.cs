using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{

    [Range(10, 90)]
    [SerializeField]
    private float swingAngle = 45f;
    [Range(0.01f, 5f)]
    [SerializeField]
    private float swingSpeed = 0.2f;


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler( new Vector3(0, 0, Mathf.Sin(Time.time * swingSpeed)) * swingAngle);
    }
}
