using System;
using Autofac;
using Caliburn.Micro;

namespace Scenario.Autofac
{
    public class ScenarioModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<Screen>()
                .AsSelf();
        }
    }
}
