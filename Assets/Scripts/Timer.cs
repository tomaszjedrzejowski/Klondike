using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _curentTime;
    public Action<string> OnTimeUpdate;

    void Start()
    {
        ResetTiemr();
    }

    void Update()
    {
        _curentTime += Time.deltaTime;
        string timeText = FormatTime(_curentTime);
        OnTimeUpdate?.Invoke(timeText);
    }

    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        return timeString;
    }

    public void ResetTiemr()
    {
        _curentTime = 0;
    }
}
