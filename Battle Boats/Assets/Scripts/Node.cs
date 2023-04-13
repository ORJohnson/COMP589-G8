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
        hitColor[0] = gameObject.GetComponent<MeshRenderer>().material.color;
        hitColor[1] = gameObject.GetComponent<MeshRenderer>().material.color;
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

    // In case we want to actually have an animation of the missile hitting the tiles
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            missleHit = true;
        } else if (collision.gameObject.CompareTag("EnemyMissile"))
        {
            hitColor[0] = new Color32(38, 57, 76, 255);
            GetComponent<Renderer>().material.color = hitColor[0];
        }
    }*/

    public void SetTileColor(int index, Color32 color)
    {
        hitColor[index] = color;
    }

    public void SwitchColors(int colorIndex)
    {
        GetComponent<Renderer>().material.color = hitColor[colorIndex];
    }
}
