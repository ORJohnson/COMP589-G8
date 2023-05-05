using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipScript : MonoBehaviour
{
    public float xOffset = 0;
    public float yOffset = 0.5f;
    private float nextZRotation = 90f;
    private GameObject clickedTile;
    int hitCount = 0;
    public int shipSize;

    private Material[] allMaterials;

    List<GameObject> touchTiles = new List<GameObject>();
    List<Color> allColors = new List<Color>();


    private void Start()
    {
        allMaterials = GetComponentInChildren<Renderer>().materials;
        for (int i = 0; i < allMaterials.Length; i++)
        {
            allColors.Add(allMaterials[i].color);
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
        return new Vector2(tilePos.x + xOffset, tilePos.y + yOffset + 0.5f);

    }

    public void RotateShip()
    {
        if (clickedTile != null) return;
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
