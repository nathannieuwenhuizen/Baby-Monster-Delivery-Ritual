using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{

    [SerializeField]
    private Transform collectPoint;
    [SerializeField]
    private List<Baby> magnetBabies;

    [SerializeField]
    private float magnetForce;

    void Start()
    {
        magnetBabies = new List<Baby>();
    }

    public void AttractBaby(Baby baby)
    {
        if (magnetBabies.Contains(baby)) { return; }
        magnetBabies.Add(baby);
        baby.Attract();
    }
    public void CollectBaby(Baby baby)
    {
        baby.InWieg();
        baby.transform.position = collectPoint.position;
        GameManager.instance.amountOfBabiesCollected++;
        GameManager.instance.AliveBabies.Remove(baby);
        GameManager.instance.UpdateBabyCount();

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Baby baby in magnetBabies)
        {
            if (!baby.inWieg)
            {
                //suck the baby back in 
                baby.RB.AddForce((collectPoint.position - baby.transform.position).normalized * magnetForce);
                if (Vector3.Distance(collectPoint.position, baby.transform.position) < 2f)
                {
                    CollectBaby(baby);
                    magnetBabies.Remove(baby);
                    break;
                }
            }
        }
    }
}
