using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class CameraController : MonoBehaviour
{
    private float ratio = 67.37f;
    private float screenWidth = UnityEngine.Screen.width;
    private float levelWidth;
    private float camYPos;
    private void OnEnable()
    {
        EventManager.SetCamPos += SetCamPos;
    }

    private void SetCamPos(float LevelWidth)
    {
        levelWidth = LevelWidth * 100;
        camYPos = levelWidth / ratio;
        var pos = transform.position;
        pos.y = camYPos;
        transform.position=pos;
    }

    private void OnDisable()
    {
        EventManager.SetCamPos -= SetCamPos;
    }
}
