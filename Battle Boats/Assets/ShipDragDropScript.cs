using UnityEngine;
using UnityEngine.UI;

public class ShipDragDropScript : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private bool isToggled = false;

    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer
    private Color originalColor; // The original color of the sprite

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the original color of the sprite
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
        else
        {
            // If the ship is not being held, snap it to the nearest grid position
            SnapToGrid();
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isToggled)
            {
                // Turn on the highlighting when clicked
                spriteRenderer.color = Color.yellow;
                isToggled = true;
                isBeingHeld = true;

                // Store the start position of the mouse relative to the object
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                startPosX = mousePos.x - this.transform.localPosition.x;
                startPosY = mousePos.y - this.transform.localPosition.y;
            }
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Release the ship
            isBeingHeld = false;

            // Turn off the highlighting when released
            if (isToggled)
            {
                spriteRenderer.color = originalColor;
                isToggled = false;
            }
        }
    }

    private void SnapToGrid()
    {
        // Snap the ship to the nearest grid position
        float gridSize = 1f;
        float x = Mathf.Round(this.transform.localPosition.x / gridSize) * gridSize;
        float y = Mathf.Round(this.transform.localPosition.y / gridSize) * gridSize;
        this.transform.localPosition = new Vector3(x, y, 0f);
    }
}
