using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButtonManager : MonoBehaviour
{
    public new GameObject gameObject;

    public void RotateObject()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 90));

    }
}
