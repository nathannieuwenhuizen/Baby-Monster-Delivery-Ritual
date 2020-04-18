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
    }
    void Update()
    {

        if (transform.position.y < disappearHeight)
        {
            Die(DeathType.lava);
        }
    }

    public void Die(DeathType type = DeathType.fence) 
    {
        if (type == DeathType.fence)
        {
            PoolManager.instance.ReuseObject(PoolManager.instance.popParticle, transform.position, transform.rotation);
        } else if (type == DeathType.lava)
        {
            PoolManager.instance.ReuseObject(PoolManager.instance.lavaParticle, transform.position, transform.rotation);

        }
        Destroy();
    }

}
