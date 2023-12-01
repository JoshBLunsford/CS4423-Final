using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseBlink;
    public Tween tween;
    public float blinkTime;
    public static bool paused;

    public void Pause()
    {
        paused = !paused;
        if (paused)
        {
            tween.Enact();
            StartCoroutine(PauseBlink());
            return;
        }
        StopAllCoroutines();
        pauseBlink.SetActive(false);
        tween.Enact(true, tween.ReverseRoutine(), true);
    }

    public IEnumerator PauseBlink()
    {
        yield return new WaitForSecondsRealtime(tween.Length());
        pauseBlink.SetActive(true);
        while (paused)
        {
            yield return new WaitForSecondsRealtime(blinkTime);
            pauseBlink.SetActive(!pauseBlink.activeSelf);
        }
    }
    public void MainMenu()
    {
        paused = false;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputAssist.pause))
        {
            Pause();
        }
    }
}
