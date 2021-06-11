using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteBank.Portal.Infra.IoC
{
    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, Type> _typesMapping = new Dictionary<Type, Type>();

        //register(typeof(IExchangeService), typeof(ExchangeServiceTest))
        //Get(typeof(IExchangeService)) => ExchangeServiceTest

        public void Register(Type origin, Type destiny)
        {
            if (_typesMapping.ContainsKey(origin))
                throw new InvalidOperationException("Tipo já mapeado");
            VerifyHierarchy(origin, destiny);
            _typesMapping.Add(origin, destiny);
        }

        private void VerifyHierarchy(Type origin, Type destiny)
        {
            if (origin.IsInterface)
            {
                var doesDestinyTypeImplementInterface =
                    destiny
                        .GetInterfaces()
                        .Any(interfaceType => interfaceType == origin);

                if (!doesDestinyTypeImplementInterface)
                    throw new InvalidOperationException("Destiny type doesn't implement interface");
            }
            else
            {
                var destinyTypeInheritOriginType = destiny.IsSubclassOf(origin);

                if (!destinyTypeInheritOriginType)
                    throw new InvalidOperationException("The destiny type doesn't inherit the origin type");
            }
        }

        public object Get(Type originType)
        {
            var isOriginMapped = _typesMapping.ContainsKey(originType);
            if (isOriginMapped)
            {
                var destinyType = _typesMapping[originType];
                return Get(destinyType); //??
            }

            var constructors = originType.GetConstructors();
            var constructorWithNoParameters = constructors.FirstOrDefault(c => c.GetParameters().Any() == false);

            if (constructorWithNoParameters != null)
            {
                var instanceOfParameterlessConstructor = constructorWithNoParameters.Invoke(new object[0]);
                return instanceOfParameterlessConstructor;
            }

            //taking any construtor
            var constructorParameters = constructors[0].GetParameters();
            var paramValues = new object[constructorParameters.Count()];

            for (int i = 0; i < constructorParameters.Length; i++)
            {
                var param = constructorParameters[i];
                var parameterType = param.GetType();

                paramValues[i] = Get(parameterType);
            }

            var instanceOfAnotherConstructor = constructors[0].Invoke(paramValues);
            return instanceOfAnotherConstructor;
        }
    }
}