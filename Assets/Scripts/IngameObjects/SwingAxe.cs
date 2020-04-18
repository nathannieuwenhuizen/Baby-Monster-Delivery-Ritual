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


    Inventory inventory;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler( new Vector3(0, 0, Mathf.Sin(Time.time * swingSpeed)) * swingAngle);
        inventory.Water.Cap();
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
