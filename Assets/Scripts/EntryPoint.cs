using ClockTime;
using ClockTime.SyncServer;
using VContainer.Unity;

public class EntryPoint: IInitializable
{
    private ISyncServerTime _syncServerTime;
    private Clock _clock;

    public EntryPoint(Clock clock, ISyncServerTime syncServerTime)
    {
        _clock = clock;
        _syncServerTime = syncServerTime;
    }


    public async void Initialize()
    { 
        await _syncServerTime.GetDataFromServer();
        await _clock.UpdateTime();
    }
}