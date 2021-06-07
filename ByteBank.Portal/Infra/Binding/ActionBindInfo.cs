using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ByteBank.Portal.Infra.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<KeyValueArgument> KeyValueTuple { get; private set; }

        public ActionBindInfo(MethodInfo methodInfo, IEnumerable<KeyValueArgument> keyValueArgument)
        {
            MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));

            if (keyValueArgument == null)
            {
                throw new ArgumentNullException(nameof(keyValueArgument));
            }

            KeyValueTuple = new ReadOnlyCollection<KeyValueArgument>(keyValueArgument.ToList());
        }

        public object Invoke(object controller)
        {
            var parametersCount = KeyValueTuple.Count;
            var hasArguments = parametersCount > 0;

            if (!hasArguments)
                return MethodInfo.Invoke(controller, new object[0]);

            var parametersInvoke = new object[parametersCount];
            var parametersMethodInfo = MethodInfo.GetParameters();

            for (var i = 0; i < parametersCount; i++)
            {
                var parameter = parametersMethodInfo[i];
                var parameterName = parameter.Name;

                var argument = KeyValueTuple.Single(tuple => tuple.Key == parameterName);
                parametersInvoke[i] = Convert.ChangeType(argument.Value, parameter.ParameterType);
            }

            return MethodInfo.Invoke(controller, parametersInvoke);
        }
    }
}