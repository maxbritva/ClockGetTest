using System;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.Networking;
using VContainer.Unity;
using DateTime = System.DateTime;

namespace ClockTime.SyncServer
{
    public class SyncServerTime: ISyncServerTime, IInitializable, IDisposable
    {
        private readonly string _serverUrl = "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";
        private readonly IUpdateTime _dateTime;
        public SyncServerTime(IUpdateTime dateTime) => _dateTime = dateTime;
        public void Initialize() => _dateTime.OnHoursChanged += NeedToSync;
        public void Dispose() => _dateTime.OnHoursChanged -= NeedToSync;

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
        private async void NeedToSync() => await GetDataFromServer();
    }
}