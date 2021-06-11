using System;
using ByteBank.Portal.Infra.IoC;

namespace ByteBank.Portal.Infra
{
    public class ControllerResolver
    {
        private readonly IContainer _container;

        public ControllerResolver(IContainer container)
        {
            _container = container;
        }

        public object GetController(string controllerName)
        {
            var controllerType = Type.GetType(controllerName);
            return _container.Get(controllerType);
        }

    }
}