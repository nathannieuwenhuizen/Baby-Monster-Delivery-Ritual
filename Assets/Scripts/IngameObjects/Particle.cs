using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : PoolObject
{

    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }
        ps.Play();
    }
    private void Update()
    {
        if (ps != null)
        {
            if (ps.IsAlive() == false)
            {
                Destroy();
            }
        }
    }
}
