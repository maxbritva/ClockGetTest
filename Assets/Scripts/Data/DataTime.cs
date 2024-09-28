using System;

namespace Data
{
    public class DataTime: IDateTime, ISetTime, IUpdateTime
    {
        public event Action OnSecondsChanged;
        public event Action OnMinutesChanged;
        public event Action OnHoursChanged;
        
        // https://www.youtube.com/watch?v=QtwCrlqLe8o
        
        private DateTime _currentDateTime;
        private float _currentHours;
        private float _currentMinutes;
        private float _currentSeconds;

        public void SetHourTime(float value)
        {
            if(CheckValidHours(value))
                _currentHours = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public void SetMinutesTime(float value)
        {
            if(CheckValidSecondsMinutes(value))
                _currentMinutes = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public void SetSecondsTime(float value)
        {
             if(CheckValidSecondsMinutes(value))
                _currentSeconds = value;
             else
                 throw new ArgumentOutOfRangeException(nameof(value));
        }
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
            if (CheckValidHours(_currentHours))
            {
                _currentHours++;
                OnHoursChanged?.Invoke();
            }
            else
                _currentHours = 0;
        }

        private void UpdateMinutes()
        {
            if(CheckValidSecondsMinutes(_currentMinutes))
            {
                OnMinutesChanged?.Invoke();
                _currentMinutes++;
            }
            else
            {
                UpdateHours();
                _currentMinutes = 0;
            }
        }

        public void UpdateSeconds()
        {
            if (CheckValidSecondsMinutes(_currentSeconds))
            {
                _currentSeconds++;
                OnSecondsChanged?.Invoke();
            }
            else
            {
                UpdateMinutes();
                _currentSeconds = 1;
            }
        }

        private bool CheckValidSecondsMinutes(float value) => value < 60 && value >= 0;
        
        private bool CheckValidHours(float value) =>  value < 24 && value >= 0;
    }
}