using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Clock
{
    public class Clock: IDisposable, IInitializable
    {
        private ClockView _clockView;
        private ClockData _clockData;
        private CancellationTokenSource _cts;

        public Clock(ClockView clockView, ClockData clockData)
        {
            _clockData = clockData;
            _clockView = clockView;
        }
        public void Dispose() => _cts?.Dispose();
        public async void Initialize() => await UpdateTime();

        public async UniTask UpdateTime()
        {
            _cts = new CancellationTokenSource();
            while (_cts.IsCancellationRequested == false)
            {
                _clockView.SecondsArrow.transform.eulerAngles = new Vector3(0f,0f, - _clockData.CurrentSeconds * 360f / 60f);
                await UniTask.Delay(TimeSpan.FromSeconds(1f),  _cts.IsCancellationRequested);
            }
            _cts.Cancel();
        }

      
    }
}