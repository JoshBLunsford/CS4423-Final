using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public TMP_InputField inputfield;
    public Scrollbar scroll;
    public TextMeshProUGUI fullscreenText, vsyncText, volumeText;
    public bool fullscreen, vsync;
    public void ChangeResolution()
    {
        string[] resXY = inputfield.text.Split('x');
        if (int.TryParse(resXY[0], out int resX) && int.TryParse(resXY[1], out int resY))
            Screen.SetResolution(resX, resY, fullscreen);
        else
            inputfield.text = "Invalid resolution!";
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreen = fullscreen = !fullscreen;
        fullscreenText.text = fullscreen ? "FULLSCREEN: ON" : "FULLSCREEN: OFF";

    }
    public void ToggleVSync()
    {
        vsync = !vsync;
        if (vsync)
        {
            QualitySettings.vSyncCount = 1;
            vsyncText.text = "V-SYNC: ON";
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            vsyncText.text = "V-SYNC: OFF";
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = scroll.value;
        volumeText.text = $"VOLUME\n({(int)(scroll.value * 100)})%";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Screen.SetResolution(1920, 1080, false);
    }
}
