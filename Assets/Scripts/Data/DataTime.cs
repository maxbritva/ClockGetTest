using System;

namespace Data
{
    public class DataTime: IUpdateTime
    {
        public event Action OnSecondsChanged;
        public event Action OnMinutesChanged;
        public event Action OnHoursChanged;
        public event Action OnUpdatedTime;

        private DateTime _currentDateTime;
        private float _currentHours;
        private float _currentMinutes;
        private float _currentSeconds;
     
        public void SetDateTime(DateTime target)
        {
            _currentDateTime = target;
            _currentHours = _currentDateTime.Hour;
            _currentMinutes = _currentDateTime.Minute;
            _currentSeconds = _currentDateTime.Second;
        }
        public float GetCurrentHours() => _currentHours;

        public float GetCurrentMinutes() => _currentMinutes;

        public float GetCurrentSeconds() => _currentSeconds;

        private void UpdateHours()
        {
            if (_currentHours < 12f)
                _currentHours++;
            else
                _currentHours = 0;
            OnHoursChanged?.Invoke();  
        }

        private void UpdateMinutes()
        {
            if (_currentMinutes <= 58)
            {
                _currentMinutes++;
                OnMinutesChanged?.Invoke();
            }
            else
            {
                UpdateHours();
                _currentMinutes= 0; 
                OnMinutesChanged?.Invoke();
            }
        }

        public void UpdateSeconds()
        {
            if (_currentSeconds <= 58)
            {
                _currentSeconds++;
                OnSecondsChanged?.Invoke();
            }
            else
            {
                UpdateMinutes();
                _currentSeconds = 0; 
                OnSecondsChanged?.Invoke();
            }
        }
        public void UpdateTime() => OnUpdatedTime?.Invoke();
    }
}