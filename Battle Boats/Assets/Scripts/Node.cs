using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameManager gameManager;
    private EnemyMissiles enemyMissileScript;
    private Missiles playerMissileScript;

    private bool missileHit = false;
    Color32[] hitColor = new Color32[2];


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hitColor[0] = gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
        hitColor[1] = gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
    }


    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!missileHit)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    gameManager.TileClicked(clickedObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerMissile"))
        {
            playerMissileScript = collision.gameObject.GetComponent<Missiles>();
            if(gameObject.transform.position == playerMissileScript.target.transform.position)
            {
                missileHit = true;
            }
        } else if (collision.gameObject.CompareTag("EnemyMissile"))
        {
            enemyMissileScript = collision.gameObject.GetComponent<EnemyMissiles>();
            if (gameObject.transform.position == enemyMissileScript.targetTileLocation)
            {
                hitColor[0] = new Color32(38, 57, 76, 255);
                gameObject.GetComponentInChildren<SpriteRenderer>().material.color = hitColor[0];
            }
        }
    }

    public void SetTileColor(int index, Color32 color)
    {
        hitColor[index] = color;
    }

    public void SwitchColors(int colorIndex)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().material.color = hitColor[colorIndex];
    }
}
