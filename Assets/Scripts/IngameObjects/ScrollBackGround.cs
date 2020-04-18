using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField]
    public float scrollSpeed;

    [Range(1, 20)]
    [SerializeField]
    private float size;

    [Header("prefabs")]
    [SerializeField]
    private GameObject basePiece;
    [SerializeField]
    private GameObject finalPiece;

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
}
