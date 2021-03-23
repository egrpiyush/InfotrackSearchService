using Autofac;
using Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMatchingTypeAsImplementedInterfaces(typeof(AutofacModule).Assembly,
                new[] { ".+Command$", ".+Query$", ".+Service$" });
        }
    }
}
