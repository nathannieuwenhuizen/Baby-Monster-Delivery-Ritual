using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawner : MonoBehaviour
{

    [SerializeField]
    private int amountOfBabies;

    [SerializeField]
    private GameObject babyPrefab;

    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private float offset;

    public void PopOut()
    {
        for (int i = 0; i < amountOfBabies; i++)
        {
            Baby newBaby = PoolManager.instance.ReuseObject(babyPrefab, spawnPos.position + new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), Random.Range(-offset, offset)), Quaternion.identity).GetComponent<Baby>();
            GameManager.instance.AliveBabies.Add(newBaby);
            GameManager.instance.UpdateBabyCount();

        }
        Destroy(gameObject);

    }

}
