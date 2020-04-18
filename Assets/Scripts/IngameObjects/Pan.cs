using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{

    private Rigidbody rb;

    private Vector2 inputDirection;

    [Header("Movement")]
    [SerializeField]
    private float horizontalSpeed = 5f;
    [SerializeField]
    private float verticalSpeed = 10f;
    [SerializeField]
    private float maxMoveAngle = 20f;
    [SerializeField]
    private float angleSpeed = 2f;

    [SerializeField]
    private float baseSpeed = 2f;

    [Header("bounds")]
    [SerializeField]
    private Vector2 maxPos;
    [SerializeField]
    private Vector2 minPos;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        inputDirection = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.y = Input.GetAxis("Vertical");
        Move(inputDirection);

        //vertical movement
        Levitate(Input.GetAxis("Levitation"));

        //rb.MovePosition(rb.position + new Vector3(0, 0, baseSpeed * Time.deltaTime));
        //rb.velocity +=  new Vector3(0, 0, baseSpeed * Time.deltaTime);

        //rotation update
        UpdateAngle();

        //bounds
        transform.position = new Vector3( Mathf.Clamp(transform.position.x, minPos.x, maxPos.x) , transform.position.y, Mathf.Clamp(transform.position.z, minPos.y, maxPos.y));

    }

    public void Move(Vector2 dir)
    {
        rb.velocity = new Vector3( inputDirection.x, rb.velocity.y , inputDirection.y) * horizontalSpeed;

    }
    public void Levitate(float val)
    {
        rb.velocity = new Vector3(rb.velocity.x, -val * verticalSpeed, rb.velocity.z) * horizontalSpeed;
    }

    public void UpdateAngle()
    {
        Vector3 angle = new Vector3(rb.velocity.z, 0, -rb.velocity.x) / horizontalSpeed;
        angle *= maxMoveAngle;
        angle += rb.velocity;

        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(angle), Time.deltaTime * angleSpeed);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
