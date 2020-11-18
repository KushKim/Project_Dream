using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLock : MonoBehaviour
{
    public int FrameRate = 60;
    private void Awake()
    {
        Application.targetFrameRate = FrameRate; // No Vsync
    }
}

