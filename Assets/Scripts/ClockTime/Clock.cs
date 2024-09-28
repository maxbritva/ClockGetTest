using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
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
        public async void Initialize() => await StartUpdateTime();

        public async UniTask StartUpdateTime()
        {
            _cts = new CancellationTokenSource();
            while (_cts.IsCancellationRequested == false)
            {
                _updateTime.UpdateSeconds();
                await UniTask.Delay(TimeSpan.FromSeconds(1f),  _cts.IsCancellationRequested);
            }
            _cts.Cancel();
        }
    }
}