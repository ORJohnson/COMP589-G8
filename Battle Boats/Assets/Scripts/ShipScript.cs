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
    public float offsetVariable = 0.5f;
    private float nextZRotation = 90f;
    private GameObject clickedTile;
    int hitCount = 0;
    public int shipSize;

    public Node nodeScript;

    private Material[] allMaterials;

    List<GameObject> touchTiles = new List<GameObject>();
    List<Color> allColors = new List<Color>();
    private GameObject[] allNodes;
    public List<BoxCollider2D> allBoxColliders = new List<BoxCollider2D>();


    private void Start()
    {
        allMaterials = GetComponentInChildren<Renderer>().materials;
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allColors.Add(allMaterials[i].color);
        }

        allNodes = GameObject.FindGameObjectsWithTag("Node");
        for (int i = 0; i < allNodes.Length; i++)
        {
            allBoxColliders.Add(allNodes[i].GetComponent<BoxCollider2D>());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        return new Vector2(tilePos.x + xOffset, tilePos.y + yOffset);

    }

    public void RotateShip()
    {
        if (clickedTile == null) return;
        touchTiles.Clear();
        gameObject.transform.parent.transform.localEulerAngles += new Vector3(0, 0, nextZRotation);
        nextZRotation *= -1;
        if (shipSize % 2 == 0)
        {
            xOffset += offsetVariable;
            yOffset += offsetVariable;
            offsetVariable *= -1;
        }
        SetPosition(clickedTile.transform.position);
        touchTiles.Add(clickedTile.gameObject);
        touchTiles = touchTiles.Distinct().ToList();
    }

    public void SetPosition(Vector2 newVec)
    {
        ClearTileList();
        gameObject.transform.parent.transform.localPosition = new Vector2(newVec.x + xOffset, newVec.y + yOffset);
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
