using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class TileController : Cube
{
    public TileCubeStates state = TileCubeStates.Empty;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<BallController>())
        {
            FillCube();
        }
    }
    public override void SetCube(UnityEngine.Color newColor, Color activeColor)
    {
        base.SetCube(newColor,activeColor);
        //rend.material.color = black;
        gameObject.layer = 6;
    }

    public void FillCube()
    {
        var firstScale = transform.localScale;
        //transform.localScale = Vector3.zero;
        rend.material.color = color;
        state = TileCubeStates.Filled;
    }
}
