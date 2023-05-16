using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    private GameManager gameManager;
    private bool isMoving = false;
    public GameObject target;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + 0.5f), 15f * Time.deltaTime);
            transform.up = new Vector3(target.transform.position.x, target.transform.position.y + 0.5f) - transform.position;
        }
    }

    public void LaunchMissile(GameObject tile)
    {
        target = tile;

        if(transform.position != tile.transform.position)
        {
            isMoving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position == target.transform.position)
        {
            isMoving = false;
            gameManager.CheckHit(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
