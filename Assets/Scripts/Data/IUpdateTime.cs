using System;

namespace Data
{
    public interface IUpdateTime
    {
        public event Action OnSecondsChanged;
        public event Action OnMinutesChanged;
        public event Action OnHoursChanged;
        float GetCurrentHours();
        float GetCurrentMinutes();
        float GetCurrentSeconds();

        void UpdateSeconds();
    }
}