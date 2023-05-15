using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameManager gameManager;
    private EnemyMissiles enemyMissileScript;

    private bool missileHit = false;
    Color32[] hitColor = new Color32[2];

    // Start is called before the first frame update
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
                    Debug.Log("THIS TILE IS BEING CLICKED");
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
            missileHit = true;
        } else if (collision.gameObject.CompareTag("EnemyMissile"))
        {
            enemyMissileScript = collision.gameObject.GetComponent<EnemyMissiles>();
            // Set tile color to be different so we know enemy has taken turn
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






    // Might delete this but we'll see. Trying to return list of all the Nodes
    //public List<BoxCollider2D> GrabbingBoxColliders()
    //{
    //    List<BoxCollider2D> allBoxColliders = new List<BoxCollider2D>();
    //    List<Node> nodes = new List<Node>();

    //    nodes = GetComponents<Node>().ToList();
    //    for (int i = 0; i < nodes.Count; i++)
    //    {
    //        allBoxColliders.Add(nodes[i].GetComponent<BoxCollider2D>());
    //    }
    //    return allBoxColliders;
    //}
}
