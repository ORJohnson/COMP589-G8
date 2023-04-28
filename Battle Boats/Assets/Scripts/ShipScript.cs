using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipScript : MonoBehaviour
{
    List<GameObject> touchTiles = new List<GameObject>();
    public float xOffset = 0;
    public float zOffset = 0;
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

    public Vector3 GetOffsetVec(Vector3 tilePos){
        return new Vector3(tilePos.x + zOffset, 2, tilePos.z + zOffset);

    }

    public void RotateShip()
    {
        // if (clickedTile == null) return;
        touchTiles.Clear();
        transform.localEulerAngles += new Vector3(0, 0, nextZRotation);
        nextZRotation *= -1;
        float temp = xOffset;
        xOffset = zOffset;
        zOffset = temp;
        SetPosition(clickedTile.transform.position);
    }

    public void SetPosition(Vector3 newVec)
    {
        // ClearTileList();
        transform.localPosition = new Vector3(newVec.x + xOffset, 2, newVec.z + zOffset);
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
