using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissiles : MonoBehaviour
{
    GameManager gameManager;
    EnemyScript enemyScript;
    public Vector3 targetTileLocation;
    private int targetTile = -1;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyScript = GameObject.Find("Enemy").GetComponent<EnemyScript>();
    }

    private void Update()
    {
        if (isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetTileLocation.x, targetTileLocation.y + 0.5f), 15f * Time.deltaTime);
            transform.up = new Vector3(targetTileLocation.x, targetTileLocation.y + 0.5f) - transform.position;
        }
    }

    public void LaunchEnemyMissile(GameObject tile)
    {
        if (transform.position != tile.transform.position)
        {
            isMoving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position == targetTileLocation)
        {
            if (collision.gameObject.CompareTag("Ship"))
            {
                //if (collision.gameObject.name == "Submarine") targetTileLocation.y += 0.3f; // need to understand this also it should be z instead of y
                isMoving = false;
                gameManager.EnemyHitPlayer(targetTileLocation, targetTile, collision.gameObject);
            }
            else
            {
                isMoving = false;
                enemyScript.PauseAndEnd(targetTile);
            }
            Destroy(gameObject);
        }
    }

    public void SetTarget(int target)
    {
        targetTile = target;
    }
}
