using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public List<Node> allTileScripts;
    private GameObject[] allNodes;
    public EnemyScript enemyScript;
    private ShipScript shipScript;
    private Missiles missileScript;
    private List<int[]> enemyShips;
    private int shipsIndex = 0;

    [Header("HUD")]
    public Button nextBtn;
    public Button rotateBtn;
    public Button replayBtn;
    public Button mainMenuBtn;
    public TextMeshProUGUI topText;
    public TextMeshProUGUI playerShipCountText;
    public TextMeshProUGUI enemyShipCountText;

    [Header("Objects")]
    public GameObject _playerMissilePrefab;
    public GameObject _enemyMissilePrefab;
    public GameObject _firePrefab;

    private bool setupComplete = false;
    private bool playerTurn = true;
 
    private List<GameObject> playerFires = new List<GameObject>();
    private List<GameObject> enemyFires = new List<GameObject>();

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
        mainMenuBtn.onClick.AddListener(() => BackToMainMenu());
        enemyShips = enemyScript.PlaceEnemyShips();
        allNodes = GameObject.FindGameObjectsWithTag("Node");
        for(int i = 0; i < allNodes.Length - 25; i++)
        {
            allTileScripts.Add(allNodes[i].GetComponent<Node>());
        }
    }

    private void NextShipClicked()
    {
         if (!shipScript.OnGameBoard())
         {
             shipScript.FlashColor(Color.red);
         } else
         {
            if (shipsIndex <= ships.Length - 2)
            {
                shipsIndex++;
                shipScript = ships[shipsIndex].transform.GetChild(0).gameObject.GetComponent<ShipScript>();
                shipScript.FlashColor(Color.yellow);
            }
            else
            {
                rotateBtn.gameObject.SetActive(false);
                nextBtn.gameObject.SetActive(false);
                topText.text = "CHOOSE AN ENEMY TILE";
                setupComplete = true;
                for (int i = 0; i < ships.Length; i++) ships[i].SetActive(false);
            }
         }
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

        var board = Instantiate(_boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(_width, _height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);
        Camera.main.orthographicSize = 8;
    }


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
        ships[0] = Instantiate(_ship1Prefab, new Vector2(-6, 4.4f), Quaternion.identity);
        ships[1] = Instantiate(_ship2Prefab, new Vector2(-5, 4), Quaternion.identity);
        ships[2] = Instantiate(_ship3Prefab, new Vector2(-4, 3.46f), Quaternion.identity);
        ships[3] = Instantiate(_ship4Prefab, new Vector2(-3, 3.46f), Quaternion.identity);
        ships[4] = Instantiate(_ship5Prefab, new Vector2(-2, 3), Quaternion.identity);
    }

    public void TileClicked(GameObject tile)
    {
        if(setupComplete && playerTurn)
        {
            Vector2 tilePos = tile.transform.position;
            tilePos.x += 20;
            playerTurn = false;
            var missile = Instantiate(_playerMissilePrefab, tilePos, _playerMissilePrefab.transform.rotation);
            missileScript = missile.gameObject.GetComponent<Missiles>();
            missileScript.LaunchMissile(tile);
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
                    Vector3 centerFire = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.5f);
                    enemyShipCount--;
                    topText.text = "SUNK";
                    enemyFires.Add(Instantiate(_firePrefab, centerFire, Quaternion.identity));
                    tile.GetComponent<Node>().SetTileColor(1, new Color32(68, 0, 0, 255));
                    tile.GetComponent<Node>().SwitchColors(1);
                }
                else
                {
                    topText.text = "HIT";
                    tile.GetComponent<Node>().SetTileColor(1, new Color32(255, 21, 0, 234));
                    tile.GetComponent<Node>().SwitchColors(1);
                }
                break;
            }
            
        }
        if(hitCount == 0)
        {
            tile.GetComponent<Node>().SetTileColor(1, new Color32(135, 135, 135, 255));
            tile.GetComponent<Node>().SwitchColors(1);
            topText.text = "MISS";
        }
        Invoke("EndPlayerTurn", 1.0f);
    }


    private void PlaceShip(GameObject tile){
        shipScript = ships[shipsIndex].transform.GetChild(0).gameObject.GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector2 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[shipsIndex].transform.localPosition = newVec; // This is where it initially sets the Ship's parent location
    }

    void RotateClicked(){
        shipScript.RotateShip();
    }


    public void EnemyHitPlayer(Vector3 tile, int tileNum, GameObject hitObj)
    {
        enemyScript.MissileHit(tileNum);
        playerFires.Add(Instantiate(_firePrefab, tile, Quaternion.identity));
        if (hitObj.GetComponent<ShipScript>().HitCheckSank())
        {
            playerShipCount--;
            playerShipCountText.text = playerShipCount.ToString();
            enemyScript.SunkPlayer();
        }
       Invoke("EndEnemyTurn", 2.0f);
    }


    private void EndPlayerTurn()
    {
        for (int i = 0; i < ships.Length; i++) ships[i].SetActive(true);
        foreach (GameObject fire in playerFires) fire.SetActive(true);
        foreach (GameObject fire in enemyFires) fire.SetActive(false);
        enemyShipCountText.text = enemyShipCount.ToString();
        topText.text = "ENEMY'S TURN";
        enemyScript.NPCTurn();
        ColorAllTiles(0);
        if (playerShipCount < 1) GameOver("ENEMY WINS!");
    }

    public void EndEnemyTurn()
    {
        for (int i = 0; i < ships.Length; i++) ships[i].SetActive(false);
        foreach (GameObject fire in playerFires) fire.SetActive(false);
        foreach (GameObject fire in enemyFires) fire.SetActive(true);
        playerShipCountText.text = playerShipCount.ToString();
        topText.text = "SELECT A TILE";
        playerTurn = true;
        ColorAllTiles(1);
        if (enemyShipCount < 1) GameOver("YOU WIN!");
    }

    private void ColorAllTiles(int colorIndex)
    {
        foreach (Node tileScript in allTileScripts)
        {
            tileScript.SwitchColors(colorIndex);
        }
    }

    void GameOver(string winner)
    {
        topText.text = "GAME OVER: " + winner;
        replayBtn.gameObject.SetActive(true);
        mainMenuBtn.gameObject.SetActive(true);
        playerTurn = false;
    }

    void ReplayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("WelcomePage");
    }
}
