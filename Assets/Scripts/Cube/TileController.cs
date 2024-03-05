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
    public override void SetCube(Color newColor, Color activeColor)
    {
        base.SetCube(newColor,activeColor);
    }

    public void FillCube()
    {
        if (state== TileCubeStates.Empty)
        {
            rend.material.color = color;
            state = TileCubeStates.Filled;
            EventManager.CheckPuzzlePercentage();
        }
    }
}
