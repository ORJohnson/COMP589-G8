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
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private SpriteRenderer _boardPrefab;
    [SerializeField] private GameObject _ship1Prefab;
    [SerializeField] private GameObject _ship2Prefab;
    [SerializeField] private GameObject _ship3Prefab;
    [SerializeField] private GameObject _ship4Prefab;
    [SerializeField] private GameObject _ship5Prefab;

    [Header("Ships")]
    public GameObject[] ships;
    public List<TileScript> allTileScripts; //Didnt exist
    public EnemyScript enemyScript;
    private ShipScript shipScript;
    private List<int[]> enemyShips;
    private int shipsIndex = 0;

    [Header("HUD")]
    public Button nextBtn;
    public Button rotateBtn;
    public Button replayBtn;
    public Text topText;
    public Text playerShipText;
    public Text enemyShipText;

    [Header("Objects")]
    public GameObject _playerMissilePrefab;
    public GameObject _enemyMissilePrefab;
    public GameObject _firePrefab;

    private bool setupComplete = false;
    private bool playerTurn = true;
 
    private List<GameObject> playerFires = new List<GameObject>();
    private List<GameObject> enemtFires = new List<GameObject>(); //didnt exist

    private int enemyShipCount = 5;
    private int playerShipCount = 5;

    private void Start()
    {
        GenerateGrid();
        GenerateDockGrid();
        DockShips();
        shipScript = ships[shipsIndex].GetComponent<ShipScript>();
        nextBtn.onClick.AddListener(() => NextShipClicked());
        rotateBtn.onClick.AddListener(() => RotateClicked());
        replayBtn.onClick.AddListener(() => ReplayClicked());
        enemyShips = enemyScript.PlaceEnemyShips();
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
        var num = 1;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x,y), Quaternion.identity);
                node.name = "Node(" + num.ToString() + ")";
                num++;
            }
        }

        var center = new Vector2((float) _width/2 - 0.5f, (float) _height/2);

        // Use that^ for the board as well
        var board = Instantiate(_boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(_width, _height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);
        Camera.main.orthographicSize = 8;
    }

    // Going to try to create a smaller grid for ships to sit before being placed
    void GenerateDockGrid()
    {
        for (int x = 0; x < _width - 5; x++)
        {
            for (int y = 0; y < _height - 5; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x - 6, y + 2), Quaternion.identity);
            }
        }
        
        var xFloat = (_width / 4) - 0.5f;
        var yFloat = _height / 4f;
        var side = new Vector2((float) xFloat - 5.5f, (float) yFloat + 2f);

        var dockBoard = Instantiate(_boardPrefab, side, Quaternion.identity);
        dockBoard.size = new Vector2(_width - 5, _height -5);
    }

    // Original Ships position
    void DockShips()
    {
        // Let's just try putting the ships on the board in their proper place with their proper sizes
        ships[0] = Instantiate(_ship1Prefab, new Vector2(-6, 4.4f), Quaternion.identity);
        ships[1] = Instantiate(_ship2Prefab, new Vector2(-5, 4), Quaternion.identity);
        ships[2] = Instantiate(_ship3Prefab, new Vector2(-4, 3.46f), Quaternion.identity);
        ships[3] = Instantiate(_ship4Prefab, new Vector2(-3, 3.46f), Quaternion.identity);
        ships[4] = Instantiate(_ship5Prefab, new Vector2(-2, 3), Quaternion.identity);
        Debug.Log(ships.Length);
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

    public void CheckHit(GameObject tile) // This needs to be looked at because the Tiles do not have names. Possibly need to give nodes names in GenerateGrid() Method
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
                    // Something to mention that the ship has sunk => topText.text = "Ship Sunk!"
                    // enemyFires
                    // Tile Color
                }
                else
                {
                    // Something to mention that the ship was hit => topText.text = "Hit!"
                    // Tile Color
                }
                break;
            }
            
        }
        if(hitCount == 0)
        {
            // color for a miss
            // Something to mention that the tile was a miss => topText.text = "Miss!"
        }
        // Invoke EndPlayerTurn
    }


    private void PlaceShip(GameObject tile){
        shipScript = ships[shipsIndex].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector2 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[shipsIndex].transform.localPosition = newVec;

    }

    void RotateClicked(){
        shipScript.RotateShip();
    }


    public void EnemyHitPlayer(Vector3 tile, int tileNum, GameObject hitObj)
    {
        //enemyScript.missileHit(tileNum);
        tile.y += 0.2f; // this might be z since we are dealing with a 2d game
        playerFires.Add(Instantiate(_firePrefab, tile, Quaternion.identity));
        if(hitObj.GetComponent<ShipScript>().HitCheckSank())
        {
            playerShipCount--;
            // update text that represents player ship count => playerShipText.text = playerShipCount.ToString();
            // execute script for enemy sinking the player's ship => enemyScript.SunkPlayer();
        }
        // Invoke("EndEnemyTurn", 2.0f);
    }

     void ReplayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
