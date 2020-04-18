using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DeathType
{
    lava,
    fence
}
public class Baby : PoolObject
{
    public static float disappearHeight = -10;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public Rigidbody RB
    {
        get { return rb; }
        set { rb = value; }
    }
    public void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.velocity = Vector3.zero;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.BabyFence)
        {
            Die(DeathType.fence);
        }
        if (other.gameObject.tag == Tags.Mother)
        {
            other.gameObject.GetComponent<Mother>().AttractBaby(this);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Lava)
        {
            Die(DeathType.lava);
        }
    }

    void Update()
    {
        //just an extra check
        if (transform.position.y < disappearHeight)
        {
            Die(DeathType.lava);
        }
    }

    public void Die(DeathType type = DeathType.fence) 
    {
        //remove from list
        GameManager.instance.aliveBabies.Remove(this);

        //check death type
        if (type == DeathType.fence)
        {
            PoolManager.instance.ReuseObject(PoolManager.instance.popParticle, transform.position, transform.rotation);
        } else if (type == DeathType.lava)
        {
            PoolManager.instance.ReuseObject(PoolManager.instance.lavaParticle, transform.position, Quaternion.identity);
        }

        //setactive false for later use.
        Destroy();
    }

}
