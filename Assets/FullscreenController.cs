using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenController : MonoBehaviour
{
    void Update()
    {
        // Toggle fullscreen when the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFullscreen();
        }
    }

    void ToggleFullscreen()
    {
        // Toggle between fullscreen and windowed mode
        Screen.fullScreen = !Screen.fullScreen;
    }
}
