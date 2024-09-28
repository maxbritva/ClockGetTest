using System;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ClockData
{
    private string _serverUrl = "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";
    private DateTime _currentDateTime;
    public float CurrentHours { get; private set; }
    public float CurrentMinutes { get; private set; }
    public float CurrentSeconds { get; private set; }

    public async UniTask GetDataFromServer()
    {
        var request = UnityWebRequest.Get(_serverUrl);
        await request.SendWebRequest();
        if (request.isDone)
        {
            Debug.Log(request.downloadHandler.text);
            _currentDateTime =  ParseDateTime(request.downloadHandler.text);
            SetCurrentTime();
        }
        else
            Debug.Log("ServerError: " + request.error);
    }
    private DateTime ParseDateTime ( string datetime ) {
        var date = Regex.Match ( datetime, @"^\d{4}-\d{2}-\d{2}" ).Value;
        var time = Regex.Match ( datetime, @"\d{2}:\d{2}:\d{2}" ).Value;

        return DateTime.Parse ($"{date} {time}");
    }
    private void SetCurrentTime()
    {
        CurrentHours = _currentDateTime.Hour;
        Debug.Log(CurrentHours);
        CurrentMinutes = _currentDateTime.Minute;
        CurrentSeconds = _currentDateTime.Second;
    }
}