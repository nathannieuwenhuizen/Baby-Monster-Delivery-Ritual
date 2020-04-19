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

    private AudioSource audioS;
    [SerializeField]
    private float maxVolume;

    [Range(0,3.14f)]
    [SerializeField]
    private float precentageOffset;

    private float index;
    void Update()
    {
        index += Time.deltaTime;

        transform.rotation = Quaternion.Euler( new Vector3(0, 0, Mathf.Sin(index * swingSpeed + precentageOffset)) * swingAngle);

        audioS.volume = Mathf.Abs(Mathf.Cos(index * swingSpeed + precentageOffset)) * maxVolume;
    }

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

}

[System.Serializable]
public class Inventory
{
    public Item Water = new Item();
    public Item Food;
    public Item Wood;
}

[System.Serializable]
public class Item
{
    public int currentAmmount = 0;
    public int maxAmount = 10;
    public int Current
    {
        get { return currentAmmount; }
        set
        {
            currentAmmount =  value;
            if (currentAmmount > maxAmount)
            {
                currentAmmount = maxAmount;
            } else if (currentAmmount < 0)
            {
                currentAmmount = 0;
            }
        }
    }
    public void Cap()
    {
        maxAmount = 0;
    }
}
