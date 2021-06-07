using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteBank.Portal.Infra.Binding
{
    public class ActionBinder
    {
        public ActionBindInfo GetMethodInfo(object controller, string path)
        {
            //Exchange/Calculate?originCurrency=BRL&destinyCurrency=USD&value=10
            //Exchange/Calculate?destinyCurrency=USD&value=10
            //Exchange/USD

            var interrogationIndex = path.IndexOf("?");
            var hasQueryString = interrogationIndex >= 0;

            if (!hasQueryString)
            {
                var actionName = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var methodInfo = controller.GetType().GetMethod(actionName);
                return new ActionBindInfo(methodInfo, Enumerable.Empty<KeyValueArgument>());
            }
            else
            {
                var controllerAndActionName = path.Substring(0, interrogationIndex);
                var actionName = controllerAndActionName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var queryString = path.Substring(interrogationIndex + 1);
                var keyValueTuples = GetKeyValueArguments(queryString);
                var arguments = keyValueTuples.Select(x => x.Key).ToArray();
                var methodInfo = GetMethodInfoFromNameAndParameters(actionName, arguments, controller);

                return new ActionBindInfo(methodInfo, keyValueTuples);
            }
        }

        private IEnumerable<KeyValueArgument> GetKeyValueArguments(string queryString)
        {
            //originCurrency=BRL&destinyCurrency=USD&value=10
            var keyValueTuples = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tuple in keyValueTuples)
            {
                var tupleParts = tuple.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                yield return new KeyValueArgument(tupleParts[0], tupleParts[1]);
            }
        }

        private MethodInfo GetMethodInfoFromNameAndParameters(string actionName, string[] parameters, object controller)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var methods = controller.GetType().GetMethods(bindingFlags);

            var overloads = methods.Where(x => x.Name == actionName);

            foreach (var overload in overloads)
            {
                var overloadParameters = overload.GetParameters();
                if (overloadParameters.Length != parameters.Length)
                    continue;

                var match =
                    overloadParameters.All(
                        parameter => parameters.Contains(parameter.Name)
                    );

                if (match)
                    return overload;

            }
            throw new ArgumentException($"The overload for the method {actionName} was not found.");
        }
    }
}