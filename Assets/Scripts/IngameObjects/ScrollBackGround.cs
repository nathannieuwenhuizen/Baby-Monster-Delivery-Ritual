using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{

    [Header("rock values")]
    [Range(0, 50)]
    [SerializeField]
    private int amountOfRocks = 5;
    [SerializeField]
    private float minScale = 1f;
    [SerializeField]
    private float maxScale = 10f;

    [SerializeField]
    public float scrollSpeed;
    [SerializeField]
    public float extraScroll;

    [Range(1, 20)]
    [SerializeField]
    private float size;

    [Header("prefabs")]
    [SerializeField]
    private GameObject basePiece;
    [SerializeField]
    private GameObject finalPiece;

    [Header("objects in level prefabs")]
    [SerializeField]
    private GameObject rock;
    [SerializeField]
    private GameObject rockParent;

    [Header("Materials")]
    [SerializeField]
    private MeshRenderer lavaRenderer;
    [SerializeField]
    private Vector2 lavaDirection;

    private bool editMode = true;
    private void Awake()
    {
        editMode = false;
    }
    void Update()
    {
        transform.position += new Vector3(0, 0, -scrollSpeed * Time.deltaTime);
        if (GameManager.instance.wokPan.pushesForward)
        {
            transform.position += new Vector3(0, 0, -extraScroll * Time.deltaTime);
        }

        lavaRenderer.material.mainTextureOffset += lavaDirection * Time.deltaTime;
    }

    public void UpdateSize ()
    {
        if (editMode)
        {
            if (basePiece != null)
            {
                basePiece.transform.localScale = new Vector3(1, 1, size);
                basePiece.transform.position = new Vector3(basePiece.transform.position.x, basePiece.transform.position.y, size * 10f / 2f - 5f);
                foreach (MeshRenderer mr in basePiece.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.sharedMaterial.mainTextureScale = new Vector2(1, size);
                }
            }
            if (finalPiece != null)
            {
                finalPiece.transform.position = new Vector3(finalPiece.transform.position.x, finalPiece.transform.position.y, size * 10f -5f);
            }
        }
    }

    public void PlaceRocks()
    {
        if (rockParent == null || rock == null) { return; }
        foreach(Transform rocks in rockParent.transform.GetComponentInChildren<Transform>())
        {
            if (rocks != rockParent)
            {
                Destroy(rocks.gameObject);
            }
        }

        for (int i = 0; i < amountOfRocks; i++)
        {
            GameObject newRock = Instantiate(rock, rockParent.transform);
            Vector3 rotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            newRock.transform.rotation = Quaternion.Euler(rotation);
            Vector3 position = new Vector3(0, 0, i / amountOfRocks * size);
            position.y = -2;
            float r = Random.value;
            if (r < 0.33f)
            {
                position.x = -4;
            } else if ( r < 0.66)
            {
                position.x = 4;
            } else
            {
                position.x = Random.Range(-2, 2);
            }
            newRock.transform.position = position;
            Vector3 scale = new Vector3(1,1,1) * Random.Range(minScale, maxScale);
            newRock.transform.localScale = scale;
        }
    }
}
