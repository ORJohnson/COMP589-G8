using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 10;
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private SpriteRenderer _boardPrefab;

    [Header("HUD")]
    public Button nextBtn;
    public Button rotateBtn;

    public GameObject[] ships;
    private int shipsIndex = 0;

    public EnemyScript enemyScript;
    private ShipScript shipScript;
    private bool setupComplete = false;
    private bool playerTurn = true;
    // Enemy ships list
    private List<int[]> enemyShips;
    private int enemyShipCount = 5;
    private int playerShipCount = 5;
    private List<GameObject> playerFires;
    public GameObject firePrefab;

    private void Start()
    {
        GenerateGrid();
        // shipScript = ships[shipsIndex].GetComponent<ShipScript>();
        // nextBtn.onClick.AddListener(() => NextShipClicked());
        // rotateBtn.onClick.AddListener(() => RotateClicked());
        enemyShips = enemyScript.PlaceEnemyShips();


        // Potentially run method to place enemy ships
    }
    private void NextShipClicked()
    {
    //     if (!shipScript.OnGameBoard())
    //     {
    //         shipScript.FlashColor(Color.red);
    //     } else
    //     {
            if(shipsIndex <= ships.Length - 2)
            {
            //  shipScript.GetComponent<SpriteRenderer>().color = Color.yellow;
                shipsIndex++;
                shipScript = ships[shipsIndex].GetComponent<ShipScript>();

                //shipScript.FlashColor(Color.yellow);
            }
            // else
            // {
            //     rotateBtn.gameObject.SetActive(false);
            //     nextBtn.gameObject.SetActive(false);
            //     woodDock.SetActive(false);
            //     topText.text = "Choose an enemy tile.";
            //     setupComplete = true;
            //     for (int i = 0; i < ships.Length; i++) ships[i].SetActive(false);
            // }
        // }
        
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x,y), Quaternion.identity);
            }
        }

        var center = new Vector2((float) _width/2 - 0.5f, (float) _height/2);

        // Use that^ for the board as well
        var board = Instantiate(_boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(_width, _height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);
    }

    public void TileClicked(GameObject tile)
    {
        if(setupComplete && playerTurn)
        {
            // drop a missile
        } else if (!setupComplete)
        {
            PlaceShip(tile);
            shipScript.SetClickedTile(tile);


        }
    }

    public void CheckHit(GameObject tile)
    {
        int tileNum = Int32.Parse(Regex.Match(tile.name, @"\d+").Value);
        int hitCount = 0;

        foreach (int[] tileNumArray in enemyShips)
        {
            if(tileNumArray.Contains(tileNum))
            {
                for (int i = 0; i < tileNumArray.Length; i++)
                {
                    if (tileNumArray[i] == tileNum)
                    {
                        tileNumArray[i] = -5;
                        hitCount++;
                    }
                    else if (tileNumArray[i] == -5)
                    {
                        hitCount++;
                    }
                }
                if (hitCount == tileNumArray.Length)
                {
                    enemyShipCount--;
                    // Something to mention that the ship has sunk
                    // Enemy Fires
                    // Tile Color
                }
                else
                {
                    // Something to mention that the ship was hit
                }
                break;
            }
            
        }
        if(hitCount == 0)
        {
            // color for a miss
            // Something to mention that the tile was a miss
        }
        // Invoke EndPlayerTurn
    }


    private void PlaceShip(GameObject tile){
        shipScript = ships[shipsIndex].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[shipsIndex].transform.localPosition = newVec;

    }

    void RotateClicked(){
        shipScript.RotateShip();
    }


    public void EnemyHitPlayer(Vector3 tile, int tileNum, GameObject hitObj)
    {
        //enemyScript.missileHit(tileNum);
        tile.y += 0.2f; // this might be z since we are dealing with a 2d game
        playerFires.Add(Instantiate(firePrefab, tile, Quaternion.identity));
        if(hitObj.GetComponent<ShipScript>().HitCheckSank())
        {
            playerShipCount--;
            // update text that represents player ship count
        }
        // Invoke("EndEnemyTurn", 2.0f);
    }

}
