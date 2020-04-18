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
        Debug.Log("start point: " + collectPoint);
        magnetBabies = new List<Baby>();
    }

    public void AttractBaby(Baby baby)
    {
        if (magnetBabies.Contains(baby)) { return; }
        magnetBabies.Add(baby);
    }
    public void CollectBaby(Baby baby)
    {
        magnetBabies.Remove(baby);
        baby.Die();
        GameManager.instance.amountOfBabiesCollected++;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Collect point: " + collectPoint);
        foreach(Baby baby in magnetBabies)
        {
            //suck the baby back in 
            baby.RB.AddForce((collectPoint.position - baby.transform.position).normalized * magnetForce);
            if (Vector3.Distance(collectPoint.position, baby.transform.position) < 1f)
            {
                CollectBaby(baby);
                break;
            }
        }
    }
}
