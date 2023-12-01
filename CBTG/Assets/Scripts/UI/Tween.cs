using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tween : MonoBehaviour
{
    public RectTransform[] rectTransforms;
    public TweenEntry[] tweenRoutine;
    public bool enactOnStart = false;
    public float reverseDelay;
    public UnityEvent endEvent;
    public int CurrentTweenIndex { get; private set; }
    public bool InRoutine { get; private set; }

    TweenEntry[] tweenRoutineBackup;
    [System.Serializable]
    public struct TweenEntry
    {
        public Vector2 start, end;
        public float desiredTime;

        public TweenEntry(Vector2 s, Vector2 e, float dt)
        {
            start = s;
            end = e;
            desiredTime = dt;
        }
        public static TweenEntry[] DuplicateRoutine(TweenEntry[] desired)
        {
            TweenEntry[] newRoutine = new TweenEntry[desired.Length];
            for (int i = 0; i < newRoutine.Length; i++)
                newRoutine[i] = new TweenEntry(desired[i].start, desired[i].end, desired[i].desiredTime);
            return newRoutine;
        }
    }
    bool completedLast;
    private void Start()
    {
        if (enactOnStart)
            Enact(true);
    }

    public float Length()
    {
        float cumul = 0f;
        foreach (TweenEntry entry in tweenRoutine)
            cumul += entry.desiredTime;
        return cumul;
    }

    public void Enact(bool useCurrentVectorAsStart = true, TweenEntry[] rtr = null, bool reversed = false, float timeBeforeStart = 0)
    {
        StopAllCoroutines();
        completedLast = !InRoutine;
        StartCoroutine(DoRoutine(useCurrentVectorAsStart, rtr, reversed, timeBeforeStart));
    }
    IEnumerator DoRoutine(bool useCurrentVectorAsStart, TweenEntry[] rtr, bool reversed, float timeBeforeStart)
    {
        InRoutine = true;
        if (reversed && completedLast)
            endEvent.Invoke();
        yield return new WaitForSecondsRealtime(timeBeforeStart);
        TweenEntry[] desiredTweenRoutine = rtr ?? TweenEntry.DuplicateRoutine(tweenRoutine);
        if (useCurrentVectorAsStart)
            desiredTweenRoutine[0].start = rectTransforms[0].sizeDelta;

        for (int i = 0; i < desiredTweenRoutine.Length; i++)
        {
            CurrentTweenIndex = i;
            TweenEntry ct = desiredTweenRoutine[i];
            for (int j = 0; j < rectTransforms.Length; j++)
                rectTransforms[j].sizeDelta = ct.start;
            float elapsedTime = 0;
            while (elapsedTime < ct.desiredTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                Vector2 sizeDelt = SmoothLerpVec2(ct.start, ct.end, elapsedTime / ct.desiredTime);
                for (int j = 0; j < rectTransforms.Length; j++)
                    rectTransforms[j].sizeDelta = sizeDelt;
                yield return null;
            }
            for (int j = 0; j < rectTransforms.Length; j++)
                rectTransforms[j].sizeDelta = ct.end;
        }
        if (!reversed)
            endEvent.Invoke();
        completedLast = true;
        InRoutine = false;
    }

    public static Vector2 SmoothLerpVec2(Vector2 min, Vector2 max, float time)
    {
        return new Vector2(Mathf.SmoothStep(min.x, max.x, time), Mathf.SmoothStep(min.y, max.y, time));
    }

    public TweenEntry[] ReverseRoutine(TweenEntry[] rtr = null)
    {
        var dr = rtr ?? tweenRoutine;
        TweenEntry[] reversed = new TweenEntry[dr.Length];
        for (int i = 0; i < dr.Length; i++)
        {
            TweenEntry cur = dr[dr.Length - i - 1];
            reversed[i] = new TweenEntry { desiredTime = cur.desiredTime, start = cur.end, end = cur.start };
        }
        return reversed;
    }
}
