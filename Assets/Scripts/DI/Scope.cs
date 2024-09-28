using ClockTime;
using ClockTime.SyncServer;
using Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class Scope : LifetimeScope
    {
        [SerializeField] private ClockView _clockView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Clock>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EntryPoint>();
            builder.Register<ISyncServerTime,SyncServerTime>(Lifetime.Singleton);
            builder.Register<IUpdateTime, DataTime>(Lifetime.Singleton);
            builder.RegisterInstance(_clockView);
        }
    }
}