using Clock;
using DefaultNamespace;
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
            builder.RegisterEntryPoint<EntryPoint>();
            builder.Register<ClockData>(Lifetime.Singleton);
            builder.Register<Clock.Clock>(Lifetime.Singleton);
            builder.RegisterInstance(_clockView);
        }
    }
}