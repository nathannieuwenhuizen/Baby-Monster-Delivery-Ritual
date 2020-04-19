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
    private SphereCollider coll;

    public bool inWieg = false;

    public float danceForce = 200f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();
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
        if (coll == null)
        {
            coll = GetComponent<SphereCollider>();
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

    public void Attract() {
        coll.enabled = false;
    }

    public void InWieg()
    {
        inWieg = true;
        gameObject.AddComponent<SphereCollider>();
        GetComponent<SphereCollider>().enabled = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        coll.enabled = true;
        StartCoroutine(Dancing());
    }

    public IEnumerator Dancing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            rb.AddForce(new Vector3(Random.Range(-danceForce, danceForce), Random.Range(-danceForce, danceForce), Random.Range(-danceForce, danceForce)));
        }
    }

    public void Die(DeathType type = DeathType.fence) 
    {
        if (inWieg) { return; }

        //remove from list
        GameManager.instance.AliveBabies.Remove(this);
        GameManager.instance.UpdateBabyCount();


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
