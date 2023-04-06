using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameManager gameManager;
    Ray ray;
    RaycastHit hit;

    private bool missleHit = false;
    Color32[] hitColor = new Color32[2];

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0) && hit.collider.gameObject.name == gameObject.name) 
            {
                if(!missleHit)
                {
                    // have to create method for TileClicked
                    // gameManager.TileClicked(hit.collider.gameObject);
                }
            }
        }
    }
}
