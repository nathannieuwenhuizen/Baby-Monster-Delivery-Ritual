using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;

    [SerializeField]
    private GameObject piecePrefab;

    [SerializeField]
    private List<GameObject> pieces;
    void Update()
    {
        transform.position += new Vector3(0, 0, -scrollSpeed * Time.deltaTime);
    }
}
