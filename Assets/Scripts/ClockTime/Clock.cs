using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using VContainer.Unity;

namespace ClockTime
{
    public class Clock: IDisposable, IInitializable
    {
        private ClockView _clockView;
        private IUpdateTime _updateTime;
        private CancellationTokenSource _cts;

        public Clock(ClockView clockView, IUpdateTime updateTime)
        {
            _updateTime = updateTime;
            _clockView = clockView;
        }
        public void Dispose() => _cts?.Dispose();
        public async void Initialize() => await UpdateTime();

        public async UniTask UpdateTime()
        {
            _cts = new CancellationTokenSource();
            while (_cts.IsCancellationRequested == false)
            {
                _updateTime.UpdateSeconds();
                _clockView.SecondsArrow.transform.eulerAngles = new Vector3(0f,0f, - _updateTime.GetCurrentSeconds() * 360f / 60f);
                await UniTask.Delay(TimeSpan.FromSeconds(1f),  _cts.IsCancellationRequested);
            }
            _cts.Cancel();
        }

      
    }
}