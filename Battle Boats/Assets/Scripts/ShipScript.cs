using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipScript : MonoBehaviour
{
    List<GameObject> touchTiles = new List<GameObject>();
    public float xOffset = 0;
    public float yOffset = 0.5f;
    private float nextZRotation = 90f;
    private GameObject clickedTile;
    int hitCount = 0;
    public int shipSize;

    private Material[] allMaterials;

    List<Color> allColors = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearTileList(){
        touchTiles.Clear();
    }

    public Vector2 GetOffsetVec(Vector2 tilePos){
        return new Vector2(tilePos.x + xOffset, tilePos.y + yOffset + 0.5f);

    }

    public void RotateShip()
    {
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
}
