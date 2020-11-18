using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrenSizefix : MonoBehaviour
{
    private CanvasScaler scaler;
    private Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        scaler = FindObjectOfType<CanvasScaler>();
        canvas = FindObjectOfType<Canvas>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
}

