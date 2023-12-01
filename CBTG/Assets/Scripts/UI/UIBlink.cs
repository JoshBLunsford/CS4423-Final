using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    #region custom types
    [System.Serializable]
    public struct BlinkEntry
    {
        public GameObject _object;
        public Image blinker;
    }
    #endregion

    public float blinkTime = 0.05f;
    [Header("Put references in blinking order!")]
    public BlinkEntry[] blinkers;
    public bool open = false;
    public bool InProgress { get; private set; }
    public void Alternate()
    {
        open = !open;
        if (open)
            Blink();
        else
            Close();
    }
    public void Blink()
    {
        StopAllCoroutines();
        StartCoroutine(EnactBlink());
    }
    public void Close()
    {
        StopAllCoroutines();
        for (int i = 0; i < blinkers.Length; i++)
            StartCoroutine(IndependentClose(blinkers[i]));
    }
    IEnumerator EnactBlink()
    {
        for (int i = 0; i < blinkers.Length; i++)
        {
            blinkers[i]._object.SetActive(true);
            blinkers[i].blinker.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(blinkTime);
            blinkers[i].blinker.gameObject.SetActive(false);
        }
    }

    IEnumerator IndependentClose(BlinkEntry blinker)
    {
        blinker.blinker.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(blinkTime);
        blinker.blinker.gameObject.SetActive(false);
        blinker._object.gameObject.SetActive(false);
    }
}
