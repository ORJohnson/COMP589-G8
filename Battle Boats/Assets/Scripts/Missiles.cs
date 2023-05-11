using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) // Might have to use OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.CheckHit(collision.gameObject);
        Destroy(gameObject);
    }
}
