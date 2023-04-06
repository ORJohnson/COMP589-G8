using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateButtonManager : MonoBehaviour
{
    public GameObject gameObjectToRotate;
    public Button button;

    private bool isRotated = false;

    void Start()
    {
        // Subscribe the OnClick method to the button's click event
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (!isRotated) // Rotate 90 degrees clockwise
        {
            gameObjectToRotate.transform.Rotate(new Vector3(0, 0, 90));
            isRotated = true;
        }
        else // Rotate 90 degrees counterclockwise
        {
            gameObjectToRotate.transform.Rotate(new Vector3(0, 0, -90));
            isRotated = false;
        }

        // Highlight the button
        button.image.color = Color.yellow;

        // Invoke the Unhighlight method after 0.5 seconds
        Invoke("Unhighlight", 0.5f);
    }

    void Unhighlight()
    {
        // Unhighlight the button
        button.image.color = Color.white;
    }
}
