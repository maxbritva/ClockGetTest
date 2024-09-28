using Cysharp.Threading.Tasks;

namespace ClockTime.SyncServer
{
    public interface ISyncServerTime
    {
        UniTask GetDataFromServer();
    }
}