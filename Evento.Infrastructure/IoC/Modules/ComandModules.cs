using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Evento.Infrastructure.Commands;

namespace Evento.Infrastructure.IoC.Modules
{
    public class ComandModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ComandModules)
                .GetType()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
