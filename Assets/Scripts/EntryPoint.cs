using VContainer.Unity;

namespace DefaultNamespace
{
    public class EntryPoint: IInitializable
    {
        private ClockData _clockData;
        private Clock.Clock _clock;

        public EntryPoint(ClockData clockData, Clock.Clock clock)
        {
            _clockData = clockData;
            _clock = clock;
        }

        public Clock.Clock Clock => _clock;

        public ClockData ClockData => _clockData;

        public async void Initialize()
        {
           await _clockData.GetDataFromServer();
           await _clock.UpdateTime();
        }
    }
}