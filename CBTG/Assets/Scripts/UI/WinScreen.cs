using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public UIBlink uiBlink;
    private void Start()
    {
        StartCoroutine(CheckForWin());
    }

    IEnumerator CheckForWin()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (EnemyStats.totalEnemies == 0)
                break;
        }
        uiBlink.Blink();
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
