using ClockTime;
using ClockTime.SyncServer;
using Data;
using VContainer.Unity;

public class EntryPoint: IInitializable
{
    private ISyncServerTime _syncServerTime;
    private Clock _clock;
    private IUpdateTime _updateTime;

    public EntryPoint(Clock clock, ISyncServerTime syncServerTime, IUpdateTime updateTime)
    {
        _clock = clock;
        _updateTime = updateTime;
        _syncServerTime = syncServerTime;
    }


    public async void Initialize()
    { 
        await _syncServerTime.GetDataFromServer();
        _updateTime.UpdateTime();
        await _clock.StartUpdateTime();
    }
}