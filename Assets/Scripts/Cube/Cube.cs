using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer rend
    {
        get { return GetComponent<MeshRenderer>(); }
    }
    protected Color color;

    public virtual void SetCube(Color newColor, Color activeColor)
    {
        rend.material.color = newColor;
        color = activeColor;
    }
}
