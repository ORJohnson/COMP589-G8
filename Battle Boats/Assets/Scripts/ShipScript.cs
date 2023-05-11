using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using Unity.VisualScripting;

public class ShipScript : MonoBehaviour
{
    public float xOffset = 0;
    public float yOffset = 0.5f;
    private float nextZRotation = 90f;
    private GameObject clickedTile;
    private GameObject previouslyClickedTile;
    int hitCount = 0;
    public int shipSize;

    public Node nodeScript;

    private Material[] allMaterials;

    List<GameObject> touchTiles = new List<GameObject>();
    List<Color> allColors = new List<Color>();
    //List<Node> allNodes = new List<Node>();
    private GameObject[] allNodes;
    List<BoxCollider2D> allBoxColliders = new List<BoxCollider2D>();

    public LayerMask otherLayer;
    private PolygonCollider2D polygonCollider;


    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();

        allMaterials = GetComponentInChildren<Renderer>().materials;
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allColors.Add(allMaterials[i].color);
        }

        allNodes = GameObject.FindGameObjectsWithTag("Node");
        Debug.Log(allNodes.Length);
        for (int i = 0;i < allNodes.Length; i++)
        {
            allBoxColliders.Add(allNodes[i].GetComponent<BoxCollider2D>());
        }
        //allBoxColliders = nodeScript.GrabbingBoxColliders();
        Debug.Log(allBoxColliders.Count);
    }

    private void Update()
    {
        if (clickedTile != null && clickedTile != previouslyClickedTile)
        {
            OverlappedNodes(allBoxColliders);
            previouslyClickedTile = clickedTile;
        }
    }

    // Here is where we are going to write potential replacement function for OnCollisionEnter2D AND OnGameBoard
    public void OverlappedNodes(List<BoxCollider2D> list)
    {
        foreach (BoxCollider2D boxCollider in list)
        {
            Vector2 boxSize = boxCollider.size;
            Vector2 boxCenter = (Vector2)boxCollider.transform.position + boxCollider.offset;
            Vector2 boxTopLeft = boxCenter + new Vector2(-boxSize.x / 2f, -boxSize.y / 2f);
            Vector2 boxBottomRight = boxCenter + new Vector2(boxSize.x / 2f, -boxSize.y / 2f);

            for (float x = boxTopLeft.x; x < boxBottomRight.x; x += 0.1f)
            {
                for(float y = boxTopLeft.y; y < boxBottomRight.y; y -= 0.1f)
                {
                    Vector2 point = new Vector2(x, y);
                    if (polygonCollider.OverlapPoint(point))
                    {
                        Debug.Log(boxCollider.gameObject.name);
                        //touchTiles.Add(boxCollider.gameObject);
                    }
                }
            }
        }
        //Collider2D[] colliders = Physics2D.OverlapCollider(polygonCollider, new ContactFilter2D(), );

        //foreach (Collider2D collider in colliders)
        //{
        //    if (collider.gameObject != gameObject)
        //    {
        //        if (collider.gameObject.CompareTag("Node"))
        //        {
        //            touchTiles.Add(collider.gameObject);
        //        }
        //    }
        //}

        //touchTiles = touchTiles.Distinct().ToList();
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject.CompareTag("Node"))
        {
            touchTiles.Add(collision.gameObject);
        }
    }

    public void ClearTileList()
    {
        touchTiles.Clear();
    }

    public Vector2 GetOffsetVec(Vector2 tilePos)
    {
        return new Vector2(tilePos.x + xOffset, tilePos.y + yOffset + 0.5f);

    }

    public void RotateShip()
    {
        if (clickedTile == null) return;
        touchTiles.Clear();
        transform.localEulerAngles += new Vector3(0, 0, nextZRotation);
        nextZRotation *= -1;
        float temp = xOffset;
        xOffset = yOffset;
        yOffset = temp;
        SetPosition(clickedTile.transform.position);
    }

    public void SetPosition(Vector2 newVec)
    {
        ClearTileList();
        transform.localPosition = new Vector2(newVec.x + xOffset, newVec.y + yOffset - 0.5f);
    }

    public void SetClickedTile(GameObject tile)
    {
        clickedTile = tile;
    }

    public bool OnGameBoard()
    {
        return touchTiles.Count == shipSize;
    }

    public bool HitCheckSank()
    {
        hitCount++;
        return shipSize <= hitCount;
    }

    public void FlashColor(Color tempColor)
    {
        foreach (Material material in allMaterials)
        {
            material.color = tempColor;
        }
        Invoke("ResetColor", 0.5f);
    }

    private void ResetColor()
    {
        int i = 0;
        foreach(Material material in allMaterials)
        {
            material.color = allColors[i++];
        }
    }
}
