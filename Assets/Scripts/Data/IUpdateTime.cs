using System;
using Cysharp.Threading.Tasks;

namespace Data
{
    public interface IUpdateTime
    {
        public event Action OnSecondsChanged;
        public event Action OnMinutesChanged;
        public event Action OnHoursChanged;
        public event Action OnUpdatedTime;
        
        float GetCurrentHours();
        float GetCurrentMinutes();
        float GetCurrentSeconds();

        void UpdateSeconds();

        void UpdateTime();
        
        void SetDateTime(System.DateTime target);
    }
}