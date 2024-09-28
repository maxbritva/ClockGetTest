using System;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.Networking;
using DateTime = System.DateTime;

namespace ClockTime.SyncServer
{
    public class SyncServerTime: ISyncServerTime
    {
        private readonly string _serverUrl = "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";
        private readonly IDateTime _dateTime;

        public SyncServerTime(IDateTime dateTime) => _dateTime = dateTime;

        public async UniTask GetDataFromServer()
        {
            var request = UnityWebRequest.Get(_serverUrl);
            await request.SendWebRequest();
            if (request.isDone)
                _dateTime.SetDateTime(ParseDateTime(request.downloadHandler.text));
            else
                Debug.Log("ServerError: " + request.error);
        }
        private DateTime ParseDateTime ( string datetime ) {
            var date = Regex.Match ( datetime, @"^\d{4}-\d{2}-\d{2}" ).Value;
            var time = Regex.Match ( datetime, @"\d{2}:\d{2}:\d{2}" ).Value;

            return DateTime.Parse ($"{date} {time}");
        }
    }
}