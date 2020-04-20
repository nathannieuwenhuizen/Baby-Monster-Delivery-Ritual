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

    [SerializeField]
    private AudioClip fallSound;
    [SerializeField]
    private AudioClip[] laughSounds;
    [SerializeField]
    private AudioClip[] idleSounds;
    [SerializeField]
    private AudioClip magnetiseSound;
    [SerializeField]
    private AudioSource audioS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();

        StopAllCoroutines();
        StartCoroutine(PlayIdleSoundLoop());
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

        StopAllCoroutines();
        StartCoroutine(PlayIdleSoundLoop());

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
        if (inWieg) { return; }

        //just an extra check
        if (transform.position.y < disappearHeight)
        {
            Die(DeathType.lava);
        }
        if (GameManager.instance.wokPan != null) {
            if (transform.position.y < GameManager.instance.wokPan.transform.position.y - .2f)
            {
                PlayBabySound(fallSound, .3f);
            }
            if (Vector3.Distance(transform.position, GameManager.instance.wokPan.transform.position) > 20f)
            {
                Die();
            }

        }
    }


    IEnumerator PlayIdleSoundLoop()
    {
        while (!inWieg)
        {
            if (GameManager.instance.AliveBabies != null)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
            } else
            {
                yield return new WaitForSeconds(1);

            }
            PlayBabySound(idleSounds, .1f + .3f * (1 /  (0.1f +GameManager.instance.AliveBabies.Count)));
        }
    }

    public void PlayBabySound(AudioClip clip, float volume, bool forcePlay = false)
    {
        if (!audioS.isPlaying || forcePlay)
        {
            audioS.clip = clip;
            audioS.volume = volume;
            audioS.Play();
        }
    }

    public void PlayBabySound(AudioClip[] clips, float volume, bool forcePlay = false)
    {
        if (!audioS.isPlaying || forcePlay)
        {
            audioS.clip = clips[Mathf.FloorToInt(Random.Range(0, clips.Length))];
            audioS.volume = volume;
            audioS.Play();
        }
    }

    public void Attract() {
        PlayBabySound(magnetiseSound, .1f);
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
            GetComponent<ParticleSystem>().Play();
            PlayBabySound(laughSounds,.1f +  .4f * (1 / GameManager.instance.amountOfBabiesCollected));
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
            AudioManager.instance?.PlaySound(AudioEffect.balloonPop, .2f);
            PoolManager.instance.ReuseObject(PoolManager.instance.popParticle, transform.position, transform.rotation);
        } else if (type == DeathType.lava)
        {
            AudioManager.instance?.PlaySound(AudioEffect.lavaSplash, .2f);
            PoolManager.instance.ReuseObject(PoolManager.instance.lavaParticle, transform.position, Quaternion.identity);
        }

        //setactive false for later use.
        Destroy();
    }

}
